namespace Kyu6.ReversePolishNotationCalculator
{
    using CodeWars.Kyu6.ReversePolishNotationCalculator;

    using System;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class CalcTest
    {
        private Calc calc = new Calc();

        [Test]
        public void ShouldWorkWithEmptyString()
        {
            Assert.AreEqual(0, calc.evaluate(""), 0, "Should work with empty string");
        }

        [Test]
        public void ShouldParseNumbers()
        {
            Assert.AreEqual(3, calc.evaluate("3"), 0, "Should parse numbers");
        }

        [Test]
        public void ShouldParseFloatNumbers()
        {
            Assert.AreEqual(3.5, calc.evaluate("3.5"), 0, "Should parse float numbers");
        }

        [Test]
        public void ShouldSupportAddition()
        {
            Assert.AreEqual(4, calc.evaluate("1 3 +"), 0, "Should support addition");
        }

        [Test]
        public void ShouldSupportMultiplication()
        {
            Assert.AreEqual(3, calc.evaluate("1 3 *"), 0, "Should support multiplication");
        }

        [Test]
        public void ShouldSupportSubstraction()
        {
            Assert.AreEqual(-2, calc.evaluate("1 3 -"), 0, "Should support substraction");
        }

        [Test]
        public void ShouldSupportDivision()
        {
            Assert.AreEqual(2, calc.evaluate("4 2 /"), 0, "Should support division");
        }

        [TestCase("+", ExpectedResult = "+", Description = "Test description")]
        [TestCase("1+2", ExpectedResult = "12+", Description = "Test description")]
        [TestCase("1.0+2", ExpectedResult = "1.02+", Description = "Test description")]

        public string ToRPNTest(string input)
        {
            return calc.ToRPN(input).Select(t => t.Value).Aggregate((current, next) => $"{next}{current}");
        }
    }

}