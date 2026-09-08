# Changelog

All notable changes to La Rasa. Format based on
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/). This project is in
pre-production and not yet versioned.

## [Unreleased]

### Added — 2026-09-08 (Sprint 1 catch-up pass)

- **Unity project skeleton** at repo root (`Assets/`, `Packages/`,
  `ProjectSettings/`), Unity 6.3 LTS / URP 2D. Unverified — needs an Editor
  pass (`S1-003`).
- **Twelve Content Framework** (`Assets/LaRasa/Scripts/Core/TwelveContent/`):
  `ArchetypeWheel` ScriptableObject + `ArchetypePosition` + `Season` +
  `CircleOfFifths` per ADR-0001; Editor "Populate Default Wheel" tool; 27
  EditMode tests (drafted, not yet run). Three story files.
- **Unity CLI integration groundwork**: `tools/unity-cli/` setup runbook and
  readiness check; `.mcp.json.example`; `.github/workflows/unity-tests.yml`
  (game-ci) and `repo-validation.yml`.
- **`design/decisions/`**: option docs for the five human-owned blocked
  decisions (combat concept, slice villagers, audio approach, portrait decay,
  zone key mode). No decisions made.
- `assets/`, `tests/`, `tools/` directories with READMEs.
- `production/session-state/active.md` living checkpoint.

### Changed — 2026-09-08

- Top-level `README.md` rewritten for La Rasa (framework section retained).
- `.claude/docs/directory-structure.md` documents the standard Unity root
  layout, superseding the framework's `src/` convention.
- Sprint 1 task statuses and epics index updated to reflect the above.

### Notes

- A concurrent Codex desktop session generated `.codex/`, `.agents/`, and
  `AGENTS.md` in the working tree. These are excluded from this branch and
  left for that session to manage.
