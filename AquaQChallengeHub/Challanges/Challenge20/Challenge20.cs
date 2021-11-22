using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge20
{
    public class Challenge20 : BaseChallenge<int>
    {
        private List<string> _cards;

        protected override int SolveChallenge()
        {
            int ans = 0;

            int total = 0;
            List<int> aceStates = new();
            foreach (var s in _cards)
            {
                int currentCount = aceStates.Count;
                switch (s)
                {
                    case "A":
                        if (currentCount > 0) //Gotta handle all states cuz any state might lead us to get 21
                        {
                            for (int i = 0; i < currentCount; i++)
                            {
                                aceStates.Add(aceStates[i] + 11);
                                aceStates[i] += 1;
                            }
                        }
                        else
                        {
                            aceStates.Add(total + 11);
                            aceStates.Add(total + 1);
                        }
                        break;
                    case "J":
                    case "Q":
                    case "K":
                        if (currentCount > 0)
                            for (int i = 0; i < currentCount; i++)
                                aceStates[i] += 10;
                        else
                            total += 10;
                        break;
                    default:
                        int num = int.Parse(s);
                        if (currentCount > 0)
                            for (int i = 0; i < currentCount; i++)
                                aceStates[i] += num;
                        else
                            total += num;
                        break;
                }

                if (aceStates.Count > 0)
                {
                    if (aceStates.Any(n => n == 21))
                    {
                        ans++;
                        total = 0;
                        aceStates.Clear();
                    }
                    else if (aceStates.All(n => n > 21))
                    {
                        total = 0;
                        aceStates.Clear();
                    }

                    aceStates.RemoveAll(n => n > 21);
                }
                else
                {
                    if (total == 21)
                    {
                        ans++;
                        total = 0;
                    }
                    else if (total > 21)
                    {
                        total = 0;
                    }
                }
            }

            return ans;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _cards = File.ReadAllLines(path).First().Split(' ').ToList();
        }
    }
}
