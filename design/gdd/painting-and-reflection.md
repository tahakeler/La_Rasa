# Reflection & Painting Scene

> **Status**: Draft — ingested from GDD v1.3 · **HIGH RISK, needs `/prototype` before further design investment**
> **Author**: game-designer (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Rasa Never Speaks; Persona and Shadow Are Structurally Separate

## Summary

The game's central differentiator. At bedtime, if a villager's story moved
that day, their portrait lights up on a ring around Rasa's head; picking one
enters a twin-stick "painting scene" where the player reads the villager's
true emotional state and pushes for depth, gradually uncovering their
portrait. Per the source GDD, the entire game is measured against whether
this is fun on its own: "If that's fun the rest is content. If it isn't,
more content won't fix it."

> **Quick reference** — Layer: `Feature` · Priority: `MVP` · Key deps: `Villager Archetype System`, `Daily Loop & Calendar`, `Sound & Adaptive Crossfade`

## Overview

Rasa never speaks, so this system is her only expressive language. It has
two parts: the **sleep screen** (selecting who to reflect on) and the
**painting scene** itself (the twin-stick minigame that reads a villager
and advances their portrait). Both are described in concrete, implementable
detail in the source GDD — this is the most fully-specified system in the
whole document, which is appropriate given its stated importance.

## Player Fantasy

Quiet, attentive intimacy without words — the satisfaction of correctly
reading someone, and watching a portrait resolve into something true.

## Detailed Rules

### Core Rules — Sleep Screen

1. On sleep, if any villager's story moved that day, their portrait appears
   in color in a ring of twelve around Rasa's head (wheel-ordered, per
   Twelve Content Framework layout).
2. Portraits with no story movement appear greyed out.
3. If nothing is lit, Rasa just sleeps — no minigame that night.
4. If multiple portraits are lit, the player picks exactly one to reflect
   on; the rest remain lit and available on a future night.
5. Whether unused lit portraits stay lit indefinitely or decay after a few
   days is an explicit open question in the source GDD ("indefinite is
   kinder, decay gives the evening more weight — worth trying both").

### Core Rules — Painting Scene

1. Two-stick control scheme:
   - **Left stick** reads how the villager is actually feeling, on two
     axes: Expressed↔Withdrawn and Grief↔Joy.
   - **Right stick** pushes for depth, moving from Surface (persona) toward
     Core (shadow) through a Middle band.
2. The player's read is compared against the villager's true emotional
   state (ground truth, not shown directly to the player).
3. **Correct read** ("opens when your read is right"): depth access
   loosens, the canvas resolves further toward the shadow layer, colour
   warms toward the villager's colour, and their musical key resolves
   underneath (see Sound & Adaptive Crossfade).
4. **Incorrect read** ("resists when your read is off"): depth access
   resists, the canvas does not advance as far, colour/key stay thin/
   unresolved.
5. There is no score and no bar — feedback is entirely diegetic (colour,
   sound, canvas resolution).
6. A "failed" or middling session simply does not advance the portrait as
   far — there is no hard failure state. The cost of a bad session is that
   evening's opportunity, which the source GDD explicitly frames as
   sufficient cost in a farming-sim's economy of time.
7. Session length target: "a couple of minutes," and only on nights where
   something is lit.
8. Each completed session uncovers more of that villager's portrait,
   advancing it toward the next of the four stages (early/middle/late/
   finished — see `villager-archetype-system.md`).

### States and Transitions

| State | Entry Condition | Exit Condition | Behavior |
|-------|-------------------|-------------------|----------|
| Sleep screen: nothing lit | Sleep, no story movement that day | Immediate | Rasa sleeps, day ends |
| Sleep screen: portraits lit | Sleep, ≥1 story moved | Player selects one | Enter painting scene for selected villager |
| Painting scene: active | Player selects a portrait | Session timer/completion condition met (not fully specified) | Twin-stick read/depth interaction, continuous colour/sound/canvas feedback |
| Painting scene: resolved | Session ends | Immediate | Portrait stage advances (partially or fully, depending on read accuracy); return to sleep |

### Interactions with Other Systems

| System | Data Flow |
|--------|-----------|
| Villager Archetype System | Provides villager identity + current portrait stage; receives stage-advancement outcome |
| Daily Loop & Calendar | Sleep is the entry trigger |
| Sound & Adaptive Crossfade | Villager's key resolves/thins live during the scene based on read accuracy |
| Farm & Economy | Milled flowers/paint are the implied resource this scene consumes — exact consumption rule not specified (see Open Questions) |

## Formulas

Not specified in source GDD beyond qualitative behavior ("resists when off,
opens when right"). Before implementation, needs:
- A quantified "read accuracy" function comparing stick position to ground
  truth emotional state
- A mapping from accumulated accuracy over the session → portrait-stage
  advancement amount

Flag for `systems-designer` — this is exactly the kind of formula a
`/prototype` pass should help derive empirically rather than guessing
up front.

## Edge Cases

| Scenario | Expected Behavior | Rationale |
|----------|---------------------|-----------|
| Player never moves either stick | Presumably reads as maximally "off" / no advancement | Inferred, not explicit in source GDD — confirm with `game-designer` |
| Player's read starts wrong then corrects mid-session | Not specified — does the scene track a moving average, worst read, or final read? | Needs design decision before implementation |
| Portrait already at "Finished" stage and lights up again | Not specified — does a finished villager still light up on story movement? | Needs design decision |

## Dependencies

| System | Direction | Nature of Dependency |
|--------|-----------|-------------------------|
| Villager Archetype System | This depends on it | Villager identity, shadow, current portrait stage |
| Daily Loop & Calendar | This depends on it | Sleep trigger |
| Sound & Adaptive Crossfade | This depends on it | Live key resolution during the scene |
| Farm & Economy | Likely depends on it (unconfirmed) | Paint/flower resource consumption — see Open Questions |
| Narrative / Portrait Story System | Provides content to this | Portrait stage art content per villager |

## Tuning Knobs

| Parameter | Current Value | Safe Range | Effect of Increase | Effect of Decrease |
|-----------|----------------|------------|----------------------|----------------------|
| Session length target | ~2 minutes | Not yet bounded | Risk of "eating the evening" per source GDD's own stated concern | Risk of feeling too shallow to matter |
| Portrait lit decay | Undecided (indefinite vs. decay) | — | Decay: more urgency, more weight per evening | Indefinite: kinder, less pressure |

## Visual/Audio Requirements

| Event | Visual Feedback | Audio Feedback | Priority |
|-------|-------------------|-------------------|----------|
| Read correct | Colour warms toward villager's colour; canvas forms sharpen | Villager's key resolves underneath | MVP |
| Read incorrect | Colour/canvas stay thin | Key stays unresolved/thinned | MVP |
| Sleep screen | Twelve-portrait ring, greyed/lit states | Not specified | MVP |

## Game Feel

### Feel Reference

Not specified in source GDD by name-drop reference. The closest available
description: diegetic, continuous, non-punishing feedback with "no score
and no bar" — closer in spirit to a rhythm-game "in the pocket" feel than a
combat or precision-platforming feel. This needs a real feel-reference
session with `game-designer` once the prototype exists — don't guess a
comparison that isn't grounded.

### Input Responsiveness

Not specified — needs measurement once a prototype build exists.

### Animation Feel Targets

Not applicable in the traditional combat-frame-data sense — this is a
continuous analog-read mechanic, not a discrete-action mechanic. If canvas
"resolving" involves discrete visual steps, those need their own timing
pass post-prototype.

### Impact Moments

Not specified — likely N/A for this mechanic's design (it's continuous, not
impact-based), but confirm with `game-designer` once prototyped.

### Weight and Responsiveness Profile

- **Weight**: Light and continuously reactive — an analog read, not a
  committed action
- **Player control**: High — the player can course-correct their read at
  any point mid-session (assuming the eventual formula rewards this; see
  Open Questions above)
- **Snap quality**: Smooth/analog, explicitly not binary — the "no score,
  no bar" design intent argues against snap feedback
- **Acceleration model**: Not specified
- **Failure texture**: Explicitly fair and low-stakes by design — "the
  cost is the evening," not a hard fail or punishment

### Feel Acceptance Criteria

- [ ] Playtesters can articulate, without prompting, whether their read
  felt "right" or "wrong" during a session (validates the diegetic
  feedback is legible without a score/bar)
- [ ] The mechanic is completable and readable in single-input/assist mode
  (accessibility requirement, see below)
- [ ] The mechanic is playable "by ear" — an explicit design test named in
  the source GDD
- [ ] No playtester describes a session as "eating the evening" at the
  target session length

## UI Requirements

| Information | Display Location | Update Frequency | Condition |
|--------------|---------------------|------------------------|-----------|
| Twelve-portrait ring (lit/greyed) | Sleep screen | Per day | At sleep |
| Live emotional-read feedback (colour/canvas) | Painting scene | Continuous during session | During painting scene |

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|-----------------------------|-----------|----------------------------------|--------|
| Villager identity, portrait stage | `design/gdd/villager-archetype-system.md` | Portrait stage state | Data dependency |
| Sleep trigger | `design/gdd/daily-loop-and-calendar.md` | Sleep state transition | State trigger |
| Live key resolution | `design/gdd/sound-and-crossfade.md` | Villager key underscore | Data dependency |
| Paint/flower resource consumption (unconfirmed) | `design/gdd/farm-and-economy.md` | Milled paint | Data dependency (tentative) |

## Acceptance Criteria

- [ ] Sleep screen correctly shows lit/greyed portraits based on daily
  story-movement state
- [ ] Painting scene twin-stick input correctly reads against ground-truth
  villager emotional state
- [ ] Correct/incorrect reads produce distinct, legible colour/sound/canvas
  feedback with no numeric score or bar
- [ ] A single-input accessibility mode exists and is fully playable
- [ ] Scene is completable "by ear" (audio-only) as an accessibility/design
  validation test
- [ ] Session completes in approximately the target length (~2 minutes)

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| **Is this mechanic fun in isolation?** | `game-designer`, `prototyper` | Immediately — via `/prototype` | This is the single highest-priority open question in the entire GDD |
| Quantified read-accuracy formula | `systems-designer` | During/after prototype | — |
| Portrait lit decay: indefinite vs. decay | `game-designer` | Before Alpha | Options drafted 2026-09-08 → `design/decisions/portrait-lit-decay.md`. No call made. |
| Does paint/flower resource gate sessions, and how? | `economy-designer`, `game-designer` | Before implementation | — |
| Mid-session read correction behavior (moving average vs. final read) | `systems-designer` | Before implementation | — |
