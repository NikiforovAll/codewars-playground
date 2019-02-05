namespace CodeWars.Kyu5.SortTheOdd
{
    /// <summary>
    /// https://www.codewars.com/kata/sort-the-odd/solutions/
    /// </summary>
    using System.Linq;
    using System.Collections.Generic;
    using System;
    public class Kata
    {
        public static int[] SortArray(int[] array)
        {
            var result = new List<int>();
            var evenNumbers = array
              .Select((el, i) => new { Ind = i, Value = el })
              .Where(el => el.Value % 2 == 0)
              .ToList();
            var oddNumbers = array
              .Where(el => el % 2 == 1)
              .OrderBy(el => el)
              .ToList();

            for (int i = 0, m = 0, k = 0; i < array.Length; i++)
            {
                //merge to lists, could be done via linq, huh?
                if (evenNumbers.Count > m && evenNumbers[m].Ind == i)
                {
                    result.Add(evenNumbers[m].Value);
                    m++;
                    continue;
                }
                result.Add(oddNumbers[k]);
                k++;
            }
            return result.ToArray();
        }
    }
}