
using CodeWars.Kyu6.BuildTower;
using NUnit.Framework;
using System;

namespace Kyu6.BuildTowerTests
{
    namespace Solution
    {
        [TestFixture]
        public class KataTests
        {
            [Test]
            public void BasicTests()
            {
                Assert.AreEqual(string.Join(",", new[] { "*" }), string.Join(",", Kata.TowerBuilder(1)));
                Assert.AreEqual(string.Join(",", new[] { " * ", "***" }), string.Join(",", Kata.TowerBuilder(2)));
                Assert.AreEqual(string.Join(",", new[] { "  *  ", " *** ", "*****" }), string.Join(",", Kata.TowerBuilder(3)));
            }
        }
    }
}
