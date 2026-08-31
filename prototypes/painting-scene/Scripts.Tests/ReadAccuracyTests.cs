using NUnit.Framework;
using LaRasa.Prototypes.PaintingScene;

namespace LaRasa.Prototypes.PaintingScene.Tests
{
    // Standard Unity NUnit pattern, per .claude/agents/qa-tester.md.
    // These are not yet runnable in this repo (no Unity Test project
    // exists), but are written ready to drop into one — see BRIEF.md.
    [TestFixture]
    public class ReadAccuracyTests
    {
        [Test]
        public void EvaluateReadAccuracy_ExactMatch_ReturnsOne()
        {
            var point = new ReadAccuracy.EmotionalPoint(0.5f, -0.3f);

            var accuracy = ReadAccuracy.EvaluateReadAccuracy(point, point);

            Assert.AreEqual(1f, accuracy, 0.001f);
        }

        [Test]
        public void EvaluateReadAccuracy_OppositeCorners_ReturnsZero()
        {
            var playerRead = new ReadAccuracy.EmotionalPoint(-1f, -1f);
            var groundTruth = new ReadAccuracy.EmotionalPoint(1f, 1f);

            var accuracy = ReadAccuracy.EvaluateReadAccuracy(playerRead, groundTruth);

            Assert.AreEqual(0f, accuracy, 0.001f);
        }

        [Test]
        public void EvaluateReadAccuracy_PartialMiss_ReturnsMidRange()
        {
            var playerRead = new ReadAccuracy.EmotionalPoint(0f, 0f);
            var groundTruth = new ReadAccuracy.EmotionalPoint(1f, 0f);

            var accuracy = ReadAccuracy.EvaluateReadAccuracy(playerRead, groundTruth);

            Assert.Greater(accuracy, 0f);
            Assert.Less(accuracy, 1f);
        }

        [Test]
        public void EvaluateDepthProgress_HighPushLowAccuracy_YieldsLittleProgress()
        {
            var progress = ReadAccuracy.EvaluateDepthProgress(depthPush: 1f, readAccuracy: 0.1f);

            Assert.Less(progress, 0.05f, "A near-zero accuracy read should barely advance depth even at full push.");
        }

        [Test]
        public void EvaluateDepthProgress_HighPushHighAccuracy_YieldsSubstantialProgress()
        {
            var progress = ReadAccuracy.EvaluateDepthProgress(depthPush: 1f, readAccuracy: 1f);

            Assert.AreEqual(1f, progress, 0.001f);
        }

        [Test]
        public void EvaluateDepthProgress_ZeroPush_YieldsNoProgressRegardlessOfAccuracy()
        {
            var progress = ReadAccuracy.EvaluateDepthProgress(depthPush: 0f, readAccuracy: 1f);

            Assert.AreEqual(0f, progress, 0.001f);
        }

        [Test]
        public void EvaluateDepthProgress_ClampsOutOfRangeInputs()
        {
            var progress = ReadAccuracy.EvaluateDepthProgress(depthPush: 5f, readAccuracy: -2f);

            Assert.GreaterOrEqual(progress, 0f);
            Assert.LessOrEqual(progress, 1f);
        }

        [Test]
        public void PaintingSessionAccumulator_AccumulatesAcrossSamples()
        {
            var accumulator = new PaintingSessionAccumulator();

            for (int i = 0; i < 10; i++)
            {
                accumulator.AddSample(depthPush: 1f, readAccuracy: 1f);
            }

            Assert.IsTrue(accumulator.HasAdvancedStage,
                "Ten full-accuracy, full-push samples should be enough to advance a stage under the placeholder threshold.");
        }

        [Test]
        public void PaintingSessionAccumulator_NeverAdvances_WithZeroAccuracyReads()
        {
            var accumulator = new PaintingSessionAccumulator();

            for (int i = 0; i < 100; i++)
            {
                accumulator.AddSample(depthPush: 1f, readAccuracy: 0f);
            }

            Assert.IsFalse(accumulator.HasAdvancedStage,
                "A session where the player never reads the villager correctly should never advance the portrait, per the GDD's 'resists when off' rule.");
        }
    }
}
