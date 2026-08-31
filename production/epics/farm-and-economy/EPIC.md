# Epic: Farm & Economy

> **Layer**: Feature
> **GDD**: design/gdd/farm-and-economy.md
> **Architecture Module**: Not yet defined — no dedicated ADR exists
> **Status**: Ready (with untraced requirements)
> **Stories**: Not yet created — run `/create-stories farm-and-economy`

## Overview

Implements the standard farming-sim economy — crops, animals, artisan
goods, cooking, crafting, fishing, foraging, shipping — plus the flower/
paint crop category that feeds the Reflection & Painting Scene epic. Source
GDD explicitly treats this as low-design-risk, genre-standard territory.

## Governing ADRs

| ADR | Decision Summary | Engine Risk |
|-----|-------------------|-------------|
| None yet | Recipe/formula design (growth timing, quality tiers, shipping prices) is unresolved — see Open Questions below | N/A |

## GDD Requirements

| TR-ID | Requirement | ADR Coverage |
|-------|-------------|----------------|
| TR-farm-001 | All twelve items exist per farm content category, correctly split | ❌ No ADR |
| TR-farm-002 | Flower milling produces paint resource consumed by Painting Scene | ❌ No ADR |
| TR-farm-003 | Crafting/inventory UI slot counts are multiples of twelve | ❌ No ADR |

## Definition of Done

This epic is complete when:
- All stories are implemented, reviewed, and closed via `/story-done`
- All acceptance criteria from `design/gdd/farm-and-economy.md` are verified
- All Logic and Integration stories have passing test files in `tests/`
- `economy-designer` has supplied concrete formulas for crop growth timing,
  quality tiers, and shipping prices (currently unspecified — see the
  source GDD's Open Questions)

## Next Step

Run `/create-stories farm-and-economy` to break this epic into implementable
stories. Recommend resolving the economy formulas with `economy-designer`
before story implementation begins, even if stories are drafted now.
