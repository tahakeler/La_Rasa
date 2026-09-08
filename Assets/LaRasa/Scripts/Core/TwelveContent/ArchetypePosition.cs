using System;
using UnityEngine;

namespace LaRasa.Core.TwelveContent
{
    /// <summary>
    /// One of the twelve positions on the archetype wheel. A single
    /// <see cref="ArchetypeWheel"/> asset holds an ordered array of twelve of
    /// these; every other content-defining asset (villager, zone, track,
    /// flower crop, enemy archetype) references a position rather than copying
    /// its fields (ADR-0001).
    ///
    /// Field status per <c>design/gdd/twelve-content-framework.md</c>:
    /// <list type="bullet">
    /// <item><description>
    /// <b>archetypeName / shadowName / flowerName / season</b> — placeholder,
    /// freely editable by narrative/art in the Inspector.
    /// </description></item>
    /// <item><description>
    /// <b>musicalKey</b> — structurally fixed (circle of fifths). Validated
    /// against <see cref="CircleOfFifths"/> by the owning wheel; do not hand-edit.
    /// </description></item>
    /// <item><description>
    /// <b>wheelColor</b> — structurally fixed (twelve-hue colour wheel).
    /// </description></item>
    /// <item><description>
    /// <b>wheelIndex</b> — fixed position order 0..11.
    /// </description></item>
    /// </list>
    /// </summary>
    [Serializable]
    public class ArchetypePosition
    {
        [SerializeField]
        [Tooltip("Fixed position order, 0..11, clockwise from the top of the wheel.")]
        private int wheelIndex;

        [SerializeField]
        [Tooltip("Placeholder — editable. The Jungian archetype for this position.")]
        private string archetypeName;

        [SerializeField]
        [Tooltip("Placeholder — editable. The archetype's characteristic failure mode.")]
        private string shadowName;

        [SerializeField]
        [Tooltip("Placeholder — editable. The paint-crop flower for this position.")]
        private string flowerName;

        [SerializeField]
        [Tooltip("Placeholder — editable. Default season assignment for this position.")]
        private Season season;

        [SerializeField]
        [Tooltip("Structurally fixed (circle of fifths). Validated against CircleOfFifths.")]
        private string musicalKey;

        [SerializeField]
        [Tooltip("Structurally fixed (twelve-hue colour wheel).")]
        private Color wheelColor = Color.white;

        /// <summary>Fixed position order, 0..11.</summary>
        public int WheelIndex => wheelIndex;

        /// <summary>Placeholder archetype name — never display the word "archetype" in-game.</summary>
        public string ArchetypeName => archetypeName;

        /// <summary>Placeholder shadow name — the archetype's failure mode.</summary>
        public string ShadowName => shadowName;

        /// <summary>Placeholder flower name — the position's paint crop.</summary>
        public string FlowerName => flowerName;

        /// <summary>Default season assignment (placeholder).</summary>
        public Season Season => season;

        /// <summary>Musical key (pitch class only — major/minor is decided elsewhere).</summary>
        public string MusicalKey => musicalKey;

        /// <summary>Colour-wheel colour for this position.</summary>
        public Color WheelColor => wheelColor;

        /// <summary>Parameterless constructor for Unity serialization.</summary>
        public ArchetypePosition() { }

        /// <summary>
        /// Explicit constructor — used by the default-wheel authoring tool and
        /// by tests. Runtime code reads positions from the serialized asset.
        /// </summary>
        public ArchetypePosition(
            int wheelIndex,
            string archetypeName,
            string shadowName,
            string flowerName,
            Season season,
            string musicalKey,
            Color wheelColor)
        {
            this.wheelIndex = wheelIndex;
            this.archetypeName = archetypeName;
            this.shadowName = shadowName;
            this.flowerName = flowerName;
            this.season = season;
            this.musicalKey = musicalKey;
            this.wheelColor = wheelColor;
        }
    }
}
