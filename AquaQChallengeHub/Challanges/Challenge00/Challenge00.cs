using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge00
{
    public class Challenge00 : BaseChallenge<string>
    {
        private readonly Dictionary<string, string> _phone = new()
        {
            { "1", "" },
            { "2", "abc" },
            { "3", "def" },
            { "4", "ghi" },
            { "5", "jkl" },
            { "6", "mno" },
            { "7", "pqrs" },
            { "8", "tuv" },
            { "9", "wxyz" },
            { "0", " " },
        };
        private List<string> input = new();

        protected override string SolveChallenge()
        {
            string ans = string.Empty;
            foreach (var s in input)
            {
                string[] split = s.Split(' ');
                string values = _phone[split.First()];
                int index = int.Parse(split.Last()) - 1;
                ans += values[index].ToString();
            }

            return ans;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            input = File.ReadAllLines(path).ToList();
        }
    }
}
