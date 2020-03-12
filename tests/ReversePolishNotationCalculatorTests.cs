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

        [Test]

        public void ShouldCalcComplex()
        {
            Assert.AreEqual(14, calc.evaluate("5 1 2 + 4 * + 3 -"), 0, "Should calc complex expressions");
        }

        [TestCase("+", ExpectedResult = "+", Description = "Test description")]
        [TestCase("1+2", ExpectedResult = "12+", Description = "Test description")]
        [TestCase("1.0+2", ExpectedResult = "1.02+", Description = "Test description")]
        [TestCase("sin(0)", ExpectedResult = "0sin", Description ="Test description")]
        [TestCase("(1+2)*3", ExpectedResult = "12+3*", Description ="Test description")]

        public string ToRPNTest(string input)
        {
            return calc.ToRPN(input).Select(t => t.Value).Aggregate((current, next) => $"{current}{next}");
        }


        [TestCase("123.45*(678.90 / (-2.5+ 11.5)-(80 -19) *33.25) / 20 + 11", ExpectedResult = -12042.760875d)]
        [TestCase("(1 - 2) + -(-(-(-4)))", ExpectedResult = 3, Description = "Test description")]
        [TestCase("-(-1)", ExpectedResult = 1, Description = "Test description")]
        [TestCase("12* 123/(-5 + 2)", ExpectedResult = -492, Description = "Test description")]
        [TestCase("1 - 1", ExpectedResult = 0, Description = "Test description")]
        [TestCase("12*-1", ExpectedResult = -12.0d, Description = "Test description")]
        public double CalculateInfixTest(string input)
        {
            return calc.evaluate(input, infix: true);
        }
    }

}
