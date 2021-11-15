using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge22
{
    public class Challenge22 : BaseChallenge<int>
    {
        private List<int> _numbers = new();

        protected override int SolveChallenge()
        {
            int ans = 0;

            foreach (var n in _numbers)
            {
                var roman = ConvertToRoman(n);
                var caesarCipher = CaesarCipher(roman);
                ans += caesarCipher;
            }

            return ans;
        }

        private static int CaesarCipher(string num)
        {
            Dictionary<char, int> values = new();
            for (char i = 'A'; i <= 'Z'; i++) 
                values.Add(i, i - 'A' + 1);

            int result = 0;
            foreach (var c in num)
                result += values[c];

            return result;
        }

        private static string ConvertToRoman(int num)
        {
            Dictionary<string, int> roman = new()
            {
                { "M", 1000 },
                { "CM", 900 },
                { "D", 500 },
                { "CD", 400 },
                { "C", 100 },
                { "XC", 90 },
                { "L", 50 },
                { "XL", 40 },
                { "X", 10 },
                { "IX", 9 },
                { "V", 5 },
                { "IV", 4 },
                { "I", 1 },
            };

            string result = string.Empty;
            foreach (var kv in roman)
            {
                int times = num / kv.Value;
                for (int i = 0; i < times; i++)
                    result += kv.Key;
                num %= kv.Value;
            }

            return result;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _numbers = File.ReadAllLines(path).First().Split(' ').Select(int.Parse).ToList();
        }
    }
}
