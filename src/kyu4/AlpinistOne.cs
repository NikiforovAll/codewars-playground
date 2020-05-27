using System.Linq;
using System.Collections.Generic;
namespace CodeWars.Kyu4.AlpinistOne
{
    public class Finder
    {
        public static bool PathFinder(string maze)
        {
            var m = ScanMaze(maze, out var start, out var finish);
            var visited = new HashSet<(int x, int y, int v)>();
            var stack = new Stack<(int x, int y, int v)>();
            stack.Push(start);
            var pathFound = false;
            while (stack.Count != 0 && !pathFound)
            {
                var current = stack.Pop();
                if (!visited.Add(current))
                {
                    continue;
                }
                var neighbors = GetNeighbors(m, current.x, current.y)
                    .Where(node => !visited.Contains(node));
                if (current == finish)
                {
                    pathFound = true;
                }
                foreach (var move in neighbors)
                {
                    stack.Push(move);
                }
            }
            return pathFound;
        }

        private static List<List<(int x, int y, int v)>> ScanMaze(string maze, out (int x, int y, int v) start, out (int x, int y, int v) finish)
        {
            var m = BuildMaze()
                .Select((il, i) => il.Select((v, j) => (x: j, y: i, v)).ToList())
                .ToList();
            start = m.First().First();
            finish = m.Last().Last();
            return m;
            IEnumerable<IEnumerable<int>> BuildMaze()
            {
                foreach (var row in maze.Split('\n'))
                {
                    yield return BuildRow(row);
                }
            }
            static IEnumerable<int> BuildRow(string row)
                => row.AsEnumerable().Select(c => c.Equals('.') ? 0 : 1);
        }

        private static IEnumerable<(int x, int y, int v)> GetNeighbors(
            List<List<(int x, int y, int v)>> m, int x, int y
        )
        {
            return GetAll().Where(room => room.v == 0);
            IEnumerable<(int x, int y, int v)> GetAll()
            {
                yield return PickNeighbor(x - 1, y);
                yield return PickNeighbor(x, y + 1);
                yield return PickNeighbor(x + 1, y);
                yield return PickNeighbor(x, y - 1);
                (int x, int y, int v) PickNeighbor(int x, int y) =>
                    (x, y) switch
                    {
                        (var x1, var y1)
                            when y1 >= 0 && y1 < m.Count && x1 >= 0 && x1 < m[y1].Count => m[y1][x1],
                        _ => (-1, -1, 1)
                    };
            }
        }
    }
}
