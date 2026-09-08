# Sprint 1 — 2026-09-01 to 2026-09-14

## Sprint Goal

Resolve every open decision blocking downstream work, get the painting-scene
prototype into an actual playable Unity build with a playtest verdict, and
start implementation on the one epic with zero blockers (Twelve Content
Framework).

## Milestone Context

- **Current Milestone**: Vertical Slice (per `design/gdd/game-concept.md` —
  1 season, 3 villagers/zones, 1 region, target: end of semester)
- **Milestone Deadline**: Not yet dated — depends on semester calendar,
  not specified in source GDD
- **Sprints Remaining**: Unknown until milestone deadline is set

## Capacity

Not estimated — team size (8 people, per `production/team-agent-mapping.md`)
and individual availability aren't specified in the source GDD beyond role
assignments. Recommend `producer` run a real capacity pass with the team
before treating the estimates below as commitments.

## Tasks

### Must Have (Critical Path)

| ID | Task | Agent/Owner | Est. Days | Dependencies | Acceptance Criteria | Status |
|----|------|-------------|-----------|---------------|------------------------|--------|
| S1-001 | Decide combat concept (paint-as-resource vs. "in her head" vs. other) | Luka, Oasis (+ `game-designer`) | 1-2 | None | Decision recorded in `design/gdd/regions-and-combat.md`, Open Questions row resolved | Options drafted (`design/decisions/combat-concept.md`) — awaiting owners |
| S1-002 | Decide vertical-slice villager/zone selection + wheel contiguity | Non, Jack (+ `game-designer`) | 1-2 | None | 3 contiguous wheel positions chosen; `design/gdd/game-concept.md` and `villager-archetype-system.md` Open Questions updated | Options drafted (`design/decisions/vertical-slice-villager-selection.md`) — awaiting owners |
| S1-003 | Create a real Unity 6.3 URP 2D project | Whoever picks up `prototypes/painting-scene/` | 0.5 | None | Project opens in Unity Editor; `prototypes/painting-scene/Scripts/ReadAccuracy.cs` compiles inside it | In Progress — skeleton committed (`Assets/`, `Packages/`, `ProjectSettings/`); needs an Editor to verify (checklist in `Assets/LaRasa/README.md`) |
| S1-004 | Build a minimal playable scene around `ReadAccuracy.cs` (two-stick input, placeholder colour/audio feedback) | `unity-specialist`, `gameplay-programmer` | 2-3 | S1-003 | Scene is playable start-to-finish per `prototypes/painting-scene/BRIEF.md` scope | Not Started (needs S1-003 Editor pass) |
| S1-005 | Run at least one playtest session against `prototypes/painting-scene/BRIEF.md` success criteria | `game-designer`, `prototyper` | 1 | S1-004 | PROCEED / PIVOT / KILL verdict recorded in a `REPORT.md` alongside the brief | Not Started |
| S1-006 | Begin Twelve Content Framework epic implementation | `unity-specialist`, `gameplay-programmer` | 2-3 | S1-003 (needs the Unity project to exist) | `ArchetypeWheel` ScriptableObject implemented per ADR-0001; TR-twelve-001/002 pass | In Progress — `ArchetypeWheel` + `CircleOfFifths` + 27 EditMode tests drafted; 3 story files created; blocked on S1-003 to compile/run |

### Should Have

| ID | Task | Agent/Owner | Est. Days | Dependencies | Acceptance Criteria | Status |
|----|------|-------------|-----------|---------------|------------------------|--------|
| S1-010 | Decide portrait lit-decay behavior (indefinite vs. decay) | `game-designer` | 0.5 | None | Decision recorded in `design/gdd/painting-and-reflection.md` Open Questions | Options drafted (`design/decisions/portrait-lit-decay.md`) — awaiting owner |
| S1-011 | Decide major/minor key per zone | Ken | 0.5 | None | Decision recorded in `design/gdd/sound-and-crossfade.md` Open Questions | Options drafted (`design/decisions/zone-key-major-minor.md`) — awaiting Ken |
| S1-012 | Decide audio technical approach (native Unity audio vs. middleware) | Ken, `audio-director`, `technical-director` | 1 | None | ADR written; `production/epics/sound-and-crossfade/EPIC.md` unblocked | Options drafted (`design/decisions/audio-technical-approach.md`) — awaiting owners |

### Nice to Have (Cut First)

| ID | Task | Agent/Owner | Est. Days | Dependencies | Acceptance Criteria | Status |
|----|------|-------------|-----------|---------------|------------------------|--------|
| S1-020 | Draft economy formulas (crop growth, quality tiers, shipping prices) | `economy-designer` | 1-2 | None | `design/gdd/farm-and-economy.md` Formulas section filled in | Not Started |

## Progress Log

**2026-09-08 — catch-up pass** (branch `catchup/unity-cli-and-sprint-1`):

- **S1-003** moved to In Progress: Unity 6.3 project skeleton committed at repo
  root. Not yet Editor-verified — see `Assets/LaRasa/README.md`.
- **S1-006** moved to In Progress: `ArchetypeWheel` / `ArchetypePosition` /
  `Season` / `CircleOfFifths` implemented per ADR-0001, plus an Editor
  "Populate Default Wheel" tool. 27 EditMode tests drafted (13 `CircleOfFifths`,
  14 `ArchetypeWheel`) — cannot run without the Editor. 3 story files created
  under `production/epics/twelve-content-framework/`.
- **S1-001 / S1-002 / S1-010 / S1-011 / S1-012**: option docs drafted in
  `design/decisions/` for the owners to choose from. No decisions made.
- Added Unity CLI setup tooling (`tools/unity-cli/`) and CI
  (`.github/workflows/`). Rewrote the top-level README for La Rasa.
- **Still the critical blocker**: no one has done the S1-003 Editor pass, which
  gates S1-004, S1-005, S1-006-completion.

## Carryover from Sprint 0

Not applicable — this is the first sprint. GDD ingestion, systems mapping,
initial architecture (ADR-0001, ADR-0002), and epic/story scaffolding were
completed as a single prior session's work (see `gdd/import`,
`architecture/foundations`, `prototype/painting-scene`,
`production/sprint-1-planning` branches) rather than as a tracked sprint.

## Risks to This Sprint

| Risk | Probability | Impact | Mitigation | Owner |
|------|------------|--------|-----------|-------|
| S1-001/S1-002 decisions slip past this sprint | Medium | High — blocks Villager Archetype System, Regions & Combat, and archetype-division art/writing work entirely | Timebox to first half of sprint; escalate to `creative-director` if undecided by day 5 | `producer` |
| Painting-scene playtest verdict is PIVOT or KILL | Unknown — genuinely the point of testing it | High — the GDD's own stated design risk | If PIVOT: revise `design/gdd/painting-and-reflection.md` before any further system depends on it. If KILL: escalate to `creative-director`, this affects the whole game's premise | `game-designer`, `creative-director` |
| No one has picked up the Unity-project-creation task (S1-003) by day 2 | Medium | High — blocks S1-004, S1-005, S1-006, everything downstream of "a Unity project exists" | Flag explicitly at sprint kickoff as the literal first task | `producer` |

## External Dependencies

None identified.

## Definition of Done

- [ ] All Must Have tasks completed
- [ ] Combat concept and vertical-slice villager selection are both decided
  and reflected in the relevant GDD files
- [ ] A real Unity project exists in this repo
- [ ] Painting-scene prototype has a recorded PROCEED/PIVOT/KILL verdict
- [ ] Twelve Content Framework epic has started implementation with passing
  unit tests for TR-twelve-001/002
- [ ] Design documents updated for any deviations from this plan
