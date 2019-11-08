using System.Collections.Generic;
namespace CodeWars.Kyu6.BuildPileOfCubes {
    public class ASum {
        private static readonly Dictionary<long, long> _cubeSumPerLevelCache = new Dictionary<long, long> {
            [0] = 0,
            [1] = 1
        };

        public static long findNb (long m) {
            long n = 1, sum = 0;
            for (; sum < m; sum = GenerateSumOfCubesCachedFor (n++)) { }
            return sum == m ? --n : -1;
        }

        private static long GenerateSumOfCubesCachedFor (long n) {
            long prev = n - 1L;
            if (!_cubeSumPerLevelCache.ContainsKey (prev)) {
                _cubeSumPerLevelCache[prev] = GenerateSumOfCubesCachedFor (prev);
            }
            return _cubeSumPerLevelCache[prev] + n * n * n;
        }

    }
}
