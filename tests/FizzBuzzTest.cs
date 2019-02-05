using CodeWars.Kyu7.FizzBuzz;

namespace Kyu7
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void FizzBuzzTest_0()
        {
            int input = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => FizzBuzz.GetFizzBuzzArray(input));
        }

        [Test]
        public void FizzBuzzTest_1()
        {
            int input = 1;
            string[] expected = { "1" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FizzBuzzTest_3()
        {
            int input = 3;
            string[] expected = { "1", "2", "Fizz" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FizzBuzzTest_5()
        {
            int input = 5;
            string[] expected = { "1", "2", "Fizz", "4", "Buzz" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FizzBuzzTest_15()
        {
            int input = 15;
            string[] expected = { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);

            Assert.AreEqual(expected, actual);
        }
    }
}