using AquaQChallengeHub.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Bases
{
    public abstract class BaseChallenge<TValue> : IChallenge
    {
        protected abstract TValue SolveChallenge();
        protected virtual void ReadData() { } //No body since not always needed

        public void TestCase()
        {
            Stopwatch watch = Stopwatch.StartNew();
            ReadData();
            TValue ans = SolveChallenge();
            Console.WriteLine($"Challange took: {watch.ElapsedMilliseconds} ms to run, the answer is: {ans}");
        }
    }
}
