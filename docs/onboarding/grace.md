# Onboarding — Grace

**Role**: Character-art / style pod lead. Landing a consistent aesthetic
across thirteen characters (twelve villagers + Rasa) worked by multiple
artists.

## Your agents

| For | Agent |
|-----|-------|
| Style leadership, the art bible, consistency rules | `art-director` |
| Cross-artist pipeline, import settings, technical consistency | `technical-artist` |
| Where a character's design meets their writing | `narrative-director` |

## On your plate now

- With Lala: get an **art bible** written (`/art-bible`) — it's what keeps
  multiple artists on-model. Nothing else in character art should scale up
  before it exists.
- Per-character `/asset-spec` sheets once the slice villagers are chosen
  (`S1-002`, owned by Non/Jack).
- Oasis is doing enemy/boss sprites in parallel — coordinate so the enemy
  style (derived from archetype *shadows*) still reads as the same world.

## First steps

1. `docs/TEAM-HANDBOOK.md` §2 — Git + Node + Claude Code. **No Unity needed.**
2. Read `game-concept.md` (Art Style / Art Pipeline Complexity rows),
   `villager-archetype-system.md`.
3. In Claude Code: *"I'm the character-art pod lead with multiple artists —
   help me and art-director define the consistency rules for thirteen
   characters."*

## Watch out for

- Twelve villagers each sit in a fixed wheel **colour** — build that into the
  character palettes from the start (`twelve-content-framework.md`).
- Deliver into `assets/`; a programmer imports to `Assets/LaRasa/Art` with
  `.meta` files. Big source files will move to Git LFS.
