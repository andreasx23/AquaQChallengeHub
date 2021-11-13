using AquaQChallengeHub.Bases;
using AquaQChallengeHub.SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge06
{
    public class Challenge06 : BaseChallenge<int>
    {
        //3 numbers which sum to 123
        private const int TARGET = 123;

        protected override int SolveChallenge()
        {
            HashSet<(int a, int b, int c)> set = new();
            for (int i = 0; i <= TARGET; i++)
            {
                for (int j = 0; j <= TARGET; j++)
                {
                    for (int k = 0; k <= TARGET; k++)
                    {
                        int sum = i + j + k;
                        if (sum == TARGET)
                        {
                            set.Add((i, j, k));
                        }
                    }
                }
            }

            int ans = 0;
            foreach (var item in set)
            {
                ans += item.a.ToString().Count(c => c == '1');
                ans += item.b.ToString().Count(c => c == '1');
                ans += item.c.ToString().Count(c => c == '1');
            }

            return ans;
        }
    }
}
