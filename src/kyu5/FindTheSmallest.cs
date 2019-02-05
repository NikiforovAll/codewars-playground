namespace CodeWars.Kyu5.FindTheSmallest
{

    /// <summary>
    /// https://www.codewars.com/kata/find-the-smallest
    /// </summary>
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ToSmallest
    {
        public static long[] Smallest(long n)
        {
            var result = new long[] { n, 0, 0 };
            var digits = n.ToString()
                .ToCharArray()
                .Select(c => (byte)int.Parse(c.ToString()))
                .ToList<byte>();

            for (var i = 0; i < digits.Count - 1; i++)
            {
                var solutionIndex = FindMinOnInterval(digits, i);
                if (i != solutionIndex)
                {
                    byte tmp = digits[i];
                    digits.Insert(i, digits[solutionIndex]);
                    digits.RemoveRange(solutionIndex + 1, 1);
                    var swap = solutionIndex - i == 1;
                    checked
                    {
                        result[0] = ToLong(digits);
                    }
                    result[1] = swap ? i : solutionIndex;
                    result[2] = swap ? solutionIndex : i;
                    break;
                }
            }
            return result;
        }

        private static int FindMinOnInterval(List<byte> source, int startIndex)
        {
            var min = source[startIndex];
            var index = startIndex;
            for (var i = startIndex + 1; i < source.Count; i++)
            {
                min = source[i] < min ? source[index = i] : min;
            }
            return index;
        }
        private static long ToLong(List<byte> digits)
        {
            long acc = 0;
            var digitsNoZeros = digits.SkipWhile(d => d == 0).ToList();
            for (int i = 0, j = (int)Math.Pow(10, digitsNoZeros.Count - 1); i < digitsNoZeros.Count; i++, j /= 10)
            {
                acc += digitsNoZeros[i] * j;
            }
            return acc;
        }
    }
}