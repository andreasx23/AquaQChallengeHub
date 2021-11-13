using AlgoKit.Collections.Heaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge33
{
    public class Challenge33 : BaseChallenge<int>
    {
        private int _input;
        private List<int> _dartboard;

        protected override int SolveChallenge() //Unsolved
        {
            int ans = 0;

            for (int i = 1; i <= _input; i++)
            {
                int bfs = BFS(i);
                ans += bfs;
            }

            return ans;
        }

        private int BFS(int target)
        {
            Comparer<int> comparer = Comparer<int>.Default;
            PairingHeap<int, int> queue = new(comparer); //Key: Darts - Value: remaining score

            foreach (var point in _dartboard)
            {
                if (target >= point)
                {
                    int remaining = target % point;
                    int darts = target / point;
                    queue.Add(darts, remaining);
                }
            }

            while (!queue.IsEmpty)
            {
                PairingHeapNode<int, int> current = queue.Pop();

                if (current.Value == 0)
                    return current.Key;

                foreach (var point in _dartboard)
                {
                    if (current.Value >= point)
                    {
                        int remaining = current.Value % point;
                        int darts = current.Value / point + current.Key;
                        queue.Add(darts, remaining);
                    }
                }
            }

            return 0;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _input = File.ReadAllLines(path).Select(int.Parse).First();
            string dartboard = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\dartboard.txt";
            _dartboard = File.ReadAllLines(dartboard).Select(row => row.Split(' ').Select(int.Parse)).First().Distinct().ToList();
        }
    }
}
