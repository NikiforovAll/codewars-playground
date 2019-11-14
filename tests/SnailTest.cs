using CodeWars.Kyu4.Snail;

namespace Kyu4.Snail {
    using System.Linq;
    using System;
    using NUnit.Framework;

    public class SnailTest {
        [Test]
        public void SnailTest1 () {
            int[][] array = {
                new [] { 1, 2, 3 },
                new [] { 4, 5, 6 },
                new [] { 7, 8, 9 }
            };
            var r = new [] { 1, 2, 3, 6, 9, 8, 7, 4, 5 };
            Test (array, r);
        }
        public void Test (int[][] array, int[] result) {
            Assert.AreEqual (result, SnailSolution.Snail (array));
        }
    }
}
