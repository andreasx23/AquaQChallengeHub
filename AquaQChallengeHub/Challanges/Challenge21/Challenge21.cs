using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge21
{
    public class Challenge21 : BaseChallenge<int>
    {
        private List<List<int>> _floor;
        private int _vaccumeSize;

        protected override int SolveChallenge()
        {
            int h = _floor.Count, w = _floor.First().Count - _vaccumeSize + 1;
            int[][] dp = new int[h][];
            for (int i = 0; i < h; i++)
                dp[i] = new int[w];

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < _vaccumeSize; k++)
                        sum += _floor[i][k + j];

                    if (i == 0)
                        dp[i][j] = sum;
                    else
                    {
                        if (j > 0 && j + 1 < w)
                        {
                            int left = dp[i - 1][j - 1], middle = dp[i - 1][j], right = dp[i - 1][j + 1];
                            dp[i][j] = Math.Max(left + sum, Math.Max(middle + sum, right + sum));
                        }
                        else
                        {
                            if (j == 0)
                            {
                                int right = dp[i - 1][j + 1], middle = dp[i - 1][j];
                                dp[i][j] = Math.Max(middle + sum, right + sum);
                            }
                            else
                            {
                                int left = dp[i - 1][j - 1], middle = dp[i - 1][j];
                                dp[i][j] = Math.Max(middle + sum, left + sum);
                            }
                        }
                    }
                }
            }

            return dp.Last().Max();
        }

        protected override void ReadData()
        {
            bool useInput = true;
            _vaccumeSize = useInput ? 5 : 3;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _floor = File.ReadAllLines(path).Select(row => row.Split(' ').Select(int.Parse).ToList()).ToList();
        }
    }
}
