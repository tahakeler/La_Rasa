# Narrative / Portrait Story System

> **Status**: Draft — ingested from GDD v1.3. Contains a spoiler-sensitive
> section (see below) — keep this file's ending-related content out of any
> external-facing document.
> **Author**: narrative-director (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Rasa Never Speaks; Persona and Shadow Are Structurally Separate

## Summary

Twelve questlines, one per villager, told entirely through the portraits
rather than a central plot — "there's no world to save." Each portrait
uncovers in four stages (early/middle/late/finished, defined in
`villager-archetype-system.md`), and there is a planned twist once all
twelve are complete: the twelve portraits share more in common with each
other than they should, which needs to be planned into the artwork before
painting, not discovered after.

> **Quick reference** — Layer: `Presentation` · Priority: `Vertical Slice` · Key deps: `Villager Archetype System`, `Reflection & Painting Scene`

## Overview

This system is the content layer that fills the persona/shadow structure
with actual story. There's no central plot — narrative weight comes
entirely from getting to know twelve people and watching what they're not
saying resolve into what they are. The ending is emergent: once all twelve
portraits are finished, their commonalities recontextualize the whole cast.

## Player Fantasy

The accumulating pleasure of pattern-recognition across twelve independent
relationships, paying off in a way only visible in hindsight.

## Detailed Design

### Core Rules

1. One questline per villager (twelve total), delivered through portrait
   stages, not through a dialogue-branch quest log.
2. **Early** stage: abstract, hard to read.
3. **Middle** stage: portrait content starts to disagree with the persona
   the player has been getting from daytime dialogue — this is the
   intended "wait, that's not what they said" moment.
4. **Late** stage: shows the shadow clearly.
5. **Finished** stage: "reads properly in hindsight" — completing a
   portrait closes that villager's questline.
6. **Spoiler-sensitive**: there is a planned reveal once all twelve
   portraits are finished — the twelve portraits are designed to share
   more in common with each other than they should. This requires the
   repeated visual details to be planned into the portrait artwork
   BEFORE painting begins, not retrofitted. Source GDD explicitly says
   this is "worth keeping quiet outside the team."

### States and Transitions

See `villager-archetype-system.md` for the shared portrait-stage state
machine — this document is the content specification for what each stage
contains, not a separate state system.

### Interactions with Other Systems

| System | Data Flow |
|--------|-----------|
| Villager Archetype System | Provides the persona/shadow split and portrait-stage state this system's content fills |
| Reflection & Painting Scene | Player interactions in the painting scene are what advance a questline's stage |

## Formulas

Not applicable — this is a content/writing system, not a numeric one.

## Edge Cases

| Scenario | Expected Behavior | Rationale |
|----------|---------------------|-----------|
| Player finishes all twelve portraits | Triggers the cross-portrait reveal/twist | Explicit in source GDD, though the exact trigger/presentation mechanism isn't specified |
| Player finishes portraits out of order | Should work fine — no stated dependency between villagers' individual questlines | Inferred; confirm with `narrative-director` |

## Dependencies

| System | Direction | Nature of Dependency |
|--------|-----------|-------------------------|
| Villager Archetype System | This depends on it | Persona/shadow split, portrait-stage state machine |
| Reflection & Painting Scene | This depends on it | Player interactions that advance questline stages |

## Tuning Knobs

None specified — this is narrative content, not a balance system.

## Visual/Audio Requirements

| Event | Visual Feedback | Audio Feedback | Priority |
|-------|-------------------|-------------------|----------|
| Portrait stage content | Painted artwork per stage (Lala, creative lead — one portrait finished, two in progress as of source GDD) | Villager's key, per Sound & Adaptive Crossfade | Vertical Slice |
| All-twelve completion reveal | Not yet specified — needs a dedicated design pass once individual questlines are further along | Not specified | Alpha+ |

## Game Feel

Not applicable — this is narrative content, not a mechanical system with
its own feel target. See `painting-and-reflection.md` for the feel of the
interaction that delivers this content.

## UI Requirements

Not specified beyond the portrait-ring sleep screen already documented in
`painting-and-reflection.md`.

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|-----------------------------|-----------|----------------------------------|--------|
| Portrait-stage state machine | `design/gdd/villager-archetype-system.md` | Early/middle/late/finished states | Ownership handoff |
| Stage advancement trigger | `design/gdd/painting-and-reflection.md` | Painting session outcome | State trigger |

## Acceptance Criteria

- [ ] All twelve villagers have complete early/middle/late/finished content
- [ ] Middle-stage content demonstrably disagrees with each villager's
  persona in a way playtesters notice
- [ ] Repeated cross-portrait details are planned and consistent across all
  twelve pieces of artwork before final painting begins
- [ ] The all-twelve-complete reveal is implemented and tested once
  designed

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| What exactly do the twelve portraits share, and how is it revealed? | `narrative-director`, Lala | Before final portrait artwork begins — this blocks the art pipeline, not just the writing | Source GDD explicitly withholds the answer even from this document |
| Presentation mechanism for the all-twelve reveal | `narrative-director`, `game-designer` | Alpha | — |
