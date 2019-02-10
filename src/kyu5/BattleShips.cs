namespace CodeWars.Kyu5.BattleShips
{

    /// <summary>
    /// https://www.codewars.com/kata/battle-ships-sunk-damaged-or-not-touched/
    /// </summary>

    using System.Collections.Generic;
    using System.Linq;
    public class Kata
    {
        private static string SUNK_LABEL = "sunk";
        private static string DAMAGED_LABEL = "damaged";
        private static string NOT_TOUCHED_LABEL = "notTouched"; 
        private static string POINTS_LABEL = "points";

        private static Dictionary<string, float> Catalog = new Dictionary<string, float>{
            [SUNK_LABEL] = 1,
            [DAMAGED_LABEL] = 0.5f,
            [NOT_TOUCHED_LABEL] = -1
        };
         
        public static Dictionary<string, double> damagedOrSunk(int[,] board, int[,] attacks)
        {
            var result = new Dictionary<string, double>
            {
                [SUNK_LABEL] = 0,
                [DAMAGED_LABEL] = 0,
                [NOT_TOUCHED_LABEL] = 0,
                [POINTS_LABEL] = 0
            };
            foreach (var ship in FindShips(board))
            {
                if(ship == null)
                    continue;
                double points = ship.Score(attacks, out var label);
                result[label]++;
                result[POINTS_LABEL] += points;
            }
            return result;
        }

        private static Ship[] FindShips(int[,] board)
        {
            var sizeY = board.GetLength(0);
            var sizeX = board.GetLength(1);
            var result = new Ship[System.Math.Max(sizeX, sizeY)];
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    int cell = board[i, j];
                    if (cell == 0)
                        continue;
                    var ship = result[cell];
                    if (ship == null)
                    {
                        ship = new Ship()
                        {
                            X = (start: j, end: j),
                            Y = (start: i, end: i),
                            BoardSize = (X: sizeX, Y: sizeY)
                        };
                        result[cell] = ship;
                    }
                    else
                    {
                        var t = ship.Y;
                        t.end = i;
                        ship.Y = t;
                        var t2 = ship.X;
                        t2.end = j;
                        ship.X = t2;
                    }
                }
            }

            return result;
        }

        private class Ship
        {
            public (int X, int Y) BoardSize { get; set; }
            public (int start, int end) X { get; set; }
            public (int start, int end) Y { get; set; }

            public double Score(int[,] attacks, out string label)
            {
                int hits = 0;
                int shipLength = this.MaxLength();
                for (int i = 0; i < attacks.GetLength(0); i++)
                {
                    int attackX = attacks[i, 0] - 1;
                    int attackY = BoardSize.Y - attacks[i, 1];
                    if (X.start <= attackX && attackX <= X.end &&
                         Y.start <= attackY && attackY <= Y.end)
                    {
                        hits++;
                    }

                }
                label = hits > 0 ? 
                    hits == shipLength ? SUNK_LABEL : DAMAGED_LABEL :
                    NOT_TOUCHED_LABEL;
                return Catalog[label];
            }

            private int MaxLength()
            {
                int xSize = X.end - X.start + 1;
                int ySize = Y.end - Y.start + 1;
                return System.Math.Max(xSize, ySize);
            }
        }
    }
}