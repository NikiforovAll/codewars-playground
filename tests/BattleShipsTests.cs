using CodeWars.Kyu5.BattleShips;
namespace Kyu5.BattleShips
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;

    namespace CodeWars.BattleShips
    {
        [TestFixture]
        class KataTestClass
        {
            [TestCase]
            public void BasicTest1()
            {
                var kata = new  Kata();
                int[,] board = { { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 } };
                int[,] attacks = { { 3, 1 }, { 3, 2 }, { 3, 3 } };
                var result = Kata.damagedOrSunk(board, attacks);
                Console.WriteLine("Test 1 - sunk = 1, damaged = 0, notTouched = 0, points = 1");
                Assert.AreEqual(1, result["sunk"]);
                Assert.AreEqual(0, result["damaged"]);
                Assert.AreEqual(0, result["notTouched"]);
                Assert.AreEqual(1, result["points"]);
            }

            [TestCase]
            public void BasicTest2()
            {
                var kata = new Kata();
                int[,] board = { { 3, 0, 1 },
                                { 3, 0, 1 },
                                { 0, 2, 1 },
                                { 0, 2, 0 } };
                int[,] attacks = { { 2, 1 }, { 2, 2 }, { 3, 2 }, { 3, 3 } };
                var result = Kata.damagedOrSunk(board, attacks);
                Console.WriteLine("Test 2 - sunk = 1, damaged = 1, notTouched = 1, points = 0.5");
                Assert.AreEqual(1, result["sunk"]);
                Assert.AreEqual(1, result["damaged"]);
                Assert.AreEqual(1, result["notTouched"]);
                Assert.AreEqual(0.5, result["points"]);
            }
        }
    }
}