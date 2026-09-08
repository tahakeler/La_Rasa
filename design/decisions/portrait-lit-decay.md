# Decision: Portrait lit-state — indefinite vs. decay

> **Owner**: game-designer (with creative-director sign-off)
> **Sprint 1 task**: S1-010 (Should Have) · **Status**: OPEN — no call made
> **GDDs**: `design/gdd/painting-and-reflection.md`, `design/gdd/villager-archetype-system.md` (Open Questions)
> **Prepared**: 2026-09-08 — options only.

## The question

When a villager's story moves during the day, their portrait lights up on
the sleep-screen ring. If the player doesn't reflect on them that night and
several days pass, does the portrait:

- **stay lit indefinitely** until the player gets to it, or
- **decay back to greyed** after N days, losing that reflection opportunity?

The source GDD says: *"indefinite is kinder, decay gives the evening more
weight — worth trying both."*

## Options

### Option A — Indefinite

Lit portraits stay lit until reflected on.

- **Pros**: Low-stress, matches the "cozy / submission" aesthetic and the
  "no hard fail" pillar; no lost content; simplest save-state (a bool per
  villager).
- **Cons**: Removes any pressure to choose; a player can bank every lit
  portrait and clear them in a binge; "which one tonight?" stops being a
  real choice.

### Option B — Decay after N days

A lit portrait greys out again N in-game days after it lit (suggested N: 3).
That story beat can re-trigger later through further interaction.

- **Pros**: Every evening is a genuine choice with a cost; reinforces the
  GDD's "players will make daily rounds specifically to light portraits"
  dynamic; makes the reflection scene feel scarcer and more valued.
- **Cons**: Can feel punishing / FOMO-inducing, which cuts against the
  cozy tone; needs a "days remaining" affordance on the ring or it's
  invisible and feels arbitrary; more save-state (a timestamp per villager).

### Option C — Decay, but the beat is never permanently lost

Lit portraits decay after N days (as B), but the underlying story progress is
retained — the next qualifying interaction re-lights it immediately, and no
portrait-stage content is ever locked out.

- **Pros**: The weight of B without the permanent-loss anxiety; "you missed
  tonight's window, not the story".
- **Cons**: The "cost" is softer — really just a nudge to keep visiting that
  villager; slightly more design/UX to communicate.

### Option D — Ship the slice with A, playtest B as a toggle

Vertical slice uses indefinite (A). Build decay (B/C) behind a debug toggle
and A/B it in playtests before Alpha locks it.

- **Pros**: Doesn't block the slice; gets real data on a genuinely
  even question; the GDD explicitly asks to try both.
- **Cons**: Two code paths to carry for a while.

## What to weigh (not a recommendation)

- Where does La Rasa sit on the cozy ↔ deliberate-weight axis, really?
- Does the sleep-screen ring have room for a decay affordance without
  clutter?
- This interacts with `sessions-per-portrait-stage` thresholds — decay plus
  slow thresholds could stall a questline.

## Decision

*Owner to complete:*

- **Chosen option** (+ N if decay):
- **Decided by / date**:
- **Rationale**:
- **Follow-ups**: update `painting-and-reflection.md` + `villager-archetype-system.md`
  Open Questions · note the save-state shape for the Save/Persistence system ·
  update `sprint-1.md` S1-010.
