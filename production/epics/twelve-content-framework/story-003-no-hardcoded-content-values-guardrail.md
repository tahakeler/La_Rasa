# Story 003: "No hardcoded content values" guardrail

> **Epic**: Twelve Content Framework
> **Status**: Ready
> **Layer**: Foundation
> **Type**: Integration
> **Estimate**: S (~2 h)
> **Manifest Version**: 2026-08-31-v1
> **Last Updated**: 2026-09-08

## Context

**GDD**: `design/gdd/twelve-content-framework.md`
**Requirement**: `TR-twelve-002`
*(Requirement text lives in `docs/architecture/tr-registry.yaml` — read fresh at review time)*

**ADR Governing Implementation**: ADR-0001 — Data-Driven "Twelve" Content via Unity ScriptableObjects
**ADR Decision Summary**: No dependent system duplicates
archetype/key/colour/flower/season data instead of referencing the wheel.

**Engine**: Unity 6.3 LTS | **Risk**: LOW
**Engine Notes**: An EditMode test that reflects over loaded assemblies /
scans script assets. No post-cutoff APIs.

**Control Manifest Rules (this layer)**:
- Forbidden: hardcoding archetype names, colours, keys, or flower names in
  code — always read from `ArchetypeWheel`.
- Guardrail: flag any PR that introduces such literals in review.

---

## Acceptance Criteria

*From GDD `design/gdd/twelve-content-framework.md`, scoped to this story:*

- [ ] No hardcoded archetype/colour/key values exist outside the canonical
  data asset (and the one pure-C# `CircleOfFifths` source of the fixed key
  ordering).
- [ ] A repeatable check exists (EditMode test) that fails if a script under
  `Assets/LaRasa/` other than the Twelve Content folder contains a literal
  from the archetype-name set or the twelve key strings.
- [ ] The allowed exceptions are explicit and documented (the wheel data,
  `CircleOfFifths`, `ArchetypeWheelDefaults`, and test files).

---

## Implementation Notes

- Add `Assets/LaRasa/Tests/EditMode/TwelveContent/NoHardcodedContentValuesTests.cs`.
- Build the forbidden-literal set from `CircleOfFifths.KeysInOrder()` plus the
  archetype names in `ArchetypeWheelDefaults.BuildDefaultPositions()`.
- Enumerate `.cs` files via `AssetDatabase.FindAssets("t:MonoScript")` (or a
  directory walk under `Application.dataPath`), skip the allow-listed paths,
  and assert none contain a whole-word match for a forbidden literal.
- Keep the allow-list tiny and in one place; a new entry needs a code-review
  reason.
- This check *grows* as dependent systems land — each new content type
  (villager, zone, track) must reference an `ArchetypePosition`, never copy
  fields. That is enforced per-epic, but this test is the backstop.

---

## Out of Scope

- Refactoring any existing offender (there are none yet — this is the
  foundation epic).
- A Roslyn analyzer / build-time enforcement (nice-to-have; a test is enough
  for MVP and runs in the same CI gate).

---

## QA Test Cases

*Integration story — automated EditMode test spec.*

- **AC-2**: guardrail catches a planted violation
  - Given: a temp script (created in the test, deleted in teardown) containing
    `const string x = "Innocent";`
  - When: the scan runs
  - Then: the scan reports that file
  - Edge cases: the literal appears in a comment only → still reported
    (conservative); the literal is a substring of a longer identifier
    (`InnocentBystander`) → not reported (whole-word match)

- **AC-1**: current tree is clean
  - Given: the tree as committed
  - When: the scan runs
  - Then: zero violations outside the allow-list

---

## Test Evidence

**Story Type**: Integration
**Required evidence**:
`Assets/LaRasa/Tests/EditMode/TwelveContent/NoHardcodedContentValuesTests.cs`
— must exist and pass. (Not yet drafted — this story is `Ready`, not
`In Progress`.)

**Status**: [ ] Not yet created

---

## Dependencies

- Depends on: Story 001, Story 002.
- Unlocks: confidence that dependent-content epics can't silently drift.
