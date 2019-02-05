namespace CodeWars.Kyu5.MaximumSubArraySum
{
    /// <summary>
    /// https://www.codewars.com/kata/maximum-subarray-sum/
    /// </summary>
    using System.Collections.Generic;
    public static class Kata
    {
        public static int MaxSequence(int[] arr)
        {
            var length = arr.Length;
            var cacheTable = new CacheTable(length);
            for (var i = 0; i < length; i++)
            {
                for (var j = i; j < length; j++)
                {
                    if (!cacheTable.TryGetValue(i, j, out var current))
                    {
                        cacheTable.TryGetValue(i, j - 1, out var previousSum);
                        cacheTable[i, j] = previousSum + arr[j];
                    }
                }
            }
            return cacheTable.MaxValue;
        }
    }

    public class CacheTable
    {
        private int[,] _table;
        private int _maxValue = 0;

        public int this[int i, int j]
        {
            get
            {
                return _table[i, j];
            }
            set
            {
                if (value > _maxValue)
                {
                    _maxValue = value;
                }
                _table[i, j] = value;
            }
        }

        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
            private set { }
        }
        public CacheTable(int size)
        {
            _table = new int[size, size];
        }

        public bool TryGetValue(int i, int j, out int value)
        {
            value = 0;
            var tableLength = _table.GetLength(1);
            if (i < 0 || i > tableLength || j < 0 || j > tableLength)
                return false;
            if (_table[i, j] != 0)
            {
                value = _table[i, j];
                return true;
            }
            return false;

        }
    }
}
