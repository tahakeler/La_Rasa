# Unity CLI — Agent-Editor Integration

<!-- Written 2026-08-31. Setup section + local-status table added 2026-09-08.
Unity CLI is a NEW, EXPERIMENTAL feature (announced 2026-07-20 at Unite
Seoul) — it postdates this framework's Godot-era docs and the model's
training cutoff. Command syntax below has NOT been re-verified against a live
install in this repo (the CLI isn't installed yet). Re-verify against Unity's
live docs (https://docs.unity.com/en-us/unity-cli) before depending on it for
anything blocking. -->

## What it is

Unity CLI is a standalone terminal binary (`unity`) for managing Editor
installs, project auth, modules, and builds from the command line — separate
from, and newer than, the older standalone Unity MCP Server (which remains
supported). Paired with the `com.unity.pipeline` package (Unity 6.0+), it
opens a live bridge between a running Editor and scripts, CI pipelines, and
AI agents: observe project state, act on it, and verify the result, without
manual Inspector clicks.

**Status**: experimental. Free, no concurrency limits on the MCP server as of
launch.

## Core commands (verified against Unity's official docs, 2026-08-31)

```bash
unity --help                              # help
unity install lts                         # install latest LTS Editor
unity install 6000.3.7f1 -m ios android   # install a specific version + modules
unity editors -i                          # list installed Editors
unity open ./MyProject                    # open a project (or: unity ./MyProject)
unity shell                               # interactive session
unity doctor                              # diagnostics
unity auth login                          # browser-based sign-in
unity auth status                         # check auth status
unity upgrade                             # self-update the CLI
```

CI/automation auth (no browser):
```bash
export UNITY_SERVICE_ACCOUNT_ID="<key-id>"
export UNITY_SERVICE_ACCOUNT_SECRET="<secret>"
```

## Agent-Editor bridge (the actual "integrate agents into Unity" part)

Requires the `com.unity.pipeline` package on top of the CLI:

```bash
unity auth login
unity pipeline install              # installs the package + deps
unity pipeline list                 # verify installation
unity command                       # connect to a running Editor (auto-discovery)
unity command --project-path=<path> # or target a specific project
```

Once connected, an agent can call `eval` / `eval_file` to run C# live inside
the running Editor (no recompile/domain reload) and read back structured
JSON/TSV output plus standard exit codes — the mechanism that lets an agent
inspect scenes, read console output, edit scripts, and trigger Editor actions
from plain-language prompts.

An **MCP Mode** was added to the CLI in June 2026 for agents that speak MCP
directly (e.g. Claude Code via `/mcp`); the older standalone Unity MCP Server
setup still works if that's already wired up.

## System requirements

- Windows 10 (21H1+), Linux (RHEL 9 / Ubuntu 22.04+, glibc 2.34+), or macOS 14+
- Unity Editor 6.0+ for the `com.unity.pipeline` package

## Install

```bash
# macOS/Linux
curl -fsSL https://public-cdn.cloud.unity3d.com/hub/prod/cli/install.sh | bash
# macOS (Homebrew)
brew install --cask unity-cli
# Windows (winget)
winget install Unity.CLI
```

## Setup in this repo

A runbook and a read-only readiness check now live at `tools/unity-cli/`:

- `tools/unity-cli/README.md` — install → editor → auth → headless tests →
  the `com.unity.pipeline` live bridge → Claude Code MCP mode, step by step.
- `tools/unity-cli/check.sh` — reports what's installed / authenticated /
  missing. Changes nothing.
- `.mcp.json.example` (repo root) — copy to `.mcp.json` (git-ignored) to wire
  MCP mode into Claude Code.
- `tools/unity-cli/unity-tests.yml` — a `game-ci/unity-test-runner` workflow
  template, **not active** (it needs a Unity license secret + a verified
  project or it fails red). Copy it into `.github/workflows/` when ready —
  steps in `tools/unity-cli/README.md`. For now, run tests locally in the
  Editor's Test Runner.

### Local status — last checked 2026-09-08

| Piece | Status |
|-------|--------|
| `unity` CLI installed | **No** — the automated installer was blocked by a sandbox policy; install it manually per the runbook |
| 6.3 LTS editor | No |
| `unity auth` | No |
| `com.unity.pipeline` in project | No |
| MCP mode wired | No — `.mcp.json.example` provided, not activated |

Nothing here is blocking for the current work: the Twelve Content Framework
code is plain file edits reviewed the normal way. The CLI matters once
someone needs to compile/run the project (`S1-003`) or drive a live Editor.

## Which of our agents use this

- **`unity-specialist`** — primary owner; decides when a task actually needs
  live-Editor automation vs. normal file-based implementation.
- **`devops-engineer`** — CI/CD: headless installs, builds, and test runs in
  pipelines.
- **`tools-programmer`** — if we ever build custom editor tooling that scripts
  against a live Editor session rather than through normal `.cs` files.

Nobody should reach for this by default — most implementation work is still
plain file edits reviewed through the normal collaboration protocol. This is
for cases that specifically need to observe or drive a *running* Editor
(automated smoke tests against a live scene, CI builds, etc.).

## Sources

- [Announcing the Unity CLI: A new way to connect your tools and agents — Unity Discussions](https://discussions.unity.com/t/announcing-the-unity-cli-a-new-way-to-connect-your-tools-and-agents/1731104)
- [Meet the Unity CLI: manage Unity from your terminal](https://unity.com/blog/meet-the-unity-cli)
- [Use the Unity command-line interface (CLI) — official docs](https://docs.unity.com/en-us/unity-cli/use-unity-cli)
- [Unity MCP Server: Connect Claude Code, Cursor, and other AI Agents](https://unity.com/blog/unity-ai-mcp-how-to-get-started)
