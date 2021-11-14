using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge12
{
    public class Challenge12 : BaseChallenge<int>
    {
        private List<List<int>> _instructions = new();

        protected override int SolveChallenge()
        {
            int currentFloor = 0, dir = _instructions[currentFloor][0];
            List<int> floor = new() { currentFloor };
            while (currentFloor < _instructions.Count)
            {
                var instruction = _instructions[currentFloor];

                if (instruction[0] == dir && dir == 0)
                    dir = 1;
                else if (dir == 1 && instruction[0] == 0)
                    dir = 0;

                var floors = instruction[1];
                if (dir == 0)
                    currentFloor -= floors;
                else
                    currentFloor += floors;

                floor.Add(currentFloor);
            }

            return floor.Count;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _instructions = File.ReadAllLines(path).Select(row => row.Split(' ').Select(int.Parse).ToList()).ToList();
        }
    }
}
