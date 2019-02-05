

namespace CodeWars.Kyu6.BuildTower
{
    /// <summary>
    /// https://www.codewars.com/kata/build-tower/
    /// </summary>
    
    using System;
    using System.Text;
    public class Kata
    {
        public static string[] TowerBuilder(int nFloors)
        {
            int baseLength = 1 + 2 * (nFloors - 1);
            StringBuilder tree = new StringBuilder(new String(' ', baseLength));
            var result = new string[nFloors];
            int padSize = nFloors - 1;
            for (int floor = 1; floor <= nFloors; floor++)
            {
                tree.Replace(' ', '*', padSize, 1 + 2 * (floor - 1));
                result[floor - 1] = tree.ToString();
                padSize--;
            }
            return result;
        }
    }
}
