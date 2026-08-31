# Prototype: Painting Scene

*Created 2026-08-31, per the framework's Concept Prototype pattern
(`agent-coordination-map.md` Pattern 8) and `.claude/rules/prototype-code.md`.*

## Hypothesis Being Tested

**Reading a villager's emotional state via continuous twin-stick input
(one stick = expressed/withdrawn × grief/joy, one stick = surface/depth) is
legible and satisfying on its own, using only diegetic feedback (colour,
sound, canvas resolution) — with no score, no bar, and no fail state.**

Per `design/gdd/painting-and-reflection.md`, the source GDD states this
outright: "If that's fun the rest is content. If it isn't, more content
won't fix it." Every other system in the game (twelve villagers, twelve
portraits, twelve tracks) is downstream investment that only pays off if
this specific mechanic works — it is the single most novel, unproven system
in the whole design.

## How to Run

**Not runnable yet.** No Unity project exists in this repo (no `Assets/`,
`ProjectSettings/`, no `.csproj`) — there is nothing to open or play. What
exists is the engine-agnostic core logic only:

- `Scripts/ReadAccuracy.cs` — plain C#, no UnityEngine dependency, can be
  read/reasoned about directly or dropped into any C# test runner
- `Scripts.Tests/ReadAccuracyTests.cs` — NUnit tests for that logic,
  written ready to drop into a Unity Test project once one exists (not
  currently runnable standalone in this repo either, since no test runner
  is configured yet)

**To make this runnable**, someone needs to:
1. Create a minimal Unity 6.3 URP 2D project (`unity install lts` or the
   standard Unity Hub New Project flow)
2. Add the New Input System package
3. Wire two stick axes to `ReadAccuracy.EvaluateReadAccuracy()` /
   `EvaluateDepthProgress()`
4. Drive a simple colour lerp + audio pitch cue off the result as
   placeholder diegetic feedback

That's a small enough scope for a single focused session — the logic
itself is already written and tested.

## Current Status

**In-progress.** Core logic implemented and unit-tested; no playable build
exists yet; no playtest has happened.

## Scope

**Included:**
- One ground-truth emotional state (a fixed point on the Expressed/
  Withdrawn × Grief/Joy plane, plus a target depth)
- Two-stick input reading player position against that ground truth
- The read-accuracy calculation (continuous, not binary)
- Placeholder diegetic feedback: colour lerp toward a target colour, simple
  pitch/volume cue — standing in for the full colour/sound/canvas treatment

**Explicitly excluded:**
- Real portrait artwork (Lala's actual paintings)
- The sleep screen / portrait ring
- Any specific villager's actual emotional writing
- Music key resolution (Sound & Adaptive Crossfade integration) — stub only
- Accessibility modes (single-input, audio-only) — real MVP requirements
  per the GDD, but come after the core read mechanic is validated as fun,
  not before

## Success Criteria

A player who has never seen the design doc should be able to, after a
short explanation of the controls only (not the "right answer" system):
1. Tell, without being told, whether a given session felt like it was
   "going well" or "going badly" — validates the diegetic feedback is
   legible without a score
2. Complete a session in roughly the GDD's target window (~2 minutes)
   without it feeling either instant or draining
3. Describe wanting to try again with a different "read," rather than
   feeling like the outcome was arbitrary or unreadable

If none of these hold up, per the GDD's own framing, the design needs to
change before any further investment (portraits, full villager writing,
twelve-track music) is justified.

## Findings

*Not yet available — update this section when the prototype concludes,
per `.claude/rules/prototype-code.md`.*

No PROCEED / PIVOT / KILL verdict can be produced without an actual
playable build and at least one playtest session. This document and the
accompanying code are the input to that session, not a substitute for it.
