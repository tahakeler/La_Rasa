# How We're Using the Claude Code Game Studio

The short version. Full detail: **`docs/TEAM-HANDBOOK.md`**.

## What it is

This repo ships with a framework of Claude Code subagents — a virtual studio.
Each agent is a specialist (game designer, gameplay programmer, Unity
specialist, art director, writer, QA, …) that only works in its own lane, so
no single context has to hold the whole game at once. You talk to it in plain
English; it routes the work, asks when it's unsure, and shows you a draft
before it writes anything. Nothing gets committed or pushed without someone
asking for it.

It **coordinates and drafts** the work. It does not run Unity — a person still
compiles, tests, and playtests.

## The game

**La Rasa** — a Stardew-tradition farming sim built in **Unity 6.3 LTS (URP,
2D, C#)**. The mute painter Rasa tends a valley of twelve Jungian-archetype
villagers by day and, at night, paints whoever's story moved — uncovering
their portrait and their shadow one session at a time. Everything is sized to
twelve, on one archetype wheel. Design pillars and the full concept:
`design/gdd/game-concept.md`.

## What we've customised from the stock framework

- **Engine locked**: Unity 6.3 LTS. The Godot and Unreal engine specialists
  were cut (roster: 49 → 39 agents). Engine-reference docs are pinned to
  Unity 6.3.
- **Adopted for La Rasa**: 9 system GDDs ingested, ADR-0001 / ADR-0002
  accepted, epics + Sprint 1 scaffolded, the painting-scene prototype started.
- **Added**: `tools/unity-cli/` (Unity CLI + agent↔Editor bridge, experimental),
  `.github/workflows/` CI, `design/decisions/` (option docs for open calls).

## Still open (owned by named people, not the tool)

Combat concept (Luka/Oasis), vertical-slice villager selection (Non/Jack),
audio technical approach (audio-director/tech-director), portrait lit-decay,
zone key mode (Ken). Options are drafted in `design/decisions/`; the tool
will not decide these.

## Roles → agents

`production/team-agent-mapping.md` is the per-person cheat sheet. Taha owns
`.claude/` (the agents); everyone else uses them via `git pull` + Claude Code.

## Getting started

`docs/TEAM-HANDBOOK.md` §2. Programmers also do the `Assets/LaRasa/README.md`
checklist. Non-programmers need only Git + Node + Claude Code.
