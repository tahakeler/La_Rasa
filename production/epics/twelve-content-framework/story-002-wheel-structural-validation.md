# Story 002: Wheel structural validation and loud authoring-time failure

> **Epic**: Twelve Content Framework
> **Status**: In Progress
> **Layer**: Foundation
> **Type**: Logic
> **Estimate**: S (~2 h)
> **Manifest Version**: 2026-08-31-v1
> **Last Updated**: 2026-09-08

## Context

**GDD**: `design/gdd/twelve-content-framework.md`
**Requirement**: `TR-twelve-001`, `TR-twelve-002`
*(Requirement text lives in `docs/architecture/tr-registry.yaml` — read fresh at review time)*

**ADR Governing Implementation**: ADR-0001 — Data-Driven "Twelve" Content via Unity ScriptableObjects
**ADR Decision Summary**: Dependent data assets should "fail loudly in the
Editor (validation, e.g. `OnValidate`) if their referenced `ArchetypePosition`
is null or if two entries reference the same position — the twelve-position
constraint should be enforced at data-entry time, not discovered at runtime."

**Engine**: Unity 6.3 LTS | **Risk**: LOW
**Engine Notes**: `OnValidate` and `Debug.LogWarning` are pre-cutoff. The
validator core is pure C# and engine-agnostic.

**Control Manifest Rules (this layer)**:
- Required: every formula-bearing / structural rule gets a unit test before Done.
- Guardrail: `OnValidate()` checks catch duplicate `ArchetypePosition`
  references at authoring time, not runtime.
- Forbidden: hand-editing the structurally-fixed key column away from the
  circle-of-fifths value.

---

## Acceptance Criteria

*From GDD `design/gdd/twelve-content-framework.md`, scoped to this story:*

- [ ] The wheel validates: exactly 12 positions; each `wheelIndex` in 0-11,
  unique, and equal to its array slot.
- [ ] The `musicalKey` on each position must equal
  `CircleOfFifths.KeyAt(wheelIndex)` — a hand-edited key is reported as a
  problem (structurally-fixed column, GDD "Status" note).
- [ ] Archetype names must be non-empty and distinct.
- [ ] Validation problems surface in the Console via `OnValidate` at
  data-entry time (not silently, not only at runtime).
- [ ] Validation logic is pure and callable without a ScriptableObject
  (`ArchetypeWheel.ValidatePositions(...)`), so it is fully unit-testable.

---

## Implementation Notes

*Derived from ADR-0001 Implementation Guidelines:*

- `ArchetypeWheel.Validate()` delegates to the static
  `ArchetypeWheel.ValidatePositions(IReadOnlyList<ArchetypePosition>)`, which
  returns `IReadOnlyList<string>` (empty == valid).
- `OnValidate()` calls `Validate()` and, if non-empty, emits a single
  `Debug.LogWarning` listing every problem, with `this` as the context object
  so double-clicking the log selects the asset.
- Do **not** throw from `OnValidate` — Unity calls it during deserialization;
  a throw there corrupts the import. Warn only.
- `GetPosition` / `GetAdjacent` still throw `InvalidOperationException` at
  runtime if the asset was shipped unpopulated — belt and braces.

---

## Out of Scope

- Story 001: the data types and accessor API themselves.
- Story 003: scanning *other* code for hardcoded values (this story only
  validates the wheel asset's own data).
- A custom Inspector / red-banner UI — a Console warning is sufficient for MVP.

---

## QA Test Cases

*Logic story — automated EditMode test specs.*

- **AC-1 / AC-3**: happy path
  - Given: `ArchetypeWheelDefaults.BuildDefaultPositions()`
  - When: `ValidatePositions` runs
  - Then: returns an empty list

- **AC-1**: wrong length
  - Given: 11 positions
  - Then: a problem string contains "exactly 12"
  - Edge cases: `null` array → a problem contains "null"

- **AC-2**: hand-edited key
  - Given: position 3 with `musicalKey = "Z#"`
  - Then: a problem contains "circle-of-fifths key"

- **AC-1**: duplicate `wheelIndex`
  - Given: two positions with `wheelIndex == 5`
  - Then: a problem contains "more than one position"

- **AC-3**: duplicate / empty archetype name
  - Given: slot 7 reuses slot 0's name / slot 2 name is whitespace
  - Then: a problem contains "more than one position" / "archetypeName is empty"

---

## Test Evidence

**Story Type**: Logic
**Required evidence**: `Assets/LaRasa/Tests/EditMode/TwelveContent/ArchetypeWheelTests.cs`
and `CircleOfFifthsTests.cs` — must exist and pass.

**Status**: [x] Test files drafted — [ ] executed green in an Editor / CI (blocked on `S1-003`)

---

## Dependencies

- Depends on: Story 001.
- Unlocks: Story 003; safe bulk authoring of dependent content.
