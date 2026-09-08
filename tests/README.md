# tests/

The framework's canonical test location. For La Rasa (Unity 6.3), the
**actual test code lives inside the Unity project** under assembly-definition
scopes, because Unity's Test Runner and `game-ci` discover tests by `.asmdef`,
not by directory:

| Framework convention | Where it actually lives (Unity) |
|----------------------|--------------------------------|
| `tests/unit/<system>/` | `Assets/LaRasa/Tests/EditMode/<System>/` (asmdef: `LaRasa.Core.EditModeTests`, …) |
| `tests/integration/<system>/` | `Assets/LaRasa/Tests/PlayMode/<System>/` or EditMode integration fixtures |
| `tests/performance/` | `Assets/LaRasa/Tests/Performance/` (Unity Performance Testing package) |
| `tests/playtest/` | `production/qa/evidence/` (manual playtest docs) |

## Current coverage

| System | EditMode tests | Status |
|--------|----------------|--------|
| Twelve Content Framework | `Assets/LaRasa/Tests/EditMode/TwelveContent/CircleOfFifthsTests.cs`, `ArchetypeWheelTests.cs` | Drafted — not yet run (needs Editor, `S1-003`) |

## Running the suite

- **Local**: Window → General → Test Runner → EditMode → Run All.
- **CI**: `.github/workflows/unity-tests.yml` runs `game-ci/unity-test-runner@v4`
  on every push and PR. It is a blocking gate (`.claude/docs/coding-standards.md`).
- **Headless via Unity CLI** (experimental): see
  `.claude/docs/unity-cli-integration.md` and `tools/unity-cli/`.

Story files under `production/epics/<epic>/` name the exact test file each
story must have green before `/story-done` can close it.
