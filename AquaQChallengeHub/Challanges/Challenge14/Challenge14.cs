using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge14
{
    public class Challenge14 : BaseChallenge<int>
    {
        private List<List<int>> _calls = new();
        private int[,] _grid;
        private int _h;
        private int _w;

        protected override int SolveChallenge()
        {
            int ans = 0;

            foreach (var current in _calls)
            {
                bool[,] isVisited = new bool[_h, _w];
                int runs = 0;
                foreach (var val in current)
                {
                    runs++;
                    bool isMatch = false;
                    for (int i = 0; i < _h && !isMatch; i++)
                    {
                        for (int j = 0; j < _w && !isMatch; j++)
                        {
                            if (_grid[i, j] == val)
                            {
                                isVisited[i, j] = true;
                                isMatch = true;
                            }
                        }
                    }
                    
                    if (runs < _h || !isMatch) continue;

                    isMatch = false;
                    int leftDiagonalCount = 0;
                    int rightDiagonalCount = 0;
                    for (int i = 0; i < _h && !isMatch; i++)
                    {
                        if (isVisited[i, i]) leftDiagonalCount++;
                        if (isVisited[_h - 1 - i, i]) rightDiagonalCount++;

                        int horizontalCount = 0;
                        int verticalCount = 0;
                        for (int j = 0; j < _w && !isMatch; j++)
                        {
                            if (isVisited[i, j]) horizontalCount++;
                            if (isVisited[j, i]) verticalCount++;
                            if (horizontalCount == _w || verticalCount == _w || leftDiagonalCount == _w || rightDiagonalCount == _w) isMatch = true;
                        }
                    }

                    if (isMatch)
                    {
                        ans += runs;
                        break;
                    }
                }
            }

            return ans;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _calls = File.ReadAllLines(path).Select(row => row.Split(' ').Select(int.Parse).ToList()).ToList();

            string grid = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\grid.txt";
            var gridLines = File.ReadAllLines(grid).Select(row => row.Split(',').Select(int.Parse).ToList()).ToList();
            _h = gridLines.Count;
            _w = gridLines.First().Count;
            _grid = new int[_h, _w];
            for (int i = 0; i < _h; i++)
            {
                for (int j = 0; j < _w; j++)
                {
                    _grid[i, j] = gridLines[i][j];
                }
            }
        }
    }
}
