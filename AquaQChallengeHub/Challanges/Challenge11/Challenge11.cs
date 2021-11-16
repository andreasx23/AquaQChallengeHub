using AquaQChallengeHub.Bases;
using AquaQChallengeHub.SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge11
{
    public class Challenge11 : BaseChallenge<int>
    {
        private enum Tiles
        {
            FLOOR = '#',
            OVERLAP = '@',
            EMPTY = ' '
        }
        private int _h, _w;
        private char[][] _grid;
        private readonly List<(int lx, int ly, int ux, int uy)> _instructions = new();

        protected override int SolveChallenge()
        {
            foreach (var (lx, ly, ux, uy) in _instructions)
            {
                for (int i = lx; i < ux; i++)
                {
                    for (int j = ly; j < uy; j++)
                        _grid[i][j] = _grid[i][j] != (char)Tiles.FLOOR ? (char)Tiles.FLOOR : (char)Tiles.OVERLAP;
                }
            }

            //Print();

            int ans = 0;
            for (int i = 0; i < _h; i++)
            {
                for (int j = 0; j < _w; j++)
                    if (_grid[i][j] == (char)Tiles.OVERLAP) ans += DFS(i, j);
            }

            //Print();

            return ans;
        }

        private int DFS(int x, int y)
        {
            if (!Direction.Inbounds(_grid, x, y) || _grid[x][y] == (char)Tiles.EMPTY) return 0;
            _grid[x][y] = (char)Tiles.EMPTY;

            int sum = 0;
            foreach (var coord in Direction.WALK)
                sum += DFS(coord.x + x, coord.y + y);
            return 1 + sum;
        }

        private void Print()
        {
            foreach (var item in _grid)
                Console.WriteLine(string.Join("", item));
            Console.WriteLine();
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var lines = File.ReadAllLines(path).Skip(1).Select(row => row.Split(',').Select(int.Parse).ToArray()).ToArray();
            foreach (var coords in lines)
            {
                var lx = coords[0];
                var ly = coords[1];
                var ux = coords[2];
                var uy = coords[3];
                _h = Math.Max(_h, Math.Max(lx, ux));
                _w = Math.Max(_w, Math.Max(ly, uy));
                _instructions.Add((lx, ly, ux, uy));
            }

            _grid = new char[_h][];
            for (int i = 0; i < _h; i++)
            {
                _grid[i] = new char[_w];
                for (int j = 0; j < _w; j++)
                    _grid[i][j] = (char)Tiles.EMPTY;
            }
        }
    }
}
