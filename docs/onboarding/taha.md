# Onboarding — Taha

**Role**: Framework/tooling owner · systems support · 4 archetypes (with Non/Jack)

## What you own

- **`.claude/`** — the whole studio. You review every PR that touches
  `.claude/agents/`, `.claude/skills/`, `.claude/hooks/`, `.claude/rules/`,
  `CLAUDE.md`, or `.claude/docs/`.
- The Unity project's technical setup (`S1-003`), CI, and `tools/unity-cli/`.
- Keeping `docs/agent-studio-onepager.md` honest about what we've customised.

## Your agents

| For | Agent |
|-----|-------|
| Framework/roster decisions, engine/tech choices | `technical-director` |
| Scheduling, coordination, risk | `producer` |
| Turning a design into code structure | `lead-programmer` |
| Unity-specific patterns, packages, project settings | `unity-specialist` |
| CI / build / branching | `devops-engineer` |
| Custom editor tooling, the Unity CLI bridge | `tools-programmer` |

## On your plate now (Sprint 1)

1. **`S1-003`** — do the Unity Editor verification pass:
   `Assets/LaRasa/README.md` checklist. This unblocks Luka, Oasis, and the
   Twelve Content Framework tests.
2. Get the team onto Claude Code seats + walk them through
   `docs/TEAM-HANDBOOK.md`.
3. *(optional, later)* Enable Unity CI — copy `tools/unity-cli/unity-tests.yml`
   into `.github/workflows/` and add the license secrets. Skip until the
   project is Editor-verified; a red CI on every push helps no one.
4. Decide `network-programmer`'s fate (co-op or not — `game-concept.md` says
   single-player; flag it in an ADR if that's final so the agent goes dormant).

## First steps

1. Read `docs/TEAM-HANDBOOK.md` end to end — it's the doc you'll point
   everyone else at.
2. `git log --oneline -15` — see the two catch-up passes.
3. Do `S1-003`, then run the EditMode tests and mark stories 001/002 done via
   `/story-done`.

## Watch out for

- A concurrent Codex desktop session drops `.codex/`, `.agents/`, `AGENTS.md`
  in the working tree. They're in `.git/info/exclude`. Stage with explicit
  `git add <paths>`, never `git add -A`.
- `.claude/settings.local.json` and `CLAUDE.local.md` are git-ignored — never
  commit them.
