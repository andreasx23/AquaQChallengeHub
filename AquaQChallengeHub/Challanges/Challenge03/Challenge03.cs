using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge03
{
    public class Challenge03 : ChallengeBase<int>
    {
        private const int mazeLength = 6;
        private string _instructions;
        private readonly char[][] _maze = new char[mazeLength][]
        {
            new char[] { ' ',' ','#','#',' ',' ' },
            new char[] { ' ','#','#','#','#',' ' },
            new char[] { '#','#','#','#','#','#' },
            new char[] { '#','#','#','#','#','#' },
            new char[] { ' ','#','#','#','#',' ' },
            new char[] { ' ',' ','#','#',' ',' ' },
        };

        protected override int SolveChallenge()
        {
            int x = 0, y = 2, ans = 0;
            foreach (var c in _instructions)
            {
                switch (c)
                {
                    case 'U':
                        if (IsInRange(x - 1, y)) x -= 1;
                        break;
                    case 'D':
                        if (IsInRange(x + 1, y)) x += 1;
                        break;
                    case 'R':
                        if (IsInRange(x, y + 1)) y += 1;
                        break;
                    case 'L':
                        if (IsInRange(x, y - 1)) y -= 1;
                        break;
                    default:
                        throw new Exception();
                }
                ans += x;
                ans += y;
            }

            return ans;
        }

        private bool IsInRange(int x, int y)
        {
            return x >= 0 && x < mazeLength && y >= 0 && y < mazeLength && _maze[x][y] == '#';
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _instructions = File.ReadAllLines(path).First();
        }
    }
}
