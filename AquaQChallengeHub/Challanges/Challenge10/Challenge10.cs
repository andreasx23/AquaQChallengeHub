using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AlgoKit.Collections.Heaps;
using AquaQChallengeHub.SharedClasses;

namespace AquaQChallengeHub.Challanges.Challenge10
{
    public class Challenge10 : BaseChallenge<int>
    {
        private readonly Dictionary<string, GenericNode<string>> _nodes = new();

        protected override int SolveChallenge()
        {
            var me = _nodes["tupac"];
            var target = _nodes["diddy"];
            return me.CostToTarget(target);
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var lines = File.ReadAllLines(path).Skip(1);
            foreach (var item in lines)
            {
                var split = item.Split(',').ToList();
                if (!_nodes.TryGetValue(split[0].ToLower(), out GenericNode<string> left))
                {
                    left = new GenericNode<string>() { Key = split[0].ToLower() };
                    _nodes.Add(left.Key, left);
                }

                if (!_nodes.TryGetValue(split[1].ToLower(), out GenericNode<string> right))
                {
                    right = new GenericNode<string>() { Key = split[1].ToLower() };
                    _nodes.Add(right.Key, right);
                }

                int cost = int.Parse(split[2]);
                left.AddChild(right, cost);
            }
        }
    }
}
