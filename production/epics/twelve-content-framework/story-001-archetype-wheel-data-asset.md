# Story 001: ArchetypeWheel data asset and accessor API

> **Epic**: Twelve Content Framework
> **Status**: In Progress
> **Layer**: Foundation
> **Type**: Logic
> **Estimate**: M (~3-4 h)
> **Manifest Version**: 2026-08-31-v1
> **Last Updated**: 2026-09-08

## Context

**GDD**: `design/gdd/twelve-content-framework.md`
**Requirement**: `TR-twelve-001`
*(Requirement text lives in `docs/architecture/tr-registry.yaml` — read fresh at review time)*

**ADR Governing Implementation**: ADR-0001 — Data-Driven "Twelve" Content via Unity ScriptableObjects
**ADR Decision Summary**: The twelve archetype positions live in one canonical
`ArchetypeWheel` ScriptableObject holding an ordered array of twelve
`ArchetypePosition` entries. Every dependent content asset references a
position rather than copying its fields.

**Engine**: Unity 6.3 LTS | **Risk**: LOW
**Engine Notes**: ScriptableObject data architecture is a long-standing stable
pattern, not a post-cutoff feature. No verification required (ADR-0001 Engine
Compatibility). `[CreateAssetMenu]`, `OnValidate`, `[SerializeField]` are all
pre-cutoff APIs.

**Control Manifest Rules (this layer)**:
- Required: all twelve-count content references the single `ArchetypeWheel`
  asset; all gameplay values live in ScriptableObject data assets, not
  hardcoded constants.
- Forbidden: hardcoding archetype names, colours, keys, or flower names in code.
- Guardrail: `OnValidate()` checks on the asset to catch bad data at
  authoring time, not runtime.

---

## Acceptance Criteria

*From GDD `design/gdd/twelve-content-framework.md`, scoped to this story:*

- [ ] All twelve positions are implemented as a single data asset (a
  ScriptableObject array) that other systems reference rather than duplicate.
- [ ] Each position carries: `wheelIndex` (0-11), `archetypeName`,
  `shadowName`, `flowerName`, `season`, `musicalKey`, `wheelColor`.
- [ ] The asset exposes `GetPosition(int wheelIndex)` and
  `GetAdjacent(int wheelIndex, int offset)` (offset wraps the ring, for
  crossfade adjacency).
- [ ] An authoring path exists to populate the twelve placeholder positions
  from the GDD table without hand-typing each field.
- [ ] Renaming a placeholder value on a position is a single edit on the
  asset (no duplicated copies to keep in sync).

---

## Implementation Notes

*Derived from ADR-0001 Implementation Guidelines:*

- `ArchetypeWheel` is a single project-wide asset. For now it is referenced
  directly; a central registry / Addressables singleton comes when a second
  system needs it (out of scope here).
- `ArchetypePosition` is `[System.Serializable]`, fields `[SerializeField]
  private` with `[Tooltip]`, exposed via read-only properties.
- Store the positions array in wheel order (array slot == `wheelIndex`).
- **Deviation from the ADR sketch (accepted, LOW risk)**: the structurally-fixed
  key column is owned by a pure-C# `CircleOfFifths` helper (no UnityEngine
  dependency) rather than hand-entered per position. This keeps the
  circle-of-fifths ordering unit-testable headlessly and is what
  `ArchetypeWheel` validates serialized data against. The ADR's public
  interface (`GetPosition`, `GetAdjacent`) is unchanged.
- Editor-only `SetPositions(...)` seam (guarded `#if UNITY_EDITOR`) is used by
  the "Populate Default Wheel" menu item and by EditMode tests. Runtime code
  never calls it.

**Files (already drafted on branch `catchup/unity-cli-and-sprint-1`):**
- `Assets/LaRasa/Scripts/Core/TwelveContent/Season.cs`
- `Assets/LaRasa/Scripts/Core/TwelveContent/CircleOfFifths.cs`
- `Assets/LaRasa/Scripts/Core/TwelveContent/ArchetypePosition.cs`
- `Assets/LaRasa/Scripts/Core/TwelveContent/ArchetypeWheel.cs`
- `Assets/LaRasa/Scripts/Editor/ArchetypeWheelDefaults.cs`

---

## Out of Scope

*Handled by neighbouring stories — do not implement here:*

- Story 002: the full structural-invariant validator, `OnValidate` loud
  failure, and its test matrix.
- Story 003: the "no hardcoded values anywhere else" audit / guardrail.
- Any dependent content type (VillagerData, TownZoneData, …) — separate epics.

---

## QA Test Cases

*Logic story — automated EditMode test specs (`Assets/LaRasa/Tests/EditMode/TwelveContent/`).*

- **AC-1**: single data asset with twelve positions
  - Given: the default wheel data
  - When: it is loaded into an `ArchetypeWheel`
  - Then: `Positions.Count == 12` and `ArchetypeWheel.PositionCount == 12`
  - Edge cases: empty array → `GetPosition` throws `InvalidOperationException`

- **AC-3**: accessor API
  - Given: a populated wheel
  - When: `GetPosition(i)` for i in 0..11
  - Then: returned position's `WheelIndex == i`
  - When: `GetPosition(-1)` / `GetPosition(12)`
  - Then: throws `ArgumentOutOfRangeException`
  - When: `GetAdjacent(11, 1)` / `GetAdjacent(0, -1)`
  - Then: `WheelIndex == 0` / `WheelIndex == 11` (wraps the ring)

- **AC-4**: authoring path
  - Given: `ArchetypeWheelDefaults.BuildDefaultPositions()`
  - Then: returns 12 positions, each `wheelIndex == slot`, `MusicalKey ==
    CircleOfFifths.KeyAt(slot)`, non-empty distinct archetype names

---

## Test Evidence

**Story Type**: Logic
**Required evidence**: `Assets/LaRasa/Tests/EditMode/TwelveContent/ArchetypeWheelTests.cs`
— must exist and pass in the Unity Test Runner / `game-ci` EditMode run.
Mirrored under the framework convention via `tests/README.md`.

**Status**: [x] Test file drafted — [ ] executed green in an Editor / CI (blocked on `S1-003`)

---

## Dependencies

- Depends on: `S1-003` (a Unity project must exist to compile and run this).
- Unlocks: Story 002; all dependent content epics (villagers, zones, sound).
