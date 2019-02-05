namespace CodeWars.Kyu5.BestTravel
{

    /// <summary>
    /// https://www.codewars.com/kata/best-travel/
    /// </summary>
    using System.Collections.Generic;
    using System.Linq;

    public static class SumOfK
    {
        //modifications of exp version of 
        //http://www.cs.dartmouth.edu/~ac/Teach/CS105-Winter05/Notes/nanda-scribe-3.pdf
        public static int? chooseBestSum(int t, int k, List<int> ls)
        {
            List<Pair<int, int>> resultList = new List<Pair<int, int>>{
          new Pair<int, int>(){Item1 = 0, Item2 = 0}
        };
            for (int i = 0; i < ls.Count; i++)
            {
                var clonedList = new List<Pair<int, int>>();
                for (int j = 0; j < resultList.Count; j++)
                {
                    clonedList.Add(new Pair<int, int>()
                    {
                        Item1 = resultList[j].Item1 + ls[i],
                        Item2 = resultList[j].Item2 + 1
                    });
                }
                resultList = resultList
                  .Union(clonedList)
                  .Where(x => x.Item1 <= t)
                  .OrderBy(l => l.Item1)
                  .ToList();
            }

            var res = resultList
              .Where(x => x.Item2 == k)
              .OrderByDescending(x => x.Item1)
              .FirstOrDefault();
            if (res != null)
            {
                return res.Item1;
            }
            return null;
        }

        public class Pair<T1, T2>
        {
            public T1 Item1 { get; set; }
            public T2 Item2 { get; set; }
        }
    }
}