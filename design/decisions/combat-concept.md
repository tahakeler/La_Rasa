# Decision: Combat concept

> **Owners**: Luka, Oasis
> **Sprint 1 task**: S1-001 · **Status**: OPEN — no call made
> **GDD**: `design/gdd/regions-and-combat.md` (Open Questions)
> **Prepared**: 2026-09-08 — options only, per the collaboration protocol.
> This document deliberately does not choose.

## Why this blocks things

The four-region *structure* is fixed (each region = a quarter of the
archetype wheel, 3 enemies + 3 bosses). What the player *does* when they
fight is not. Until it's chosen:

- `production/epics/twelve-content-framework` is fine, but **Regions & Combat
  cannot be epic'd** (no ADR possible — see `control-manifest.md` "Open /
  Blocked Areas").
- Enemy/boss sprite work (Oasis + Grace) has no brief.
- The Villager Archetype System's "shadows feed enemy design" link stays
  abstract.
- `farm-and-economy.md` can't finalise whether paint is one resource or two.

It is named a **Week-1 blocking decision** in `game-concept.md`,
`systems-index.md`, and the Sprint 1 risk table.

## What the source GDD actually says

Two directions are *named, not chosen*:
1. **Paint as a combat resource** — fighting and painting draw on the same
   pigment, so combat trades against reflection.
2. **Regions are "in her head"** — they're not literal places, which
   sidesteps the tonal oddity of a mute painter carrying a sword.

The GDD also says the region names/themes are first drafts ("throw the whole
set out. Only the structure matters").

## Options

### Option A — Paint-as-resource, real regions

Combat is a real-time action verb in literal explorable regions. Milled
flowers/paint are spent on both combat abilities and painting sessions.

- **Pros**: Genre-legible (Stardew has combat regions); makes the farm
  economy matter for more than one system; creates a real strategic tension
  the GDD's "Key Dynamics" section already predicts players will feel.
- **Cons**: A mute painter with a weapon is the exact tonal risk the GDD
  flags; real-time combat is the single biggest new implementation surface
  in the game; competes for scope with the painting scene, which is the
  actual differentiator.
- **Unblocks**: full Regions & Combat GDD + ADR; enemy sprite briefs;
  tool-tier gating design.
- **Scope**: L — new input scheme, enemy AI, balance pass, VFX.

### Option B — Regions "in her head", abstracted encounters

Regions are interior/symbolic. "Combat" is a reflective or puzzle-like
interaction with an archetype's shadow — closer to the painting scene's
verb than to action combat. No weapon.

- **Pros**: Tonally coherent with "Rasa Never Speaks" and the reflection
  core; lets the painting-scene mechanic (or a sibling of it) carry more of
  the game; less new tech.
- **Cons**: Further from Stardew expectations (a market the GDD is
  deliberately narrowing anyway); risk of feeling like "the painting scene
  again but worse"; "encounter" design is less well-understood than action
  combat, so more design iteration.
- **Unblocks**: same as A, but the ADR is about a bespoke encounter system
  rather than an action-combat stack.
- **Scope**: M-L — depends how much it reuses the painting-scene tech.

### Option C — Fold "combat" into resource gathering (no distinct combat verb)

There is no fight. Regions gate by tool tier and hold fishing/foraging/mining
nodes; the "enemies" become environmental hazards or timed resource
challenges. The 3-enemies-3-bosses structure becomes 3 hazard types + 3
"region challenge" set-pieces per region.

- **Pros**: Smallest scope; keeps the game entirely non-violent; the
  vertical slice's "1 region with combat" line becomes "1 region",
  removing a slice risk; nothing competes with the painting scene.
- **Cons**: Abandons an explicit GDD structural element (enemies from
  shadows); "bosses" need reframing; may read as content-thin to the
  hardcore farming-sim audience the GDD targets.
- **Unblocks**: Regions epic immediately, as a Farm & Economy extension
  rather than its own combat system.
- **Scope**: S-M.

### Option D — Defer past the vertical slice

Explicitly cut combat from the vertical slice. The slice ships with farming
+ one villager's full painting arc + one region's *non-combat* content.
Decide combat during Alpha planning with a real prototype.

- **Pros**: Removes the Week-1 blocker entirely; matches the MVP definition
  in `game-concept.md` which already lists combat as "explicitly NOT in
  MVP"; buys time for a combat `/prototype`.
- **Cons**: `regions-and-combat.md` stays a stub for months; Oasis's
  enemy-sprite track has no work; the slice demonstrates less of the full
  vision.
- **Unblocks**: nothing new now — but unblocks *everyone else* by taking
  combat off the critical path.

## What to weigh (not a recommendation)

- How much of the team's remaining semester capacity should go to a system
  the GDD itself calls secondary to the painting scene?
- Does "Rasa carries a weapon" survive a read of Pillar 2?
- The vertical slice needs *3 contiguous zones* regardless (see
  `vertical-slice-villager-selection.md`) — only Option A/B give the "1
  region with combat" slice line real content.

## Decision

*Owner to complete:*

- **Chosen option**:
- **Decided by / date**:
- **Rationale (1-2 lines)**:
- **Follow-ups**: update `regions-and-combat.md` Open Questions · run
  `/architecture-decision` if A/B/C · update `control-manifest.md` ·
  update `sprint-1.md` S1-001.
