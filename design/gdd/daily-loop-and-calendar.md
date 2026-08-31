# Daily Loop & Calendar

> **Status**: Draft — ingested from GDD v1.3
> **Author**: game-designer (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Twelve Is the Structure, Not the Content

## Summary

The core session structure: a 12-hour day (wake → farm → town/region →
process → ship → sleep), a 12-day month, a 12-month year (144 days total).
Standard Stardew-tradition daily loop with the "twelve" constraint applied
to time itself.

> **Quick reference** — Layer: `Core` · Priority: `MVP` · Key deps: `Twelve Content Framework`

## Overview

Most of this is deliberately familiar to anyone who's played Stardew Valley:
wake, tend crops/animals, go into town or a region, process/craft, ship
goods before bed, sleep. The differences are the compressed 12-hour clock
(vs. the genre-standard ~16–24 waking hours) and the addition of an optional
reflection/painting beat at the very end of the day (see
`painting-and-reflection.md`).

## Player Fantasy

A day that feels complete but tight — "a bit less room and a bit more
choosing," per the source GDD. The player should feel like every day has
real opportunity cost, not enough time to do everything.

## Detailed Rules

### Core Rules

1. The in-game day runs from hour 6 to hour 18 (12 hours), per the source
   GDD's stated beat structure: Wake (06) → Water/plant (07) → Animals (08)
   → Into town (10) → Out to a region (13) → Process/craft (16) → Ship (17)
   → Sleep (18).
2. Calendar: 12 hours/day, 12 days/month, 12 months/year = 144 days/year,
   36 days/season, 4 seasons/year.
3. Crops grow overnight; shipping resolves overnight; energy caps how much
   the player can accomplish in a day — all standard genre mechanics, no
   deviation specified.
4. Winter is NOT a dead season: hardier crops and slower growth keep it
   planted year-round, explicitly to avoid the "winter is empty" problem
   common in the genre. A greenhouse mid-game unlock is under consideration
   but not committed.
5. Sleep triggers the reflection/painting system if any villager's story
   moved that day (see `painting-and-reflection.md`) — otherwise the day
   simply ends.

### States and Transitions

| State | Entry Condition | Exit Condition | Behavior |
|-------|-------------------|-------------------|----------|
| Waking day | Player wakes (hour 6) | Player sleeps or passes out from exhaustion | Normal play; energy depletes with actions |
| Overnight resolution | Player sleeps | Next day's wake | Crop growth, shipping payout, calendar advance resolve |
| Reflection (conditional) | Player sleeps AND ≥1 portrait is lit | Player picks a portrait or the game auto-advances | Enters painting scene (see separate GDD) |

### Interactions with Other Systems

| System | Data Flow |
|--------|-----------|
| Farm & Economy | Crop growth ticks, shipping resolves, energy is spent on farm actions |
| Reflection & Painting Scene | Sleep is the trigger point for whether a reflection scene starts |
| Regions & Combat | Region access happens during the "out to a region" beat of the day |

## Formulas

### Calendar Math

```
days_per_year = hours_per_day_unit * days_per_month * months_per_year
              = 12 * 12 = 144 (days_per_month and months_per_year both fixed at 12)
days_per_season = days_per_year / 4 = 36
```

| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| days_per_month | int | 12 (fixed) | data file | Twelve Content Framework constraint |
| months_per_year | int | 12 (fixed) | data file | Twelve Content Framework constraint |
| days_per_year | int | 144 (derived) | calculated | days_per_month × months_per_year |

**Expected output range**: N/A — fixed constants, not a runtime calculation.
**Edge case**: None — this is calendar structure, not player-affected math.

## Edge Cases

| Scenario | Expected Behavior | Rationale |
|----------|---------------------|-----------|
| Player runs out of energy mid-day | Not specified in source GDD | Standard genre behavior (forced return home / energy penalty) should be confirmed with `game-designer` before implementation |
| Player sleeps with multiple portraits lit | Player picks one; the rest stay lit for a future night (per `painting-and-reflection.md`) | Explicit source GDD rule |
| Player never goes to sleep (theoretical) | Not specified | Flag for `gameplay-programmer` — likely a forced-sleep timeout exists in the source inspiration (Stardew) and should be matched unless deliberately changed |

## Dependencies

| System | Direction | Nature of Dependency |
|--------|-----------|-------------------------|
| Twelve Content Framework | This depends on it | Day/month/year counts are all "12" by the same structural constraint |
| Farm & Economy | Depends on this | Overnight crop growth and shipping resolve on this day/sleep cycle |
| Reflection & Painting Scene | Depends on this | Sleep is the entry trigger |

## Tuning Knobs

| Parameter | Current Value | Safe Range | Effect of Increase | Effect of Decrease |
|-----------|----------------|------------|----------------------|----------------------|
| Hours per day | 12 (fixed by design pillar) | Not intended to change | More player actions per day, less "tight" feeling | Fewer actions, tighter choices |
| Days per month / months per year | 12 / 12 (fixed) | Not intended to change | Longer game, more content needed | Shorter game |

## Visual/Audio Requirements

| Event | Visual Feedback | Audio Feedback | Priority |
|-------|-------------------|-------------------|----------|
| Time-of-day change | Lighting/color shift (not detailed in source GDD) | Not specified | Not yet specified — flag for `art-director`/`technical-artist` |
| Sleep transition | Fade to sleep screen (see `painting-and-reflection.md`) | Not specified | MVP |

## Game Feel

Not detailed in source GDD beyond pacing intent ("a bit less room and a bit
more choosing"). Needs a dedicated pass once the farm action set is
implemented — flag for `game-designer` + `ux-designer`.

## UI Requirements

| Information | Display Location | Update Frequency | Condition |
|--------------|---------------------|------------------------|-----------|
| Current time | HUD (not detailed further) | Continuous | Always |
| Current date/season | HUD (not detailed further) | Per day | Always |
| Energy | HUD (not detailed further) | Continuous | Always |

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|-----------------------------|-----------|----------------------------------|--------|
| Day/month/year = 12 | `design/gdd/twelve-content-framework.md` | Twelve constraint | Rule dependency |
| Sleep triggers reflection | `design/gdd/painting-and-reflection.md` | Reflection entry condition | State trigger |
| Overnight crop growth/shipping | `design/gdd/farm-and-economy.md` | Growth/shipping resolution | Data dependency |

## Acceptance Criteria

- [ ] A full in-game day runs 06:00–18:00 with the beat structure listed above
- [ ] Calendar correctly computes 144 days/year, 36 days/season
- [ ] Winter maintains plantable crops (does not go "dead")
- [ ] Sleep correctly checks for lit portraits and branches to reflection or ends the day

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| Energy depletion / forced-return behavior | `game-designer` | Before Farm & Economy implementation | — |
| Greenhouse mid-game unlock — committed or cut? | `game-designer` | Before Alpha content scoping | — |
