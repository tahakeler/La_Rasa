# Control Manifest — La Rasa

**Manifest Version**: 2026-08-31-v1

Flat, actionable rules for programmers. Derived from Accepted ADRs
(ADR-0001, ADR-0002), `.claude/docs/technical-preferences.md`, and
`.claude/docs/coding-standards.md`. This is the "what you must do / must
never do" sheet — read the ADRs for *why*.

---

## Data Architecture

**Required:**
- All twelve-count content (villagers, zones, tracks, flowers, enemies)
  references the single `ArchetypeWheel` ScriptableObject (ADR-0001) —
  never duplicates archetype/key/color/flower/season fields locally
- All gameplay values live in ScriptableObject data assets, not hardcoded
  constants (project-wide coding standard, reinforced by ADR-0001)
- Villager persona and shadow content are separate ScriptableObject types
  (ADR-0002) — `VillagerShadowData` is referenced ONLY by the Painting
  Scene system

**Forbidden:**
- Hardcoding archetype names, colors, keys, or flower names in code —
  always read from `ArchetypeWheel`
- Any reference from the Daytime Dialogue System (or any system other than
  the Painting Scene controller) to `VillagerShadowData` or its contents
- Querying portrait-stage content before its prerequisite stage is marked
  complete on save data

**Guardrails:**
- Add `OnValidate()` checks on villager/zone/track data assets to catch
  duplicate `ArchetypePosition` references at authoring time, not runtime
- If assembly-definition separation (ADR-0002) isn't in place yet, flag any
  PR touching shadow data in code review

---

## Unity Engine Conventions

(Inherited from `.claude/agents/unity-specialist.md` — repeated here for a
single flat reference.)

**Required:**
- ScriptableObjects for all data-driven content
- `[SerializeField] private` for inspector fields, never bare `public`
- Cache component references in `Awake()` — never `GetComponent<>()` in `Update()`
- Addressables for runtime asset loading — never `Resources.Load()`
- New Input System (not legacy `Input.GetKey()`) — relevant early, since the
  Painting Scene's twin-stick input is a first-class input requirement
- URP for rendering (per `technical-preferences.md`)

**Forbidden:**
- `Find()`, `FindObjectOfType()`, `SendMessage()` in production code
- Allocations in `Update()` or physics callbacks (hot paths)
- `DontDestroyOnLoad` as a default pattern — use proper scene management

---

## Testing

**Required:**
- Unity NUnit test pattern (`[TestFixture]` / `[Test]`), per
  `.claude/agents/qa-tester.md`
- Every formula-bearing GDD (Twelve Content Framework math, farm economy
  formulas once written, painting-scene read-accuracy formula once written)
  gets a corresponding unit test before being marked Done

**Forbidden:**
- Disabling or skipping failing tests to unblock CI (project-wide standard)

---

## Accessibility (Painting Scene specific)

**Required:**
- A single-input mode for the twin-stick painting mechanic (explicit GDD
  requirement, not optional)
- The painting scene must be completable using audio feedback alone (the
  GDD's own "playable by ear" design test)

**Guardrails:**
- Treat these as MVP acceptance criteria for the Painting Scene system, not
  a post-launch accessibility pass — they're named in the source GDD as
  core requirements, not stretch goals

---

## Open / Blocked Areas — No Manifest Rules Yet

These areas have no Accepted ADR and therefore no control-manifest rules.
Do not start implementation-level code here until the blocking decision
resolves and a corresponding ADR exists:

- **Combat system** — blocked on Luka/Oasis's combat-concept decision
  (see `design/gdd/regions-and-combat.md`)
- **Audio crossfade technical approach** (Unity native audio vs.
  middleware) — open question in `design/gdd/sound-and-crossfade.md`,
  needs `audio-director` + `technical-director` decision before an ADR
  can be written

---

## Manifest Maintenance

- Update this file's version stamp whenever a new ADR is Accepted that adds
  or changes a rule
- `/story-done` checks story-embedded manifest version against this file's
  current version and flags staleness
