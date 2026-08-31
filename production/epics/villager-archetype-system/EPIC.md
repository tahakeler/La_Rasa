# Epic: Villager Archetype System

> **Layer**: Core
> **GDD**: design/gdd/villager-archetype-system.md
> **Architecture Module**: Persona/Shadow structural separation
> **Status**: Ready (with untraced requirements)
> **Stories**: Not yet created — run `/create-stories villager-archetype-system`

## Overview

Implements twelve villagers, each mapped to an archetype wheel position,
each carrying structurally separate persona (daytime dialogue) and shadow
(painting-scene-only) content, plus the four-stage portrait progression
that gates shadow reveal.

## Governing ADRs

| ADR | Decision Summary | Engine Risk |
|-----|-------------------|-------------|
| ADR-0001 | Villagers reference `ArchetypeWheel` positions rather than duplicating data | LOW |
| ADR-0002 | Persona/shadow content structurally separated (separate ScriptableObject types, shadow reachable only from Painting Scene) | LOW |

## GDD Requirements

| TR-ID | Requirement | ADR Coverage |
|-------|-------------|----------------|
| TR-villager-001 | All twelve villagers have distinct, standalone persona content | ❌ No ADR (content authoring, not architecture) |
| TR-villager-002 | Shadow content never appears in daytime dialogue | ADR-0002 ✅ |
| TR-villager-003 | Portrait stage state persists and gates reveal content | ADR-0002 ✅ (partial — stage-gating logic itself needs a story-level design pass) |
| TR-villager-004 | All twelve villagers are romanceable with functioning relationship hearts | ❌ No ADR (relationship-heart formula unspecified) |

## Definition of Done

This epic is complete when:
- All stories are implemented, reviewed, and closed via `/story-done`
- All acceptance criteria from `design/gdd/villager-archetype-system.md` are verified
- A content audit confirms zero shadow-tagged content is reachable from
  daytime dialogue (per TR-villager-002 and ADR-0002's validation criteria)
- **Vertical slice scope note**: only 3 of the 12 villagers are needed for
  the vertical slice — which 3, and whether their wheel positions are
  contiguous, is still an open decision owned by Non/Jack (see
  `design/gdd/game-concept.md` Open Questions). Story creation for this
  epic should wait on that decision, or scope the first 3 stories to
  placeholder villagers pending confirmation.

## Next Step

Run `/create-stories villager-archetype-system` to break this epic into
implementable stories, once the vertical-slice villager selection is
confirmed.
