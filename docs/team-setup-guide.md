# Getting Claude Code Running (For Everyone on the Team)

This is the "ok but how do I actually use this thing" doc — the 15-minute
version. The one-pager (`docs/agent-studio-onepager.md`) explains *what* this
tool is; **`docs/TEAM-HANDBOOK.md`** is the full reference (roles, Unity
setup, the dev workflow, Unity+Git rules, repo structure, troubleshooting).
This doc is just the steps to get it on your machine and start typing at it.

If you're not a programmer (Lala, Non, Jack, Grace, Ken) — don't worry,
this isn't a coding tool you write code in. You just type what you want in
plain English and it does the work, asks you questions when it's unsure,
and shows you what it's about to do before it does it. More on that below.

## 1. Install the stuff

You need two things installed:

**Git** — if you don't have this yet: [git-scm.com](https://git-scm.com/).
On Windows this also gives you "Git Bash," which Claude Code needs to run
some of its checks. Just install with the defaults, you don't need to
configure anything weird.

**Claude Code** — this is the actual tool. Open a terminal (Git Bash on
Windows, Terminal on Mac) and run:

```
npm install -g @anthropic-ai/claude-code
```

If `npm` isn't a recognized command, you need Node.js first —
[nodejs.org](https://nodejs.org/), grab the LTS version, then re-run the
line above.

You'll also need to be signed in / have access on Anthropic's side — ask
Taha if you're not sure whether you're set up for that part.

## 2. Get the repo and open it

Clone the repo (or if you already have it from GitHub Desktop or whatever,
just make sure you're pulled up to date). Then from a terminal, `cd` into
the project folder and run:

```
claude
```

That drops you into a Claude Code session sitting inside the project. It
already knows about the game (`CLAUDE.md`), the agent roster, our GDDs, all
of it — you don't need to explain the project to it, it reads the repo.

If you use VS Code, there's also a Claude Code extension that puts this
inside your editor instead of a separate terminal window — same thing,
just more convenient if you're already living in VS Code. Cursor works too.

### Programmers (Taha, Luka, Oasis): also install Unity

You additionally need **Unity Hub** + **Unity 6.3 LTS** (`6000.3.x`) and a
free **Unity Personal** license (Unity Hub activates it when you sign in with
a Unity ID — it's per-person, per-machine, no cost). Then open the repo root
as a project in Unity Hub and work through `Assets/LaRasa/README.md`. Details
in the handbook §2.2.

Writers (Non, Jack) can skip Unity for now — you mostly touch `design/` and
dialogue data, which Claude Code edits without opening the Editor. Art/audio
(Lala, Grace, Ken) don't need Unity at all — you deliver files and a
programmer imports them.

## 3. How you actually talk to it

Just type what you want, like you're asking a person. Some real examples
for people on this team:

- Luka: *"I want to implement the crafting menu, where's the design for
  that?"*
- Oasis: *"help me think through the combat concept, paint vs weapon"*
- Non: *"I'm writing the Sage villager's persona dialogue, what's their
  archetype/shadow again?"*
- Grace: *"what's our art direction doc situation, do we have one yet?"*
- Ken: *"what key is zone 7 in and why"*

It'll go read the relevant files, might ask you a clarifying question or
two (it's genuinely good about not guessing on stuff that matters), and
then show you what it's about to do — a draft, a plan, a list of files —
before actually doing it. **It will not just go write/change files without
telling you first and getting a yes.** That's a hard rule baked into
`CLAUDE.md`, not just a suggestion, so if it ever does something without
asking, that's a bug, tell Taha.

### Slash commands (shortcuts for common stuff)

You don't need these, plain English works fine, but they're faster once
you know them. A few actually useful ones for us right now:

| Type this | What happens |
|---|---|
| `/help` | "what should I do next" — reads where we are and suggests it |
| `/story-readiness [story]` | checks if a task is actually ready to start |
| `/dev-story [story]` | picks up a story and implements it |
| `/design-review [file]` | sanity-checks a design doc before it's "done" |
| `/sprint-status` | quick read on how the current sprint is going |

Full list is in `.claude/docs/quick-start.md` if you're curious, but
honestly don't memorize it, just ask in plain English and it'll point you
at the right command if there is one.

## 4. Where stuff actually lives

Quick map so you're not hunting around:

- `design/gdd/` — the actual design docs, one file per system (the daily
  loop, the painting scene, villagers, etc.) Start here if you want to
  know how something is *supposed* to work.
- `design/decisions/` — the calls that are still open, with a named owner.
- `docs/architecture/` — the technical decisions (ADRs) for how things get
  built in Unity. More Taha/Luka/Oasis territory.
- `Assets/LaRasa/` — the actual Unity project: code, tests, and (later) art,
  audio, scenes, data.
- `production/epics/` and `production/sprints/` — what's being worked on
  and in what order.
- `production/team-agent-mapping.md` — who's doing what, and which "agent"
  (specialist) to reach for when you're doing your part. Worth a look,
  it's basically a cheat sheet with your name on it.

## 5. The one thing worth understanding: "agents"

When you ask for something, Claude Code doesn't just wing it — it hands
the work to a specialist that only knows its own lane. Ask for a shader,
you get `unity-shader-specialist`. Ask for dialogue, you get `writer` and
`narrative-director`. You don't have to pick these yourself, it figures out
who's right for the job, but if you want to be specific you can just say
the name, e.g. *"have game-designer look at this."*

Why this matters for you: if an answer feels off — like it's making a
design call that isn't its call to make — that's usually a sign it should've
asked you or routed to a different specialist. Push back, it'll adjust.

## 6. A couple of ground rules we're actually following

- **It asks before it writes.** Every time. If you don't like what it's
  about to do, say so before you approve, not after.
- **Nothing gets decided that isn't yours to decide.** E.g. it won't
  invent the combat mechanic on Luka/Oasis's behalf, or divide up the
  twelve archetypes for Non/Jack — those stay open until the actual owner
  makes the call.
- **We work on branches, not directly on `main`.** If you're doing
  anything that touches files, ask it to create a branch first (or it'll
  usually offer to). Keeps `main` clean.
- **No commits or pushes without someone actually asking for it.** It
  won't quietly push your half-finished work to GitHub.

## If something's broken

Run the verification check:

```
git --version
bash --version
```

Both should print a version. If `bash` doesn't work on Windows, Git Bash
probably isn't on your PATH — reinstall Git and make sure you don't
uncheck anything during setup.

Beyond that — just ask Taha, or honestly, just ask Claude Code itself
("this isn't working, X is happening") — it's usually pretty good at
figuring out what's wrong with its own setup too.

The handbook (`docs/TEAM-HANDBOOK.md` §10) has a fuller troubleshooting
table, including Unity + Git issues (`.meta` files, scene merges, CI).
