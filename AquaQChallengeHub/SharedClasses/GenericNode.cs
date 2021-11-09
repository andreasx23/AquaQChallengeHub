using AlgoKit.Collections.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.SharedClasses
{
    public class GenericNode<TKey>
    {
        public TKey Key { get; set; }
        public Dictionary<GenericNode<TKey>, int> Children { get; private set; } = new();

        /// <summary>
        /// Append a child to the Childrens map
        /// </summary>
        /// <param name="child">The child to append</param>
        /// <param name="cost">The cost to the child if none specified it will be defaulted to 1</param>
        public void AddChild(GenericNode<TKey> child, int cost = 1)
        {
            if (!Children.ContainsKey(child))
                Children.Add(child, cost);
        }

        /// <summary>
        /// Calculate cheapest cost to target
        /// </summary>
        /// <param name="target">The target to find</param>
        /// <returns>-1 if not found else cost</returns>
        public virtual int CostToTarget(GenericNode<TKey> target)
        {
            Comparer<int> comparer = Comparer<int>.Default;
            PairingHeap<int, GenericNode<TKey>> queue = new(comparer);
            HashSet<(GenericNode<TKey> left, GenericNode<TKey> right)> isVisited = new();
            foreach (var kv in Children)
            {
                int cost = kv.Value;
                queue.Add(cost, kv.Key);
                isVisited.Add((this, kv.Key));
            }

            int ans = -1;
            while (!queue.IsEmpty)
            {
                PairingHeapNode<int, GenericNode<TKey>> current = queue.Pop();

                if (Compare(current.Value.Key, target.Key))
                {
                    ans = current.Key;
                    break;
                }

                foreach (var kv in current.Value.Children)
                {
                    int cost = current.Key + kv.Value;
                    if (isVisited.Add((current.Value, kv.Key)))
                        queue.Add(cost, kv.Key);
                }
            }

            return ans;
        }

        /// <summary>
        /// Shortest path to target
        /// </summary>
        /// <param name="target">The target to find</param>
        /// <returns>null if no path found else a list of nodes with the path</returns>
        public virtual List<GenericNode<TKey>> PathToTarget(GenericNode<TKey> target)
        {
            Comparer<int> comparer = Comparer<int>.Default;
            PairingHeap<int, (GenericNode<TKey> node, List<GenericNode<TKey>> path)> queue = new(comparer);
            HashSet<(GenericNode<TKey> left, GenericNode<TKey> right)> isVisited = new();
            foreach (var kv in Children)
            {
                isVisited.Add((this, kv.Key));
                queue.Add(kv.Value, (kv.Key, new List<GenericNode<TKey>>() { kv.Key }));
            }

            while (!queue.IsEmpty)
            {
                PairingHeapNode<int, (GenericNode<TKey> node, List<GenericNode<TKey>> path)> current = queue.Pop();

                if (Compare(current.Value.node.Key, target.Key))
                    return current.Value.path;

                foreach (var kv in current.Value.node.Children)
                {
                    if (isVisited.Add((current.Value.node, kv.Key)))
                        queue.Add(kv.Value, (kv.Key, new List<GenericNode<TKey>>(current.Value.path) { kv.Key }));
                }
            }

            return null;
        }

        private static bool Compare<T>(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }
}
