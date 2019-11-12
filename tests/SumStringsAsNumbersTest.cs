using CodeWars.Kyu4.SumStringsAsNumbers;

namespace Kyu6.SumStringsAsNumbers {
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CodeWarsTest {
        [Test]
        public void Given123And456Returns579 () {
            Assert.AreEqual ("579", Kata.sumStrings ("123", "456"));
        }

        [Test]
        public void Given001And1Returns2 () {
            Assert.AreEqual ("2", Kata.sumStrings ("001", "1"));
        }
    }
}
