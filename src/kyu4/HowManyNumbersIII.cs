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
            // .Where(num => num.ToString().Length == numDigits);
            return isFound
                ? new List<long>() { res.Count, res.First(), res.Last() }
                : new List<long>() { };
        }

        public static IEnumerable<long> FindAllInternal(
            int sumDigits, int numDigits, int currentMaxDigit, out bool isFound)
        {
            isFound = false;
            if (sumDigits < 0 || numDigits < 1)
            {
                return new List<long>();
            }
            long multiplier = GetMultiplierOfTen(numDigits - 1);
            var nextNumDigits = numDigits - 1;
            var res = Enumerable.Range(currentMaxDigit, 10 - currentMaxDigit)
                .Select(d => (d, sums: FindAllInternal(sumDigits - d, nextNumDigits, d, out var found),
                    found: found || d == sumDigits))
                .Where(solution => solution.found
                    && sumDigits <= nextNumDigits * 9 + solution.d
                    && sumDigits >= nextNumDigits * currentMaxDigit)
                .SelectMany(el => numDigits != 1
                    ? el.sums.Select(sum => sum + el.d * multiplier)
                    : new long[1] { el.d });
            isFound = res.Any();
            return res;
        }

        public static long GetMultiplierOfTen(int num) =>
            Enumerable.Repeat(10, num).Aggregate(1, (acc, el) => acc * el);
    }
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
