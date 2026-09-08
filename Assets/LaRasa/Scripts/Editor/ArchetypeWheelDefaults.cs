using LaRasa.Core.TwelveContent;
using UnityEditor;
using UnityEngine;

namespace LaRasa.Core.Editor
{
    /// <summary>
    /// Authoring helper: fills a selected <see cref="ArchetypeWheel"/> asset
    /// with the twelve placeholder positions from
    /// <c>design/gdd/twelve-content-framework.md</c>.
    ///
    /// The key column and colour column are structurally fixed (circle of
    /// fifths / colour wheel). The archetype, shadow, flower and season columns
    /// are placeholders the GDD marks "all editable" — this just seeds them so
    /// dependent content authoring has something to reference. Non/Jack's
    /// archetype-division work is expected to rename them.
    /// </summary>
    public static class ArchetypeWheelDefaults
    {
        // Colour-wheel hex values, one per wheel position, matching the colour
        // NAMES in the GDD table (Yellow, Yellow-Green, Green, ...). Structurally
        // fixed — a twelve-hue wheel in wheel order.
        private static readonly (string archetype, string shadow, string flower, Season season, string colorHex)[] Table =
        {
            ("Innocent",  "Denial",         "Daffodil",         Season.Spring, "#F4D03F"),
            ("Jester",    "Cruelty",        "Nasturtium",       Season.Summer, "#AACC00"),
            ("Caregiver", "Martyrdom",      "Lady's Mantle",    Season.Autumn, "#4CAF50"),
            ("Explorer",  "Rootlessness",   "Sea Holly",        Season.Winter, "#009E8E"),
            ("Sage",      "Dogma",          "Salvia",           Season.Spring, "#2196F3"),
            ("Orphan",    "Victimhood",     "Forget-me-not",    Season.Summer, "#5C6BC0"),
            ("Magician",  "Manipulation",   "Monkshood",        Season.Autumn, "#7E57C2"),
            ("Lover",     "Obsession",      "Fuchsia",          Season.Winter, "#AB47BC"),
            ("Hero",      "Arrogance",      "Gladiolus",        Season.Spring, "#E53935"),
            ("Rebel",     "Nihilism",       "Crocosmia",        Season.Summer, "#F4511E"),
            ("Creator",   "Perfectionism",  "Bird of Paradise", Season.Autumn, "#FB8C00"),
            ("Ruler",     "Tyranny",        "Crown Imperial",   Season.Winter, "#FFB300"),
        };

        [MenuItem("LaRasa/Twelve Content/Populate Default Wheel")]
        private static void PopulateSelectedWheel()
        {
            if (Selection.activeObject is not ArchetypeWheel wheel)
            {
                EditorUtility.DisplayDialog(
                    "Populate Default Wheel",
                    "Select an ArchetypeWheel asset in the Project window first.\n\n" +
                    "Create one via: Assets → Create → LaRasa → Archetype Wheel.",
                    "OK");
                return;
            }

            Undo.RecordObject(wheel, "Populate Default Archetype Wheel");
            wheel.SetPositions(BuildDefaultPositions());
            EditorUtility.SetDirty(wheel);
            AssetDatabase.SaveAssetIfDirty(wheel);

            var issues = wheel.Validate();
            if (issues.Count == 0)
            {
                Debug.Log($"ArchetypeWheel '{wheel.name}' populated with 12 default positions. Validation passed.", wheel);
            }
            else
            {
                Debug.LogWarning(
                    $"ArchetypeWheel '{wheel.name}' populated but validation reported {issues.Count} issue(s):\n - " +
                    string.Join("\n - ", issues), wheel);
            }
        }

        /// <summary>
        /// The twelve canonical placeholder positions. Public so EditMode tests
        /// can assert the default data satisfies every wheel invariant.
        /// </summary>
        public static ArchetypePosition[] BuildDefaultPositions()
        {
            var positions = new ArchetypePosition[CircleOfFifths.PositionCount];
            for (int i = 0; i < positions.Length; i++)
            {
                var row = Table[i];
                Color color = ParseHex(row.colorHex);
                positions[i] = new ArchetypePosition(
                    wheelIndex: i,
                    archetypeName: row.archetype,
                    shadowName: row.shadow,
                    flowerName: row.flower,
                    season: row.season,
                    musicalKey: CircleOfFifths.KeyAt(i),
                    wheelColor: color);
            }

            return positions;
        }

        private static Color ParseHex(string hex)
        {
            return ColorUtility.TryParseHtmlString(hex, out Color c) ? c : Color.magenta;
        }
    }
}
