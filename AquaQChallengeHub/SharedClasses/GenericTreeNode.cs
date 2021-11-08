using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.SharedClasses
{
    public class GenericTreeNode<TValue>
    {
        public GenericTreeNode<TValue> Left { get; set; }
        public GenericTreeNode<TValue> Right { get; set; }
        public GenericTreeNode<TValue> Parent { get; set; }
        public TValue Value { get; set; }
        public int Depth { get; set; }
        public bool IsLeaf => Left == null && Right == null;

        public void PreOrderTraversal()
        {
            List<TValue> values = new();
            PreOrderTraversal(this, values);
            Console.WriteLine(string.Join(", ", values));
        }

        protected virtual void PreOrderTraversal(GenericTreeNode<TValue> root, List<TValue> values)
        {
            if (root == null) return;
            values.Add(root.Value);
            PreOrderTraversal(root.Left, values);
            PreOrderTraversal(root.Right, values);
        }

        public void InOrderTraversal()
        {
            List<TValue> values = new();
            InOrderTraversal(this, values);
            Console.WriteLine(string.Join(", ", values));
        }

        protected virtual void InOrderTraversal(GenericTreeNode<TValue> root, List<TValue> values)
        {
            if (root == null) return;
            InOrderTraversal(root.Left, values);
            values.Add(root.Value);
            InOrderTraversal(root.Right, values);
        }

        public void PostOrderTraversal()
        {
            List<TValue> values = new();
            PostOrderTraversal(this, values);
            Console.WriteLine(string.Join(", ", values));
        }

        protected virtual void PostOrderTraversal(GenericTreeNode<TValue> root, List<TValue> values)
        {
            if (root == null) return;
            PostOrderTraversal(root.Left, values);
            PostOrderTraversal(root.Right, values);
            values.Add(root.Value);
        }
    }
}
