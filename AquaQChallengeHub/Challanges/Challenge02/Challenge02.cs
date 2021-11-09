using AquaQChallengeHub.SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge02
{
    public class Challenge02 : BaseChallenge<int>
    {
        private string input = "";

        protected override int SolveChallenge()
        {
            string[] sections = input.Split(' ');

            int ans = 0, n = sections.Length;
            for (int i = 0; i < n; i++)
            {
                string current = sections[i];
                for (int j = n - 1; j > i; j--)
                {
                    string next = sections[j];
                    if (current == next)
                    {
                        i = j;
                        break;
                    }
                }
                ans += int.Parse(current);
            }

            return ans;
        }
        
        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            input = File.ReadAllLines(path).First();
        }
    }
}
