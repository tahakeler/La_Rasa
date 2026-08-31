# Systems Index: La Rasa

> **Status**: Draft — ingested from GDD v1.3, needs team review
> **Created**: 2026-08-31
> **Last Updated**: 2026-08-31
> **Source Concept**: design/gdd/game-concept.md

---

## Overview

La Rasa is a Stardew-tradition farming sim (Unity/URP/2D) whose systems split
into two groups: the farming-sim baseline (daily loop, crops, animals,
economy — well-understood, low design risk) and the game's differentiators
(archetype-driven villagers, the painting/reflection mechanic, region combat
tied to archetype shadows, circle-of-fifths adaptive music). The differentiator
systems all key off a single shared structure — twelve archetypes mapped to a
wheel — so that system (`twelve-content-framework.md`) is the true foundation
layer beneath almost everything else, including the town layout, the color
palette, and the music.

---

## Systems Enumeration

| # | System Name | Category | Priority | Status | Design Doc | Depends On |
|---|-------------|----------|----------|--------|------------|------------|
| 1 | Twelve Content Framework (archetype wheel, data architecture) | Core | MVP | In Design | `design/gdd/twelve-content-framework.md` | None |
| 2 | Daily Loop & Calendar | Core | MVP | In Design | `design/gdd/daily-loop-and-calendar.md` | Twelve Content Framework (day/month/year counts) |
| 3 | Farm & Economy (crops, animals, artisan goods, shipping) | Economy | MVP | In Design | `design/gdd/farm-and-economy.md` | Daily Loop & Calendar |
| 4 | Villager Archetype System (persona/shadow, twelve villagers) | Narrative | MVP | In Design | `design/gdd/villager-archetype-system.md` | Twelve Content Framework |
| 5 | Reflection & Painting Scene | Gameplay | MVP | In Design (HIGH RISK — needs prototype) | `design/gdd/painting-and-reflection.md` | Villager Archetype System, Daily Loop & Calendar, Sound System (key resolution) |
| 6 | Regions & Combat | Gameplay | Vertical Slice | Blocked — combat mechanic undecided | `design/gdd/regions-and-combat.md` | Villager Archetype System (enemies from shadows), Farm & Economy (tool gating) |
| 7 | Sound & Adaptive Crossfade | Audio | MVP | In Design | `design/gdd/sound-and-crossfade.md` | Twelve Content Framework (zone/key mapping) |
| 8 | Narrative / Portrait Story System | Narrative | Vertical Slice | In Design | `design/gdd/narrative-portrait-system.md` | Villager Archetype System, Reflection & Painting Scene |
| 9 | Town/Zone Layout (inferred) | Core | MVP | Not Started | — | Twelve Content Framework |
| 10 | Save/Persistence (inferred) | Persistence | MVP | Not Started | — | Farm & Economy, Villager Archetype System |
| 11 | Farming UI: crafting/build menus, shipping, inventory (inferred) | UI | MVP | Not Started | — | Farm & Economy |

Systems 9–11 are marked "(inferred)" — the source GDD describes their content
but doesn't scope them as standalone systems. They're listed because
`/create-architecture` and `/create-epics` will need them regardless.

---

## Categories

| Category | Description | Typical Systems |
|----------|-------------|-----------------|
| **Core** | Foundation everything depends on | Twelve Content Framework, Daily Loop & Calendar, Town/Zone Layout |
| **Gameplay** | The systems that make the game fun | Reflection & Painting Scene, Regions & Combat |
| **Economy** | Resource creation and consumption | Farm & Economy |
| **Persistence** | Save state and continuity | Save/Persistence |
| **UI** | Player-facing information displays | Farming UI |
| **Audio** | Sound and music systems | Sound & Adaptive Crossfade |
| **Narrative** | Story and dialogue delivery | Villager Archetype System, Narrative/Portrait Story System |

---

## Priority Tiers

Per the source GDD's own vertical-slice table (1 season, 3 villagers, 3
zones, 1 region, 3-of-everything):

| Tier | Definition | Target Milestone |
|------|------------|------------------|
| **MVP** | Farming loop + one full villager's persona/shadow/painting arc, playable and readable on its own | First playable prototype (painting scene specifically) |
| **Vertical Slice** | 1 season, 3 villagers/zones/crops/etc., 1 region with combat (once designed) | End of semester, per source GDD |
| **Alpha** | All twelve, placeholder content | Not yet scoped |
| **Full Vision** | Twelve of everything, polished | Not yet scoped |

---

## Dependency Map

### Foundation Layer (no dependencies)

1. **Twelve Content Framework** — the archetype wheel (persona, shadow,
   flower, color, key, zone per archetype) is the shared data structure
   every other differentiator system reads from. Nothing else can be
   correctly scoped without this being locked first.

### Core Layer (depends on foundation)

1. **Daily Loop & Calendar** — depends on: Twelve Content Framework (12-hour
   day, 12-day month, 12-month year all come from the same constraint)
2. **Villager Archetype System** — depends on: Twelve Content Framework
   (each villager IS an archetype position)
3. **Town/Zone Layout** *(inferred)* — depends on: Twelve Content Framework
   (zones ring the square in wheel order)
4. **Sound & Adaptive Crossfade** — depends on: Twelve Content Framework
   (zone → key mapping via circle of fifths)

### Feature Layer (depends on core)

1. **Farm & Economy** — depends on: Daily Loop & Calendar
2. **Reflection & Painting Scene** — depends on: Villager Archetype System,
   Daily Loop & Calendar (bedtime trigger), Sound & Adaptive Crossfade (key
   resolution feedback inside the scene)
3. **Regions & Combat** — depends on: Villager Archetype System (enemies
   built from archetype shadows, per source GDD), Farm & Economy (tool-tier
   gating)

### Presentation Layer (depends on features)

1. **Narrative / Portrait Story System** — depends on: Villager Archetype
   System, Reflection & Painting Scene (portrait stages advance through
   painting sessions)
2. **Farming UI** *(inferred)* — depends on: Farm & Economy

### Polish Layer (depends on everything)

1. **Save/Persistence** *(inferred)* — technically needed earlier than
   "polish" implies for a real production, but listed last here only
   because the source GDD gives it no design detail to build from yet.

---

## Recommended Design Order

| Order | System | Priority | Layer | Agent(s) | Est. Effort |
|-------|--------|----------|-------|----------|-------------|
| 1 | Twelve Content Framework | MVP | Foundation | `game-designer`, `systems-designer` | M |
| 2 | Daily Loop & Calendar | MVP | Core | `systems-designer` | S |
| 3 | Villager Archetype System | MVP | Core | `narrative-director`, `writer`, `game-designer` | L |
| 4 | Reflection & Painting Scene | MVP | Feature | `game-designer`, `systems-designer`, `ux-designer` — then `/prototype` before full spec | L |
| 5 | Farm & Economy | MVP | Feature | `systems-designer`, `economy-designer` | M |
| 6 | Sound & Adaptive Crossfade | MVP | Core | `audio-director`, `sound-designer` | M |
| 7 | Narrative / Portrait Story System | Vertical Slice | Presentation | `narrative-director`, `writer` | M |
| 8 | Regions & Combat | Vertical Slice | Feature | `game-designer`, `systems-designer` — **blocked until Luka/Oasis resolve the combat concept** | L |

---

## Circular Dependencies

- None found. The Twelve Content Framework as a shared foundation avoids the
  obvious cycle risk (e.g., villagers needing zones needing music needing
  villagers) by having everything key off archetype position rather than off
  each other directly.

---

## High-Risk Systems

| System | Risk Type | Risk Description | Mitigation |
|--------|-----------|-------------------|------------|
| Reflection & Painting Scene | Design | Source GDD states the whole game's value proposition rests on this being fun in isolation | `/prototype` before writing the full GDD spec; source GDD already flags accessibility (single-input mode, playable-by-ear) as a hard requirement, not a stretch goal |
| Sound & Adaptive Crossfade | Technical | Beat-matched crossfades (not timer-based) plus painting-scene key resolution is real audio-engineering work, not just asset creation | Scope an early technical spike with `audio-director` + `unity-specialist` before committing to full twelve-track production |
| Regions & Combat | Design | Undecided concept risks a generic bolt-on combat system if resolved late/under time pressure | Force the Luka/Oasis decision in Week 1 per the rollout plan, before any region GDD work starts |
| Vertical Slice Zone Contiguity | Scope | The proposed season split isn't wheel-contiguous, but the slice needs 3 adjacent zones for crossfades to be tested — as flagged, this must be resolved before Non/Jack divide the twelve archetypes, or their split work gets redone | Resolve as the very first Week 1 decision, since it blocks archetype division itself |

---

## Progress Tracker

| Metric | Count |
|--------|-------|
| Total systems identified | 11 (8 explicit + 3 inferred) |
| Design docs started | 8 |
| Design docs reviewed | 0 |
| Design docs approved | 0 |
| MVP systems designed (drafted) | 6/6 |
| Vertical Slice systems designed (drafted) | 1/2 (Regions & Combat blocked) |

---

## Next Steps

- [ ] Team review of this index — confirm the inferred systems (Town/Zone
  Layout, Save/Persistence, Farming UI) aren't already covered elsewhere
- [ ] Resolve the two Week-1 blocking decisions (combat concept, zone
  contiguity) before Regions & Combat or archetype-division work proceeds
- [ ] `/design-review` on each drafted GDD in this pass
- [ ] `/gate-check pre-production` once MVP-tier systems are reviewed
- [ ] `/prototype painting-scene` before committing further design time to
  the Reflection & Painting Scene GDD's Formulas/Tuning Knobs sections
