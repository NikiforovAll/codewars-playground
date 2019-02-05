namespace Kyu5.BestTravel
{
    using CodeWars.Kyu5.BestTravel;

    using System;
    using NUnit.Framework;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    [TestFixture]
    public class SumOfKTests
    {
        [Test]
        public void Test1()
        {
            Console.WriteLine("****** Basic Tests");
            List<int> ts = new List<int> { 50, 55, 56, 57, 58 };
            int? n = SumOfK.chooseBestSum(163, 3, ts);
            Assert.AreEqual(163, n);

            ts = new List<int> { 50 };
            n = SumOfK.chooseBestSum(163, 3, ts);
            Assert.AreEqual(null, n);

            ts = new List<int> { 91, 74, 73, 85, 73, 81, 87 };
            n = SumOfK.chooseBestSum(230, 3, ts);
            Assert.AreEqual(228, n);
        }
    }
}