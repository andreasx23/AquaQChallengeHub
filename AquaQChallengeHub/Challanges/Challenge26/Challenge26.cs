using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge26
{
    public class Challenge26 : BaseChallenge<BigInteger>
    {
        private List<string> _values;

        protected override BigInteger SolveChallenge()
        {
            BigInteger ans = 0;

            foreach (var s in _values)
            {
                int n = s.Length;
                Dictionary<int, List<int>> map = GetIndexes(s);
                BigInteger actualNumber = BigInteger.Parse(s);
                BigInteger currentNumber = actualNumber;
                bool firstRun = true;
                for (int i = 0; i < n + 1; i++)
                {
                    bool isMatch = false;
                    string sCurrentNumber = currentNumber.ToString();
                    for (int j = n - 1; j >= 0; j--)
                    {
                        int current = firstRun ? int.Parse(s[j].ToString()) : int.Parse(sCurrentNumber[j].ToString());
                        foreach (var kv in map.Where(kv => kv.Key > current))
                        {
                            foreach (var index in kv.Value)
                            {
                                char[] array = firstRun ? s.ToCharArray() : sCurrentNumber.ToCharArray();
                                array[index] = char.Parse(current.ToString());
                                array[j] = char.Parse(kv.Key.ToString());
                                BigInteger possible = BigInteger.Parse(new string(array));
                                if (firstRun && possible > actualNumber || !firstRun && possible > actualNumber && possible < currentNumber)
                                {
                                    currentNumber = possible;
                                    firstRun = false;
                                    isMatch = true;
                                    break;
                                }
                            }
                            if (isMatch) break;
                        }
                        if (isMatch) break;
                    }
                    map.Clear();
                    map = GetIndexes(currentNumber.ToString());
                }
                ans += currentNumber - actualNumber;
            }

            return ans;
        }

        private static Dictionary<int, List<int>> GetIndexes(string s)
        {
            Dictionary<int, List<int>> map = new(); //Num -- Indexes
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                var c = s[i];
                var val = int.Parse(c.ToString());
                if (!map.ContainsKey(val)) map.Add(val, new List<int>());
                map[val].Add(i);
            }
            map = map.OrderBy(kv => kv.Key).ToDictionary(k => k.Key, v => v.Value);
            return map;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _values = File.ReadAllLines(path).ToList();
        }
    }
}
