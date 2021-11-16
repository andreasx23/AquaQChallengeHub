using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge32
{
    public class Challenge32 : BaseChallenge<int>
    {
        private List<string> _input;

        protected override int SolveChallenge()
        {
            int ans = 0;

            foreach (var w in _input)
            {
                Stack<char> stack = new();
                bool isInvalid = false;
                foreach (var c in w)
                {
                    switch (c)
                    {
                        case '(':
                        case '[':
                        case '{':
                            stack.Push(c);
                            break;
                        case ')':
                            if (stack.Count > 0 && stack.Peek() == '(') 
                                stack.Pop();
                            else
                                isInvalid = true;
                            break;
                        case ']':
                            if (stack.Count > 0 && stack.Peek() == '[')
                                stack.Pop();
                            else
                                isInvalid = true;
                            break;
                        case '}':
                            if (stack.Count > 0 && stack.Peek() == '{') 
                                stack.Pop();
                            else
                                isInvalid = true;
                            break;
                    }
                    if (isInvalid) break;
                }

                if (!isInvalid && stack.Count == 0) ans++;
            }

            return ans;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _input = File.ReadAllLines(path).ToList();
        }
    }
}
