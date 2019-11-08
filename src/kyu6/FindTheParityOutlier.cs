using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.Kyu6.FindTheParityOutlier {
    public class Kata {
        internal static List<Func<int, bool>> _filters = new List<Func<int, bool>> {
            (i) => i % 2 == 0,
            (i) => i % 2 == 1
        };
        public static int Find (int[] integers) {
            return integers.First (
                i => Kata
                ._filters
                .First (f => integers.Count (f) == 1)
                .Invoke (i)
            );
        }
    }
}
