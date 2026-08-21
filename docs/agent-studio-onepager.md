# How We're Using the Claude Code Game Studio Tool

## What this even is
This repo comes with a framework of Claude Code subagents — basically a virtual
studio. Each agent is a specialist (game designer, gameplay programmer, AI
programmer, art director, etc.) that only touches its own lane, so we don't
end up with one giant context trying to hold the whole game in its head at
once. It shipped as a generic scaffold supporting three engines; we've since
trimmed it down to just what we need.

## The game, quickly
Stardew Valley-style farming sim (daily loop, crops, relationships, town),
built in **Godot 4.6 / GDScript**. The twist is a painting mechanic tied to
the protagonist's imagination (working name: Rasa) — what she paints seems
to bleed into the world somehow. Exact mechanic + how it ties into combat is
still TBD, see below.

## Engine setup — locked in
Godot 4.6, GDScript by default (we can move performance-critical systems to
C# later via `godot-csharp-specialist` if we need to). This is reflected in
`CLAUDE.md` and `technical-preferences.md` now — no more `[TO BE CONFIGURED]`
placeholders. The engine-reference docs (API snapshots pinned to 4.6, since
the model's training data stops well before this version) were already
sitting in the repo, so that part was free.

## Agent roster — what we did
**Cut:** all 5 Unity specialists and all 5 Unreal specialists — 10 agents
total. We don't need three engines' worth of opinions in the repo. Roster
went from 49 agents down to 39. Also cleaned up every doc that listed
Unity/Unreal as options (roster tables, the coordination map, the workflow
guide, quick-start) so nothing points at an agent that no longer exists.

**Kept as-is:** the 5 Godot specialists (`godot-specialist` plus
GDScript/C#/shader/GDExtension sub-specialists), and the standard
design/production/QA bench — `game-designer`, `systems-designer`,
`economy-designer`, `ai-programmer`, `gameplay-programmer`, `ui-programmer`,
`narrative-director`, `world-builder`, `art-director`, `technical-artist`,
`producer`, `qa-lead`/`qa-tester`, etc. None of these needed edits.

**Still open:** whether `network-programmer` stays active. Stardew-style
games often ship drop-in co-op — if we want that, keep it; if we're staying
single-player, it can go dormant. Not deciding this blind.

**Not created yet:** a dedicated painting-system specialist. We don't know
its shape yet — depends on the brainstorm below.

## Stuff we don't need to reinvent
A lot of farming-sim scaffolding is well-trodden ground and existing agents
already cover it:
- **Crafting/build menus** → `systems-designer` for recipe/formula logic,
  `ui-programmer` + `godot-specialist` for the menu itself (Control nodes,
  theming). Note: our crafting system runs on a "rule of 12" (12
  categories/slots — needs a real spec, see below).
- **Daily cycle + NPC schedules/pathing** → `ai-programmer` (behavior
  trees/state machines, Godot navmesh pathing) is the direct fit — standard
  Stardew-style daily-routine AI.
- **Basic economy** (selling, shop loops) → `economy-designer`.

None of this needs custom agents — it's exactly what the framework already
ships with.

## What actually needs brainstorming (not solved yet)
- **The rule of 12**: is this literally 12 crafting archetypes/categories?
  The Jungian 12-archetype wheel we're looking at (Innocent/Sage/Explorer/
  Outlaw/Magician/Hero/Lover/Jester/Everyman/Caregiver/Ruler/Creator, grouped
  under Provide Structure / Spiritual Journey / Leave a Mark / Connect to
  Others) looks like a candidate frame for organizing crafting, painting
  styles, or companion personalities — but that's a hypothesis, not a
  decision yet.
- **Painting mechanic**: what does painting actually *do* in-world? Cosmetic,
  functional, narrative?
- **Combat**: "basic but differentiated" — is combat literally an extension
  of painting (paint as weapon/tool)? Undecided.

These three are tangled together and belong in a real `/brainstorm` session
with `game-designer` + `narrative-director` (Rasa's imagination angle) before
any GDD gets written.

## Next steps
1. Decide `network-programmer`'s fate (co-op or not)
2. `/brainstorm` on painting + combat + the "rule of 12" framing
3. Once that's settled, `/map-systems` to lay out the full systems list and
   dependency order
