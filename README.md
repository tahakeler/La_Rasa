<h1 align="center">La Rasa</h1>

<p align="center">
  A farming sim in the Stardew tradition, where the silent painter Rasa tends a
  valley of twelve Jungian-archetype villagers by day and, at night, reflects on
  whoever's story moved — uncovering their portrait, and the part of themselves
  they can't look at, one brushstroke at a time.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/engine-Unity%206.3%20LTS-000?logo=unity" alt="Unity 6.3 LTS">
  <img src="https://img.shields.io/badge/pipeline-URP%202D-2196F3" alt="URP 2D">
  <img src="https://img.shields.io/badge/language-C%23-239120?logo=csharp" alt="C#">
  <img src="https://img.shields.io/badge/stage-Pre--production-orange" alt="Pre-production">
  <img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="MIT License">
</p>

---

## Status

**Pre-production — Sprint 1** (`2026-09-01 → 09-14`). Milestone: a vertical
slice (1 season, 3 villagers/zones, 1 region) by end of semester.

| Area | Where it stands |
|------|-----------------|
| Game concept + 9 system GDDs | Drafted, ingested from GDD v1.3 (`design/gdd/`) |
| Architecture | ADR-0001 (twelve-content data), ADR-0002 (persona/shadow) accepted; `docs/architecture/` |
| Unity project | **Skeleton only, unverified** — needs an Editor pass (`S1-003`, see `Assets/LaRasa/README.md`) |
| Twelve Content Framework | Code + 27 EditMode tests drafted; 3 stories; blocked on `S1-003` |
| Painting-scene prototype | Core logic + tests written; **no playable build, no playtest yet** (`prototypes/painting-scene/`) |
| Blocked on named people | Combat concept, slice villagers, audio approach, 2 smaller calls — options in `design/decisions/` |

Run `/sprint-status` or `/project-stage-detect` in Claude Code for a live read.

## Getting started

### Prerequisites

- [Git](https://git-scm.com/) (with LFS, once art/audio land)
- **Unity 6.3 LTS** (`6000.3.x`) — via Unity Hub or the [Unity CLI](tools/unity-cli/README.md)
- [Claude Code](https://docs.anthropic.com/en/docs/claude-code) — the studio runs on the agent framework below
- Recommended: `jq` and Python 3 (for hook validation)

### First run

```bash
git clone https://github.com/tahakeler/La_Rasa.git
cd La_Rasa

# 1. Open the repo root as a project in Unity Hub (Assets/, Packages/,
#    ProjectSettings/ are at the root). Set ProjectSettings/ProjectVersion.txt
#    to your installed 6.3 LTS patch first.
# 2. Work through Assets/LaRasa/README.md — the S1-003 verification checklist.
# 3. Open a Claude Code session for design/production work:
claude
```

Then `/help` for what to do next, or jump to a skill (`/sprint-status`,
`/story-readiness`, `/dev-story`).

## Project structure

```
Assets/LaRasa/          Unity project — runtime code, tests, data, art, audio
  Scripts/Core/           TwelveContent/ (ArchetypeWheel, CircleOfFifths, …)
  Scripts/Editor/         authoring tools ("Populate Default Wheel")
  Tests/EditMode/         NUnit EditMode suites
Packages/ ProjectSettings/  Unity manifest + settings
design/
  gdd/                   9 system design docs (8-section format)
  decisions/             option docs for choices owned by named people
  registry/entities.yaml
docs/
  architecture/          ADRs, control manifest, TR registry
  engine-reference/unity/  version-pinned Unity 6.3 API notes
production/
  epics/                 one epic per architecture module + story files
  sprints/               sprint plans
  session-state/         active.md — the living checkpoint (gitignored)
prototypes/painting-scene/  the read-accuracy prototype (never migrated to src)
tools/unity-cli/         Unity CLI setup + agent↔Editor bridge (experimental)
.github/workflows/       unity-tests (game-ci) + repo-validation
.claude/                 the studio: 49 agents, 73 skills, 12 hooks, 11 rules
```

Full map: `.claude/docs/directory-structure.md`.

## How the team works

**Start here: [`docs/TEAM-HANDBOOK.md`](docs/TEAM-HANDBOOK.md)** — setup (per
role), how to talk to the agents, the command reference, the dev workflow,
Unity + Git rules, and where the game code lives. New teammates: the
15-minute version is [`docs/team-setup-guide.md`](docs/team-setup-guide.md);
your personalised page is in [`docs/onboarding/`](docs/onboarding/).

**User-driven, not autonomous.** Every task runs
**Question → Options → Decision → Draft → Approval**. Agents ask before writing
files; nothing merges without a person. See
`docs/COLLABORATIVE-DESIGN-PRINCIPLE.md`.

Development follows a 7-phase pipeline — concept → systems → architecture →
stories → implementation → QA → release — with director gates between phases.
`docs/WORKFLOW-GUIDE.md` walks it end to end.

### Design pillars

1. **Twelve is the structure, not the content** — every category is twelve, on one archetype wheel.
2. **Rasa never speaks** — she acts through gifts, gesture, and painting only.
3. **Persona and shadow are structurally separate** — daytime dialogue vs. the painting scene, never mixed.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md). Commits use Conventional Commits and
reference a story/task ID. Tests are a blocking CI gate — never skip them.

---

## Built on Claude Code Game Studios

The `.claude/` directory is the [Claude Code Game Studios](https://github.com/Donchitos/Claude-Code-Game-Studios)
template — a studio hierarchy of specialized AI agents (directors → leads →
specialists), slash-command skills for every phase, automated hooks, and
path-scoped coding rules. It turns a single Claude Code session into a
structured dev team that asks the right questions and keeps the project
organized. La Rasa adopts it with the Unity agent set.

- Agent roster: `.claude/docs/agent-roster.md`
- Skill catalog: `.claude/docs/skills-reference.md`
- Upgrading the template: [UPGRADING.md](UPGRADING.md)

## License

MIT — see [LICENSE](LICENSE).
