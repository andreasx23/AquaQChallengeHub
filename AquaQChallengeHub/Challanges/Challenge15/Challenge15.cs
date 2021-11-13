using AquaQChallengeHub.Bases;
using AquaQChallengeHub.SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge15
{
    public class Challenge15 : BaseChallenge<long>
    {
        private readonly Dictionary<GenericNode<string>, GenericNode<string>> _instructions = new();

        protected override long SolveChallenge()
        {
            long ans = 1;
            foreach (var kv in _instructions)
                ans *= kv.Key.CostToTarget(kv.Value) + 1; //Plus one to include myself
            return ans;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var targets = File.ReadAllLines(path);
            string wordList = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\words.txt";
            var words = File.ReadAllLines(wordList);

            Dictionary<string, GenericNode<string>> nodes = new();
            foreach (var word in words)
                nodes.Add(word, new GenericNode<string>() { Key = word });

            foreach (var word in words)
            {
                GenericNode<string> parent = nodes[word];
                for (int i = 0; i < word.Length; i++)
                {
                    char[] array = word.ToCharArray();
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        array[i] = c;
                        string currentWord = new(array);
                        if (word != currentWord && nodes.TryGetValue(currentWord, out GenericNode<string> child))
                        {
                            parent.AddChild(child);
                            child.AddChild(parent);
                        }
                    }
                }
            }

            foreach (var s in targets)
            {
                var split = s.Split(',');
                var left = nodes[split.First()];
                var right = nodes[split.Last()];
                _instructions.Add(left, right);
            }
        }
    }
}
