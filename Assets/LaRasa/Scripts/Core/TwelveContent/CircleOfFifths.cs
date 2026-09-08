using System;

namespace LaRasa.Core.TwelveContent
{
    /// <summary>
    /// The structurally-fixed half of the Twelve Content Framework: the
    /// twelve musical keys in circle-of-fifths order and the adjacency math
    /// that the town-zone layout and the adaptive-music crossfades both read.
    ///
    /// Per <c>design/gdd/twelve-content-framework.md</c> the key assignment is
    /// "structurally fixed (derived from circle of fifths)" and much harder to
    /// move than the archetype/flower/season placeholders. It lives here as
    /// plain C# — no UnityEngine dependency — so the ordering invariant can be
    /// unit-tested headlessly and reused by non-Editor tooling.
    ///
    /// Wheel order (position 0..11), clockwise from the top of the wheel:
    /// C, G, D, A, E, B, F#, Db, Ab, Eb, Bb, F.
    ///
    /// ADR: ADR-0001 (data-driven twelve-content). This type is the canonical
    /// source for the key column; <see cref="ArchetypeWheel"/> validates its
    /// serialized data against it rather than letting designers hand-type keys.
    /// </summary>
    public static class CircleOfFifths
    {
        /// <summary>Number of positions on the archetype wheel. Structural constant.</summary>
        public const int PositionCount = 12;

        // Index = wheelIndex. Each step of +1 around the wheel is a perfect
        // fifth up. Do not reorder — the town-zone adjacency and the
        // beat-matched crossfade design both depend on neighbours being a
        // fifth apart (design/gdd/sound-and-crossfade.md, Core Rule 2).
        private static readonly string[] KeysInWheelOrder =
        {
            "C",  // 0
            "G",  // 1
            "D",  // 2
            "A",  // 3
            "E",  // 4
            "B",  // 5
            "F#", // 6
            "Db", // 7
            "Ab", // 8
            "Eb", // 9
            "Bb", // 10
            "F"   // 11
        };

        /// <summary>
        /// The canonical musical key for a wheel position, e.g. position 0 → "C".
        /// Keys are pitch-class names only; major/minor per zone is a separate
        /// open decision (owner: Ken — see design/gdd/sound-and-crossfade.md).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="wheelIndex"/> is not in [0, 11].
        /// </exception>
        public static string KeyAt(int wheelIndex)
        {
            if (wheelIndex < 0 || wheelIndex >= PositionCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(wheelIndex), wheelIndex,
                    $"wheelIndex must be in [0, {PositionCount - 1}].");
            }

            return KeysInWheelOrder[wheelIndex];
        }

        /// <summary>
        /// A fresh copy of the twelve keys in wheel order. Returns a copy so
        /// callers cannot mutate the canonical ordering.
        /// </summary>
        public static string[] KeysInOrder()
        {
            var copy = new string[PositionCount];
            Array.Copy(KeysInWheelOrder, copy, PositionCount);
            return copy;
        }

        /// <summary>
        /// The wheel index <paramref name="offset"/> steps clockwise from
        /// <paramref name="wheelIndex"/>, wrapping around the ring. Negative
        /// offsets step counter-clockwise. Used for crossfade adjacency
        /// (offset ±1 = the neighbouring zone, a fifth away).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="wheelIndex"/> is not in [0, 11].
        /// </exception>
        public static int AdjacentIndex(int wheelIndex, int offset)
        {
            if (wheelIndex < 0 || wheelIndex >= PositionCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(wheelIndex), wheelIndex,
                    $"wheelIndex must be in [0, {PositionCount - 1}].");
            }

            // C# % can return negative for negative operands — normalise into [0, 11].
            int wrapped = ((wheelIndex + offset) % PositionCount + PositionCount) % PositionCount;
            return wrapped;
        }
    }
}
