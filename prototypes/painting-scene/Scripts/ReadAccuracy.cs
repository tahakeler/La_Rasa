using System;

namespace LaRasa.Prototypes.PaintingScene
{
    /// <summary>
    /// Engine-agnostic core logic for the painting scene's twin-stick read
    /// mechanic. Deliberately has no UnityEngine dependency so it can be
    /// unit tested standalone and dropped into a Unity project later.
    ///
    /// Left stick = emotional read: (expressedWithdrawn, griefJoy), each in
    /// [-1, 1]. Right stick = depth push, magnitude in [0, 1].
    ///
    /// Source: design/gdd/painting-and-reflection.md — "opens when your
    /// read is right, resists when your read is off." No score or bar is
    /// ever surfaced to the player; this class produces the numbers that
    /// drive diegetic feedback (colour warmth, key resolution, canvas
    /// sharpening), not a displayed number.
    /// </summary>
    public static class ReadAccuracy
    {
        /// <summary>
        /// A point on the two-axis emotional plane the GDD describes:
        /// Expressed/Withdrawn on one axis, Grief/Joy on the other.
        /// </summary>
        public readonly struct EmotionalPoint
        {
            public readonly float ExpressedWithdrawn; // -1 = Withdrawn, +1 = Expressed
            public readonly float GriefJoy;            // -1 = Grief,     +1 = Joy

            public EmotionalPoint(float expressedWithdrawn, float griefJoy)
            {
                ExpressedWithdrawn = Clamp(expressedWithdrawn);
                GriefJoy = Clamp(griefJoy);
            }

            private static float Clamp(float v) => v < -1f ? -1f : (v > 1f ? 1f : v);
        }

        /// <summary>
        /// How close the player's left-stick read is to the villager's true
        /// emotional state, as a continuous 0..1 value (1 = exact match).
        /// Uses normalized Euclidean distance on the 2-unit-radius plane.
        /// </summary>
        public static float EvaluateReadAccuracy(EmotionalPoint playerRead, EmotionalPoint groundTruth)
        {
            float dx = playerRead.ExpressedWithdrawn - groundTruth.ExpressedWithdrawn;
            float dy = playerRead.GriefJoy - groundTruth.GriefJoy;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            // Max possible distance on a [-1,1] x [-1,1] plane is the diagonal, 2*sqrt(2).
            const float maxDistance = 2.8284271f; // 2 * sqrt(2)
            float normalized = distance / maxDistance;

            float accuracy = 1f - normalized;
            return accuracy < 0f ? 0f : (accuracy > 1f ? 1f : accuracy);
        }

        /// <summary>
        /// How much depth progress a single input sample contributes.
        /// Depth access is gated by read accuracy: pushing hard on the
        /// right stick with an inaccurate read yields little to no
        /// progress ("resists when off"); an accurate read lets the same
        /// push reach further ("opens when right").
        ///
        /// depthPush: right-stick magnitude, [0, 1].
        /// readAccuracy: output of EvaluateReadAccuracy, [0, 1].
        /// Returns: depth progress contributed by this sample, [0, 1].
        /// </summary>
        public static float EvaluateDepthProgress(float depthPush, float readAccuracy)
        {
            depthPush = depthPush < 0f ? 0f : (depthPush > 1f ? 1f : depthPush);
            readAccuracy = readAccuracy < 0f ? 0f : (readAccuracy > 1f ? 1f : readAccuracy);

            // Deliberately not linear: squaring accuracy makes the "resists
            // when off" feel more pronounced at low accuracy, and "opens
            // when right" more rewarding near a true read, rather than a
            // flat proportional response. This is a first guess, not a
            // tuned value — expect this to change after playtesting.
            return depthPush * (readAccuracy * readAccuracy);
        }
    }

    /// <summary>
    /// Accumulates depth progress across a painting-scene session and maps
    /// it onto the four portrait stages described in
    /// design/gdd/villager-archetype-system.md (early/middle/late/finished).
    /// The per-stage threshold values below are placeholders — see
    /// BRIEF.md and painting-and-reflection.md's Open Questions.
    /// </summary>
    public class PaintingSessionAccumulator
    {
        // Placeholder thresholds — NOT tuned, NOT sourced from the GDD
        // (which leaves this unspecified). A real value needs a playtest.
        public const float StageAdvanceThreshold = 1.0f;

        private float _accumulatedDepth;

        public float AccumulatedDepth => _accumulatedDepth;

        public void AddSample(float depthPush, float readAccuracy)
        {
            _accumulatedDepth += ReadAccuracy.EvaluateDepthProgress(depthPush, readAccuracy);
        }

        /// <summary>True once this session has accumulated enough depth to
        /// advance the villager's portrait one stage.</summary>
        public bool HasAdvancedStage => _accumulatedDepth >= StageAdvanceThreshold;
    }
}
