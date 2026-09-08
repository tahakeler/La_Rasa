using System;
using LaRasa.Core.TwelveContent;
using NUnit.Framework;

namespace LaRasa.Core.Tests.TwelveContent
{
    /// <summary>
    /// TR-twelve-001: the twelve positions are a single canonical structure.
    /// These cover the structurally-fixed key ordering and adjacency math that
    /// the town-zone layout and adaptive-music crossfades depend on.
    /// </summary>
    [TestFixture]
    public class CircleOfFifthsTests
    {
        [Test]
        public void PositionCount_is_twelve()
        {
            Assert.AreEqual(12, CircleOfFifths.PositionCount);
        }

        [Test]
        public void KeysInOrder_matches_circle_of_fifths_from_C()
        {
            string[] expected = { "C", "G", "D", "A", "E", "B", "F#", "Db", "Ab", "Eb", "Bb", "F" };
            CollectionAssert.AreEqual(expected, CircleOfFifths.KeysInOrder());
        }

        [Test]
        public void KeysInOrder_returns_a_copy_callers_cannot_mutate_the_canonical_order()
        {
            string[] first = CircleOfFifths.KeysInOrder();
            first[0] = "MUTATED";
            Assert.AreEqual("C", CircleOfFifths.KeyAt(0));
            Assert.AreEqual("C", CircleOfFifths.KeysInOrder()[0]);
        }

        [Test]
        public void KeyAt_returns_the_wheel_order_key([Range(0, 11)] int wheelIndex)
        {
            string[] expected = { "C", "G", "D", "A", "E", "B", "F#", "Db", "Ab", "Eb", "Bb", "F" };
            Assert.AreEqual(expected[wheelIndex], CircleOfFifths.KeyAt(wheelIndex));
        }

        [Test]
        public void KeyAt_throws_below_range()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CircleOfFifths.KeyAt(-1));
        }

        [Test]
        public void KeyAt_throws_at_and_above_position_count()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CircleOfFifths.KeyAt(12));
            Assert.Throws<ArgumentOutOfRangeException>(() => CircleOfFifths.KeyAt(99));
        }

        [Test]
        public void AdjacentIndex_zero_offset_is_identity([Range(0, 11)] int wheelIndex)
        {
            Assert.AreEqual(wheelIndex, CircleOfFifths.AdjacentIndex(wheelIndex, 0));
        }

        [Test]
        public void AdjacentIndex_wraps_forward_past_the_top_of_the_wheel()
        {
            Assert.AreEqual(0, CircleOfFifths.AdjacentIndex(11, 1));
            Assert.AreEqual(1, CircleOfFifths.AdjacentIndex(11, 2));
            Assert.AreEqual(2, CircleOfFifths.AdjacentIndex(10, 4));
        }

        [Test]
        public void AdjacentIndex_wraps_backward_below_zero()
        {
            Assert.AreEqual(11, CircleOfFifths.AdjacentIndex(0, -1));
            Assert.AreEqual(10, CircleOfFifths.AdjacentIndex(0, -2));
            Assert.AreEqual(11, CircleOfFifths.AdjacentIndex(1, -2));
        }

        [Test]
        public void AdjacentIndex_wraps_large_offsets_in_both_directions()
        {
            Assert.AreEqual(3, CircleOfFifths.AdjacentIndex(3, 24));
            Assert.AreEqual(3, CircleOfFifths.AdjacentIndex(3, -24));
            Assert.AreEqual(4, CircleOfFifths.AdjacentIndex(3, 25));
        }

        [Test]
        public void AdjacentIndex_throws_on_out_of_range_start()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CircleOfFifths.AdjacentIndex(12, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => CircleOfFifths.AdjacentIndex(-1, 1));
        }
    }
}
