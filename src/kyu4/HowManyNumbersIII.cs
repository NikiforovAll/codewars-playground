namespace CodeWars.Kyu4.HowManyNumbersIII
{
    using System.Linq;
    using System.Collections.Generic;
    public class HowManyNumbers
    {
        /// <summary>
        /// accepts sum of digits. E.g. for 123 sumDigits = 6 and numDigits = 3
        /// </summary>
        /// <param name="sumDigits"></param>
        /// <param name="numDigits"></param>
        /// <returns>{[numberOfPossibleValues, minValue, maxValue]}</returns>
        public static List<long> FindAll(int sumDigits, int numDigits)
        {
            var res = FindAllInternal(sumDigits, numDigits, 1, out var isFound).ToList();
            return isFound
                ? new List<long>() { res.Count , long.Parse(res.First()), long.Parse(res.Last()) }
                : new List<long>() { };
        }

        public static IEnumerable<string> FindAllInternal(
            int sumDigits, int numDigits, int currentMaxDigit, out bool isFound)
        {
            isFound = false;
            if (sumDigits < 0 || numDigits < 1)
            {
                return new List<string>();
            }
            var nextNumDigits = numDigits - 1;
            var res = Enumerable.Range(currentMaxDigit, 10 - currentMaxDigit)
                .Where(d => sumDigits <= nextNumDigits * 9 + d)
                .Select(d => (d, sums: FindAllInternal(sumDigits - d, nextNumDigits, d, out var found),
                    found: found || (d == sumDigits && numDigits == 1)))
                .Where(solution => solution.found)
                .SelectMany(el => numDigits != 1
                    ? el.sums.Select(sum => el.d.ToString() + sum )
                    : new string[] { el.d.ToString() });
            isFound = res.Any();
            return res;
        }
    }
    //   public static long GetMultiplierOfTen(int num) =>
    //      Enumerable.Repeat(10, num).Aggregate(1, (acc, el) => acc * el);
    
    // public static bool IsAscendingNumber(long num) {
    //     var digits = num.ToString()
    //         .AsEnumerable()
    //         .Select(c => Char.GetNumericValue(c)).ToList();
    //     var cnt = 0;
    //     foreach(var d in digits.Skip(1))
    //     {
    //         if (digits[cnt++] > d) {
    //             return false;
    //         }
    //     }
    //     return true;
    // }
}
