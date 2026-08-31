# Farm & Economy

> **Status**: Draft — ingested from GDD v1.3
> **Author**: game-designer (ingestion pass)
> **Last Updated**: 2026-08-31
> **Last Verified**: 2026-08-31 against source GDD
> **Implements Pillar**: Twelve Is the Structure, Not the Content

## Summary

Standard farming-sim economy — crops, animals, artisan goods, cooking,
crafting, fishing, foraging, shipping — sized to twelve per category, plus
one addition: flowers are a separate, paint-yielding crop category that
feeds the Reflection & Painting Scene system. Source GDD explicitly says
this is well-trodden ground: "worth looking at how they've handled it
rather than starting from scratch."

> **Quick reference** — Layer: `Feature` · Priority: `MVP` · Key deps: `Daily Loop & Calendar`, `Twelve Content Framework`

## Overview

Twelve of nearly every farm content category (vegetables, fruit trees,
flowers, animals, fish, forageables, ores, artisan goods, machines, tool
upgrades), split either 3-per-season or archetype-linked depending on
category. Money is made primarily through artisan goods by mid-game;
flowers/paint are explicitly called out as "the best thing you can sell,"
making the sell-vs-mill choice a real economic decision, not a formality.

## Player Fantasy

The familiar farming-sim satisfaction of a productive, growing farm —
nothing novel is claimed here by design; the source GDD is explicit that
this system should feel like genre baseline, freeing player attention for
the game's actual differentiators.

## Detailed Design

### Core Rules

1. **Crops**: untilled → tilled → watered → growing → ready. Quality tiers,
   fertilizer, sprinklers, and regrowth on some plants (genre-standard,
   mechanics not further specified).
2. **Flowers**: a separate crop category from food. Season-locked, slower
   and pricier than vegetables. Mill into paint (feeds the painting system)
   or sell directly for high value.
3. **Animals**: coop, barn, and pasture housing. Happiness drives product
   quality (genre-standard).
4. **Artisan goods**: twelve machines producing twelve goods from raw farm
   output (cheese, mayonnaise, honey, wine, preserves, cloth, oil, vinegar,
   pickles, dried fruit, smoked fish). Explicitly the primary money source
   by mid-game.
5. **Cooking**: recipes unlocked via relationships, shops, exploration.
   Restores energy, better gift value than raw produce.
6. **Crafting**: tools, machines, fences, paths, farm buildings. Twelve
   tool-upgrade steps spread across the toolset (axe, pickaxe, hoe,
   watering can, scythe, rod — two steps each).
7. **Fishing**: tension-bar minigame (mechanic not further detailed).
   Twelve fish split across river/coast/lake/cave water, 3 per season.
8. **Foraging**: twelve seasonal forageables (3 per season) spread across
   the twelve town zones and four regions.
9. **Economy**: shipping box, shops, house/barn upgrades.

### Full Content Inventory

| Content | Count | Split | Notes |
|---------|-------|-------|-------|
| Vegetables | 12 | 3 per season | Parsnip, pea, radish, tomato, pepper, melon, pumpkin, beet, cabbage, leek, turnip, squash |
| Fruit trees | 12 | 3 per season | Cherry, apricot, plum, peach, fig, mulberry, apple, pear, quince, persimmon, pomegranate, medlar |
| Flowers | 12 | 1 per archetype | The paint crop. Season-locked, slower and pricier than food. |
| Farm animals | 12 | Coop/barn/pasture | Chicken, duck, goose, quail, cow, goat, sheep, alpaca, pig, rabbit, bees, donkey |
| Fish | 12 | 3 per season | Split across river, coast, lake, cave water |
| Forageables | 12 | 3 per season | Across twelve zones and four regions |
| Ores/minerals | 12 | Region-gated | Deeper regions hold later ones |
| Artisan goods | 12 | From machines | Cheese, mayonnaise, honey, wine, preserves, cloth, oil, vinegar, pickles, dried fruit, smoked fish |
| Machines | 12 | Craftable | Churn, cheese press, loom, keg, preserves jar, oil maker, smoker, kiln, mill, bee house, dehydrator |
| Tool upgrades | 12 | Across toolset | Axe, pickaxe, hoe, watering can, scythe, rod — 2 steps each |

**Crops/trees by season**:

| Season | Vegetables | Fruit Trees |
|--------|------------|-------------|
| Spring | Parsnip, pea, radish | Cherry, apricot, plum |
| Summer | Tomato, pepper, melon | Peach, fig, mulberry |
| Autumn | Pumpkin, beet, cabbage | Apple, pear, quince |
| Winter | Leek, turnip, winter squash | Persimmon, pomegranate, medlar |

**Farm buildings**: coop, barn, silo, mill, well, shed, greenhouse, stable.
The mill does double duty — grinds grain AND presses flowers into pigment
for the painting system.

## Formulas

Not specified in source GDD. Crop growth timing, quality-tier probability,
animal happiness → product quality, and shipping prices all need concrete
formulas before implementation — flag for `economy-designer` +
`systems-designer`.

## Edge Cases

| Scenario | Expected Behavior | Rationale |
|----------|---------------------|-----------|
| Player mills a flower vs. sells it directly | Both valid; milling feeds the painting system, selling is pure economy | Explicit design tension called out in source GDD |
| Out-of-season crop planted | Not specified | Standard genre behavior (fails to grow / can't plant) should be confirmed |

## Dependencies

| System | Direction | Nature of Dependency |
|--------|-----------|-------------------------|
| Daily Loop & Calendar | This depends on it | Overnight growth/shipping resolution, season gating |
| Twelve Content Framework | This depends on it | Flower→archetype mapping |
| Reflection & Painting Scene | Depends on this | Milled flowers are the paint resource input |
| Regions & Combat | Shares a dependency | Tool-tier gates both crafting progression and region access |

## Tuning Knobs

| Parameter | Current Value | Safe Range | Effect of Increase | Effect of Decrease |
|-----------|----------------|------------|----------------------|----------------------|
| Flower sell price vs. mill value | Not yet specified | — | Push players toward selling | Push players toward milling/painting |
| Content counts per category | 12 (fixed) | Structural constraint | — | — |

## Visual/Audio Requirements

Not specified in source GDD — standard genre asset needs (crop sprites,
growth stages, shop UI, shipping bin feedback). Flag for `art-director`.

## Game Feel

Not detailed — genre-standard expectations apply. Flag for `game-designer`
to confirm no deviation from Stardew-tradition feel is intended here.

## UI Requirements

| Information | Display Location | Update Frequency | Condition |
|--------------|---------------------|------------------------|-----------|
| Crop growth state | World-space sprite | Per day | Always |
| Shipping bin contents/value | Shipping UI | On interaction | Always |
| Shop inventory/prices | Shop UI | On interaction | Always |

*(Twelve is the count, so slots/grids in crafting and inventory menus should
line up with it — explicit note from the team assignment doc, owned by
Luka.)*

## Cross-References

| This Document References | Target GDD | Specific Element Referenced | Nature |
|-----------------------------|-----------|----------------------------------|--------|
| Overnight resolution | `design/gdd/daily-loop-and-calendar.md` | Sleep/overnight cycle | State trigger |
| Milled flowers feed painting | `design/gdd/painting-and-reflection.md` | Paint resource input | Data dependency |
| Flower→archetype mapping | `design/gdd/twelve-content-framework.md` | Flower column | Data dependency |
| Tool-tier gates region access | `design/gdd/regions-and-combat.md` | Region progression gating | Rule dependency |

## Acceptance Criteria

- [ ] All twelve items exist per content category, correctly split per the
  inventory table
- [ ] Flower milling produces paint resource consumed by the painting system
- [ ] Crafting/inventory UI slot counts are multiples of twelve
- [ ] No hardcoded crop/animal/good values — all data-driven per project
  coding standards

## Open Questions

| Question | Owner | Deadline | Resolution |
|----------|-------|----------|------------|
| Crop growth timing, quality-tier, and shipping price formulas | `economy-designer` | Before Farm & Economy implementation | — |
| Energy costs per farm action | `economy-designer` | Before Farm & Economy implementation | — |
| Flower sell-vs-mill value balance | `economy-designer` | Before Alpha | — |
