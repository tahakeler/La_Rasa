# tools/

Build and pipeline tooling. Per `.claude/docs/directory-structure.md`:
`tools/ci`, `tools/build`, `tools/asset-pipeline`.

| Path | Purpose |
|------|---------|
| `tools/unity-cli/` | Unity CLI setup + the agent↔Editor bridge (experimental). See its README. |
| `tools/ci/` | CI helper scripts (empty — CI lives in `.github/workflows/`). |
| `tools/build/` | Build scripts (empty — add `unity build` wrappers here as platforms are chosen). |

Editor extensions and in-Editor tooling do **not** go here — they live inside
the Unity project at `Assets/LaRasa/Scripts/Editor/` so Unity compiles them.
`tools/` is for things that run *outside* a Unity session (shell/Python/CI).
