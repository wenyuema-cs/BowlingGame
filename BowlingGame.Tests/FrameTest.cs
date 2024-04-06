namespace BowlingGame.Tests
{
    [TestFixture]
    public class FrameTests
    {
        [Test]
        public void AddRoll_IncreasesRollsCount()
        {
            var frame = new Frame(false);
            frame.AddRoll(4);
            Assert.That(frame.Rolls.Count, Is.EqualTo(1));

        }

        [Test]
        public void IsStrike_FirstRollTen_ReturnsTrue()
        {
            var frame = new Frame(false);
            frame.AddRoll(10);
            Assert.IsTrue(frame.IsStrike);
        }

        [Test]
        public void IsSpare_TwoRollsTotalTen_ReturnsTrue()
        {
            var frame = new Frame(false);
            frame.AddRoll(6);
            frame.AddRoll(4);
            Assert.IsTrue(frame.IsSpare);
        }

        [Test]
        public void IsFrameComplete_Strike_ReturnsTrue()
        {
            var frame = new Frame(false);
            frame.AddRoll(10);
            Assert.IsTrue(frame.IsFrameComplete);
        }

        [Test]
        public void IsFrameComplete_TwoRolls_ReturnsTrue()
        {
            var frame = new Frame(false);
            frame.AddRoll(5);
            frame.AddRoll(2);
            Assert.IsTrue(frame.IsFrameComplete);
        }

        [Test]
        public void IsFrameComplete_LastFrameThreeRolls_ReturnsTrue()
        {
            var frame = new Frame(true); // This is the last frame
            frame.AddRoll(10); // Strike
            frame.AddRoll(10); // Bonus roll
            frame.AddRoll(10); // Bonus roll
            Assert.IsTrue(frame.IsFrameComplete);
        }

        [Test]
        public void IsFrameComplete_LastFrameNotComplete_ReturnsFalse()
        {
            var frame = new Frame(true); // This is the last frame
            frame.AddRoll(10); // Strike
            frame.AddRoll(10); // Bonus roll
            Assert.IsFalse(frame.IsFrameComplete);
        }

        // Test to ensure non-last frames that aren't strikes/spares aren't marked as complete prematurely
        [Test]
        public void IsFrameComplete_NonLastFrameOneRollNotStrike_ReturnsFalse()
        {
            var frame = new Frame(false);
            frame.AddRoll(5);
            Assert.IsFalse(frame.IsFrameComplete);
        }
    }
}
