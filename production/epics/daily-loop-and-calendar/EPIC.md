# Epic: Daily Loop & Calendar

> **Layer**: Core
> **GDD**: design/gdd/daily-loop-and-calendar.md
> **Architecture Module**: Not yet defined — no dedicated ADR exists
> **Status**: Ready (with untraced requirements)
> **Stories**: Not yet created — run `/create-stories daily-loop-and-calendar`

## Overview

Implements the 12-hour day / 12-day month / 12-month year calendar and the
wake→farm→town-or-region→process→ship→sleep daily beat structure. Sleep is
the trigger point for the Reflection & Painting Scene epic.

## Governing ADRs

| ADR | Decision Summary | Engine Risk |
|-----|-------------------|-------------|
| None yet | This epic has no dedicated architecture decision — the calendar math is simple enough it may not need one, but confirm with `technical-director` before implementation | N/A |

## GDD Requirements

| TR-ID | Requirement | ADR Coverage |
|-------|-------------|----------------|
| TR-daily-001 | In-game day runs 06:00-18:00 with the stated beat structure | ❌ No ADR |
| TR-daily-002 | Calendar computes 144 days/year, 36 days/season | ❌ No ADR |
| TR-daily-003 | Winter maintains plantable crops | ❌ No ADR |
| TR-daily-004 | Sleep checks for lit portraits and branches correctly | ❌ No ADR |

⚠️ All four requirements in this epic are untraced. Stories can be created,
but should be marked Blocked (or proceed with explicit placeholder
assumptions) until `technical-director` confirms an ADR isn't needed, or
one is written.

## Definition of Done

This epic is complete when:
- All stories are implemented, reviewed, and closed via `/story-done`
- All acceptance criteria from `design/gdd/daily-loop-and-calendar.md` are verified
- All Logic and Integration stories have passing test files in `tests/`
- Two Open Questions from the source GDD are resolved before Done: energy
  depletion/forced-return behavior, and greenhouse mid-game unlock scope

## Next Step

Run `/create-stories daily-loop-and-calendar` to break this epic into
implementable stories.
