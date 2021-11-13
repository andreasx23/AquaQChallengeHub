using AquaQChallengeHub.Bases;
using AquaQChallengeHub.SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge24
{
    public class Challenge24 : BaseChallenge<string>
    {
        private class TreeNode : GenericTreeNode<int>
        {
            public string Key { get; set; }

            public new void PreOrderTraversal()
            {
                PreOrderTraversal(this);
            }

            private void PreOrderTraversal(TreeNode root)
            {
                if (root == null) return;
                Console.WriteLine(root.Key + " " + root.Value);
                PreOrderTraversal((TreeNode)root.Left);
                PreOrderTraversal((TreeNode)root.Right);
            }
        }

        private string _input;
        private string _bits;

        protected override string SolveChallenge()
        {
            TreeNode root = ConstructTree();
            TreeNode placeholderRoot = root;

            root.PreOrderTraversal();

            string ans = string.Empty;
            foreach (var c in _bits)
            {
                if (c == '0')
                    root = (TreeNode)root.Left;
                else
                    root = (TreeNode)root.Right;

                if (root.IsLeaf)
                {
                    ans += root.Key;
                    root = placeholderRoot;
                }
            }

            return ans;
        }

        private TreeNode ConstructTree()
        {
            Dictionary<string, int> nodes = new();
            foreach (var c in _input)
            {
                string val = c.ToString();
                if (!nodes.ContainsKey(val)) nodes.Add(val, 0);
                nodes[val]++;
            }
            nodes = nodes.OrderBy(kv => kv.Value).ThenBy(kv => kv.Key.First()).ToDictionary(k => k.Key, v => v.Value);

            Dictionary<string, TreeNode> tree = new();
            List<string> keys = nodes.Keys.ToList();
            while (keys.Count > 1)
            {
                string leftKey = keys[0];
                int leftValue = nodes[leftKey];
                string rightKey = keys[1];
                int rightValue = nodes[rightKey];

                if (!tree.TryGetValue(leftKey, out TreeNode left))
                {
                    left = new() { Key = leftKey, Value = leftValue };
                    tree.Add(leftKey, left);
                }

                if (!tree.TryGetValue(rightKey, out TreeNode right))
                {
                    right = new() { Key = rightKey, Value = rightValue };
                    tree.Add(rightKey, right);
                }

                TreeNode parent = new() { Key = left.Key + right.Key, Value = left.Value + right.Value, Left = left, Right = right };
                tree.Add(parent.Key, parent);

                nodes.Add(parent.Key, parent.Value);
                nodes.Remove(leftKey);
                nodes.Remove(rightKey);
                nodes = nodes.OrderBy(kv => kv.Value).ToDictionary(k => k.Key, v => v.Value);
                keys = nodes.Keys.ToList();
            }

            TreeNode root = tree[keys.First()];
            return root;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var lines = File.ReadAllLines(path);
            _input = lines.First();
            _bits = lines.Last();
        }
    }
}
