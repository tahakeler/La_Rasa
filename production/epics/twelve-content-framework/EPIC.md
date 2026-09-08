# Epic: Twelve Content Framework

> **Layer**: Foundation
> **GDD**: design/gdd/twelve-content-framework.md
> **Architecture Module**: ArchetypeWheel data architecture
> **Status**: In Progress
> **Stories**: 3 created (see table below)

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

## Stories

| # | Story | Type | Status | ADR |
|---|-------|------|--------|-----|
| 001 | ArchetypeWheel data asset and accessor API | Logic | In Progress | ADR-0001 |
| 002 | Wheel structural validation and loud authoring-time failure | Logic | In Progress | ADR-0001 |
| 003 | "No hardcoded content values" guardrail | Integration | Ready | ADR-0001 |

Stories 001 and 002 have code + EditMode tests drafted on branch
`catchup/unity-cli-and-sprint-1`. They are blocked on `S1-003` (a Unity
Editor to compile and run them) before they can move to Done via
`/story-done`.

## Next Step

Complete `S1-003` (Editor verification — see `Assets/LaRasa/README.md`), then
run `/story-readiness` → `/dev-story` → `/story-done` on stories 001-003 in
order.
