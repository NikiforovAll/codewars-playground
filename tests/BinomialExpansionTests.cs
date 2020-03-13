namespace CodeWars.Kyu3.BinomialExpansion
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExampleTest
    {
        [Test]
        public void testBPositive()
        {
            Assert.AreEqual("1", KataSolution.Expand("(x+1)^0"));
            Assert.AreEqual("x+1", KataSolution.Expand("(x+1)^1"));
            Assert.AreEqual("x^2+2x+1", KataSolution.Expand("(x+1)^2"));
        }

        [Test]
        public void testRandom()
        {
            Assert.AreEqual("81p^4-1404p^3+9126p^2-26364p+28561", KataSolution.Expand("(-3p+13)^4"));
            Assert.AreEqual("1296h^4", KataSolution.Expand("(-6h+0)^4"));
        }

        [Test]
        public void testBNegative()
        {
            Assert.AreEqual("1", KataSolution.Expand("(x-1)^0"));
            Assert.AreEqual("x-1", KataSolution.Expand("(x-1)^1"));
            Assert.AreEqual("x^2-2x+1", KataSolution.Expand("(x-1)^2"));
        }

        [Test]
        public void testAPositive()
        {
            Assert.AreEqual("8x^3-36x^2+54x-27", KataSolution.Expand("(2x-3)^3"));
            Assert.AreEqual("1", KataSolution.Expand("(7x-7)^0"));
            Assert.AreEqual("625m^4+1500m^3+1350m^2+540m+81", KataSolution.Expand("(5m+3)^4"));
        }

        [Test]
        public void testANegative()
        {
            Assert.AreEqual("625m^4-1500m^3+1350m^2-540m+81", KataSolution.Expand("(-5m+3)^4"));
            Assert.AreEqual("-8k^3-36k^2-54k-27", KataSolution.Expand("(-2k-3)^3"));
            Assert.AreEqual("1", KataSolution.Expand("(-7x-7)^0"));
        }
    }
}
