# ADR-0001: Data-Driven "Twelve" Content via Unity ScriptableObjects

## Status

Accepted

## Date

2026-08-31

## Last Verified

2026-08-31

## Decision Makers

Ingested from existing project conventions (`unity-specialist.md` Unity Best
Practices: "Use ScriptableObjects for data-driven content") applied to the
GDD's Twelve Content Framework. Recommend `technical-director` confirm
before treating as final team consensus.

## Summary

Every twelve-count content category in La Rasa (villagers, crops, fish,
zones, tracks, recipes, etc.) shares one underlying structure: the
twelve-position archetype wheel. This ADR decides that structure is
implemented as a single canonical Unity `ScriptableObject` asset (an
`ArchetypeWheel` containing 12 `ArchetypePosition` entries), which every
other content-defining `ScriptableObject` references by position rather
than duplicating archetype/key/color/flower/season data.

## Engine Compatibility

| Field | Value |
|-------|-------|
| **Engine** | Unity 6.3 LTS |
| **Domain** | Core |
| **Knowledge Risk** | LOW — ScriptableObject-based data architecture is a long-standing, stable Unity pattern, not a post-cutoff feature |
| **References Consulted** | `.claude/agents/unity-specialist.md` (Architecture Patterns: "Use ScriptableObjects for data-driven content") |
| **Post-Cutoff APIs Used** | None |
| **Verification Required** | None |

## ADR Dependencies

| Field | Value |
|-------|-------|
| **Depends On** | None |
| **Enables** | ADR-0002 (Persona/Shadow Content Separation); all per-system content implementation (crops, villagers, zones, tracks) |
| **Blocks** | Any content-authoring epic/story that creates twelve-count data — should reference `ArchetypeWheel`, not invent parallel data |
| **Ordering Note** | This should be the first content-architecture ADR implemented, since nearly everything else references it |

## Context

### Problem Statement

`design/gdd/twelve-content-framework.md` establishes that archetype, shadow,
flower, season, color, and musical key are shared across every twelve-count
content category. Without a single canonical data source, this mapping will
get duplicated and drift (e.g., a villager's color hardcoded in one place,
the zone's color hardcoded elsewhere, disagreeing after an edit).

### Current State

No implementation exists yet — this is a foundational decision made before
any code is written.

### Constraints

- Must support easy re-ordering/renaming during Non/Jack's archetype
  division work (names, flowers, seasons are explicitly marked placeholder
  and expected to change; keys/colors are structurally fixed)
- Must be editable by non-programmers (narrative/art team members editing
  flower or season assignments) — argues for Inspector-editable data, not
  hardcoded C# constants

### Requirements

- A single source of truth for the twelve archetype positions
- Referenced (not duplicated) by every dependent system: villagers, zones,
  music tracks, enemy/region theming, flowers
- Editable in the Unity Editor by designers without code changes

## Decision

Implement the Twelve Content Framework as one `ArchetypeWheel`
`ScriptableObject` asset holding an ordered array of 12 `ArchetypePosition`
entries. Every other content-defining ScriptableObject (Villager, TownZone,
MusicTrack, EnemyArchetype, FlowerCrop) holds a reference to its
`ArchetypePosition` rather than copying its fields.

### Architecture

```
ArchetypeWheel (ScriptableObject, singleton data asset)
  └── ArchetypePosition[12]
        ├── archetypeName: string       (placeholder — editable)
        ├── shadowName: string          (placeholder — editable)
        ├── flowerName: string          (placeholder — editable)
        ├── season: SeasonEnum          (placeholder — editable)
        ├── musicalKey: string          (structurally fixed — circle of fifths)
        ├── wheelColor: Color           (structurally fixed — color wheel)
        └── wheelIndex: int (0-11)      (fixed — position order)

VillagerData (ScriptableObject)          → references ArchetypePosition
TownZoneData (ScriptableObject)          → references ArchetypePosition
MusicTrackData (ScriptableObject)        → references ArchetypePosition
FlowerCropData (ScriptableObject)        → references ArchetypePosition
EnemyArchetypeData (ScriptableObject)    → references ArchetypePosition (thematic, not 1:1 villager mapping)
```

### Key Interfaces

```csharp
[CreateAssetMenu(menuName = "LaRasa/Archetype Wheel")]
public class ArchetypeWheel : ScriptableObject
{
    [SerializeField] private ArchetypePosition[] positions; // fixed length 12

    public ArchetypePosition GetPosition(int wheelIndex);
    public ArchetypePosition GetAdjacent(int wheelIndex, int offset); // for crossfade adjacency
}

[System.Serializable]
public class ArchetypePosition
{
    public int wheelIndex;
    public string archetypeName;
    public string shadowName;
    public string flowerName;
    public Season season;
    public string musicalKey;
    public Color wheelColor;
}
```

### Implementation Guidelines

- `ArchetypeWheel` should be a single project-wide asset, referenced via a
  central registry (e.g., an Addressables-loaded singleton or a
  project-settings-style asset) rather than instantiated per-scene.
- Dependent data assets (`VillagerData`, etc.) should fail loudly in the
  Editor (validation, e.g. `OnValidate`) if their referenced
  `ArchetypePosition` is null or if two villagers reference the same
  position — the twelve-position constraint should be enforced at data-entry
  time, not discovered at runtime.

## Alternatives Considered

### Alternative 1: Hardcoded C# enum + switch statements

- **Description**: Define archetype/color/key mappings directly in code
- **Pros**: No asset management overhead
- **Cons**: Not editable by non-programmers; violates the project's own
  coding standard ("gameplay values must be data-driven, never hardcoded")
- **Estimated Effort**: Lower short-term, higher long-term (every rename
  requires a code change + rebuild)
- **Rejection Reason**: Directly conflicts with `.claude/docs/coding-standards.md`

### Alternative 2: JSON/external data file instead of ScriptableObject

- **Description**: Load archetype data from a JSON file at runtime
- **Pros**: Engine-agnostic, easier external tooling
- **Cons**: Loses Unity Inspector editing, type safety, and asset reference
  linking (other ScriptableObjects can't directly reference a JSON entry the
  way they can a ScriptableObject)
- **Estimated Effort**: Similar
- **Rejection Reason**: Unity-native ScriptableObject referencing is the
  stronger fit per `unity-specialist.md`'s established guidance, and this is
  a single-engine project with no cross-engine tooling need

## Consequences

### Positive

- Single source of truth prevents the "color defined twice, now disagrees"
  class of bug
- Non-programmers (narrative/art) can edit placeholder values directly in
  the Unity Inspector
- Downstream systems (villagers, zones, music, enemies) stay in sync
  automatically when the wheel changes

### Negative

- Adds one layer of indirection (`GetPosition(index)`) that every dependent
  system must respect rather than reading fields directly — needs to be
  documented clearly for new contributors

### Neutral

- Requires the wheel to be finalized (or at least stable) before dependent
  ScriptableObjects are authored in bulk — reinforces why the Non/Jack
  archetype-division decision is a Week-1 blocker

## Risks

| Risk | Probability | Impact | Mitigation |
|------|------------|--------|-----------|
| Someone bypasses the wheel and hardcodes a value anyway | Medium | Medium | `OnValidate` checks + code review via `lead-programmer`/`unity-specialist` |
| Wheel changes after content is authored, requiring re-linking | Medium (given placeholders are still in flux) | Low — references survive renames since they're object references, not string lookups | None needed beyond using object references, not name-string lookups |

## Performance Implications

Not measured — this is a small, load-once data asset with no runtime
performance concern at this content scale (12 entries). No budget
established because none is needed.

## Migration Plan

Not applicable — no existing system to migrate from.

## Validation Criteria

- [ ] All twelve positions exist in one `ArchetypeWheel` asset
- [ ] No dependent system (villager, zone, track, flower, enemy) duplicates
  archetype/key/color/flower/season data instead of referencing the wheel
- [ ] Renaming a placeholder value propagates correctly to every system
  that displays it

## GDD Requirements Addressed

| GDD Document | System | Requirement | How This ADR Satisfies It |
|-------------|--------|-------------|--------------------------|
| `design/gdd/twelve-content-framework.md` | Twelve Content Framework | "All twelve positions are implemented as a single data asset... that other systems reference rather than duplicate" | Directly implements this acceptance criterion via `ArchetypeWheel` |

## Related

- Enables ADR-0002 (Persona/Shadow Content Separation)
- Referenced by all future per-system ADRs for villagers, zones, music,
  flowers, and enemies
