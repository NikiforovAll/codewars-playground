namespace CodeWars.Kyu4.PyramidSlideDown
{
    public class PyramidSlideDown
    {
        // https://projecteuler.net/problem=18
        // https://projecteuler.net/problem=67
        public static int LongestSlideDown(int[][] pyramid)
        {
            int h = pyramid.Length;
            int[][] sumPyramid = new int[h][];
            sumPyramid[h - 1] = pyramid[h - 1];
            for (int i = h - 2; i >= 0; i--)
            {
                var row = new int[(sumPyramid[i + 1].Length - 1)];
                var previousRow = sumPyramid[i + 1];
                for (int j = 0; j < row.Length; j++)
                {
                    row[j] = pyramid[i][j] + System.Math.Max(previousRow[j], previousRow[j + 1]);
                }
                sumPyramid[i] = row;
            }
            return sumPyramid[0][0];
        }
    }
}
