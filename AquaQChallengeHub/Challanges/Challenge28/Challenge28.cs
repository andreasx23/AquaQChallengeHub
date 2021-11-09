using AquaQChallengeHub.SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge28
{
    public class Challenge28 : BaseChallenge<string>
    {
        private string _word;
        private char[][] _grid;
        private int _h, _w;

        protected override string SolveChallenge()
        {
            string ans = string.Empty;
      
            foreach (var c in _word)
            {
                Dirs direction = Dirs.RIGHT;
                var (x, y) = FindStartCoordinates(c);
                bool isMatch = false;
                while (!isMatch)
                {
                    (int x, int y) coordinatesToAdd = Direction.GetCoordinateForDirection(direction);
                    x += coordinatesToAdd.x;
                    y += coordinatesToAdd.y;
                    char current = _grid[x][y];
                    //Print(x, y);
                    switch (current)
                    {
                        case '\\':
                            switch (direction)
                            {
                                case Dirs.RIGHT:
                                    direction = Dirs.DOWN;
                                    break;
                                case Dirs.LEFT:
                                    direction = Dirs.UP;
                                    break;
                                case Dirs.UP:
                                    direction = Dirs.LEFT;
                                    break;
                                case Dirs.DOWN:
                                    direction = Dirs.RIGHT;
                                    break;
                            }
                            _grid[x][y] = '/';
                            break;
                        case '/':
                            switch (direction)
                            {
                                case Dirs.RIGHT:
                                    direction = Dirs.UP;
                                    break;
                                case Dirs.LEFT:
                                    direction = Dirs.DOWN;
                                    break;
                                case Dirs.UP:
                                    direction = Dirs.RIGHT;
                                    break;
                                case Dirs.DOWN:
                                    direction = Dirs.LEFT;
                                    break;
                            }
                            _grid[x][y] = '\\';
                            break;
                        case ' ':
                            break;
                        default:
                            ans += current;
                            isMatch = true;                            
                            break;
                    }                    
                }
            }

            return ans;
        }

        private void Print(int x, int y)
        {
            for (int i = 0; i < _grid.Length; i++)
            {
                for (int j = 0; j < _grid[i].Length; j++)
                {
                    if (i == x && j == y)
                        Console.Write("X");
                    else
                        Console.Write(_grid[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private (int x, int y) FindStartCoordinates(char letter)
        {
            int x = 0, y = 0;
            for (int i = 0; i < _h; i++)
            {
                if (_grid[i][0] == letter)
                {
                    x = i;
                    break;
                }
            }
            return (x, y);
        }

        protected override void ReadData()
        {
            bool useInput = true;
            _word = useInput ? "FISSION_MAILED" : "DAD";
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var maze = File.ReadAllLines(path);
            _h = maze.Length;
            _w = maze.First().Length;
            _grid = new char[_h][];
            for (int i = 0; i < _h; i++)
            {
                _grid[i] = new char[_w];
                for (int j = 0; j < _w; j++)
                {
                    var c = maze[i][j];
                    if (char.IsLetterOrDigit(c) || c == '\\' || c == '/' || c == '_')
                        _grid[i][j] = c;
                    else
                        _grid[i][j] = ' ';
                }
            }
        }
    }
}
