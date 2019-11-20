using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CodeWars.Kyu5.FactorialDecomposition {

    public class FactDecomp {
        // contains number of occurrences of each prime number in factorization
        private static SortedDictionary<int, List<int>> solutions = new SortedDictionary<int, List<int>> {
            [1] = new List<int> { }
        };
        public static string Decomp (int n) {
            for (int i = 2; i <= n; i++) {
                List<int> solution = new List<int> ();
                int res = i;
                int factor = i - 1;
                if(solutions.ContainsKey(i)){
                    continue;
                }
                do {
                    res = findResidual (res, factor, out var gcd);
                    if (gcd != 1) {
                        solution.AddRange (solutions[gcd]);
                    }
                    if(solutions.ContainsKey(res)){
                        solution.AddRange(solutions[res]);
                        break;
                    }
                    factor = factor > res ? res: factor;
                    factor--;
                } while (factor > 1);
                if (solution.Count == 0) {
                    solution.Add (i);
                }
                solutions.Add (i, solution);
            }
            return GenerateResult (n);
            int findResidual (int res, int factor, out int gcd) {
                gcd = GCD (res, factor);
                return res / gcd;
            };
        }
        private static string GenerateResult (int n) {
            var accumulatedResults = new SortedDictionary<int, int> () { };
            foreach (var number in solutions.Take(n).SelectMany(kvp => kvp.Value))
            {
                if(!accumulatedResults.ContainsKey(number)) {
                    accumulatedResults[number] = 1;
                }else{
                    accumulatedResults[number]++;
                }
            }
            StringBuilder sb = new StringBuilder ();
            sb.AppendJoin (" * ",accumulatedResults.Select(kvp => FormatResult(kvp)));
            string FormatResult(KeyValuePair<int, int> kvp) =>
                String.Format("{0}{1}",
                    kvp.Key.ToString(),
                    kvp.Value !=1 ? "^"+kvp.Value : String.Empty
                );
            return sb.ToString ();
        }

        private static int GCD (int a, int b) {
            while (a != 0 && b != 0) {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;
        }
    }
}
