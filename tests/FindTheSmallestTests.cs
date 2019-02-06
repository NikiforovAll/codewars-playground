using CodeWars.Kyu5.FindTheSmallest;

namespace Kyu5
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public static class ToSmallestTests
    {
        private static void testing(long n, string res)
        {
            Assert.AreEqual(res, Array2String(ToSmallest.Smallest(n)));
        }
        private static string Array2String(long[] list)
        {
            return "[" + string.Join(", ", list) + "]";
        }
        [Test]
        public static void test1()
        {
            Console.WriteLine("Basic Tests smallest");
            testing(261235, "[126235, 2, 0]");
            testing(209917, "[29917, 0, 1]");
            testing(285365, "[238565, 3, 1]");
            testing(269045, "[26945, 3, 0]");
            testing(296837, "[239687, 4, 1]");
            //TODO: overflow examples don't pass tests
            testing(187863002809, "[18786300289, 10, 0]"); //?
            testing(16706692062651338, "[1670669262651338, 8, 0]"); //?
        }
    }
}