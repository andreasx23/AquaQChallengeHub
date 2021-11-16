using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge27
{
    public class Challenge27 : BaseChallenge<long>
    {
        private char[][] _grid;

        protected override long SolveChallenge()
        {
            List<string> words = new();
            for (int i = 0; i < _grid.Length; i++)
            {
                string temp = string.Empty;
                for (int j = 0; j < _grid[i].Length; j++)
                {
                    char horizontal = _grid[i][j];

                    if (char.IsLetter(horizontal))
                        temp += horizontal;
                    else
                    {
                        if (temp.Length > 1)
                            words.Add(temp);
                        temp = string.Empty;
                    }
                }
                if (temp.Length > 1)
                    words.Add(temp);
            }

            for (int i = 0; i < _grid.First().Length; i++)
            {
                string temp = string.Empty;
                for (int j = 0; j < _grid.Length; j++)
                {
                    char vertical = _grid[j][i];
                    if (char.IsLetter(vertical))
                        temp += vertical;
                    else
                    {
                        if (temp.Length > 1)
                            words.Add(temp);
                        temp = string.Empty;
                    }
                }
                if (temp.Length > 1)
                    words.Add(temp);
            }

            long ans = 0;
            foreach (var word in words)
            {
                int sum = word.Sum(c => c - 'a' + 1) * word.Length;
                ans += sum;
            }

            return ans;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _grid = File.ReadAllLines(path).Select(s => s.ToCharArray()).ToArray();
        }
    }
}
