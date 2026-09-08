# Unity CLI — setup & agent↔Editor bridge

> **Experimental.** The Unity CLI was announced 2026-07-20 (Unite Seoul) and
> is still moving fast. This runbook was last checked **2026-09-08**. Verify
> every command against <https://docs.unity.com/en-us/unity-cli> before
> depending on it for anything blocking.
>
> Background and the "which agents use this" policy: `.claude/docs/unity-cli-integration.md`.

## What this gives La Rasa

1. **Headless editor + test runs** — CI and local one-command test runs
   without opening the Editor GUI (`S1-003` verification, the `unity-tests`
   CI gate).
2. **Live agent↔Editor bridge** — with `com.unity.pipeline` installed, an
   agent (Claude Code via MCP) can inspect the open scene, read the Console,
   edit scripts, and trigger Editor actions against a *running* Editor. Used
   only for tasks that genuinely need a live Editor (automated smoke tests
   against a live scene, driving a repro) — not for normal file edits.

## 1. Install the CLI

Not yet installed in this repo's dev environment (the automated installer was
blocked by a sandbox policy on 2026-09-08). Install it yourself:

```bash
# macOS / Linux — official installer (verifies a SHA-256 checksum)
curl -fsSL https://public-cdn.cloud.unity3d.com/hub/prod/cli/install.sh | bash

# macOS (Homebrew)
brew install --cask unity-cli

# Windows (winget)
winget install Unity.CLI
```

macOS installs to `~/.unity/bin/unity` and appends a PATH line to your shell
rc file. Restart the shell, then:

```bash
unity --version
unity --help
```

`tools/unity-cli/check.sh` verifies the install and reports what's missing.

## 2. Install a 6.3 LTS Editor

```bash
unity install lts                 # latest LTS
unity editors -i                  # list what's installed
```

Match the version to `ProjectSettings/ProjectVersion.txt` (currently a
placeholder — set it to your installed 6.3 LTS patch and commit).

## 3. Authenticate

```bash
unity auth login          # interactive, browser-based
unity auth status
```

For CI / non-interactive (no browser) — set a Unity **service account**:

```bash
export UNITY_SERVICE_ACCOUNT_ID="<key-id>"
export UNITY_SERVICE_ACCOUNT_SECRET="<secret>"
```

Store these as GitHub Actions secrets, never in the repo.

## 4. Headless test run (local)

```bash
# from the repo root
unity open .                                  # first open generates Library/, meta files
unity command --project-path . \
  --exec 'TestRunnerApi.RunEditModeTests()'   # exact invocation: check `unity command --help`
```

The authoritative CI path is `game-ci/unity-test-runner` (see
`.github/workflows/unity-tests.yml`); the `unity` CLI route above is the
experimental local equivalent.

## 5. Live agent↔Editor bridge (`com.unity.pipeline`)

```bash
unity auth login
unity pipeline install            # adds com.unity.pipeline + deps to the project
unity pipeline list               # verify
unity command                     # connect to a running Editor (auto-discovery)
```

Once connected, `eval` / `eval_file` run C# live inside the Editor (no domain
reload) and return structured JSON + exit codes.

### MCP mode (Claude Code)

The CLI added an **MCP Mode** (June 2026) so MCP-speaking agents can connect
directly. Copy `.mcp.json.example` (repo root) to `.mcp.json`, adjust the
command to match your `unity` install path, and approve the server when
Claude Code prompts. The older standalone Unity MCP Server also still works if
you already have it wired.

`.mcp.json` is **git-ignored** — it's per-developer and points at a local
binary + a running Editor.

## System requirements

- macOS 14+, Windows 10 (21H1+), or Linux (Ubuntu 22.04+ / RHEL 9, glibc ≥ 2.34)
- Unity Editor 6.0+ for `com.unity.pipeline`
