
using System;
namespace CodeWars.Kyu4.Permutations
{
    using System.Collections.Generic;
    using System.Linq;
    public class Permutations
    {
        public static List<string> SinglePermutations(string s)
        {
            return s.ToCharArray().GetPermutations()
                .Select(chars => new string(chars.ToArray()))
                .Distinct()
                .ToList();
        }

        public static IEnumerable<string> SinglePermutations2(string s)
        {
            return s.GetPermutations2();
        }
    }

    // https://www.w3resource.com/csharp-exercises/recursion/csharp-recursion-exercise-11.php
    // backtracking version, slightly inefficient in memory and complexity, could be considered as brute force solution
    static class PermutationExtensions2
    {
        public static IEnumerable<string> GetPermutations2(this string s)
        {
            return SinglePermutations(s);
        }

        public static List<string> SinglePermutations(string s)
        {
            IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> array)
            {
                var arr = array.ToArray();
                return arr.Length == 1 ? new[] { arr } : arr.SelectMany((e, i) => Permute(arr.Where((_, i2) => i != i2)).Select(m => new[] { e }.Concat(m)));
            }

            return Permute(s).Select(e => string.Concat(e)).Distinct().ToList();
        }
    }
    // https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
    // https://stackoverflow.com/questions/1506078/fast-permutation-number-permutation-mapping-algorithms/1506337#1506337 (mapping between permutations and natural numbers)
    // this solution is not mine but rather interesting to investigate and delve to.
    static class PermutationExtensions
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            var factorials = Enumerable.Range(0, array.Length + 1)
                .Select(Factorial)
                .ToArray();

            for (var i = 0L; i < factorials[array.Length]; i++)
            {
                var sequence = GenerateSequence(i, array.Length - 1, factorials);

                yield return GeneratePermutation(array, sequence);
            }
        }

        private static IEnumerable<T> GeneratePermutation<T>(T[] array, IReadOnlyList<int> sequence)
        {
            var clone = (T[])array.Clone();

            for (int i = 0; i < clone.Length - 1; i++)
            {
                Swap(ref clone[i], ref clone[i + sequence[i]]);
            }

            return clone;
        }

        private static int[] GenerateSequence(long number, int size, IReadOnlyList<long> factorials)
        {
            var sequence = new int[size];

            for (var j = 0; j < sequence.Length; j++)
            {
                var facto = factorials[sequence.Length - j];

                sequence[j] = (int)(number / facto);
                number = (int)(number % facto);
            }

            return sequence;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private static long Factorial(int n)
        {
            long result = n;

            for (int i = 1; i < n; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
