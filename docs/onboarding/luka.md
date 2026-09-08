# Onboarding — Luka

**Role**: Core systems — crafting/build logic + menus, daily-cycle AI/pathing.
**Co-owns** the combat concept decision with Oasis.

## Your agents

| For | Agent |
|-----|-------|
| Crafting recipes, slot math, progression curves | `systems-designer` (+ `economy-designer` for prices) |
| Daily-routine NPC AI, schedules, pathfinding | `ai-programmer` |
| The menu screens themselves | `unity-ui-specialist` (UXML/USS or UGUI) |
| Implementing designed mechanics as C# | `gameplay-programmer` |
| Unity patterns / packages / project settings questions | `unity-specialist` |

## On your plate now

- **Combat concept (`S1-001`)** — jointly with Oasis. Read
  `design/decisions/combat-concept.md`, then run a session with
  `game-designer` + `systems-designer` to work the trade-offs. **Record the
  call in the doc's `## Decision` section** — don't decide it informally.
- Crafting/inventory: `design/gdd/farm-and-economy.md` says slot counts are
  multiples of twelve and flower-milling produces paint. The Formulas section
  isn't written yet — that's a `/design-system` or `/quick-design` job before
  code.
- Nothing in crafting/AI should start implementation until the Twelve Content
  Framework epic is Done (everything references `ArchetypeWheel`).

## First steps

1. `docs/TEAM-HANDBOOK.md` §2.2 — install Unity 6.3, do the
   `Assets/LaRasa/README.md` checklist with Taha.
2. Read `design/gdd/farm-and-economy.md`, `daily-loop-and-calendar.md`, and
   ADR-0001.
3. In Claude Code: *"walk me through the combat concept options doc — I'm
   co-deciding this with Oasis."*

## Watch out for

- `docs/architecture/control-manifest.md` "Forbidden" list: no `Find()` /
  `FindObjectOfType()` / `SendMessage()`, no allocations in `Update()`, no
  hardcoded gameplay values (data-driven ScriptableObjects only).
- Combat has **no ADR yet** — don't write combat implementation code until
  `S1-001` resolves and an ADR exists.
