# Epics Index

Last Updated: 2026-08-31
Engine: Unity 6.3 LTS

| Epic | Layer | System | GDD | Stories | Status |
|------|-------|--------|-----|---------|--------|
| Twelve Content Framework | Foundation | twelve-content-framework | `design/gdd/twelve-content-framework.md` | Not yet created | Ready |
| Daily Loop & Calendar | Core | daily-loop-and-calendar | `design/gdd/daily-loop-and-calendar.md` | Not yet created | Ready (untraced requirements) |
| Farm & Economy | Feature | farm-and-economy | `design/gdd/farm-and-economy.md` | Not yet created | Ready (untraced requirements) |
| Villager Archetype System | Core | villager-archetype-system | `design/gdd/villager-archetype-system.md` | Not yet created | Ready (blocked on vertical-slice villager selection) |
| Reflection & Painting Scene | Feature | painting-and-reflection | `design/gdd/painting-and-reflection.md` | Not yet created | **Blocked** — pending prototype playtest verdict |
| Sound & Adaptive Crossfade | Core | sound-and-crossfade | `design/gdd/sound-and-crossfade.md` | Not yet created | **Blocked** — pending audio technical-approach ADR |

**Not yet epic'd** (Vertical-Slice tier, lower urgency than the above, or
structurally blocked):
- Regions & Combat — not epic-ready; combat concept undecided (owner:
  Luka, Oasis)
- Narrative / Portrait Story System — presentation-layer, depends on
  Villager Archetype System and Painting Scene reaching further maturity
  first

## Recommended Order

1. **Twelve Content Framework** — nothing else can start correctly without
   this
2. **Villager Archetype System** — once vertical-slice villagers are picked
3. **Reflection & Painting Scene prototype playtest** — this determines
   whether the epic proceeds at all; run in parallel with #1/#2's early
   work, not after
4. **Daily Loop & Calendar** and **Farm & Economy** — can proceed in
   parallel with #2, both are genre-standard and lower-risk
5. **Sound & Adaptive Crossfade** — once the technical-approach ADR exists
