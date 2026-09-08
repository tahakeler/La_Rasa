# Onboarding — Non

**Role**: Archetype division (with Jack), persona/shadow narrative writing,
some character art/animation (~half week).

## Your agents

| For | Agent |
|-----|-------|
| Story architecture, the persona/shadow split, character arcs | `narrative-director` |
| Actual dialogue and prose | `writer` |
| World rules, faction/culture consistency | `world-builder` |
| The character-art side of your time | `art-director` / `technical-artist` |
| A coordinated narrative pass across all of the above | `/team-narrative` |

## On your plate now

- **Vertical-slice villager selection (`S1-002`)** — jointly with Jack.
  `design/decisions/vertical-slice-villager-selection.md` lays out the options
  and the zone-contiguity tension (the proposed season split isn't
  wheel-contiguous). **This blocks your own archetype-division work** — decide
  it first, record it in the doc's `## Decision` section.
- Then divide the twelve archetype positions between writers/artists, starting
  from the slice 3.
- Persona/shadow rule (Pillar 3): shadow content appears **only** through the
  painting scene, **never** in daytime dialogue. `/content-audit` checks this.

## First steps

1. `docs/TEAM-HANDBOOK.md` §2 — Git + Node + Claude Code. You can skip Unity;
   you mostly edit `design/` and (later) dialogue data assets.
2. Read `design/gdd/villager-archetype-system.md`, `twelve-content-framework.md`,
   `narrative-portrait-system.md`, and ADR-0002 (persona/shadow separation).
3. In Claude Code: *"help me and Jack decide the vertical-slice villagers —
   start from the options doc, and check the crossfade/contiguity constraint."*

## Watch out for

- Archetype/flower/season names are **placeholders** you're expected to
  rename — but keys and colours are structurally fixed (circle of fifths /
  colour wheel). Don't touch those.
- Do the villager selection **before** dividing archetypes, or the division
  work gets redone.
