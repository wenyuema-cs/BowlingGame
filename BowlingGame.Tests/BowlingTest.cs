namespace BowlingGame.Tests
{
    [TestFixture]
    public class BowlingTests
    {
        private Bowling? _bowling;

        [SetUp]
        public void Setup()
        {
            _bowling = new Bowling();
        }

        private void RollMany(int rolls, int pins)
        {
            for (int i = 0; i < rolls; i++)
            {
                _bowling.Roll(pins);
            }
        }

        [Test]
        public void GutterGame_ReturnsZero()
        {
            List<int> rolls = Enumerable.Repeat(0, 20).ToList();
            _bowling!.RollMultiple(rolls);
            var score = _bowling.Score();
            Assert.That(score, Is.EqualTo(0)); 
        }

        [Test]
        public void MixedGame_SomeStrikesAndSpares_ReturnsExpectedScore()
        {
            List<int> rolls = new List<int> { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10, 2, 8, 6 };
            _bowling!.RollMultiple(rolls);
            var score = _bowling.Score();
            Assert.That(score, Is.EqualTo(133)); 
        }

        [Test]
        public void AllOnes_ReturnsTwenty()
        {
            RollMany(20, 1);
            Assert.That(_bowling!.Score(), Is.EqualTo(20));
        }

        [Test]
        public void OneSpare_FollowedByThree_ReturnsSixteen()
        {
            _bowling.Roll(5);
            _bowling.Roll(5); // Spare
            _bowling.Roll(3);
            RollMany(17, 0);
            Assert.That(_bowling!.Score(), Is.EqualTo(16));
        }

        [Test]
        public void OneStrike_FollowedByThreeThree_ReturnsTwentyTwo()
        {
            _bowling.Roll(10); // Strike
            _bowling.Roll(3);
            _bowling.Roll(3);
            RollMany(16, 0);
            Assert.That(_bowling!.Score(), Is.EqualTo(22));
        }

        [Test]
        public void PerfectGame_ReturnsThreeHundred()
        {
            RollMany(12, 10);
            Assert.That(_bowling!.Score(), Is.EqualTo(300));
        }

        // Additional tests can include mixed scenarios, testing the 10th frame logic more thoroughly, etc.
    }
}
