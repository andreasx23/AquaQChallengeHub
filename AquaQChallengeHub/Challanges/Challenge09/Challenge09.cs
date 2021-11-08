using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge09
{
    public class Challenge09 : ChallengeBase<BigInteger>
    {
        private List<int> _values = new();

        protected override BigInteger SolveChallenge()
        {
            BigInteger ans = 1;
            foreach (var item in _values)
                ans *= item;
            return ans;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _values = File.ReadAllLines(path).Select(int.Parse).ToList();
        }

    }
}
