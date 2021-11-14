using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge16
{
    public class Challenge16 : BaseChallenge<int>
    {
        private const int H = 6;
        private const int W = 7;
        private const int SPACING = 2;

        private readonly Dictionary<char, char[][]> _alphabet = new();
        private string _word;

        protected override int SolveChallenge()
        {
            List<List<char>> grid = new();

            //Console.WriteLine(_word.Substring(_word.Length - 10));

            _word = string.Empty;
            for (int i = 0; i < 1; i++)
            {
                string temp = string.Empty;
                for (char j = 'A'; j <= 'Z'; j++)
                {
                    temp += j;
                }

                Random rand = new();
                HashSet<int> taken = new();
                while (taken.Count != temp.Length)
                {
                    int num = rand.Next(0, temp.Length);
                    while (taken.Contains(num)) num = rand.Next(0, temp.Length);
                    _word += temp[num];
                    taken.Add(num);
                }
            }

            for (int i = 0; i < H; i++)
            {
                grid.Add(new List<char>());
                for (int j = 0; j < W * _word.Length; j++)
                {
                    grid[i].Add('.');
                }
            }

            int pointer = 0;
            foreach (var c in _word)
            {
                var ascii = _alphabet[c];
                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        grid[i][j + pointer] = ascii[i][j];
                    }
                }

                //Print(grid);

                if (pointer > 0)
                {
                    while (true)
                    {
                        List<int> indexes = new();
                        for (int i = 0; i < H; i++)
                        {
                            for (int j = 0; j < W; j++)
                            {
                                int index = j + pointer;
                                if (index + 1 < grid.First().Count && grid[i][index] == '#')
                                {
                                    if (grid[i][index - SPACING] == '.') indexes.Add(index - SPACING);
                                    break;
                                }
                            }
                        }

                        //Print(grid);

                        if (indexes.Count == H)
                        {
                            pointer--;
                            for (int i = 0; i < indexes.Count; i++)
                                grid[i].RemoveAt(indexes[i]);

                            if (pointer == 0)
                                break;
                        }
                        else
                            break;
                    }
                }

                pointer += W;
            }

            
            while (true)
            {
                int count = 0;
                for (int i = 0; i < H; i++)
                {
                    if (grid[i].Last() == '.') count++;
                }

                if (count == H)
                {
                    for (int i = 0; i < H; i++)
                    {
                        grid[i].RemoveAt(grid[i].Count - 1);
                    }
                }
                else break;
            }

            Print(grid, false);
            Print(grid, true);

            return grid.Sum(row => row.Count(c => c == '.'));
        }

        private void Print(List<List<char>> grid, bool t)
        {
            foreach (var item in grid)
            {
                var temp = t ? new string(item.ToArray()).Replace(".", " ") : new string(item.ToArray());
                Console.WriteLine(temp);
            }
            Console.WriteLine();
        }

        protected override void ReadData()
        {
            bool useInput = false;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample2")}.txt";
            _word = File.ReadAllLines(path).First();
            string alphabetPath = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\alphabet.txt";
            var alphabet = File.ReadAllLines(alphabetPath).ToList();

            int i = 0, letterIndex = 0;
            while (i < alphabet.Count)
            {
                char c = Convert.ToChar('A' + letterIndex++);
                List<string> strings = new();
                while (i < alphabet.Count && !string.IsNullOrEmpty(alphabet[i]))
                {
                    strings.Add(alphabet[i]);
                    i++;
                }
                i++;
                var ascii = GenerateAsciiLetter(strings);
                _alphabet.Add(c, ascii);
            }
        }

        private static char[][] GenerateAsciiLetter(List<string> letter)
        {
            char[][] ascii = new char[H][];
            for (int i = 0; i < H; i++)
            {
                ascii[i] = letter[i].PadRight(W).ToCharArray();
                for (int j = 0; j < W; j++)
                {
                    if (ascii[i][j] != '#') ascii[i][j] = '.';
                }
            }
            return ascii;
        }
    }
}
