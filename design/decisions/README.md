# design/decisions/

Option docs for decisions that are **owned by named people**, not by the
agent framework. Each doc lays out the choices, trade-offs, and what each one
unblocks. **None of them makes the call** — the `## Decision` section is left
blank for the owner to fill in.

When a decision is made:
1. The owner fills in `## Decision` (what + who + date + one-line rationale).
2. Update the linked GDD's Open Questions row.
3. If it's an architecture choice, run `/architecture-decision` to turn it
   into an ADR, then update `docs/architecture/control-manifest.md`.
4. Update `production/sprints/sprint-1.md` task status.

| Doc | Decision | Owner(s) | Blocks | Sprint 1 task |
|-----|----------|----------|--------|---------------|
| [combat-concept.md](combat-concept.md) | What combat actually is | Luka, Oasis | Regions & Combat epic, enemy/boss art & writing | S1-001 |
| [vertical-slice-villager-selection.md](vertical-slice-villager-selection.md) | Which 3 wheel positions the slice uses | Non, Jack | Villager Archetype epic, archetype division, zone contiguity | S1-002 |
| [audio-technical-approach.md](audio-technical-approach.md) | Unity native audio vs. FMOD vs. Wwise | audio-director, technical-director (+ Ken) | Sound & Adaptive Crossfade epic, its ADR | S1-012 |
| [portrait-lit-decay.md](portrait-lit-decay.md) | Lit portraits: indefinite vs. decay | game-designer | Painting Scene tuning, sleep-screen impl detail | S1-010 |
| [zone-key-major-minor.md](zone-key-major-minor.md) | Major/minor key per zone | Ken | Full 12-track music production | S1-011 |

## Not a decision doc

**Painting-scene playtest verdict (`S1-005`)** is not here — it's not a
choice between options, it's a playtest that has to happen. It needs a
playable build (`S1-004`, needs the Editor) and a real session, producing a
PROCEED / PIVOT / KILL in `prototypes/painting-scene/REPORT.md`.
