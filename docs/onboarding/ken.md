# Onboarding — Ken

**Role**: OST composer. Twelve tracks, one per zone, in circle-of-fifths keys,
with beat-matched crossfades between neighbours. Also intended as a standalone
album.

## Your agents

| For | Agent |
|-----|-------|
| Music direction, crossfade implementation strategy, the native-vs-middleware call | `audio-director` |
| Track-level specs, mixing parameters, audio event lists | `sound-designer` |
| A full audio pipeline pass (direction → spec → implementation) | `/team-audio` |

## On your plate now

- **Audio technical approach (`S1-012`)** — with `audio-director` and
  `technical-director`. `design/decisions/audio-technical-approach.md` lays
  out Unity-native vs FMOD vs Wwise. This blocks the Sound epic's ADR. Record
  the call in `## Decision`.
- **Major/minor per zone (`S1-011`)** — your call.
  `design/decisions/zone-key-major-minor.md` has the options. Only the slice's
  3 tracks need locking now; the rest can be a guideline.
- The twelve **keys** are fixed (circle of fifths, in wheel order: C G D A E B
  F# Db Ab Eb Bb F). The code already encodes this in
  `Assets/LaRasa/Scripts/Core/TwelveContent/CircleOfFifths.cs`. Neighbours are
  a fifth apart — that's what makes the crossfades resolve.

## First steps

1. `docs/TEAM-HANDBOOK.md` §2 — Git + Node + Claude Code. **No Unity needed** —
   you compose in your DAW and hand over audio files.
2. Read `design/gdd/sound-and-crossfade.md` and
   `design/gdd/twelve-content-framework.md` (the key table).
3. In Claude Code: *"walk me through the audio technical-approach options —
   I need beat-matched crossfades and live key resolution in the painting
   scene. What does each option cost me as the composer?"*

## Watch out for

- Crossfades must be **held to the bar**, never a fixed-timer fade — "the
  whole point is lost" otherwise.
- The painting scene reuses your keys: the selected villager's key resolves or
  thins live with the player's read accuracy.
- Hand final audio exports to a programmer to import to `Assets/LaRasa/Audio`.
  Keep stems and DAW projects on a shared drive, not in the repo.
