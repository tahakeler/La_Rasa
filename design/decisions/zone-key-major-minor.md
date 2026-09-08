# Decision: Major / minor key per zone

> **Owner**: Ken
> **Sprint 1 task**: S1-011 (Should Have) · **Status**: OPEN — no call made
> **GDD**: `design/gdd/sound-and-crossfade.md` (Open Questions)
> **Prepared**: 2026-09-08 — options only.

## The question

The twelve zone keys are fixed by the circle of fifths (C, G, D, A, E, B, F#,
Db, Ab, Eb, Bb, F). Their **mode** is not. The source GDD notes: *"all-major
risks reading as uniformly cheerful; some minor keys would add tonal texture
to the valley."*

This only affects composition, not the crossfade tech — a beat-matched
crossfade between relative/parallel or fifth-related keys works regardless of
mode. But it should be decided before full 12-track production so the set
hangs together (and so the "album in circle-of-fifths order" sequences well).

## Options

### Option A — All major

Every zone track is in the major mode of its wheel key.

- **Pros**: Simplest; guaranteed smooth fifth-relationships between
  neighbours; unambiguously "cozy".
- **Cons**: The exact risk the GDD names — twelve major tracks in a row can
  read as flat / saccharine; less characterisation per zone.

### Option B — Mode follows the archetype's shadow weight

Zones whose archetype has a heavier shadow (Orphan/Victimhood,
Lover/Obsession, Rebel/Nihilism, Ruler/Tyranny) get minor; lighter ones
(Innocent, Jester, Explorer, Creator) stay major; the rest are the composer's
call.

- **Pros**: Mode carries meaning — the valley sounds like its people;
  supports the persona/shadow theme; gives Ken a rationale, not a coin flip.
- **Cons**: Minor keys a fifth apart still resolve, but major↔minor
  neighbour transitions need more care to stay smooth; ties music to
  archetype placeholders that Non/Jack may still rename.

### Option C — Fixed pattern around the wheel

A repeating mode pattern by wheel position, e.g. major on positions
1/3/5/7/9/11, minor on 2/4/6/8/10/12 (or a 3-major / 1-minor cycle).

- **Pros**: Predictable voice-leading between neighbours; the album has an
  audible structure; decoupled from archetype naming churn.
- **Cons**: Arbitrary — mode doesn't mean anything, it's just alternation;
  may fight the archetype characterisation Ken wants.

### Option D — Composer's discretion per track, within a stated ratio

Ken picks per track, with a guideline like "roughly 8 major / 4 minor, no
three consecutive minors" to keep the whole coherent.

- **Pros**: Maximum musical judgement; the GDD trusts Ken here; the ratio
  keeps it from drifting either way.
- **Cons**: Least predictable for anyone downstream planning against it; the
  slice's 3 tracks might not represent the final balance.

## What to weigh (not a recommendation)

- The vertical slice only needs 3 tracks — the full major/minor balance can
  be a guideline now and locked when the other 9 are scoped.
- Whether mode should *mean* something (Option B) or just *vary* (C/D).
- Neighbour transitions: minor→minor and major→major a fifth apart are the
  safest; plan mixed transitions deliberately.

## Decision

*Owner to complete:*

- **Chosen approach** (+ the slice's 3 tracks' modes):
- **Decided by / date**:
- **Rationale**:
- **Follow-ups**: update `sound-and-crossfade.md` Open Questions · record the
  per-position mode map alongside the key table in
  `twelve-content-framework.md` when the full set is locked · update
  `sprint-1.md` S1-011.
