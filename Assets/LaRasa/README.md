# La Rasa — Unity Project

This is the Unity 6.3 LTS (URP, 2D) project for La Rasa. It sits at the repo
root in the standard Unity layout (`Assets/`, `Packages/`, `ProjectSettings/`)
so `game-ci` and the `unity` CLI work with zero extra configuration.

## ⚠️ Status: unverified skeleton

This skeleton was authored **without a running Unity Editor** (Sprint 1 task
`S1-003`). The C# is written to spec (ADR-0001) but has **not been compiled,
and the tests have not been run**. Before relying on it, someone with Unity
6.3 LTS installed must complete the checklist below.

## Editor verification checklist (`S1-003`)

- [ ] Open the repo root as a project in Unity Hub. Set the editor version in
      `ProjectSettings/ProjectVersion.txt` to your installed 6.3 LTS patch
      first (currently a placeholder `6000.3.0f1`).
- [ ] Let Package Manager resolve `Packages/manifest.json`. Confirm the pinned
      versions exist for your editor; bump any that don't and commit the
      resulting `Packages/packages-lock.json`.
- [ ] Confirm URP is the active render pipeline (Project Settings → Graphics /
      Quality). Create a URP asset if the 2D feature set didn't.
- [ ] Confirm the project compiles with no errors. `.meta` files will be
      generated on first import — commit them.
- [ ] Open **Window → General → Test Runner**, run the **EditMode** suite.
      All `LaRasa.Core.EditModeTests` tests must pass (13 in `CircleOfFifths`,
      14 in `ArchetypeWheel`).
- [ ] Create the canonical data asset: **Assets → Create → LaRasa → Archetype
      Wheel**, name it `ArchetypeWheel`, then **LaRasa → Twelve Content →
      Populate Default Wheel**. Confirm the console reports "Validation passed".
- [ ] Wire the CI workflow secrets (`UNITY_LICENSE`, `UNITY_EMAIL`,
      `UNITY_PASSWORD`) so `.github/workflows/unity-tests.yml` runs green.

Once every box is checked, update `production/sprints/sprint-1.md` (`S1-003`)
and this section.

## Layout

```
Assets/LaRasa/
  Scripts/
    Core/                       LaRasa.Core.asmdef  (runtime, all platforms)
      TwelveContent/
        Season.cs               four-season enum (no UnityEngine dependency)
        CircleOfFifths.cs       structurally-fixed key ordering + adjacency (pure C#)
        ArchetypePosition.cs    one wheel position (serializable, [SerializeField])
        ArchetypeWheel.cs       the single canonical ScriptableObject (ADR-0001)
    Editor/                     LaRasa.Core.Editor.asmdef  (Editor only)
      ArchetypeWheelDefaults.cs  "Populate Default Wheel" menu item + canonical placeholder data
  Tests/
    EditMode/                   LaRasa.Core.EditModeTests.asmdef
      TwelveContent/
        CircleOfFifthsTests.cs
        ArchetypeWheelTests.cs
```

## Where things go

- **Runtime game code** → `Assets/LaRasa/Scripts/<Layer>/` with an `.asmdef`
  per folder (Core, Gameplay, UI, Audio, …), per `unity-specialist` guidance.
- **Editor tooling** → `Assets/LaRasa/Scripts/Editor/` or a sibling `Editor/`
  folder inside a feature folder.
- **Tests** → `Assets/LaRasa/Tests/EditMode/` and `Assets/LaRasa/Tests/PlayMode/`.
  The framework's `tests/` convention (`tests/unit/<system>/`) is satisfied by
  these asmdef-scoped folders — see `tests/README.md`.
- **Data assets** (ScriptableObjects) → `Assets/LaRasa/Data/<system>/`.
- **Art / audio / prefabs** → `Assets/LaRasa/Art`, `/Audio`, `/Prefabs`.

## The painting-scene prototype

`prototypes/painting-scene/` stays **outside** this project by design
(`.claude/rules/prototype-code.md`: prototype code is never migrated, only
rewritten). Wiring its `ReadAccuracy.cs` into a playable scene is `S1-004`, a
separate task that needs the Editor.
