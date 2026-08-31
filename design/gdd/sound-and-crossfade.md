# Sound & Adaptive Crossfade

> **Status**: Draft — ingested from GDD v1.3
> **Author**: audio-director (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Twelve Is the Structure, Not the Content

## Summary

Twelve tracks, one per town zone, each written in that zone's key, arranged
in circle-of-fifths order so neighboring zones are a fifth apart and
crossing a zone boundary musically resolves. The same key system underlies
the Reflection & Painting Scene, where a villager's key resolves or thins
based on the player's read accuracy.

> **Quick reference** — Layer: `Core` · Priority: `MVP` · Key deps: `Twelve Content Framework`

## Overview

The zone layout and the music are the same structure viewed two ways: the
twelve archetype positions are ordered by circle of fifths, so physically
adjacent zones are musically adjacent (a fifth apart), and moving between
them should feel like a musical resolution, not a hard cut. The same key
system reappears inside the painting scene as the "correct read" feedback
channel.

## Player Fantasy

A world that sounds coherent as you move through it, and where correctly
understanding a person is something you can hear as well as see.

## Detailed Design

### Core Rules

1. Twelve tracks, one per zone/archetype position, in that position's
   fixed key (see `twelve-content-framework.md` for the full key table).
2. Zone order follows circle-of-fifths order, so neighboring zones are
   always a fifth apart.
3. Crossing a zone boundary triggers a beat-matched crossfade between the
   two zones' tracks — explicitly NOT a timer-based fade, since a
   timer-based fade risks landing mid-phrase and defeating the point of the
   key relationship.
4. Inside the painting scene, the selected villager's key plays underneath
   the scene and resolves (strengthens) or thins (weakens) live, in
   real-time response to the player's read accuracy.
5. The full twelve-track set is also intended for release as a standalone
   album, sequenced in circle-of-fifths order.
6. Open question, explicitly named in source GDD: major or minor key per
   zone. All-major risks reading as "uniformly cheerful"; some minor keys
   would add tonal texture to the valley.

### States and Transitions

| State | Entry Condition | Exit Condition | Behavior |
|-------|-------------------|-------------------|----------|
| Zone track playing | Player is in a zone | Player crosses a zone boundary | Standard looping playback |
| Crossfade | Player crosses a zone boundary | Crossfade completes (beat-matched, not timer-based) | Both tracks blend, held to the bar |
| Painting-scene underscore | Player enters a painting scene | Scene ends | Villager's key plays, resolving/thinning live with read accuracy |

### Interactions with Other Systems

| System | Data Flow |
|--------|-----------|
| Twelve Content Framework | Provides key + wheel position per zone |
| Reflection & Painting Scene | Consumes live read-accuracy signal to drive key resolution/thinning |
| Town/Zone Layout *(inferred)* | Zone boundaries trigger crossfades |

## Formulas

Not applicable in the traditional numeric sense — this is an audio
implementation system. The one implementation constraint stated explicitly:
crossfades must be **held to the bar**, not faded on a fixed timer.

## Edge Cases

| Scenario | Expected Behavior | Rationale |
|----------|---------------------|-----------|
| Player rapidly crosses a zone boundary back and forth | Not specified — needs a debounce/hysteresis rule to avoid crossfade thrashing | Standard adaptive-audio implementation concern, not addressed in source GDD |
| Player is in a painting scene when normally a zone crossfade would trigger (edge case, likely N/A since painting scene is a distinct screen) | Not specified | Confirm painting scene fully replaces zone audio context |

## Dependencies

| System | Direction | Nature of Dependency |
|--------|-----------|-------------------------|
| Twelve Content Framework | This depends on it | Key + wheel position per zone |
| Reflection & Painting Scene | Depends on this | Live key resolution/thinning as scene feedback |

## Tuning Knobs

| Parameter | Current Value | Safe Range | Effect of Increase | Effect of Decrease |
|-----------|----------------|------------|----------------------|----------------------|
| Major/minor per zone | Undecided | — | All-major: more uniformly cheerful | Mixed major/minor: more tonal variety, per source GDD's own note |
| Crossfade length (in bars) | Not specified | — | Smoother but slower transitions | Snappier but riskier if not phrase-aligned |

## Visual/Audio Requirements

| Event | Visual Feedback | Audio Feedback | Priority |
|-------|-------------------|-------------------|----------|
| Zone boundary crossing | Not specified | Beat-matched crossfade between zone tracks | MVP |
| Painting scene read | Colour (see `painting-and-reflection.md`) | Villager key resolves/thins live | MVP |

## Game Feel

Not applicable in the frame-data sense — this is a music-implementation
system. The relevant "feel" target is stated directly: transitions must
never land mid-phrase, or "the whole point is lost."

## UI Requirements

Not specified — no direct music UI described in source GDD (e.g., no
in-game jukebox mentioned, though one could be considered later).

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|-----------------------------|-----------|----------------------------------|--------|
| Key + wheel position per zone | `design/gdd/twelve-content-framework.md` | Key column | Data dependency |
| Live key resolution during painting scene | `design/gdd/painting-and-reflection.md` | Read-accuracy signal | Data dependency |

## Acceptance Criteria

- [ ] All twelve zone tracks implemented in their assigned key
- [ ] Zone-boundary crossfades are beat-matched, never landing mid-phrase
- [ ] Painting-scene key resolution responds live to read accuracy
- [ ] Major/minor assignment decided and consistently applied

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| Major vs. minor per zone | Ken | Before full-track production begins | Source GDD suggests some minor keys for texture |
| Crossfade debounce/hysteresis behavior on rapid boundary crossing | `audio-director`, `unity-specialist` | Before implementation | — |
| Technical approach: Unity's native audio system vs. middleware (e.g., FMOD/Wwise) for beat-matched crossfades | `audio-director`, `technical-director` | Before implementation — flagged as a real technical risk in `systems-index.md` | — |
