# Onboarding — Jack

**Role**: Archetype division (with Non); write one villager end-to-end first
as a measuring stick for the rest.

## Your agents

| For | Agent |
|-----|-------|
| Story structure, the persona/shadow split | `narrative-director` |
| Dialogue and prose | `writer` |
| World/lore consistency checks | `world-builder` |
| A full coordinated narrative pass | `/team-narrative` |

## On your plate now

- **Vertical-slice villager selection (`S1-002`)** — jointly with Non.
  See `design/decisions/vertical-slice-villager-selection.md`. Decide, record
  in `## Decision`.
- Then: take **one** of the slice villagers and write them completely —
  persona dialogue, gift reactions, schedule notes, and the four portrait
  stages of shadow content (early/middle/late/finished). This becomes the
  template everyone else measures against.

## First steps

1. `docs/TEAM-HANDBOOK.md` §2 — Git + Node + Claude Code (Unity optional).
2. Read `villager-archetype-system.md`, `painting-and-reflection.md` (portrait
   stages), `narrative-portrait-system.md`, ADR-0002.
3. In Claude Code: *"I'm writing [villager] end to end. Set me up: pull their
   archetype/shadow from the wheel, and let's structure the four portrait
   stages with narrative-director before I draft lines."*

## Watch out for

- Persona must stand alone as a coherent character **without** the shadow
  (TR-villager-001). The shadow reframes it later; it doesn't complete it.
- Shadow content never leaks into daytime dialogue (Pillar 3, TR-villager-002,
  checked by `/content-audit`).
- Portrait-stage session-count thresholds aren't decided yet — write the
  content for each stage; the pacing numbers are `systems-designer`'s call.
