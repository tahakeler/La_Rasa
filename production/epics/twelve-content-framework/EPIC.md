# Epic: Twelve Content Framework

> **Layer**: Foundation
> **GDD**: design/gdd/twelve-content-framework.md
> **Architecture Module**: ArchetypeWheel data architecture
> **Status**: Ready
> **Stories**: Not yet created — run `/create-stories twelve-content-framework`

## Overview

Implements the `ArchetypeWheel` ScriptableObject holding the twelve
archetype positions (archetype, shadow, flower, season, color, key) that
every other content system in the game references rather than duplicates.
This is the true foundation layer — nothing else in the vertical slice can
be correctly built before this exists.

## Governing ADRs

| ADR | Decision Summary | Engine Risk |
|-----|-------------------|-------------|
| ADR-0001 | `ArchetypeWheel` ScriptableObject as single source of truth for the twelve positions | LOW |

## GDD Requirements

| TR-ID | Requirement | ADR Coverage |
|-------|-------------|----------------|
| TR-twelve-001 | All twelve positions implemented as a single data asset referenced (not duplicated) by other systems | ADR-0001 ✅ |
| TR-twelve-002 | No hardcoded archetype/color/key values exist outside the canonical data asset | ADR-0001 ✅ |

## Definition of Done

This epic is complete when:
- All stories are implemented, reviewed, and closed via `/story-done`
- All acceptance criteria from `design/gdd/twelve-content-framework.md` are verified
- All Logic and Integration stories have passing test files in `tests/`
- No dependent epic (villagers, daily loop, sound) has started implementation
  before this epic reaches Done — everything else references this data

## Next Step

Run `/create-stories twelve-content-framework` to break this epic into
implementable stories.
