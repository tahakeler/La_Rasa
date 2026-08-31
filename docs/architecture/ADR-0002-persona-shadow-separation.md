# ADR-0002: Villager Persona/Shadow Content Structural Separation

## Status

Accepted

## Date

2026-08-31

## Last Verified

2026-08-31

## Decision Makers

Ingested from GDD Pillar 3 ("Persona and Shadow Are Structurally Separate").
Recommend `technical-director` + `narrative-director` confirm before
treating as final team consensus.

## Summary

The GDD requires that a villager's "shadow" content never leak into normal
daytime dialogue — it must only surface through the Reflection & Painting
Scene. This ADR decides that separation is enforced by storing persona and
shadow content in physically separate data assets/systems, not just by
writer discipline, so the constraint is structural rather than a
content-review-only rule.

## Engine Compatibility

| Field | Value |
|-------|-------|
| **Engine** | Unity 6.3 LTS |
| **Domain** | Core / Narrative |
| **Knowledge Risk** | LOW — standard ScriptableObject + dialogue-system patterns |
| **References Consulted** | `design/gdd/villager-archetype-system.md`, `design/gdd/painting-and-reflection.md` |
| **Post-Cutoff APIs Used** | None |
| **Verification Required** | None |

## ADR Dependencies

| Field | Value |
|-------|-------|
| **Depends On** | ADR-0001 (villagers reference `ArchetypePosition` from the wheel) |
| **Enables** | Narrative content authoring for all twelve villagers; Reflection & Painting Scene implementation |
| **Blocks** | Any dialogue-system implementation — must be built against this separation from the start, not retrofitted |
| **Ordering Note** | Should be decided before `writer`/`narrative-director` begin bulk villager content authoring |

## Context

### Problem Statement

If persona (daytime dialogue) and shadow (painting-scene reveal) content
live in the same dialogue database with just a tag or category field
distinguishing them, it's easy for a shadow-tagged line to get pulled into
a daytime dialogue query by accident (wrong filter, copy-paste error,
future contributor unaware of the rule). The GDD treats this separation as
a hard pillar, not a soft guideline, so the risk of an accidental leak
should be an implementation impossibility, not just a review checklist
item.

### Current State

No implementation exists yet.

### Constraints

- Twelve villagers, each needing both persona and shadow content, authored
  by multiple writers (Non, Jack) in parallel
- Shadow content is consumed exclusively by the Reflection & Painting Scene
  system at four distinct stages (early/middle/late/finished)
- Persona content is consumed by whatever standard daytime dialogue/NPC
  interaction system gets built (not yet specified in the GDD beyond
  "gifts, gestures, expression")

### Requirements

- Shadow content must be structurally unreachable from any daytime
  dialogue query
- Persona content must stand alone as complete without any shadow data
  present
- Portrait-stage-gated access: shadow content for stage N+1 should not be
  queryable before stage N is complete

## Decision

Persona and shadow content are stored as two separate ScriptableObject
types (`VillagerPersonaData` and `VillagerShadowData`), owned by different
systems, with the shadow data type having no code path reachable from the
daytime dialogue/NPC interaction system at all.

### Architecture

```
VillagerData (ScriptableObject, ADR-0001 wheel reference)
  ├── personaData: VillagerPersonaData     [consumed by: Daytime Dialogue System]
  └── shadowData: VillagerShadowData        [consumed by: Painting Scene System ONLY]

VillagerPersonaData
  ├── dialogueLines: DialogueLine[]
  ├── giftReactions: GiftReaction[]
  └── scheduleData: ScheduleEntry[]         (feeds daily-cycle AI, see farm/AI systems)

VillagerShadowData
  ├── portraitStages: PortraitStageContent[4]   (early/middle/late/finished)
  └── groundTruthEmotionalStates: EmotionalStateData[]  (consumed only inside painting-scene read logic)

# Compile-time / architectural enforcement:
# Daytime Dialogue System component/class has NO reference, field, or
# dependency on VillagerShadowData or any type inside it.
# Painting Scene System is the ONLY system with a reference to VillagerShadowData.
```

### Key Interfaces

```csharp
// Daytime dialogue system — deliberately has no access to shadow data
public class DaytimeDialogueSystem
{
    public DialogueLine GetLine(VillagerPersonaData persona, DialogueContext ctx);
    // No method here ever takes or returns VillagerShadowData or its contents.
}

// Painting scene system — the only consumer of shadow data
public class PaintingSceneController
{
    public PaintingSceneSession StartSession(VillagerShadowData shadow, int currentStage);
    public void AdvanceStage(VillagerShadowData shadow, ReadAccuracyResult result);
}
```

### Implementation Guidelines

- Enforce via assembly definitions (`.asmdef`) if practical: put
  `VillagerShadowData` and the Painting Scene system in an assembly the
  Daytime Dialogue assembly does not reference. This makes the separation a
  compile error, not just a convention.
- Content authoring tools (if any get built later, e.g. a custom dialogue
  editor) should present persona and shadow content in visually distinct
  editing contexts so writers don't accidentally cross-author.
- Portrait-stage gating: `VillagerShadowData.portraitStages[n]` should only
  be readable by the Painting Scene system once stage `n-1` is marked
  complete on that villager's save data.

## Alternatives Considered

### Alternative 1: Single dialogue database with a `isShadow: bool` tag

- **Description**: One data type, filtered by a boolean flag at query time
- **Pros**: Simpler initial data model, one less type to author against
- **Cons**: Leak risk is a runtime filter bug away, not a structural
  impossibility; doesn't match how seriously the GDD treats this as a
  pillar ("Rasa doesn't speak... nothing in the UI written in her voice"
  extends by clear intent to "shadow content doesn't appear where it
  shouldn't")
- **Estimated Effort**: Lower short-term
- **Rejection Reason**: Directly conflicts with Pillar 3's design intent;
  the whole narrative structure (persona reads one way, shadow reveals
  something different) depends on this separation being trustworthy

## Consequences

### Positive

- A shadow-content leak into daytime dialogue becomes a compile-time or
  architectural error, not a content-review miss
- Writers can work on persona and shadow content independently without
  needing to coordinate on a shared flat database

### Negative

- Two data types to author per villager instead of one — slightly more
  authoring overhead for Non/Jack
- Requires assembly-definition discipline to get the strongest version of
  this guarantee (compile-time enforcement); without it, the separation is
  still real but only enforced by "nothing calls this," not "nothing CAN
  call this"

### Neutral

- This ADR doesn't specify the daytime dialogue system's own design (line
  selection logic, gift reaction triggers) — that's a separate design/
  architecture task

## Risks

| Risk | Probability | Impact | Mitigation |
|------|------------|--------|-----------|
| Assembly-definition separation skipped for expediency, weakening the guarantee to "convention only" | Medium | Medium | Flag explicitly in code review (`lead-programmer`) if `VillagerShadowData` is referenced outside the Painting Scene assembly |

## Performance Implications

Not measured — data-loading overhead for two ScriptableObject types per
villager (24 assets total at full scope) is negligible.

## Migration Plan

Not applicable — no existing system to migrate from.

## Validation Criteria

- [ ] No code path exists from the Daytime Dialogue System to any
  `VillagerShadowData` content
- [ ] Portrait-stage content is inaccessible before its prerequisite stage
  is complete
- [ ] A content audit (grep-style, per `design/gdd/villager-archetype-system.md`'s
  own acceptance criteria) confirms zero shadow-tagged lines appear in
  daytime dialogue output

## GDD Requirements Addressed

| GDD Document | System | Requirement | How This ADR Satisfies It |
|-------------|--------|-------------|--------------------------|
| `design/gdd/villager-archetype-system.md` | Villager Archetype System | "Shadow content never appears in daytime dialogue — verified by content audit, not just code review" | Structural separation makes this a stronger guarantee than the requirement asks for |
| `design/gdd/painting-and-reflection.md` | Reflection & Painting Scene | Portrait stage gating (early/middle/late/finished) | `VillagerShadowData.portraitStages` gated by save-data stage completion |

## Related

- Depends on ADR-0001 (Twelve Content Data Architecture)
