# Regions & Combat

> **Status**: Draft — structure only. **Combat concept is explicitly undecided in source GDD and is NOT resolved here.**
> **Author**: game-designer (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Twelve Is the Structure, Not the Content

## Summary

Four regions (Greenhollow, Tidewrack, The Deep Kiln, The Burnt Reach), each
covering a quarter of the archetype wheel (three archetypes each), for ore,
forage, fish, and fighting. The region and enemy/boss *structure* is fixed
by source GDD; the actual combat mechanic is explicitly open, owned by Luka
and Oasis. **This document does not invent an answer to that question** —
per the collaboration protocol, that decision belongs to its owners, not to
this ingestion pass.

> **Quick reference** — Layer: `Feature` · Priority: `Vertical Slice` · Key deps: `Villager Archetype System`, `Farm & Economy` — **BLOCKED on combat concept decision**

## Overview

Structurally, the four regions are settled: each is a quarter of the twelve-
archetype wheel, giving 3 enemy types and 3 bosses per region (12 total of
each). What happens when the player actually fights something is not
settled. Two directions are named in the source GDD, not chosen between:
paint as a combat resource (fighting and painting compete for the same
pigment), or the regions existing somewhere in Rasa's head rather than as
real places (which sidesteps the tonal oddity of a mute painter carrying a
sword).

## Player Fantasy

Not yet definable — depends entirely on the combat concept decision below.

## Detailed Rules

### Core Rules — Fixed Structure

1. Four regions, each a quarter of the archetype wheel:

| Region | Theme | Archetypes | Enemy Types | Bosses |
|--------|-------|------------|--------------|--------|
| Greenhollow | Woodland, early region | Innocent, Jester, Caregiver | 3 | 3 |
| Tidewrack | Coast & marsh | Explorer, Sage, Orphan | 3 | 3 |
| The Deep Kiln | Caves below, goes down in floors, late materials | Magician, Lover, Hero | 3 | 3 |
| The Burnt Reach | Highlands & ruin | Rebel, Creator, Ruler | 3 | 3 |

2. Enemies are built from archetype **shadows**, not from the villagers
   themselves — e.g., the Innocent's shadow (Denial) might manifest as an
   enemy that ignores damage for a beat before it lands; the Ruler's shadow
   (Tyranny) might manifest as something that restricts player movement.
   This is a thematic derivation, illustrative in the source GDD, not a
   commitment to specific mechanics.
3. Regions gate by tool tier and ore. Greenhollow is the early region; The
   Deep Kiln goes down in floors and holds the latest materials.
4. Region names/themes are explicitly marked as first drafts in the source
   GDD ("they sound it. Rename them, rethink them, or throw the whole set
   out. Only the structure matters: four regions, three archetypes each.")

### Core Rules — Explicitly Undecided

**Not specified, and not to be guessed at by this document:**
- What combat actually is mechanically (real-time? turn-based? something
  else entirely?)
- Whether paint is a combat resource, and if so, how it trades off against
  the painting/reflection economy
- Whether "regions are in her head" changes the fiction, the mechanics,
  both, or neither
- Whether combat exists as a distinct verb at all, versus being folded into
  fishing/foraging/mining-style resource interactions

### States and Transitions

Not specified — depends on the combat decision.

### Interactions with Other Systems

| System | Data Flow |
|--------|-----------|
| Villager Archetype System | Provides shadow themes for enemy/boss design |
| Farm & Economy | Tool-tier progression gates region access; ore is a farm-economy resource |
| Reflection & Painting Scene | Possible resource competition with paint, pending combat decision |

## Formulas

None — cannot be specified before the combat concept is chosen.

## Edge Cases

Not specified — cannot be meaningfully defined before the combat concept is
chosen.

## Dependencies

| System | Direction | Nature of Dependency |
|--------|-----------|-------------------------|
| Villager Archetype System | This depends on it | Shadow theming for enemies |
| Farm & Economy | This depends on it | Tool-tier gating, ore as resource |
| Reflection & Painting Scene | Possible dependency (unconfirmed) | If paint is a combat resource |

## Tuning Knobs

None specified — pending combat decision.

## Visual/Audio Requirements

Not specified — Oasis is noted as working on enemy/boss sprites alongside
Grace as "the natural overlap" with the art pod, but no concrete asset list
exists yet.

## Game Feel

Cannot be specified — this section is entirely gated on the combat concept
decision. Do not fill this in speculatively; a feel target written before
the mechanic is chosen will just be wrong and will need to be rewritten.

## UI Requirements

Not specified.

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|-----------------------------|-----------|----------------------------------|--------|
| Shadow theming | `design/gdd/villager-archetype-system.md` | Shadow column | Data dependency |
| Tool-tier gating | `design/gdd/farm-and-economy.md` | Tool upgrade progression | Rule dependency |
| Possible paint resource competition | `design/gdd/painting-and-reflection.md` | Paint/flower resource | Data dependency (tentative) |

## Acceptance Criteria

Cannot be written before the combat concept exists. Placeholder — do not
treat as complete:
- [ ] BLOCKED: combat concept decision (owner: Luka, Oasis)

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| **Combat concept** — paint-as-resource vs. "regions in her head" vs. a third direction | Luka, Oasis | This is a Week-1 blocking decision — nothing else in this document can proceed until it's resolved | — |
| Final region names/themes (currently first-draft placeholders) | `game-designer`, `narrative-director` | Before Alpha content production | — |
