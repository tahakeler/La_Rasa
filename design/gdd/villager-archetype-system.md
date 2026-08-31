# Villager Archetype System

> **Status**: Draft — ingested from GDD v1.3
> **Author**: narrative-director (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Persona and Shadow Are Structurally Separate

## Summary

Twelve romanceable villagers, each built on one of Jung's twelve archetypes,
each written with two structurally distinct layers: a **persona** (what
they show the valley and say to Rasa — coherent, likeable, a construction)
and a **shadow** (the part of the archetype they can't look at — revealed
only through the painting system). The archetype is writing scaffolding;
the player never sees the word "archetype" in-game.

> **Quick reference** — Layer: `Core` · Priority: `MVP` · Key deps: `Twelve Content Framework`

## Overview

Every villager exists at one of the twelve wheel positions and inherits
that position's archetype, shadow, flower, season, color, and key from the
Twelve Content Framework. Persona is delivered through ordinary daytime
dialogue (which this document does not fully specify — that's writer-owned
content). Shadow is delivered exclusively through the Reflection & Painting
Scene system, gradually, across the relationship.

## Player Fantasy

Getting to know someone as more than their surface — the pleasure of an
early read turning out to be incomplete, and eventually understanding what
someone couldn't say to your face.

## Detailed Rules

### Core Rules

1. Each villager maps 1:1 to one of the twelve archetype positions (see
   `twelve-content-framework.md`) and inherits: archetype name, shadow,
   associated flower, season, color, key, and town zone.
2. **Persona** content (daytime dialogue, gift reactions, scheduling/AI
   behavior) is written and implemented independently of shadow — it must
   stand alone as a complete, coherent character on its own.
3. **Shadow** content is delivered ONLY through portrait reveal stages
   inside the Reflection & Painting Scene (see that GDD) — never through
   daytime dialogue. This is a hard separation (Pillar 3).
4. All twelve villagers are romanceable (relationship hearts, per-villager,
   count of 12).
5. Enemy design in Regions & Combat is explicitly derived from villager
   shadows, not from villagers themselves — e.g., "the Innocent's shadow is
   denial, so something in the first region might ignore damage for a beat
   before it lands." This is a thematic link, not a literal 1:1 villager→
   enemy mapping.

### States and Transitions

| State | Entry Condition | Exit Condition | Behavior |
|-------|-------------------|-------------------|----------|
| Story not yet moved | Default / no recent interaction | A relationship-advancing interaction occurs | Portrait stays greyed out on sleep screen |
| Story moved | A relationship-advancing interaction occurred that day | Player reflects on them (enters painting scene) or the day ends unused | Portrait lit on sleep screen |
| Portrait stage: Early | First reflection completed | Sufficient painting sessions accumulate | Abstract, hard to read imagery |
| Portrait stage: Middle | Early stage complete | Sufficient sessions accumulate | Portrait content starts to disagree with the daytime persona |
| Portrait stage: Late | Middle stage complete | Sufficient sessions accumulate | Shows the shadow clearly |
| Portrait stage: Finished | Late stage complete | Terminal | Reads properly in hindsight; closes that villager's questline |

*(Exact session-count thresholds per stage are not specified in source
GDD — see Open Questions.)*

### Interactions with Other Systems

| System | Data Flow |
|--------|-----------|
| Reflection & Painting Scene | Provides villager identity, current portrait stage, and "story moved today" flag; receives painting-session outcomes that advance portrait stage |
| Regions & Combat | Provides shadow themes for enemy/boss design (thematic, not 1:1) |
| Twelve Content Framework | Consumes archetype, shadow, flower, season, color, key, zone per position |

## Formulas

Not specified — relationship-heart progression and portrait-stage
thresholds need concrete values before implementation. Flag for
`systems-designer` + `narrative-director`.

## Edge Cases

| Scenario | Expected Behavior | Rationale |
|----------|---------------------|-----------|
| Player never interacts with a villager | Their portrait never lights up; their questline never advances | Implicit from source GDD's design — no forced progression |
| A villager's persona and shadow content contradicts in a way that reads as inconsistent rather than revealing | Flag to `narrative-director` for review | This is the core writing risk the persona/shadow split creates — needs an editorial pass, not just individual writers working in isolation |

## Dependencies

| System | Direction | Nature of Dependency |
|--------|-----------|-------------------------|
| Twelve Content Framework | This depends on it | Archetype/shadow/flower/season/color/key/zone per villager |
| Reflection & Painting Scene | Bidirectional | Provides trigger data; receives portrait-stage advancement |
| Regions & Combat | Depends on this | Shadow themes for enemy design |
| Narrative / Portrait Story System | Depends on this | Persona/shadow content IS the questline content |

## Tuning Knobs

| Parameter | Current Value | Safe Range | Effect of Increase | Effect of Decrease |
|-----------|----------------|------------|----------------------|----------------------|
| Portrait stages per villager | 4 (early/middle/late/finished) | Not specified as adjustable | — | — |
| Sessions required per stage | Not specified | — | Slower reveal, more grinding | Faster reveal, less depth per session |

## Visual/Audio Requirements

| Event | Visual Feedback | Audio Feedback | Priority |
|-------|-------------------|-------------------|----------|
| Portrait stage advances | Portrait artwork updates to reveal more (painted by Lala, creative lead) | Villager's key/theme, per Sound & Adaptive Crossfade | MVP |

## Game Feel

Not applicable directly to this data/writing system — feel targets belong
to the Reflection & Painting Scene GDD, which is where the player actually
interacts with this content.

## UI Requirements

| Information | Display Location | Update Frequency | Condition |
|--------------|---------------------|------------------------|-----------|
| Relationship hearts | Villager info UI (not further detailed) | On interaction | Always |
| Portrait lit/unlit state | Sleep screen (ring layout, see `painting-and-reflection.md`) | Per day | At sleep |

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|-----------------------------|-----------|----------------------------------|--------|
| Archetype/shadow/flower/season/color/key per villager | `design/gdd/twelve-content-framework.md` | Full position table | Data dependency |
| Portrait stage advancement | `design/gdd/painting-and-reflection.md` | Painting scene session outcome | State trigger |
| Shadow themes feed enemy design | `design/gdd/regions-and-combat.md` | Enemy behavior design | Rule dependency |
| Persona/shadow content forms questlines | `design/gdd/narrative-portrait-system.md` | Twelve questlines | Ownership handoff |

## Acceptance Criteria

- [ ] All twelve villagers implemented with distinct persona content
  (dialogue, gift reactions, schedule/AI) that stands alone as coherent
- [ ] Shadow content never appears in daytime dialogue — verified by
  content audit, not just code review
- [ ] Portrait stage state persists per villager and correctly gates
  reveal content
- [ ] All twelve are romanceable with functioning relationship hearts

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| Which 3 villagers are in the vertical slice, and are their wheel positions contiguous? | Non, Jack | Before archetype division work starts — this is the zone-contiguity decision flagged in `game-concept.md` | — |
| Sessions-per-portrait-stage thresholds | `narrative-director`, `systems-designer` | Before Reflection & Painting Scene implementation | — |
| Relationship-heart gain formula (what actions advance a relationship, by how much) | `economy-designer` or `systems-designer` | Before implementation | — |
