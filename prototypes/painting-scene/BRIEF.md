# Prototype Brief: Painting Scene

*Created 2026-08-31, per the framework's Concept Prototype pattern
(`agent-coordination-map.md` Pattern 8).*

## Why this prototype, and why first

Per `design/gdd/painting-and-reflection.md`, the source GDD states this
outright: **"If that's fun the rest is content. If it isn't, more content
won't fix it."** Every other system in the game (twelve villagers, twelve
portraits, twelve tracks) is downstream investment that only pays off if
this specific mechanic works. It is also the single most novel, unproven
system in the whole design — nothing else in the GDD is genuinely new
mechanically.

## Hypothesis

**Reading a villager's emotional state via continuous twin-stick input
(one stick = expressed/withdrawn × grief/joy, one stick = surface/depth) is
legible and satisfying on its own, using only diegetic feedback (colour,
sound, canvas resolution) — with no score, no bar, and no fail state.**

## Success Criteria

A player who has never seen the design doc should be able to, after a short
explanation of the controls only (not the "right answer" system):
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

## Scope — What This Prototype Includes

- One ground-truth emotional state (a fixed point on the Expressed/
  Withdrawn × Grief/Joy plane, plus a target depth)
- Two-stick input reading player position against that ground truth
- A read-accuracy calculation (see `Scripts/ReadAccuracy.cs`) — continuous,
  not binary
- Placeholder diegetic feedback: colour lerp toward a target colour, and a
  simple pitch/volume cue, standing in for the full colour/sound/canvas
  treatment
- No portrait art, no real villager content, no save data, no sleep-screen
  flow — this tests the moment-to-moment feel of the read itself, nothing
  around it

## Scope — Explicitly NOT in This Prototype

- Real portrait artwork (Lala's actual paintings)
- The sleep screen / portrait ring
- Any specific villager's actual emotional writing
- Music key resolution (Sound & Adaptive Crossfade integration) — stub only
- Accessibility modes (single-input, audio-only) — these are real MVP
  requirements per the GDD, but come after the core read mechanic is
  validated as fun, not before

## What's Actually Done Here vs. What Needs a Human

**Done in this pass:**
- `Scripts/ReadAccuracy.cs` — a plain, engine-agnostic C# class implementing
  the read-accuracy calculation described in the GDD's qualitative rules
  ("opens when your read is right, resists when your read is off"), with
  unit tests in `Scripts.Tests/ReadAccuracyTests.cs`

**NOT done, and NOT possible without a human at a Unity Editor:**
- No Unity project exists in this repo yet (no `Assets/`, `ProjectSettings/`,
  no `.csproj`) — there is nothing to open, run, or attach these scripts to
- No scene, no input bindings, no actual playable build
- No playtest — the success criteria above cannot be evaluated until
  someone builds a scene around this logic and actually plays it

**Recommended next step for whoever picks this up**: create a minimal
Unity 6.3 URP 2D project (`unity install lts` / standard New Project flow),
add the New Input System package, wire two stick axes to
`ReadAccuracy.Evaluate()`, and drive a simple colour lerp + audio pitch off
the result. That's a small enough scope to build and feel out in a single
focused session — the logic itself is already here and tested.

## Verdict

**Not yet evaluated.** Cannot produce a PROCEED / PIVOT / KILL verdict
without an actual playable build and at least one playtest session, per
this framework's own prototyping pattern. This brief and the accompanying
code are the input to that session, not a substitute for it.
