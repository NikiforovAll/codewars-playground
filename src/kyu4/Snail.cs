using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.Kyu4.Snail {
    public class SnailSolution {
        // this solution was made based on calculation deliberately, other approach may be based on caching moves in nxn matrix.
        public static int[] Snail (int[][] array) {
            return new SnailScanner (array).AsEnumerable ().ToArray ();
        }
        class SnailScanner : IEnumerable<int> {
            private readonly Board _board;
            private (int x, int y) _position = (0, 0);
            private (int x, int y) _direction = (1, 0);
            private (int x, int y) _prevDirection = (0, -1);
            private int _steps = 0;
            private int _rotations = 0;
            public SnailScanner (int[][] array) {
                this._board = new Board (array);
            }

            public IEnumerator<int> GetEnumerator () {
                if(_board.Height == 0 || _board.Width != _board.Height)
                    yield break;
                do {
                    yield return _board[_position.x, _position.y];
                } while (TryMakeNextTurn ());
            }

            IEnumerator IEnumerable.GetEnumerator () {
                return this.GetEnumerator ();
            }

            private bool TryMakeNextTurn () {
                var newPosition = (_position.x + _direction.x, _position.y + _direction.y);
                if (_board.IsValidMove (newPosition)) {
                    _steps++;
                    _position = newPosition;
                    return true;
                }
                if (TryMakeRotation ()) {
                    return TryMakeNextTurn ();
                }
                return false;
            }

            private bool TryMakeRotation () {
                _rotations = ++_rotations % 4;
                var factor = _rotations > 1 ? -1 : 1;
                var newDirection = (x: (_direction.x + factor) % 2, y: (_direction.y + factor) % 2);
                var newPosition = (x: _position.x + newDirection.x, y: _position.y + newDirection.y);
                if (_board.IsValidMove (newPosition)) {
                    _board.Edges[_prevDirection]++;
                    _prevDirection = _direction;
                    _direction = newDirection;
                    return true;
                }
                return false;
            }
        }

        class Board {
            public int Height { get; set; }
            public int Width { get; set; }

            public int this [int i, int j] {
                get { return this._array[j][i]; }
            }

            public Dictionary < (int, int), int > Edges = new Dictionary < (int, int), int > {
                [(1, 0)] = 0, // 0 -> x
                [(0, 1)] = 0, // 0 -> y
                [(-1, 0)] = 0, //x -> 0
                [(0, -1)] = 0 // y -> 0
            };
            private readonly int[][] _array;

            public Board (int[][] array) {
                this.Width = array.Length;
                this.Height = array[0].Length;
                this._array = array;
            }

            public bool IsValidMove ((int x, int y) move) {
                return move.y.IsWithin (Edges[(0, -1)], Height - Edges[(0, 1)] - 1) &&
                    move.x.IsWithin (Edges[(-1, 0)], Width - Edges[(1, 0)] - 1);
            }
        }
    }
    static class Utils {
        public static bool IsWithin (this int value, int minimum, int maximum) {
            return value >= minimum && value <= maximum;
        }
    }
}
