#!/usr/bin/env bash
# tools/unity-cli/check.sh
# Reports whether the Unity CLI + agent↔Editor bridge are ready to use.
# Read-only: installs nothing, changes nothing. Exit 0 = all green.
#
# See tools/unity-cli/README.md for how to install/fix anything this flags.

set -uo pipefail

green() { printf '  \033[0;32m✔\033[0m  %s\n' "$1"; }
red()   { printf '  \033[0;31mx\033[0m  %s\n' "$1"; }
warn()  { printf '  \033[1;33m•\033[0m  %s\n' "$1"; }

fail=0

echo ""
echo "Unity CLI readiness check"
echo ""

# 1. unity binary
if command -v unity >/dev/null 2>&1; then
  green "unity CLI on PATH: $(command -v unity)"
  if ver=$(unity --version 2>/dev/null | head -n1); then
    green "unity --version: ${ver}"
  else
    warn "'unity --version' did not report cleanly — check the install"
  fi
else
  red "unity CLI not found on PATH"
  warn "install: curl -fsSL https://public-cdn.cloud.unity3d.com/hub/prod/cli/install.sh | bash"
  fail=1
fi

# 2. installed editors
if command -v unity >/dev/null 2>&1; then
  if unity editors -i >/dev/null 2>&1; then
    editors=$(unity editors -i 2>/dev/null | grep -cE '6000\.3\.' || true)
    if [ "${editors:-0}" -gt 0 ]; then
      green "a Unity 6000.3.x (6.3 LTS) editor is installed"
    else
      warn "no 6.3 LTS editor detected — run: unity install lts"
    fi
  fi
fi

# 3. auth
if command -v unity >/dev/null 2>&1; then
  if unity auth status >/dev/null 2>&1; then
    green "unity auth: signed in"
  elif [ -n "${UNITY_SERVICE_ACCOUNT_ID:-}" ] && [ -n "${UNITY_SERVICE_ACCOUNT_SECRET:-}" ]; then
    green "unity auth: service-account env vars set (CI mode)"
  else
    warn "not authenticated — run: unity auth login  (or set UNITY_SERVICE_ACCOUNT_ID/SECRET)"
  fi
fi

# 4. project version pin
if [ -f ProjectSettings/ProjectVersion.txt ]; then
  pv=$(grep -E '^m_EditorVersion:' ProjectSettings/ProjectVersion.txt | awk '{print $2}')
  case "$pv" in
    6000.3.0f1) warn "ProjectVersion.txt is still the placeholder ${pv} — set it to your installed patch" ;;
    6000.3.*)   green "ProjectVersion.txt pinned to ${pv}" ;;
    *)          warn "ProjectVersion.txt is ${pv} — expected a 6000.3.x (6.3 LTS) version" ;;
  esac
else
  red "ProjectSettings/ProjectVersion.txt missing"
  fail=1
fi

# 5. pipeline package (live bridge)
if command -v unity >/dev/null 2>&1; then
  if unity pipeline list >/dev/null 2>&1; then
    green "com.unity.pipeline present (live agent↔Editor bridge available)"
  else
    warn "com.unity.pipeline not installed — run: unity pipeline install  (optional; only for live-Editor tasks)"
  fi
fi

# 6. MCP config
if [ -f .mcp.json ]; then
  green ".mcp.json present"
elif [ -f .mcp.json.example ]; then
  warn "no .mcp.json — copy .mcp.json.example and adjust it for MCP mode (optional)"
fi

echo ""
if [ "$fail" -eq 0 ]; then
  echo "Core CLI checks passed. Warnings above are optional / per-developer."
else
  echo "Something required is missing — see tools/unity-cli/README.md."
fi
exit "$fail"
