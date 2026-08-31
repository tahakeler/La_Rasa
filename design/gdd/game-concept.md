# Game Concept: La Rasa

*Created: 2026-08-31 (ingested from GDD v1.3, "for the team")*
*Status: Approved by team (v1.3) — this file is a framework-formatted ingestion of that document, not a new draft*

---

## Elevator Pitch

> A farming sim in the Stardew tradition, where the silent painter Rasa tends
> a valley of twelve Jungian-archetype villagers by day and, at night,
> reflects on whoever's story moved — uncovering their portrait, and the part
> of themselves they can't look at, one brushstroke at a time.

---

## Core Identity

| Aspect | Detail |
| ---- | ---- |
| **Genre** | Farming / Life Sim |
| **Platform** | PC (implied by Unity/URP/2D scope; not explicitly stated in source GDD) |
| **Target Audience** | Niche, hardcore, committed fans of the farming-sim genre — smaller but more dedicated market than the Stardew mainstream |
| **Player Count** | Single-player (not specified in source GDD — flag if co-op is ever wanted, since that would need `network-programmer` reactivated) |
| **Session Length** | Not specified in source GDD |
| **Monetization** | Not specified in source GDD |
| **Estimated Scope** | Vertical slice targeted by end of semester; full game scope beyond that is undefined |
| **Comparable Titles** | Stardew Valley (explicit primary reference) |

---

## Core Fantasy

You are a painter who doesn't speak, tending a farm and getting to know
twelve townspeople entirely through gifts, gestures, and what you notice
about them — and at night, painting what you've noticed. The game trusts
silence and observation over dialogue trees: what a villager tells the valley
and what their portrait eventually reveals are two different things, and
closing that gap is the emotional core of the game.

---

## Unique Hook

It's Stardew Valley, **and also** every villager is built on a Jungian
archetype with a persona (what they show the town) and a shadow (what only
the player's nightly painting sessions can uncover) — delivered through a
mute protagonist and a bespoke twin-stick "read the room" painting minigame
rather than a dialogue-choice system. The hook is structural, not cosmetic:
it drives the villager writing, the town layout (twelve zones ringing the
square), the music (twelve keys around the circle of fifths), and the color
palette (twelve-position wheel) from a single organizing system.

---

## Player Experience Analysis (MDA Framework)

### Target Aesthetics

| Aesthetic | Priority | How We Deliver It |
| ---- | ---- | ---- |
| **Sensation** | 2 | Art, music, and the painting scene's audiovisual feedback — GDD explicitly states these are elevated above genre norm |
| **Fantasy** | 3 | Being a painter whose work has weight in the world |
| **Narrative** | 1 | Twelve questlines told entirely through portraits filling in over time; a secret twist once all twelve are complete |
| **Challenge** | Not primary — farming-sim pacing, not a challenge-driven game | — |
| **Fellowship** | Supporting | Twelve romanceable villagers, relationship hearts |
| **Discovery** | Supporting | Portraits reveal gradually; four regions gated by tools/ore |
| **Expression** | 2 | The player literally paints; flower-milling ties farming output to the expression system |
| **Submission** | Supporting | Standard farming-sim loop: low-stress, rhythmic, cozy |

### Key Dynamics
- Players will make daily "rounds" through town specifically to light up portraits for that night's reflection, rather than visiting at random
- Players will start reading villagers' shadows into their daytime persona once they've seen a few reflection scenes — the "unreadable" early portraits should retroactively make sense
- Players will treat flowers/paint as a second economy (sell vs. mill), creating a real tradeoff rather than a free resource

### Core Mechanics
1. Standard farming-sim daily loop (12-hour day) — crops, animals, artisan goods, shipping
2. Reflection-at-bedtime: villager story-progress lights up a portrait; player picks one to enter the painting scene
3. Painting scene: twin-stick mechanic — one stick reads the villager's actual emotional state, the other pushes for depth; correct reads uncover shadow, incorrect reads resist
4. Region exploration: fishing/foraging/mining/combat across four regions, each tied to three archetypes' shadows
5. "Twelve of everything" content structure — a deliberate design constraint, not an emergent count

---

## Player Motivation Profile

### Primary Psychological Needs Served

| Need | How This Game Satisfies It | Strength |
| ---- | ---- | ---- |
| **Autonomy** | Player chooses which portrait to reflect on each night, which crops/goods to focus on, which region to work | Core |
| **Competence** | Correctly "reading" a villager's emotional state in the painting scene is a legible skill that improves with attention to their daytime behavior | Core |
| **Relatedness** | Twelve romanceable villagers with persona/shadow depth; reflection is explicitly relationship-gated | Core |

### Player Type Appeal (Bartle Taxonomy)
- [x] **Achievers** — twelve-of-everything completion structure (12 portraits, 12 recipes, 12 relationship tracks)
- [x] **Explorers** — four regions, gradual portrait reveal, a hidden endgame twist
- [x] **Socializers** — the entire persona/shadow/relationship system
- [ ] **Killers/Competitors** — not a design target; combat exists but isn't PvP/competitive

### Flow State Design
- **Onboarding curve**: Not yet specified — needs design once vertical-slice scope is locked
- **Difficulty scaling**: Regions gate by tool tier and ore; The Deep Kiln goes down in floors for late materials
- **Feedback clarity**: Painting scene gives continuous color/sound feedback as the player moves the stick, with no score or bar — feedback is diegetic, not numeric
- **Recovery from failure**: A "failed" painting session just doesn't advance that portrait — cost is the evening, not a hard fail state (explicit design decision in source GDD)

---

## Core Loop

### Moment-to-Moment (30 seconds)
Standard farming actions — water/plant, tend animals, gather — identical in kind to Stardew Valley.

### Short-Term (5–15 minutes)
A pass through town (shop/inn/forge/clinic/hall + villagers) or a trip into one of the four regions for fishing/foraging/mining/combat.

### Session-Level (30–120 minutes)
A full in-game day: wake → farm → town or region → process/craft → ship → sleep, ending (some nights) with a reflection/painting scene if a villager's story moved.

### Long-Term Progression
Twelve villager questlines advancing through portrait stages (early/middle/late/finished), tool and building upgrades, region unlocks, and a full calendar year (144 days) across four seasons.

### Retention Hooks
- **Curiosity**: what a portrait looks like once fully uncovered; the secret ending twist
- **Investment**: relationship hearts, farm/building upgrades, in-progress portraits
- **Social**: not applicable — single-player
- **Mastery**: reading villagers accurately in the painting scene gets easier with attention paid during the day

---

## Game Pillars

*Extracted from source GDD's structural commitments. Not yet formally ratified via `/architecture-decision` or a dedicated pillars session — recommend `creative-director` confirm these read correctly before they're treated as binding.*

### Pillar 1: Twelve Is the Structure, Not the Content
Every content category in the game — villagers, crops, fish, zones, tracks,
recipes — is sized to twelve and mapped onto a single archetype wheel.

*Design test*: If a new content category is proposed, ask whether it fits the
twelve-count structure or whether it's actually decoration on an existing one.

### Pillar 2: Rasa Never Speaks
No dialogue lines, no player response text, nothing UI-facing in her voice.
She acts through gifts, gestures, expression, and painting only.

*Design test*: Any feature that would require giving Rasa a written voice
(response wheels, narrated interior monologue) is out — express it through
the painting system or villager reactions instead.

### Pillar 3: Persona and Shadow Are Structurally Separate
What a villager shows the valley (persona, delivered via daytime dialogue)
and what they're hiding (shadow, delivered only via the painting scene) must
stay legible as two different channels.

*Design test*: If a piece of shadow information is about to leak into normal
daytime dialogue, it belongs in the painting scene instead.

### Anti-Pillars (What This Game Is NOT)
- **NOT a game with a world to save**: Source GDD states this explicitly — no central plot, no stakes beyond the personal.
- **NOT dialogue-choice-driven**: The protagonist has no lines; conversation is one-directional from villagers.
- **NOT punishing on a failed painting read**: A bad session costs an evening, not more.

---

## Inspiration and References

| Reference | What We Take From It | What We Do Differently | Why It Matters |
| ---- | ---- | ---- | ---- |
| Stardew Valley | Daily loop structure, farm/economy systems, town social loop | Twelve-hour day, silent protagonist, archetype-driven villager design, painting-based relationship system | Validates the core farming loop is a known-good foundation; our differentiation is layered on top, not a replacement |

**Non-game inspirations**: Jungian archetype theory (the twelve-archetype wheel), the circle of fifths (zone/music layout), the color wheel (zone palette) — all explicitly load-bearing structural choices, not thematic dressing.

---

## Target Player Profile

| Attribute | Detail |
| ---- | ---- |
| **Age range** | Not specified in source GDD |
| **Gaming experience** | Mid-core to hardcore — source GDD explicitly targets "niche, hardcore, committed fan of the genre" over the Stardew mainstream |
| **Time availability** | Not specified |
| **Platform preference** | PC (implied) |
| **Current games they play** | Stardew Valley and similar farming/life sims |
| **What they're looking for** | More depth in art, narrative, and OST than the genre typically offers |
| **What would turn them away** | A shallow reflection mechanic, or a painting minigame that isn't fun on its own — source GDD states this as the make-or-break risk |

---

## Technical Considerations

| Consideration | Assessment |
| ---- | ---- |
| **Recommended Engine** | Unity, URP, 2D — confirmed by team, matches `.claude/agents/unity-*` roster on this branch |
| **Key Technical Challenges** | The painting scene's twin-stick emotional-read mechanic (bespoke, unproven — needs `/prototype`); data-driven "twelve of everything" content architecture; beat-matched adaptive music crossfades tied to both zone transitions and the painting scene |
| **Art Style** | 2D, stylized — portraits are "real paintings" per the source GDD |
| **Art Pipeline Complexity** | Medium — custom 2D, thirteen characters (12 villagers + Rasa) needing a consistent aesthetic across multiple artists |
| **Audio Needs** | Music-heavy, adaptive — twelve tracks, one per key, beat-matched crossfades, plus dynamic key resolution inside the painting scene |
| **Networking** | None specified — single-player |
| **Content Volume** | Twelve of nearly everything (see `twelve-content-framework.md` for the full inventory) |
| **Procedural Systems** | None specified |

---

## Risks and Open Questions

### Design Risks
- The painting scene may not be fun on its own — source GDD states this outright as the bar the whole game is measured against ("if that's fun the rest is content")
- Combat concept is undecided (see below) — risk of feeling generic/bolted-on if not resolved with intent

### Technical Risks
- Beat-matched adaptive audio crossfades are a real implementation risk if not scoped early
- Twin-stick emotional-read input needs an accessibility fallback (single-input mode, per source GDD) — that's a second control scheme to design and test, not an afterthought

### Market Risks
- Explicitly targeting a smaller, more dedicated niche rather than the broad Stardew audience — a deliberate scope choice, not an oversight

### Scope Risks
- Twelve-of-everything is a lot of content for a semester-timeline team; the vertical slice deliberately cuts to 3-of-everything for exactly this reason

### Open Questions (from source GDD, owners as stated there)
| Question | Owner | Notes |
| ---- | ---- | ---- |
| Combat concept (paint-as-resource vs. regions being "in her head") | Luka, Oasis | Structural decision — blocks region/enemy implementation |
| Vertical-slice zone contiguity vs. season-mapping | Non, Jack (archetype division) | The proposed season split (spring at wheel positions 1/5/9) isn't contiguous, but the 3-zone slice needs adjacent zones for crossfades to be tested |
| Portrait decay vs. indefinite lit state | Unassigned in source GDD | Indefinite is kinder; decay adds weight — GDD says "worth trying both" |
| Major vs. minor key per zone | Ken | All-major reads uniformly cheerful per source GDD's own note |

---

## MVP Definition

**Core hypothesis**: The painting scene (reading a villager's emotional state via twin-stick input, uncovering their portrait over time) is fun and legible on its own, independent of full content volume.

**Required for MVP**:
1. Farming daily loop (12-hour day, plant/water/harvest, shipping)
2. At least one full villager with persona + shadow + reflection + painting scene, portrait uncovering across all four stages
3. The painting scene itself, playable and readable without a score/bar

**Explicitly NOT in MVP**:
- Combat (undecided)
- Full twelve-of-everything content volume
- Full four-region exploration

### Scope Tiers

| Tier | Content | Features | Timeline |
| ---- | ---- | ---- | ---- |
| **MVP** | 1 villager, 1 season | Farming loop + painting scene only | Not yet estimated |
| **Vertical Slice** | 3 villagers, 3 zones, 1 season, 1 region (per source GDD's own slice table) | Full loop incl. one region's fishing/foraging/mining/combat | By end of semester |
| **Alpha** | All twelve, placeholder polish | All systems present | Not yet scoped |
| **Full Vision** | Twelve of everything, polished | Complete per source GDD | Not yet scoped |

---

## Next Steps

- [x] Concept exists (this document) — recommend `creative-director` sign-off on the Game Pillars section above, since those were extracted, not originally written as pillars
- [x] Engine confirmed: Unity (see `CLAUDE.md`, `technical-preferences.md` on this branch stack)
- [ ] Resolve the four open questions above before `/architecture-decision` on region/combat and audio systems
- [ ] `/prototype` the painting scene specifically — highest-risk, most novel system, explicitly gates the rest of the game per source GDD
- [ ] `/map-systems` — see `design/gdd/systems-index.md` (this ingestion pass)
- [ ] Design each system (`/design-system [system-name]`) — per-system files started in this same pass, need review
- [ ] `/vertical-slice` once systems + architecture are locked
- [ ] `/sprint-plan new` for first production sprint
