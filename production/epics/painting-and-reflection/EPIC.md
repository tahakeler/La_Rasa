# Epic: Reflection & Painting Scene

> **Layer**: Feature
> **GDD**: design/gdd/painting-and-reflection.md
> **Architecture Module**: Not yet defined — no dedicated ADR exists
> **Status**: BLOCKED — pending prototype verdict
> **Stories**: Not yet created — run `/create-stories painting-and-reflection`

## Overview

Implements the sleep screen (twelve-portrait ring, lit/greyed state) and
the twin-stick painting scene that reads a villager's emotional state and
advances their portrait. This is the single highest-risk, highest-priority
system in the game per the source GDD — "if that's fun the rest is
content."

## Governing ADRs

| ADR | Decision Summary | Engine Risk |
|-----|-------------------|-------------|
| None yet | Core mechanic architecture (input handling, feedback systems) not yet decided — see `prototypes/painting-scene/` for the engine-agnostic read-accuracy logic already drafted | N/A |

## GDD Requirements

| TR-ID | Requirement | ADR Coverage |
|-------|-------------|----------------|
| TR-painting-001 | Sleep screen shows lit/greyed portraits correctly | ❌ No ADR |
| TR-painting-002 | Twin-stick input reads correctly against ground-truth state | ❌ No ADR (prototype logic exists in `prototypes/painting-scene/Scripts/ReadAccuracy.cs`, not yet promoted to an ADR) |
| TR-painting-003 | Correct/incorrect reads produce distinct diegetic feedback, no score/bar | ❌ No ADR |
| TR-painting-004 | Single-input accessibility mode exists and is fully playable | ❌ No ADR |
| TR-painting-005 | Scene is completable using audio feedback alone | ❌ No ADR |

## Definition of Done

This epic is **explicitly blocked** until:
1. A real Unity project exists and the `prototypes/painting-scene/` logic
   is built into an actual playable scene
2. At least one playtest session produces a PROCEED/PIVOT/KILL verdict per
   `prototypes/painting-scene/BRIEF.md`'s success criteria
3. On PROCEED: promote the prototype's approach into a formal ADR before
   story creation begins in earnest

Only after that:
- All stories are implemented, reviewed, and closed via `/story-done`
- All acceptance criteria from `design/gdd/painting-and-reflection.md` are verified
- Accessibility requirements (single-input, audio-only) are treated as MVP
  acceptance criteria, not a later pass — per the control manifest

## Next Step

**Do not run `/create-stories` yet.** Build and playtest the prototype
first (`prototypes/painting-scene/BRIEF.md`). This epic exists now so the
GDD requirements are tracked, not because implementation should start.
