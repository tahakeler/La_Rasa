# Team → Agent Mapping

*Created: 2026-08-31, from GDD v1.3 team assignments ("Who's doing what")*

Maps each team member's stated responsibilities to the Claude Code agents
that cover that domain, so everyone knows which agent to reach for. This is
a starting point, not a constraint — anyone can invoke any agent for any
task; this just maps the GDD's existing assignments onto the roster.

| Person | GDD Role | Relevant Agents |
|--------|----------|-------------------|
| **Lala** | Creative lead, painter — twelve portraits, art direction across the whole project | `art-director` (direction questions), `technical-artist` (portrait implementation as game assets), `narrative-director` (portrait content ties into questlines) |
| **Taha** | Tooling, systems, 4 archetypes | `technical-director` / `producer` (tooling and framework setup — this session's work), `systems-designer` (archetype-linked system design), `narrative-director` (archetype writing) |
| **Non** | Archetype division, narrative (persona/shadow), character art/animation (~half week) | `narrative-director`, `writer` (persona/shadow content), `art-director`/`technical-artist` (character art side) |
| **Jack** | Archetype division, narrative — writing one villager end-to-end first as a measuring stick | `narrative-director`, `writer` |
| **Grace** | Character art/style pod lead — landing a consistent aesthetic across 13 characters | `art-director` (style leadership), `technical-artist` (pipeline/consistency across artists) |
| **Luka** | Core systems — crafting/build logic + menus, daily-cycle AI/pathing | `systems-designer` (crafting/menu design), `ai-programmer` (daily-cycle AI, pathing), `ui-programmer` + `unity-ui-specialist` (menus) |
| **Oasis** | Core systems — combat concept (with Luka); art/animation (~half week, enemy/boss overlap) | `systems-designer`/`game-designer` (combat design, once the concept is chosen), `gameplay-programmer` (implementation), `technical-artist` (enemy/boss sprites) |
| **Ken** | OST composer — twelve tracks, circle-of-fifths keys, beat-matched crossfades | `audio-director` (direction, crossfade implementation strategy), `sound-designer` (track-level specs) |

## Notes

- **Luka and Oasis jointly own the combat concept decision** flagged
  throughout `design/gdd/regions-and-combat.md` — this is the single
  biggest blocker in the current design docs. Recommend they use
  `game-designer` + `systems-designer` together for that session rather
  than deciding informally, since it has downstream effects on
  `ai-programmer` (enemy behavior) and `technical-artist` (enemy/boss
  visual design).
- **Non and Jack jointly own the archetype-division / zone-contiguity
  decision** — also flagged as a Week-1 blocker, since it determines which
  three villagers/zones are in the vertical slice.
- No one on the team is currently assigned to QA, audio implementation
  (vs. composition), or release/build management — expected, given this is
  a pre-production semester team, but worth knowing `qa-lead`/`qa-tester`
  and `devops-engineer` exist for when that need arrives.
- `unity-specialist` and its sub-specialists (`unity-dots-specialist`,
  `unity-shader-specialist`, `unity-addressables-specialist`,
  `unity-ui-specialist`) are available to anyone doing Unity-specific
  implementation work, regardless of which "core systems" person is asking.
