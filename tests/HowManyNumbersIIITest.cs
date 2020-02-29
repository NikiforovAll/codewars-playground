namespace CodeWars.Kyu4.HowManyNumbersIII
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    // using CodeWars.Kyu4.HowManyNumbersIII;
    public class SolutionTest
    {
        [Test]
        public void ReturnsCorrectResultForTwoDigitNumberWithSumOfFive()
        {
            Assert.AreEqual(new List<long> { 2, 14, 23 }, HowManyNumbers.FindAll(5, 2));
        }

        [Test]
        public void ExampleTests()
        {
            Assert.AreEqual(new List<long> { 8L, 118L, 334L }, HowManyNumbers.FindAll(10, 3));
            Assert.AreEqual(new List<long> { 1L, 999L, 999L }, HowManyNumbers.FindAll(27, 3));
            Assert.AreEqual(new List<long> { }, HowManyNumbers.FindAll(84, 4));
            Assert.AreEqual(new List<long> { 123L, 116999L, 566666L }, HowManyNumbers.FindAll(35, 6));
        }
    }
}
