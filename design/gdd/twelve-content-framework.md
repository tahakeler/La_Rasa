# Twelve Content Framework

> **Status**: Draft — ingested from GDD v1.3
> **Author**: game-designer (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Twelve Is the Structure, Not the Content

## Summary

A single shared data structure — twelve positions on an archetype wheel,
each carrying an archetype, flower, color, musical key, and town zone —
that every other content system in the game (villagers, crafting, town
layout, music, enemies) is built on top of. This is the foundation layer;
nothing else should invent its own counting or mapping scheme.

> **Quick reference** — Layer: `Foundation` · Priority: `MVP` · Key deps: `None`

## Overview

The game commits to "twelve" as its content constraint across nearly every
category — twelve villagers, twelve vegetables, twelve fish, and so on. Per
the source GDD, this is "mostly useful as a stopping point": it isn't a
narrative requirement, it's a scope discipline that also happens to map
cleanly onto a Jungian twelve-archetype wheel, the twelve-key circle of
fifths, and the twelve-hue color wheel.

## Player Fantasy

Not player-facing directly — this is a production/design constraint, not a
mechanic the player interacts with as such. Its effect on player experience
is coherence: every system in the game feels like it belongs to the same
world because it's built from the same twelve-position structure.

## Detailed Design

### Core Rules

1. Twelve archetype positions exist, ordered clockwise starting from the
   top of the wheel: Innocent (C), Jester (G), Caregiver (D), Explorer (A),
   Sage (E), Orphan (B), Magician (F#), Lover (Db), Hero (Ab), Rebel (Eb),
   Creator (Bb), Ruler (F).
2. Each position has five fixed attributes: **archetype**, **shadow**
   (the archetype's characteristic failure mode), **flower**, **season**,
   **color**, and **musical key**.
3. Position order follows the circle of fifths (musical key assignment),
   which also determines color-wheel position (colors assigned in the same
   order) and town-zone adjacency (zones ring the square in wheel order).
4. Content categories that use "one per archetype" (villagers, town zones,
   enemy types, bosses, festivals) inherit their ordering and grouping
   directly from this table — they do not get independently authored
   orderings.
5. Content categories that use "twelve, split some other way" (e.g., "3 per
   season") still total twelve but are NOT directly indexed to archetype
   position — see the full inventory table below.

### The Twelve Positions (fixed data)

| Pos | Key | Archetype | Shadow | Flower | Season | Color |
|-----|-----|-----------|--------|--------|--------|-------|
| 1 | C | Innocent | Denial | Daffodil | Spring | Yellow |
| 2 | G | Jester | Cruelty | Nasturtium | Summer | Yellow-Green |
| 3 | D | Caregiver | Martyrdom | Lady's Mantle | Autumn | Green |
| 4 | A | Explorer | Rootlessness | Sea Holly | Winter | Blue-Green |
| 5 | E | Sage | Dogma | Salvia | Spring | Blue |
| 6 | B | Orphan | Victimhood | Forget-me-not | Summer | Blue-Violet |
| 7 | F# | Magician | Manipulation | Monkshood | Autumn | Violet |
| 8 | Db | Lover | Obsession | Fuchsia | Winter | Red-Violet |
| 9 | Ab | Hero | Arrogance | Gladiolus | Spring | Red |
| 10 | Eb | Rebel | Nihilism | Crocosmia | Summer | Red-Orange |
| 11 | Bb | Creator | Perfectionism | Bird of Paradise | Autumn | Orange |
| 12 | F | Ruler | Tyranny | Crown Imperial | Winter | Yellow-Orange |

**Status**: names, flowers, and season assignments are explicitly marked
placeholder in the source GDD ("all editable"). Keys and colors are
structurally fixed (derived from circle of fifths / color wheel) and much
harder to move without breaking the music/zone-adjacency design.

### States and Transitions

Not applicable — this is a static data table, not a stateful system.

### Interactions with Other Systems

| System | Data Consumed |
|--------|----------------|
| Villager Archetype System | Archetype, shadow, flower, season → villager identity |
| Sound & Adaptive Crossfade | Key, wheel position → zone music assignment, crossfade adjacency |
| Regions & Combat | Shadow, wheel-quarter grouping → enemy/boss design per region |
| Town/Zone Layout *(inferred)* | Color, wheel position → zone visual identity and physical adjacency |
| Farm & Economy | Flower, season → the twelve paint-crop flowers |

## Formulas

No numeric formulas — this system is a fixed lookup table, not a
calculation.

## Edge Cases

| Scenario | Expected Behavior | Rationale |
|----------|---------------------|-----------|
| Content category doesn't map 1:1 to archetype (e.g., "3 per season" fish) | Use season/category split instead of direct archetype indexing | Source GDD's own full inventory (see `farm-and-economy.md`) mixes both schemes deliberately |
| A future content category doesn't fit twelve at all | Flag to `game-designer` before adding — twelve is a deliberate constraint, not an accident | Protects Pillar 1 |

## Dependencies

| System | Direction | Nature of Dependency |
| ---- | ---- | ---- |
| Villager Archetype System | Depends on this | Consumes archetype/shadow/flower/season per position |
| Sound & Adaptive Crossfade | Depends on this | Consumes key + wheel position |
| Regions & Combat | Depends on this | Consumes shadow + wheel-quarter grouping |
| Town/Zone Layout *(inferred)* | Depends on this | Consumes color + wheel position |

## Tuning Knobs

| Parameter | Current Value | Safe Range | Effect of Increase | Effect of Decrease |
|-----------|----------------|------------|----------------------|----------------------|
| Content count per category | 12 (fixed) | N/A — structural constraint | N/A | N/A |
| Archetype/flower/season name assignments | Placeholder | Freely editable | — | — |
| Key/color assignments | Fixed (circle of fifths / color wheel derived) | Not recommended to change without re-deriving music + zone design | — | — |

## Visual/Audio Requirements

Not applicable directly — see downstream systems (Sound, Town/Zone Layout)
for concrete asset requirements derived from this table.

## Game Feel

Not applicable — this is a data/production system with no direct player
interaction.

## UI Requirements

Not applicable directly, though any UI that displays "the wheel" (e.g., the
sleep-screen portrait ring described in `painting-and-reflection.md`) reads
its layout from this table.

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|---------------------------|-----------|-------------------------------|--------|
| Villager identity is built from this table | `design/gdd/villager-archetype-system.md` | Archetype, shadow, flower, season columns | Data dependency |
| Zone/key mapping is built from this table | `design/gdd/sound-and-crossfade.md` | Key, wheel position columns | Data dependency |
| Enemy/region design is built from this table | `design/gdd/regions-and-combat.md` | Shadow, wheel-quarter grouping | Data dependency |

## Acceptance Criteria

- [ ] All twelve positions are implemented as a single data asset (e.g., a
  ScriptableObject array) that other systems reference rather than
  duplicate
- [ ] Renaming a placeholder value (archetype name, flower, season) in one
  place propagates everywhere it's used
- [ ] No hardcoded archetype/color/key values exist outside this data asset

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| Should key/color assignment ever be allowed to change, or are they permanently fixed once music production starts? | `technical-director`, `audio-director` | Before Sound & Adaptive Crossfade production begins | — |
