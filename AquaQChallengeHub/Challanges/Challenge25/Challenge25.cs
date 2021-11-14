using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge25
{
    public class Challenge25 : BaseChallenge<string>
    {
        private Dictionary<char, string> _morseCode = new();
        private List<DateTime> _clicks = new();

        protected override string SolveChallenge() //Unsolved
        {
            string ans = "";

            List<string> words = new();
            StringBuilder sb = new();
            DateTime? prev = null;
            foreach (var date in _clicks)
            {
                if (date == DateTime.MinValue)
                {
                    words.Add(sb.ToString());
                    sb.Clear();
                    prev = null;
                    return "";
                }
                else
                {
                    if (prev == null)
                    {
                        prev = date;
                    }
                    else
                    {
                        var diff = date.Subtract(prev.Value);

                        Console.WriteLine(diff.TotalSeconds);
                        prev = date;
                    }
                }
            }

            return ans;
        }

        protected override void ReadData()
        {
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\input.txt";
            _clicks = File.ReadAllLines(path).Select(s =>
            {
                return !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s) ? DateTime.Parse(s) : DateTime.MinValue;
            }).ToList();
            string morseCodePath = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\morseCode.txt";
            _morseCode = File.ReadAllLines(morseCodePath).Select(row => row.Split(',')).ToDictionary(k => k.First().First(), v => $"{v.Last()} ");
            _morseCode.Add(' ', "  ");
        }
    }
}
