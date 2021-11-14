using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge30
{
    public class Challenge30 : BaseChallenge<int>
    {
        private List<string> _games = new();

        protected override int SolveChallenge()
        {
            int ans = 0;

            foreach (var game in _games)
            {
                int n = game.Length;
                for (int i = 0; i < n; i++)
                    if (game[i] == '1') ans += DFS(game.ToList(), i);
            }
            
            return ans;
        }

        private int DFS(List<char> game, int index)
        {
            if (!game.Contains('1')) return game.All(c => c == '.') ? 1 : 0;
            else if (index == -1) return 0;

            if (index > 0) game[index - 1] = Flip(game[index - 1]);
            game[index] = '.';
            if (index + 1 < game.Count) game[index + 1] = Flip(game[index + 1]);
            return DFS(game, game.IndexOf('1'));
        }

        private static char Flip(char current)
        {
            if (current == '0') return '1';
            else if (current == '1') return '0';
            else return current;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _games = File.ReadAllLines(path).ToList();
        }
    }
}
