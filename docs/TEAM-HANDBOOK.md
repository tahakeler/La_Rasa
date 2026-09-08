# La Rasa — Team Handbook

The one document that explains how we build this game. If you read one thing,
read this.

- **New to the tool?** → [Setup](#2-setup) then [Talking to the agents](#4-talking-to-the-agents)
- **Ready to work?** → [The development workflow](#6-the-development-workflow)
- **Not a programmer?** → [Who needs what](#3-who-needs-what) — you probably don't need Unity
- **Maintaining the agents (Taha)?** → [Maintaining the studio](#8-maintaining-the-studio)

Related docs: `docs/agent-studio-onepager.md` (short "what is this"),
`docs/WORKFLOW-GUIDE.md` (the 7-phase pipeline in depth),
`docs/COLLABORATIVE-DESIGN-PRINCIPLE.md` (the ask-before-acting rule),
`production/team-agent-mapping.md` (who uses which agents),
`Assets/LaRasa/README.md` (the Unity project setup checklist).

---

## 1. What this is

Two things live in this repo:

1. **The game** — La Rasa, a Unity 6.3 LTS farming/painting sim. Code, scenes,
   data, and (later) art/audio live under `Assets/LaRasa/`.
2. **The studio** — `.claude/`: a set of ~39 specialised AI agents, ~73
   slash-command "skills", validation hooks, and coding rules that run inside
   [Claude Code](https://docs.anthropic.com/en/docs/claude-code). It turns a
   Claude Code session into a structured dev team: directors who guard the
   vision, leads who own a domain, specialists who do the hands-on work.

**The studio coordinates and drafts the work. It does not run Unity.** It
writes `.cs` files, design docs, test files, sprint plans. A person still
opens Unity, compiles, runs the tests, and playtests. Every agent asks before
it writes a file and shows a draft before it finalises — nothing happens
without a human saying yes.

Taha owns `.claude/` (the agents). Everyone else uses them.

---

## 2. Setup

### 2.1 Everyone installs (≈15 min)

| Tool | Why | Get it |
|------|-----|--------|
| **Git** | version control; on Windows it also installs "Git Bash" which the studio's hooks need | [git-scm.com](https://git-scm.com/) — install with defaults |
| **Node.js LTS** | Claude Code is an npm package | [nodejs.org](https://nodejs.org/) — LTS |
| **Claude Code** | the tool the studio runs in | `npm install -g @anthropic-ai/claude-code` |
| **Anthropic access** | you need a seat / login | ask Taha |

Then clone the repo and start a session:

```bash
git clone https://github.com/tahakeler/La_Rasa.git
cd La_Rasa
claude
```

Claude Code reads `CLAUDE.md`, the agent roster, and the GDDs automatically —
you don't explain the project to it. If you use VS Code or Cursor, the Claude
Code extension puts the same thing inside your editor.

Verify your setup:

```bash
git --version      # should print a version
bash --version     # should print a version (Windows: from Git Bash)
node --version
claude --version
```

### 2.2 Programmers also install (≈30 min)

| Tool | Notes |
|------|-------|
| **Unity Hub** | [unity.com/download](https://unity.com/download) |
| **Unity 6.3 LTS** (`6000.3.x`) | install from Unity Hub → Installs → Install Editor → 6.3 LTS. Add the **WebGL** / **Windows**/**Mac** build support modules you need. |
| **A free Unity Personal license** | Unity Hub → sign in with a Unity ID → it activates Personal automatically. **Per-person, per-machine. No shared license, no seats, no cost.** Only switch to Plus/Pro if Unity's revenue terms ever require it. |
| **A C# IDE** | Rider (free for students) or Visual Studio — Unity Hub can install VS for you |

First time opening the project:

1. Set `ProjectSettings/ProjectVersion.txt` to your exact installed 6.3 patch.
2. Unity Hub → **Add** → pick the repo root folder → open.
3. Let Package Manager resolve packages. Commit the generated
   `Packages/packages-lock.json` **and** all generated `.meta` files.
4. Work through the checklist in `Assets/LaRasa/README.md` (the `S1-003` task).

### 2.3 Writers, artists, composers

You need **2.1 only**. See [Who needs what](#3-who-needs-what).

---

## 3. Who needs what

| Person | Role | Clone repo? | Unity? | Main tools |
|--------|------|-------------|--------|-----------|
| **Taha** | Framework/tooling, systems, 4 archetypes | ✅ | ✅ | Claude Code + Unity; owns `.claude/` |
| **Luka** | Core systems — crafting/menus, daily-cycle AI | ✅ | ✅ | Claude Code + Unity |
| **Oasis** | Core systems — combat concept, enemy/boss art | ✅ | ✅ | Claude Code + Unity |
| **Non** | Archetype division, persona/shadow writing | ✅ | Optional | Claude Code (edits `design/` + dialogue data). Unity only to see it in context. |
| **Jack** | Archetype division, one villager end-to-end | ✅ | Optional | Claude Code |
| **Lala** | Creative lead, twelve portraits, art direction | ✅ (for docs) | ❌ | Claude Code for art-bible / direction; paints in her own tools; hands PNGs to a programmer to import |
| **Grace** | Character-art pod lead, consistent aesthetic | ✅ (for docs) | ❌ | Claude Code for art-bible / specs; art delivered as files |
| **Ken** | OST — twelve tracks, circle-of-fifths keys | ✅ (for docs) | ❌ | Claude Code for keys/crossfade strategy; composes in a DAW; hands over audio files |

**Art & audio delivery:** put finished assets in `assets/` (authoring sources)
and hand exports to a programmer, who imports them into `Assets/LaRasa/Art` or
`/Audio` and commits them (with `.meta` files). Once binaries get large, we'll
turn on Git LFS — the patterns are pre-written in `.gitignore`.

---

## 4. Talking to the agents

**Just type what you want, in plain English.** The studio figures out which
specialist should handle it. Real examples:

- Luka: *"I want to implement the crafting menu — where's the design for that, and what does ADR-0001 say I have to follow?"*
- Oasis: *"help me work through the combat concept — walk me through the options doc and the trade-offs"*
- Non: *"I'm writing the Sage villager's persona dialogue. Remind me of their archetype, shadow, and what must never leak into daytime lines."*
- Ken: *"what key is zone 7 in, why is it fixed, and what are my options for major vs minor?"*
- Grace: *"do we have an art bible yet? If not, start one with me."*

It reads the relevant files, asks clarifying questions when something matters,
then **shows you a plan or a draft and asks before writing anything.** That's
a hard rule in `CLAUDE.md`, not a preference. If it ever writes or commits
without asking, that's a bug — tell Taha.

### Naming a specific agent

You don't have to, but you can: *"have `game-designer` review this,"* or
*"ask `unity-ui-specialist` how to structure this screen."* The full roster is
in `.claude/docs/agent-roster.md`; who-does-what is in
`production/team-agent-mapping.md`.

### If an answer feels wrong

If the tool makes a call that isn't its to make — inventing the combat
mechanic, dividing the twelve archetypes, picking a key mode — push back.
Those decisions belong to their owners (see `design/decisions/`). It should
have asked or routed to a different specialist.

---

## 5. Slash commands (skills)

Plain English always works. Slash commands are faster once you know them.
Type `/` in Claude Code to see all ~73. The ones this team needs now:

### Orientation
| Command | What it does |
|---------|-------------|
| `/help` | "what should I do next" — reads project state and suggests |
| `/sprint-status` | quick read on the current sprint |
| `/project-stage-detect` | full "where are we" analysis |
| `/onboard [your-role]` | generates a personalised onboarding doc |

### Design
| Command | What it does |
|---------|-------------|
| `/design-system [system]` | author a GDD section by section |
| `/quick-design` | a lightweight spec for a small change |
| `/design-review [file]` | sanity-check a design doc before it's "done" |
| `/consistency-check` | find contradictions across the GDDs |
| `/architecture-decision` | record a technical decision as an ADR |

### Building a feature
| Command | What it does |
|---------|-------------|
| `/create-stories [epic]` | break an epic into implementable stories |
| `/story-readiness [story]` | is this story actually ready to start? |
| `/dev-story [story]` | pick up a story and implement it (routes to the right programmer agent) |
| `/story-done [story]` | end-of-story review; checks acceptance criteria and tests |
| `/code-review` | review the current diff for bugs and cleanups |

### Content
| Command | What it does |
|---------|-------------|
| `/art-bible` | author the visual identity spec |
| `/asset-spec` | per-asset specs + AI generation prompts |
| `/team-narrative` | coordinate narrative-director + writer + world-builder |
| `/team-ui` | coordinate the full UX → visual → implementation pipeline |

### QA
| Command | What it does |
|---------|-------------|
| `/qa-plan` | test plan for a sprint or feature |
| `/smoke-check` | the critical-path gate before QA hand-off |
| `/balance-check` | check economy/progression data for outliers |

Don't memorise these — ask in plain English and the tool points you at the
right one.

---

## 6. The development workflow

We follow a 7-phase pipeline (concept → systems → architecture → stories →
implementation → QA → release) with director gates between phases. Full
detail: `docs/WORKFLOW-GUIDE.md`. Day to day, a programmer's loop is:

```
git checkout main && git pull                 # start from latest
/sprint-status                                # what's open
/story-readiness production/epics/<epic>/story-00X-*.md
git checkout -b feat/<epic>-<short-slug>      # ALWAYS branch first
/dev-story production/epics/<epic>/story-00X-*.md
  → agent reads story + GDD + ADR + control manifest
  → proposes an approach, you approve
  → writes code + test, you approve each file
open Unity → run the EditMode tests → confirm green
/story-done production/epics/<epic>/story-00X-*.md
git add <explicit paths> && git commit        # Conventional Commits, ref the story ID
git push -u origin feat/<epic>-<short-slug>
open a Pull Request → review → merge to main
```

### Ground rules

- **Branch first, always.** Never edit on `main` directly. The tool will
  usually offer to branch; if it doesn't, ask.
- **`main` is protected and always shippable.** Changes reach it through PRs.
- **No commits or pushes unless you ask for them.** The tool will not quietly
  push your half-finished work.
- **Nothing gets decided that isn't yours to decide.** Open decisions live in
  `design/decisions/` with a named owner and a blank `## Decision` section.
- **Tests are a blocking gate.** Never skip or disable a failing test — fix
  the cause. CI (`.github/workflows/`) runs on every push and PR.
- **Commit messages:** Conventional Commits (`feat:`, `fix:`, `docs:`, …) and
  reference the story/task ID in the body (`Story: TWELVE-001`).

---

## 7. Unity + Git rules

Unity and Git need a few conventions or references break for everyone:

- **Always commit `.meta` files.** Unity creates one per asset/folder; it holds
  the GUID other files reference. A missing `.meta` = broken references on
  every other machine. If you add a file in the OS, let Unity import it and
  commit the `.meta` it generates.
- **`Library/`, `Temp/`, `Logs/`, `obj/`, `*.csproj`, `*.sln` are ignored** —
  they regenerate. Don't force-add them.
- **Commit `Packages/packages-lock.json`** — it pins exact package versions so
  everyone resolves the same.
- **One person per scene / prefab at a time.** Unity scene/prefab YAML merges
  badly. Coordinate. If a conflict happens, use Unity's *Smart Merge*
  (`UnityYAMLMerge`) — configure it in your global `.gitconfig` (Unity docs:
  "Smart merge").
- **Large binaries → Git LFS.** When art/audio land, we enable the LFS
  patterns already staged in `.gitignore`. Don't commit multi-MB PSDs/WAVs to
  plain Git.
- **`.mcp.json` is per-developer and git-ignored.** Copy `.mcp.json.example`
  if you set up the experimental Unity MCP bridge (`tools/unity-cli/`).

---

## 8. Maintaining the studio (Taha)

### Where the studio lives

```
.claude/
  agents/       39 agent definitions (markdown + YAML frontmatter)
  skills/       73 slash commands (one folder each, SKILL.md)
  hooks/        12 bash scripts (session lifecycle, commit/push validation)
  rules/        11 path-scoped coding-standard files
  docs/         agent roster, coordination map, templates, this framework's own docs
  settings.json hooks + permission allow/deny lists
```

`.claude/settings.local.json` and `CLAUDE.local.md` are **git-ignored** —
per-developer permission overrides and private notes. Never commit them.

### Changing an agent

1. `git checkout -b studio/<what-changed>`
2. Edit `.claude/agents/<agent>.md` (or a skill's `SKILL.md`).
3. Run `/skill-test <skill>` after any skill change (a hook reminds you).
4. Commit, push, PR. In the PR description say what behaviour changed and who
   it affects.
5. Announce in the team channel: *"pulled? `unity-specialist` now does X."*

Teammates get the change with `git pull` — the studio is just files in the
repo. There is no separate install or distribution step.

### Adding a project-specific agent

If La Rasa needs a specialist the roster doesn't have (e.g. a
`painting-system-specialist` once that mechanic's shape is known):

1. Copy the closest existing agent file as a starting point.
2. Keep the frontmatter shape (`name`, `description`, `tools`, `model`,
   `maxTurns`) and the Collaboration Protocol section.
3. Add it to `.claude/docs/agent-roster.md` and
   `production/team-agent-mapping.md`.
4. Pick a model tier per `.claude/docs/coordination-rules.md` (Haiku for
   read-only, Sonnet default, Opus for heavy synthesis).

### Tuning without editing agents

Most project knowledge belongs in **`CLAUDE.md`** and the docs it `@`-includes
(`.claude/docs/technical-preferences.md`, `coding-standards.md`,
`coordination-rules.md`), not in individual agent prompts. Change it once
there and every agent picks it up.

### Keeping the studio current

`UPGRADING.md` tracks upstream template changes. `docs/agent-studio-onepager.md`
records what we've customised (cut the Godot/Unreal specialists, pinned Unity
6.3, etc.). Update it when you change the roster.

---

## 9. Where does the game code live? (repo structure)

**We use one repo: the game and the studio together (this repo).** Here is
why, and what the alternatives would cost.

### Option A — one repo (what we do)

`.claude/` and `Assets/` in the same GitHub repo.

- ✅ One clone, one `git pull`. Non-programmers deal with exactly one thing.
- ✅ The agents are always in sync with the code they help write. `/dev-story`,
  the hooks, the path-scoped rules, and `CLAUDE.md` all "just work" because
  Claude Code reads them from the repo root.
- ✅ It's exactly how the framework (Claude Code Game Studios) is designed to
  be used — you clone the template *per project*.
- ⚠️ Studio changes and game changes share one commit history. Cosmetic.
- ⚠️ The repo grows once art/audio land — mitigated by Git LFS.

### Option B — two repos, studio pulled in as a git submodule

Studio in its own repo; the game repo references it as a submodule at
`.claude/`.

- ✅ Clean separation; the studio could be reused on another game by version.
- ❌ Submodules are a real friction tax — `clone --recursive`, `submodule
  update`, detached-HEAD confusion. Rough for Lala / Ken / Non / Jack / Grace.
- ❌ Cross-cutting changes need two PRs.
- ❌ Project context (`CLAUDE.md`, GDDs, `production/` state) still has to live
  in the game repo anyway, so the split is only partial.

**Not worth it for an 8-person semester team.**

### Option C — two fully separate repos, copy `.claude/` by hand

- ❌ Guaranteed drift. Don't.

### Option D — package the studio as a Claude Code plugin

Claude Code supports plugins: agents + skills + hooks can be published to a
marketplace repo and installed with `claude plugin install`, updated with
`claude plugin update`.

- ✅ Genuinely decoupled. The game repo stays clean; teammates run one update
  command.
- ⚠️ More up-front work, and it's a newer mechanism.
- ⚠️ You still split: reusable studio bits → the plugin; project-specific
  context (`CLAUDE.md`, GDDs, `production/`, the path-scoped rules that point
  at *our* directories) → the game repo.

**When to revisit:** if we start a **second** game and want the same studio
there. Extract `.claude/` into a plugin then. Not now — the one-repo setup is
the least friction for shipping La Rasa this semester.

### The bottom line

The game **does not** need its own repo. Clone this one, and everyone —
programmer, writer, artist, composer — is working against the same agents,
the same design docs, and the same source of truth.

---

## 10. Troubleshooting

| Symptom | Fix |
|---------|-----|
| `claude: command not found` | `npm install -g @anthropic-ai/claude-code`; if `npm` is missing, install Node.js LTS first |
| `bash: command not found` (Windows) | reinstall Git, don't uncheck anything — you need Git Bash on PATH |
| The tool did something without asking | that's a bug — tell Taha; it violates `CLAUDE.md` |
| Unity: "the type or namespace X could not be found" after pull | someone didn't commit a `.meta` file or `packages-lock.json` — check with them; reimport (`Assets → Reimport All`) |
| Scene won't merge | Unity Smart Merge (`UnityYAMLMerge`); worst case, one person redoes their scene change |
| CI is red on `unity-tests` | expected until the `UNITY_LICENSE` / `UNITY_EMAIL` / `UNITY_PASSWORD` secrets are set (Taha) — see `tools/unity-cli/README.md` |
| "which agent / command do I use?" | just ask the tool in plain English |

Still stuck: ask Taha, or ask Claude Code itself (*"this isn't working, X is
happening"*) — it's good at diagnosing its own setup.
