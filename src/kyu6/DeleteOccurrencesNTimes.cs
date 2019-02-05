namespace CodeWars.Kyu6.DeleteOccurrencesNTimes
{
    /// <summary>
    /// https://www.codewars.com/kata/delete-occurrences-of-an-element-if-it-occurs-more-than-n-times/solutions/
    /// </summary>

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Console;
    public class Kata
    {
        public static int[] DeleteNth(int[] arr, int x)
        {
            var dictionary = new Dictionary<int, int>();
            return arr.Select(el =>
            {
                if (dictionary.ContainsKey(el))
                {
                    dictionary[el]++;
                }
                else
                {
                    dictionary.Add(el, 1);
                }
                return el;
            })
              .Where(el => dictionary[el] <= x).ToArray();
        }
    }
}
