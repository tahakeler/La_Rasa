# Onboarding — Oasis

**Role**: Core systems — combat concept (with Luka); enemy/boss art &
animation (~half week, overlaps Grace's art pod).

## Your agents

| For | Agent |
|-----|-------|
| Combat design once the concept is chosen | `game-designer` / `systems-designer` |
| Combat implementation as C# | `gameplay-programmer` |
| Enemy/boss sprites, VFX, the art→engine pipeline | `technical-artist` |
| Style consistency with the rest of the character art | `art-director` (Grace leads this) |

## On your plate now

- **Combat concept (`S1-001`)** — jointly with Luka. `design/decisions/combat-concept.md`
  lays out four options (paint-as-resource / regions-in-her-head / fold into
  gathering / defer past the slice). Work it with `game-designer` +
  `systems-designer`; **fill in the `## Decision` section** when you land it.
  This blocks your own enemy/boss art brief.
- Enemy design is derived from archetype **shadows**, not villagers — see
  `design/gdd/regions-and-combat.md` and `villager-archetype-system.md`.
- No enemy sprite work has a brief until the concept is chosen.

## First steps

1. `docs/TEAM-HANDBOOK.md` §2.2 — Unity 6.3 + the `Assets/LaRasa/README.md`
   checklist.
2. Read `design/gdd/regions-and-combat.md` (it's deliberately a stub) and the
   combat options doc.
3. In Claude Code: *"help me and Luka run the combat concept decision — start
   from the options doc, pull in game-designer and systems-designer."*

## Watch out for

- Pillar 2 ("Rasa Never Speaks") — the "mute painter with a weapon" tension is
  the exact risk the GDD flags. Weigh it explicitly.
- Combat has **no ADR and no control-manifest rules yet** — design only, no
  implementation code, until `S1-001` resolves.
