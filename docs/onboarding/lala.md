# Onboarding — Lala

**Role**: Creative lead. The twelve portraits (you paint them). Art direction
across the whole project.

## Your agents

| For | Agent |
|-----|-------|
| Visual identity, the art bible, style consistency | `art-director` |
| Portraits as game assets (import, resolution stages, pipeline) | `technical-artist` |
| How a portrait's reveal ties into that villager's questline | `narrative-director` |
| A full visual pipeline pass (spec → visual → implementation) | `/team-ui` (for screens), `/art-bible` + `/asset-spec` (for assets) |

## On your plate now

- No **art bible** exists yet. It gates all asset production — running
  `/art-bible` with `art-director` is a high-value early move.
- The portrait system has four reveal stages per villager
  (early/middle/late/finished) — abstract → disagrees with the persona →
  shows the shadow → reads true in hindsight
  (`design/gdd/villager-archetype-system.md`).
- Each wheel position has a fixed **colour** (twelve-hue wheel) — portraits
  should sit in their position's colour family. The map is in
  `design/gdd/twelve-content-framework.md`.

## First steps

1. `docs/TEAM-HANDBOOK.md` §2 — Git + Node + Claude Code. **You don't need
   Unity.** You paint in your own tools and hand PNGs to a programmer.
2. Read `game-concept.md` (pillars + aesthetics), `villager-archetype-system.md`,
   `narrative-portrait-system.md`.
3. In Claude Code: *"start an art bible with me — I'm the creative lead, this
   is a 2D stylized farming sim, portraits are 'real paintings'."*

## Watch out for

- Repeated cross-portrait details need to be planned across all twelve
  *before* final painting (TR-narrative-002) — coordinate with Non/Jack.
- Hand exports to a programmer, who imports them to `Assets/LaRasa/Art` and
  commits the `.meta` files. Keep your layered painting files on a shared
  drive, not in the repo.
