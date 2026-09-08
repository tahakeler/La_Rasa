# assets/

The framework's canonical location for source/authoring assets and data
files. For La Rasa (Unity), **runtime assets live inside the Unity project**
at `Assets/LaRasa/` (Unity requires them under `Assets/`). This top-level
`assets/` folder is for **pre-import authoring sources** that should not ship:

| Put here | Not here |
|----------|----------|
| Layered art sources (`.psd`, `.kra`, `.aseprite`) | Exported sprites → `Assets/LaRasa/Art/` |
| DAW project files, stems, reference tracks | Final audio → `Assets/LaRasa/Audio/` |
| Reference boards, palette studies | — |
| Data spreadsheets before they become ScriptableObjects | ScriptableObject assets → `Assets/LaRasa/Data/` |

Naming and structure are validated by `.claude/hooks/validate-assets.sh` on
Write/Edit. See `.claude/rules/data-files.md` for the data-file conventions.

Large binaries should go through Git LFS — see the commented block at the
bottom of `.gitignore` and enable the patterns your art/audio pipeline needs.
