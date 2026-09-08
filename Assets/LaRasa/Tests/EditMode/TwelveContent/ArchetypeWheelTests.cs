using System;
using System.Linq;
using LaRasa.Core.Editor;
using LaRasa.Core.TwelveContent;
using NUnit.Framework;
using UnityEngine;

namespace LaRasa.Core.Tests.TwelveContent
{
    /// <summary>
    /// TR-twelve-001 / TR-twelve-002: the twelve positions live in one asset,
    /// referenced not duplicated, and the structurally-fixed columns cannot be
    /// silently hand-edited away from their canonical values.
    /// </summary>
    [TestFixture]
    public class ArchetypeWheelTests
    {
        private static ArchetypeWheel NewWheel(ArchetypePosition[] positions)
        {
            var wheel = ScriptableObject.CreateInstance<ArchetypeWheel>();
            wheel.SetPositions(positions);
            return wheel;
        }

        private static ArchetypePosition[] ValidPositions() => ArchetypeWheelDefaults.BuildDefaultPositions();

        [TearDown]
        public void TearDown()
        {
            // Nothing persistent is created; ScriptableObject instances are
            // collected. Explicit for clarity of the isolation contract.
        }

        [Test]
        public void Default_wheel_data_has_exactly_twelve_positions()
        {
            Assert.AreEqual(12, ValidPositions().Length);
            Assert.AreEqual(12, ArchetypeWheel.PositionCount);
        }

        [Test]
        public void Default_wheel_data_passes_every_structural_invariant()
        {
            var wheel = NewWheel(ValidPositions());
            CollectionAssert.IsEmpty(wheel.Validate());
        }

        [Test]
        public void GetPosition_returns_the_entry_whose_wheelIndex_matches([Range(0, 11)] int index)
        {
            var wheel = NewWheel(ValidPositions());
            Assert.AreEqual(index, wheel.GetPosition(index).WheelIndex);
        }

        [Test]
        public void GetPosition_key_column_matches_the_circle_of_fifths([Range(0, 11)] int index)
        {
            var wheel = NewWheel(ValidPositions());
            Assert.AreEqual(CircleOfFifths.KeyAt(index), wheel.GetPosition(index).MusicalKey);
        }

        [Test]
        public void GetPosition_throws_outside_range()
        {
            var wheel = NewWheel(ValidPositions());
            Assert.Throws<ArgumentOutOfRangeException>(() => wheel.GetPosition(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => wheel.GetPosition(12));
        }

        [Test]
        public void GetPosition_throws_when_the_asset_is_not_populated()
        {
            var wheel = NewWheel(Array.Empty<ArchetypePosition>());
            Assert.Throws<InvalidOperationException>(() => wheel.GetPosition(0));
        }

        [Test]
        public void GetAdjacent_wraps_around_the_ring_for_crossfade_adjacency()
        {
            var wheel = NewWheel(ValidPositions());
            Assert.AreEqual(0, wheel.GetAdjacent(11, 1).WheelIndex);
            Assert.AreEqual(11, wheel.GetAdjacent(0, -1).WheelIndex);
            Assert.AreEqual(5, wheel.GetAdjacent(5, 0).WheelIndex);
        }

        [Test]
        public void Validate_flags_a_wrong_length_array()
        {
            var eleven = ValidPositions().Take(11).ToArray();
            var issues = ArchetypeWheel.ValidatePositions(eleven);
            Assert.IsTrue(issues.Any(i => i.Contains("exactly 12")));
        }

        [Test]
        public void Validate_flags_a_null_positions_array()
        {
            var issues = ArchetypeWheel.ValidatePositions(null);
            Assert.IsTrue(issues.Any(i => i.Contains("null")));
        }

        [Test]
        public void Validate_flags_a_hand_edited_musical_key()
        {
            var positions = ValidPositions();
            positions[3] = new ArchetypePosition(
                wheelIndex: 3,
                archetypeName: positions[3].ArchetypeName,
                shadowName: positions[3].ShadowName,
                flowerName: positions[3].FlowerName,
                season: positions[3].Season,
                musicalKey: "Z#",             // not the circle-of-fifths key for position 3
                wheelColor: positions[3].WheelColor);

            var issues = ArchetypeWheel.ValidatePositions(positions);
            Assert.IsTrue(issues.Any(i => i.Contains("circle-of-fifths key")));
        }

        [Test]
        public void Validate_flags_a_duplicate_wheel_index()
        {
            var positions = ValidPositions();
            positions[4] = new ArchetypePosition(
                wheelIndex: 5,                 // collides with slot 5
                archetypeName: "Dup",
                shadowName: positions[4].ShadowName,
                flowerName: positions[4].FlowerName,
                season: positions[4].Season,
                musicalKey: CircleOfFifths.KeyAt(5),
                wheelColor: positions[4].WheelColor);

            var issues = ArchetypeWheel.ValidatePositions(positions);
            Assert.IsTrue(issues.Any(i => i.Contains("more than one position")));
        }

        [Test]
        public void Validate_flags_a_duplicate_archetype_name()
        {
            var positions = ValidPositions();
            positions[7] = new ArchetypePosition(
                wheelIndex: 7,
                archetypeName: positions[0].ArchetypeName, // duplicate of slot 0
                shadowName: positions[7].ShadowName,
                flowerName: positions[7].FlowerName,
                season: positions[7].Season,
                musicalKey: CircleOfFifths.KeyAt(7),
                wheelColor: positions[7].WheelColor);

            var issues = ArchetypeWheel.ValidatePositions(positions);
            Assert.IsTrue(issues.Any(i => i.Contains("used by more than one position")));
        }

        [Test]
        public void Validate_flags_an_empty_archetype_name()
        {
            var positions = ValidPositions();
            positions[2] = new ArchetypePosition(
                wheelIndex: 2,
                archetypeName: "   ",
                shadowName: positions[2].ShadowName,
                flowerName: positions[2].FlowerName,
                season: positions[2].Season,
                musicalKey: CircleOfFifths.KeyAt(2),
                wheelColor: positions[2].WheelColor);

            var issues = ArchetypeWheel.ValidatePositions(positions);
            Assert.IsTrue(issues.Any(i => i.Contains("archetypeName is empty")));
        }
    }
}
