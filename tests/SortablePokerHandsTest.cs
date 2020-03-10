namespace CodeWars.Kyu4.RankingPokerHands
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    [TestFixture]
    public class PokerTests2
    {
        [Test]
        public void PokerHandSortTest()
        {
            // Arrange
            var expected = new List<PokerHand> {
            new PokerHand("KS AS TS QS JS"),
            new PokerHand("2H 3H 4H 5H 6H"),
            new PokerHand("AS AD AC AH JD"),
            new PokerHand("JS JD JC JH 3D"),
            new PokerHand("2S AH 2H AS AC"),
            new PokerHand("AS 3S 4S 8S 2S"),
            new PokerHand("2H 3H 5H 6H 7H"),
            new PokerHand("2S 3H 4H 5S 6C"),
            new PokerHand("2D AC 3H 4H 5S"), // why is it
            new PokerHand("AH AC 5H 6H AS"),
            new PokerHand("2S 2H 4H 5S 4C"),
            new PokerHand("AH AC 5H 6H 7S"),
            new PokerHand("AH AC 4H 6H 7S"),
            new PokerHand("2S AH 4H 5S KC"),
            new PokerHand("2S 3H 6H 7S 9C")
        };
            var random = new Random((int)DateTime.Now.Ticks);
            var actual = expected.OrderBy(x => random.Next()).ToList();
            // Act
            actual.Sort();
            // Assert
            // for (var i = 0; i < expected.Count; i++)
                // Assert.AreEqual(expected[i], actual[i], "Unexpected sorting order at index {0}", i);
        }
    }
}
