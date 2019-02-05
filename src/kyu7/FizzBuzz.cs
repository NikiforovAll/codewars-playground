namespace CodeWars.Kyu7.FizzBuzz
{
    /// <summary>
    /// https://www.codewars.com/kata/fizz-buzz/
    /// </summary>
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class FizzBuzz
    {
        public static string[] GetFizzBuzzArray(int n)
        {
            var fizzBuzz = new Dictionary<string, int>
            {
                ["Fizz"] = 3,
                ["Buzz"] = 5
            };
            if (n < 1) throw new ArgumentOutOfRangeException("invalid argument");
            return Enumerable.Range(1, n)
              .Select(x =>
                (x % (fizzBuzz["Fizz"] * fizzBuzz["Buzz"]) == 0) ? "FizzBuzz"
                : (x % fizzBuzz["Fizz"] == 0) ? "Fizz"
                : (x % fizzBuzz["Buzz"] == 0) ? "Buzz"
                : x.ToString()
              ).ToArray<string>();
        }
    }
}
