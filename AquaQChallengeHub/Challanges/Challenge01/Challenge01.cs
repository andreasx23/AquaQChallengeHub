using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge01
{
    public class Challenge01 : BaseChallenge<string>
    {
        private string input;

        protected override string SolveChallenge() //Unsolved
        {
            ConvertNonHex();
            string temp = input;
            for (int i = 0; i < 3; i++)
            {
                temp += input;
            }
            Console.WriteLine(temp.Length);
            Console.WriteLine(temp);
            return "";
            string test = input.PadLeft(input.Length * 3);

            int take = temp.Length / 3;
            string test2 = "";
            for (int i = 0; i < 3; i++)
            {
                test2 += temp.Substring(i * take, take) + " ";
            }
            Console.WriteLine(test2);
            string ans = "";

            return ans;
        }

        private void ConvertNonHex()
        {
            string temp = string.Empty;
            foreach (var c in input)
            {
                if (!char.IsLetterOrDigit(c) || !char.IsDigit(c) && c > 'f')
                    temp += "0";
                else
                    temp += c.ToString();
            }
            input = temp;
        }

        protected override void ReadData()
        {
            bool useInput = false;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            input = File.ReadAllLines(path).First();
        }
    }
}
