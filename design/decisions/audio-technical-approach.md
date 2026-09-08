# Decision: Audio technical approach

> **Owners**: audio-director, technical-director (Ken consulted)
> **Sprint 1 task**: S1-012 · **Status**: OPEN — no call made
> **GDD**: `design/gdd/sound-and-crossfade.md` (Open Questions)
> **Prepared**: 2026-09-08 — options only. This document does not choose.

## Why this blocks things

`sound-and-crossfade.md` has two hard requirements that are not trivial:

1. **Beat-matched crossfades** between zone tracks — "held to the bar", never
   a timer fade, or "the whole point is lost".
2. **Live key resolution** inside the painting scene — the villager's key
   strengthens/thins in real time with read accuracy.

`systems-index.md` lists this as a top **technical risk**. No ADR can be
written for the Sound epic until the approach is chosen, so
`production/epics/sound-and-crossfade` stays **Blocked**
(`control-manifest.md` "Open / Blocked Areas").

## Options

### Option A — Unity native audio (AudioSource + Timeline + scripted scheduling)

Use `AudioSettings.dspTime` + `AudioSource.PlayScheduled` for
sample-accurate beat-matched transitions; Timeline or custom code for the
painting-scene layer mixing.

- **Pros**: Zero new dependencies or licences; `unity-specialist` can own it
  directly; no external tooling in CI; smallest onboarding cost for a
  student team.
- **Cons**: Beat-matched crossfades and real-time adaptive layering are
  hand-rolled — this is real DSP-scheduling work, and the "held to the bar"
  requirement means writing a musical clock and phrase-aware transition
  logic yourself; no built-in vertical-remix / horizontal-resequencing tools.
- **Verification risk**: MEDIUM — `dspTime` scheduling is well-documented but
  the framework's engine-reference notes Unity 6 audio changes the model
  hasn't seen; spike it.
- **Cost**: free.

### Option B — FMOD Studio

Middleware built for exactly this: parameter-driven adaptive music,
transition timelines with beat/bar quantisation, real-time parameter
automation for the painting-scene key resolution.

- **Pros**: Beat-matched transitions and adaptive layers are authored by the
  composer in FMOD Studio, not coded; Ken gets direct control; battle-tested
  in shipped games; the "album in circle-of-fifths order" deliverable maps
  cleanly to FMOD's event structure.
- **Cons**: New dependency + integration package; free only under a revenue
  threshold (check current indie terms); a second tool for the team to
  learn; CI needs the FMOD plugin; adds build complexity.
- **Verification risk**: LOW for the audio behaviour, MEDIUM for the Unity 6.3
  integration package version.
- **Cost**: free below a revenue/budget threshold — confirm current terms.

### Option C — Wwise

Similar capability to FMOD; interactive-music system with segments, transition
rules, and RTPCs for real-time parameter control.

- **Pros**: The most powerful interactive-music toolset; strong transition
  rule system (exit/entry cues, bar/beat/cue sync).
- **Cons**: Steeper learning curve than FMOD; licensing tiers; heavier
  integration; likely overkill for a 12-track game.
- **Cost**: free tier exists with limits — confirm current terms.

### Option D — Native audio now, revisit at Alpha

Build the vertical slice's 3-track crossfade + painting-scene layer with
Unity native audio (Option A). Treat it as the spike. If it's fighting the
engine, adopt FMOD/Wwise for Alpha before 12-track production starts.

- **Pros**: Unblocks the Sound epic for the slice immediately; defers the
  dependency decision until there's real evidence; slice scope is only 3
  tracks.
- **Cons**: Possible rework if the slice implementation is thrown away;
  composer workflow question stays open.

## What to weigh (not a recommendation)

- Does Ken want to author adaptive behaviour in a tool, or hand stems to a
  programmer?
- Is "held to the bar" crossfading something the team wants to *own* as code,
  or buy?
- Revenue/licence thresholds vs. the project's actual commercial plan
  (`game-concept.md` says monetization is unspecified).
- CI impact — the `unity-tests.yml` workflow currently assumes a vanilla
  project.

## Decision

*Owners to complete:*

- **Chosen approach**:
- **Decided by / date**:
- **Rationale**:
- **Follow-ups**: run `/architecture-decision` (this becomes ADR-0003) ·
  update `sound-and-crossfade.md` Open Questions · update
  `control-manifest.md` (remove from "Blocked Areas", add audio rules) ·
  unblock `production/epics/sound-and-crossfade/EPIC.md` · update
  `sprint-1.md` S1-012 · adjust CI if middleware is chosen.
