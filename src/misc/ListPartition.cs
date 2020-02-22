using System;

namespace CodeWars.MISC.ListPartition
{
    public static class ListPartition
    {
        /// <summary>
        /// Partitions a given array into three sections (this is very similar to quicksort pivot)
        /// space: O(1)
        /// complexity: O(n)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pivot"></param>
        /// <typeparam name="T"></typeparam>
        public static void Partition<T>(T[] array, T pivot)
            where T : IComparable
        {
            static bool IsPartitioned(int l, int m, int g) => m >=g;
            void Swap(int i, int j) => (array[i], array[j]) = (array[j], array[i]);
            int less = 0, mid = 0, greater = array.Length - 1;
            while (!IsPartitioned(less, mid, greater))
            {
                switch (array[mid].CompareTo(pivot))
                {
                    case -1:
                        Swap(mid++, less++);
                        break;
                    case 1:
                        Swap(greater--, mid);
                        break;
                    case 0:
                        mid++;
                        break;
                }
            }
        }
    }
}
