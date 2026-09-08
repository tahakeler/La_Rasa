# Directory Structure

```text
/
├── CLAUDE.md                    # Master configuration
├── .claude/                     # Agent definitions, skills, hooks, rules, docs
├── Assets/                      # Unity project — all runtime game code, assets, scenes
│   └── LaRasa/                  #   Scripts/{Core,Gameplay,UI,Audio}, Tests/, Data/, Art/, Audio/
├── Packages/                    # Unity package manifest + lock file
├── ProjectSettings/             # Unity project settings (pinned editor version, URP, input)
├── src/                         # (framework default — unused for Unity; code lives in Assets/)
├── assets/                      # Pre-import authoring sources only (PSDs, DAW files) — NOT shipped
├── design/                      # Game design documents (gdd, decisions, narrative, levels)
│   └── decisions/               # Decision option docs for choices owned by named humans
├── docs/                        # Technical documentation (architecture, api, postmortems)
│   └── engine-reference/        # Curated engine API snapshots (version-pinned)
├── tests/                       # Points at Assets/LaRasa/Tests/ (see tests/README.md)
├── tools/                       # Build/pipeline tools that run OUTSIDE a Unity session
│   └── unity-cli/               # Unity CLI setup runbook + readiness check
├── prototypes/                  # Throwaway prototypes (isolated from Assets/)
└── production/                  # Production management (sprints, milestones, epics, releases)
    ├── session-state/           # Ephemeral session state (active.md — gitignored)
    └── session-logs/            # Session audit trail (gitignored)
```

## Unity layout note

This project uses the **standard Unity layout at the repo root** (`Assets/`,
`Packages/`, `ProjectSettings/`) so `game-ci` and the `unity` CLI work with no
extra configuration. The framework's Godot-era `src/` / `assets/` convention
is superseded:

| Framework doc says | For La Rasa (Unity) it means |
|--------------------|------------------------------|
| `src/gameplay/**`  | `Assets/LaRasa/Scripts/Gameplay/` (asmdef-scoped) |
| `src/core/**`      | `Assets/LaRasa/Scripts/Core/` |
| `src/ui/**`        | `Assets/LaRasa/Scripts/UI/` |
| `assets/` (runtime)| `Assets/LaRasa/Art`, `/Audio`, `/Data`, `/Prefabs` |
| `tests/unit/**`    | `Assets/LaRasa/Tests/EditMode/` |

The path-scoped rules in `.claude/rules/` still describe the right *standards*
for each layer — apply them by layer, not by literal path prefix.
