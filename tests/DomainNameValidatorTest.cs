namespace CodeWars.DomainNameValidator
{
    using NUnit.Framework;
    using CodeWars.Kyu5.DomainNameValidator;
    [TestFixture]
    public class DomainNameValidatorTest
    {
        DomainNameValidator v = new DomainNameValidator();

        [Test]
        public void DomainTests()
        {
            Assert.AreEqual(false, v.validate("codewars"));
            Assert.AreEqual(true, v.validate("g.co"));
            Assert.AreEqual(true, v.validate("codewars.com"));
            Assert.AreEqual(true, v.validate("CODEWARS.COM"));
            Assert.AreEqual(true, v.validate("sub.codewars.com"));
            Assert.AreEqual(false, v.validate("codewars.com-"));
            Assert.AreEqual(false, v.validate(".codewars.com"));
            Assert.AreEqual(false, v.validate("example@codewars.com"));
            Assert.AreEqual(false, v.validate("127.0.0.1"));
        }
    }
}
