using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge13
{
    public class Challenge13 : BaseChallenge<int>
    {
        private List<string> _words = new();

        protected override int SolveChallenge()
        {
            int ans = 0;

            foreach (var word in _words)
            {
                int n = word.Length;
                Dictionary<string, int> map = new();
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j <= n; j++)
                    {
                        string substring = word.Substring(i, j - i);
                        if (map.ContainsKey(substring)) continue;
                        int count = SubstringCount(word, substring);
                        map.Add(substring, count);
                    }
                }
                ans += map.OrderByDescending(kv => kv.Value).First().Value;
            }

            return ans;
        }

        private int SubstringCount(string word, string substring)
        {
            int currentMax = 0, currentCount = 0, n = word.Length, m = substring.Length;
            for (int i = 0; i <= n - m; i++)
            {
                string sub = word[i] == substring[0] ? word.Substring(i, m) : string.Empty;
                if (sub == substring)
                {
                    currentCount++;
                    i += m - 1;
                }
                else
                {
                    currentMax = Math.Max(currentMax, currentCount);
                    currentCount = 0;
                }
            }
            return Math.Max(currentMax, currentCount);
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _words = File.ReadAllLines(path).ToList();
        }
    }
}
