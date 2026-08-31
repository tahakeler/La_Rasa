# Epic: Sound & Adaptive Crossfade

> **Layer**: Core
> **GDD**: design/gdd/sound-and-crossfade.md
> **Architecture Module**: Not yet defined — no dedicated ADR exists
> **Status**: BLOCKED — pending technical approach decision
> **Stories**: Not yet created — run `/create-stories sound-and-crossfade`

## Overview

Implements twelve zone tracks (one per archetype key), beat-matched
crossfades between adjacent zones, and live key resolution/thinning inside
the Reflection & Painting Scene based on read accuracy.

## Governing ADRs

| ADR | Decision Summary | Engine Risk |
|-----|-------------------|-------------|
| None yet | Technical approach (Unity native audio vs. middleware such as FMOD/Wwise) is an explicit open question — flagged as a real technical risk in `design/gdd/systems-index.md` | N/A |

## GDD Requirements

| TR-ID | Requirement | ADR Coverage |
|-------|-------------|----------------|
| TR-sound-001 | All twelve zone tracks implemented in assigned key | ❌ No ADR |
| TR-sound-002 | Zone-boundary crossfades are beat-matched, never mid-phrase | ❌ No ADR |
| TR-sound-003 | Painting-scene key resolution responds live to read accuracy | ❌ No ADR |

## Definition of Done

This epic is **explicitly blocked** until `audio-director` + `technical-director`
decide the technical approach (native Unity audio vs. middleware) and that
decision is written up as an ADR. Beat-matched, non-timer-based crossfades
are real audio-engineering work — starting implementation before this
decision risks a costly rebuild.

Only after that:
- All stories are implemented, reviewed, and closed via `/story-done`
- All acceptance criteria from `design/gdd/sound-and-crossfade.md` are verified
- Major/minor key assignment per zone (open question, owned by Ken) is
  resolved before full twelve-track production

## Next Step

Resolve the technical-approach decision first. Then run
`/create-stories sound-and-crossfade`.
