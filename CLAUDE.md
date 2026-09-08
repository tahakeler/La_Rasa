# Claude Code Game Studios -- Game Studio Agent Architecture

Indie game development managed through 39 coordinated Claude Code subagents.
Each agent owns a specific domain, enforcing separation of concerns and quality.

## Technology Stack

- **Engine**: Unity 6.3 LTS
- **Language**: C#
- **Version Control**: Git with trunk-based development
- **Build System**: Unity Build Pipeline
- **Asset Pipeline**: Unity Asset Import Pipeline + Addressables

> **Note**: Engine-specialist agents are Unity-only: `unity-specialist` plus
> `unity-dots-specialist`, `unity-shader-specialist`, `unity-addressables-specialist`,
> `unity-ui-specialist`.

## Agent-Editor Integration (Unity CLI)

Unity announced the **Unity CLI** on 2026-07-20 (Unite Seoul) — a standalone
terminal binary (`unity`) for managing Editor installs, projects, builds, and
auth, paired with the `com.unity.pipeline` package (Unity 6.0+) that opens a
live bridge from a running Editor to scripts, CI, and AI agents (`unity
command`, exposing `eval`/`eval_file` over a local HTTP API). It also added an
**MCP Mode** so agents like Claude Code can inspect scenes, read console
output, edit scripts, and trigger Editor actions directly.

**Status: experimental, launched ~6 weeks ago as of this writing.** Treat it
as an emerging capability, not a settled pipeline — verify current behavior
against the live docs before relying on it for anything blocking. See
`.claude/docs/unity-cli-integration.md` for setup and which agents use it.

## Project Structure

@.claude/docs/directory-structure.md

## Engine Version Reference

@docs/engine-reference/unity/VERSION.md

## Technical Preferences

@.claude/docs/technical-preferences.md

## Coordination Rules

@.claude/docs/coordination-rules.md

## Collaboration Protocol

**User-driven collaboration, not autonomous execution.**
Every task follows: **Question -> Options -> Decision -> Draft -> Approval**

- Agents MUST ask "May I write this to [filepath]?" before using Write/Edit tools
- Agents MUST show drafts or summaries before requesting approval
- Multi-file changes require explicit approval for the full changeset
- No commits without user instruction

See `docs/COLLABORATIVE-DESIGN-PRINCIPLE.md` for full protocol and examples.

> **Team members:** `docs/TEAM-HANDBOOK.md` is the entry point — setup per role,
> how to use the agents and slash commands, the dev workflow, Unity + Git
> rules, and the repo-structure rationale. Per-person pages: `docs/onboarding/`.

> **First session?** If the project has no engine configured and no game concept,
> run `/start` to begin the guided onboarding flow.

## Coding Standards

@.claude/docs/coding-standards.md

## Context Management

@.claude/docs/context-management.md
