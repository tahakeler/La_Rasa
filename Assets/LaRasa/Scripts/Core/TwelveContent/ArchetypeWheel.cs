using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaRasa.Core.TwelveContent
{
    /// <summary>
    /// The single canonical data asset for the Twelve Content Framework
    /// (ADR-0001). Holds the ordered array of twelve <see cref="ArchetypePosition"/>
    /// entries that every other content system in La Rasa references rather
    /// than duplicating.
    ///
    /// There should be exactly one of these assets in the project. Dependent
    /// assets (VillagerData, TownZoneData, MusicTrackData, FlowerCropData,
    /// EnemyArchetypeData) hold a reference to a position on this wheel, not a
    /// copy of its fields — so a rename of a placeholder archetype/flower/season
    /// propagates everywhere automatically.
    ///
    /// GDD: design/gdd/twelve-content-framework.md
    /// ADR: docs/architecture/ADR-0001-twelve-content-data-architecture.md
    /// Requirements: TR-twelve-001, TR-twelve-002
    /// </summary>
    [CreateAssetMenu(menuName = "LaRasa/Archetype Wheel", fileName = "ArchetypeWheel")]
    public class ArchetypeWheel : ScriptableObject
    {
        /// <summary>Structural constant — the wheel always has twelve positions.</summary>
        public const int PositionCount = CircleOfFifths.PositionCount;

        [SerializeField]
        [Tooltip("The twelve archetype-wheel positions, ordered by wheelIndex 0..11. " +
                 "Length must be exactly 12.")]
        private ArchetypePosition[] positions = Array.Empty<ArchetypePosition>();

        /// <summary>The twelve positions in wheel order. Read-only.</summary>
        public IReadOnlyList<ArchetypePosition> Positions => positions;

        /// <summary>
        /// The position at a given wheel index (0..11).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="wheelIndex"/> is outside [0, 11].
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The asset is not populated with exactly twelve positions.
        /// </exception>
        public ArchetypePosition GetPosition(int wheelIndex)
        {
            if (positions == null || positions.Length != PositionCount)
            {
                throw new InvalidOperationException(
                    $"ArchetypeWheel '{name}' is not populated with {PositionCount} positions " +
                    $"(has {(positions?.Length ?? 0)}). Author it in the Inspector or run " +
                    "LaRasa → Twelve Content → Populate Default Wheel.");
            }

            if (wheelIndex < 0 || wheelIndex >= PositionCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(wheelIndex), wheelIndex,
                    $"wheelIndex must be in [0, {PositionCount - 1}].");
            }

            return positions[wheelIndex];
        }

        /// <summary>
        /// The position <paramref name="offset"/> steps clockwise around the
        /// wheel from <paramref name="wheelIndex"/>, wrapping around the ring.
        /// Negative offsets step counter-clockwise. Offset ±1 is the
        /// neighbouring zone (a fifth away) used for beat-matched crossfades.
        /// </summary>
        public ArchetypePosition GetAdjacent(int wheelIndex, int offset)
        {
            int adjacent = CircleOfFifths.AdjacentIndex(wheelIndex, offset);
            return GetPosition(adjacent);
        }

        /// <summary>
        /// Checks every structural invariant of the wheel and returns a list of
        /// human-readable problems (empty when the wheel is valid). Pure — no
        /// side effects — so it is exercised directly by unit tests and reused
        /// by <see cref="OnValidate"/>.
        /// </summary>
        public IReadOnlyList<string> Validate()
        {
            return ValidatePositions(positions);
        }

        /// <summary>
        /// Structural validation of a raw positions array, factored out so it
        /// can be unit-tested without constructing a ScriptableObject.
        /// </summary>
        public static IReadOnlyList<string> ValidatePositions(IReadOnlyList<ArchetypePosition> candidate)
        {
            var issues = new List<string>();

            if (candidate == null)
            {
                issues.Add("positions array is null.");
                return issues;
            }

            if (candidate.Count != PositionCount)
            {
                issues.Add($"positions must contain exactly {PositionCount} entries (found {candidate.Count}).");
            }

            var seenIndices = new HashSet<int>();
            var seenArchetypeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < candidate.Count; i++)
            {
                ArchetypePosition p = candidate[i];
                if (p == null)
                {
                    issues.Add($"position at array slot {i} is null.");
                    continue;
                }

                if (p.WheelIndex < 0 || p.WheelIndex >= PositionCount)
                {
                    issues.Add($"slot {i}: wheelIndex {p.WheelIndex} is outside [0, {PositionCount - 1}].");
                }
                else
                {
                    if (!seenIndices.Add(p.WheelIndex))
                    {
                        issues.Add($"slot {i}: wheelIndex {p.WheelIndex} is used by more than one position.");
                    }

                    if (p.WheelIndex != i && candidate.Count == PositionCount)
                    {
                        issues.Add($"slot {i}: wheelIndex {p.WheelIndex} does not match its array position " +
                                   "(the array must be stored in wheel order).");
                    }

                    string expectedKey = CircleOfFifths.KeyAt(p.WheelIndex);
                    if (!string.Equals(p.MusicalKey, expectedKey, StringComparison.Ordinal))
                    {
                        issues.Add($"slot {i} (wheelIndex {p.WheelIndex}): musicalKey '{p.MusicalKey}' " +
                                   $"does not match the circle-of-fifths key '{expectedKey}'. " +
                                   "The key column is structurally fixed — do not hand-edit it.");
                    }
                }

                if (string.IsNullOrWhiteSpace(p.ArchetypeName))
                {
                    issues.Add($"slot {i}: archetypeName is empty.");
                }
                else if (!seenArchetypeNames.Add(p.ArchetypeName))
                {
                    issues.Add($"slot {i}: archetypeName '{p.ArchetypeName}' is used by more than one position.");
                }
            }

            return issues;
        }

        private void OnValidate()
        {
            IReadOnlyList<string> issues = Validate();
            if (issues.Count == 0)
            {
                return;
            }

            // Fail loudly at data-entry time, not at runtime (ADR-0001
            // Implementation Guidelines).
            Debug.LogWarning(
                $"ArchetypeWheel '{name}' has {issues.Count} problem(s):\n - " +
                string.Join("\n - ", issues),
                this);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Editor/authoring-only setter. Used by the "Populate Default Wheel"
        /// menu item and by EditMode tests. Runtime code must never call this —
        /// the wheel is authored data.
        /// </summary>
        public void SetPositions(ArchetypePosition[] newPositions)
        {
            positions = newPositions ?? Array.Empty<ArchetypePosition>();
        }
#endif
    }
}
