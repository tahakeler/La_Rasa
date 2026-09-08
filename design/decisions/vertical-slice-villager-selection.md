# Decision: Vertical-slice villager / zone selection

> **Owners**: Non, Jack (archetype division)
> **Sprint 1 task**: S1-002 · **Status**: OPEN — no call made
> **GDDs**: `design/gdd/game-concept.md`, `design/gdd/villager-archetype-system.md` (Open Questions)
> **Prepared**: 2026-09-08 — options only. This document does not choose.

## Why this blocks things

The vertical slice is 3 villagers / 3 zones / 1 season / 1 region. Which
3 wheel positions those are determines:

- **Zone contiguity** — the adaptive-music crossfade design only works if the
  3 slice zones are *adjacent on the wheel* (neighbours are a fifth apart).
  `sound-and-crossfade.md` can't be validated in the slice otherwise.
- **Archetype division** — Non/Jack's work of assigning writers/artists to
  the twelve archetypes should start from the slice 3, and gets *redone* if
  the slice changes after they start (ADR-0001 "Neutral" consequence).
- **Which villager gets the full persona/shadow/painting arc** for MVP
  (`game-concept.md` MVP requires "at least one full villager").

Flagged as **the very first Week-1 decision** in `systems-index.md` (it
blocks archetype division itself).

## The tension in the source GDD

The proposed season split puts spring at wheel positions **1, 5, 9** (every
fourth position). That is **not contiguous**, so it can't be the slice
selection if crossfades are to be tested. Something has to give: either the
slice uses 3 contiguous positions from *different* seasons, or the
season→position mapping changes.

## The twelve positions (from `twelve-content-framework.md`)

| Pos | Key | Archetype (placeholder) | Season (placeholder) | Region quarter |
|-----|-----|--------------------------|----------------------|----------------|
| 1 | C | Innocent | Spring | Greenhollow |
| 2 | G | Jester | Summer | Greenhollow |
| 3 | D | Caregiver | Autumn | Greenhollow |
| 4 | A | Explorer | Winter | Tidewrack |
| 5 | E | Sage | Spring | Tidewrack |
| 6 | B | Orphan | Summer | Tidewrack |
| 7 | F# | Magician | Autumn | The Deep Kiln |
| 8 | Db | Lover | Winter | The Deep Kiln |
| 9 | Ab | Hero | Spring | The Deep Kiln |
| 10 | Eb | Rebel | Summer | The Burnt Reach |
| 11 | Bb | Creator | Autumn | The Burnt Reach |
| 12 | F | Ruler | Winter | The Burnt Reach |

## Options

### Option A — Positions 1-2-3 (Innocent / Jester / Caregiver), Greenhollow

- **Pros**: Fully contiguous (C→G→D, fifths); all in one region quarter, so
  "1 region" is unambiguous; Greenhollow is the designated *early* region, so
  tool-tier gating is simplest; keys C/G/D are the most approachable to
  compose first.
- **Cons**: Spans Spring/Summer/Autumn — the "1 season" slice line means only
  one of them is in-season during the slice unless season assignments move;
  Innocent/Jester/Caregiver may not be the most *interesting* persona/shadow
  trio to show off the mechanic.
- **Unblocks**: archetype division for 3 positions; one region; a coherent
  3-track crossfade demo.

### Option B — Positions 4-5-6 (Explorer / Sage / Orphan), Tidewrack

- **Pros**: Contiguous (A→E→B); Tidewrack is coast/marsh — visually distinct,
  good for a slice screenshot; Sage/Orphan shadows (Dogma / Victimhood) are
  strong painting-scene material.
- **Cons**: Not the early region, so tool-gating has to be hand-waved for the
  slice; keys A/E/B are sharp-side, slightly harder first compositions.

### Option C — 3 contiguous positions + re-map seasons so all 3 share a season

Pick any 3 adjacent positions (e.g. 1-2-3) **and** move their season
assignments so all three are the slice season. Seasons are explicitly
editable placeholders.

- **Pros**: Satisfies *both* "3 contiguous zones" and "1 season" literally;
  keeps the crossfade demo honest.
- **Cons**: Breaks the tidy "3-per-season, one per wheel-quarter" pattern the
  GDD's inventory implies; Non/Jack have to re-derive the full 12-position
  season map, not just pick 3.
- **Unblocks**: everything, but with more up-front archetype-division work.

### Option D — Drop crossfade testing from the slice; pick the 3 best *characters*

Accept non-contiguous zones for the slice. Test crossfades separately with a
throwaway 2-zone setup. Choose the 3 villagers whose persona/shadow writing
best sells the painting mechanic, wherever they sit on the wheel.

- **Pros**: The slice leads with its strongest content; character quality
  over structural neatness.
- **Cons**: The slice no longer proves the zone/music integration, which
  `systems-index.md` calls a top technical risk; "1 region" may become "3
  regions, one zone each".

## What to weigh (not a recommendation)

- Is the vertical slice's job to prove the *music/zone tech* or to prove the
  *painting mechanic is fun*? (The GDD says the latter is the whole point.)
- How locked are the season placeholders, really?
- Which trio of shadows makes the best 2-minute painting-scene demo?

## Decision

*Owner to complete:*

- **Chosen positions**:
- **Slice season**:
- **Decided by / date**:
- **Rationale**:
- **Follow-ups**: update `game-concept.md` + `villager-archetype-system.md`
  Open Questions · update `twelve-content-framework.md` if seasons move ·
  start archetype division from these 3 · update `sprint-1.md` S1-002.
