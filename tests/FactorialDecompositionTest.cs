using System;
using CodeWars.Kyu5.FactorialDecomposition;
using NUnit.Framework;

namespace Kyu5.FactorialDecomposition {

    [TestFixture]
    public class FactDecompTest {
        private static void testing (int n, string expected) {
            Console.WriteLine ("n: {0}, expected: {1}", n, expected);
            Assert.AreEqual (expected, FactDecomp.Decomp (n));
        }

        [Test]
        public static void FiveFactorial() {
            testing (5, "2^3 * 3 * 5");
        }
        [Test]
        public static void test () {
            testing (17, "2^15 * 3^6 * 5^3 * 7^2 * 11 * 13 * 17");
            testing (22, "2^19 * 3^9 * 5^4 * 7^3 * 11^2 * 13 * 17 * 19");
            testing (14, "2^11 * 3^5 * 5^2 * 7^2 * 11 * 13");
            testing (25, "2^22 * 3^10 * 5^6 * 7^3 * 11^2 * 13 * 17 * 19 * 23");
        }
    }

}
