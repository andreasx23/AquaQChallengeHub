using AquaQChallengeHub.Bases;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge35
{
    public class Challenge35 : BaseChallenge<string>
    {
        private List<string> _codeWords;
        private string _text;
        private HashSet<string> _englishWords;

        protected override string SolveChallenge()
        {
            string ans = string.Empty;

            if (_text.Last() == '#') _text = _text.Remove(_text.Length - 1);

            int max = 0;            
            string text = string.Empty;
            Parallel.ForEach(_codeWords, codeWord =>
            {
                var decrypt = Decrypt(_text, codeWord);
                var count = decrypt.Split(' ').Count(w => _englishWords.Contains(w));
                if (count > max)
                {
                    max = count;
                    ans = codeWord;
                    text = decrypt;
                }
            });

            //Console.WriteLine(text);

            return ans;
        }

        private static void Print(char[][] grid)
        {
            foreach (var item in grid)
                Console.WriteLine(string.Join("", item));
            Console.WriteLine();
        }

        private static string Encrypt(string textToEncrypt, string codeWord)
        {
            char[][] grid = GenerateGrid(textToEncrypt, codeWord);
            int[] order = GenerateOrder(codeWord);
            char[][] column = ConvertToColumn(grid, order);
            string flatten = Flatten(column);
            return flatten;
        }

        private static string Decrypt(string textToDecrypt, string codeWord)
        {
            char[][] columns = ConvertFlattenToColumns(textToDecrypt, codeWord);
            string flatten = Flatten(columns);
            return flatten;
        }

        private static char[][] ConvertFlattenToColumns(string flatten, string codeWord)
        {
            int h = flatten.Length / codeWord.Length, w = codeWord.Length, index = 0;
            char[][] grid = new char[w][];
            for (int i = 0; i < w; i++)
            {
                grid[i] = new char[h];
                for (int j = 0; j < h; j++)
                    grid[i][j] = flatten[index++];
            }

            List<char[]> storage = new();
            var order = GenerateOrder(codeWord);
            for (int i = 0; i < order.Length; i++)
            {
                var orderIndex = order[i];
                storage.Add(grid[orderIndex]);
            }

            char[][] temp = new char[h][];
            for (int i = 0; i < h; i++)
            {
                temp[i] = new char[w];
                for (int j = 0; j < w; j++)
                    temp[i][j] = storage[j][i];
            }

            return temp;
        }

        private static string Flatten(char[][] grid)
        {
            StringBuilder sb = new();
            foreach (var word in grid)
                sb.Append(word);
            return sb.ToString();
        }

        private static char[][] ConvertToColumn(char[][] grid, int[] order)
        {
            int h = grid.Length, w = grid.First().Length;
            char[][] result = new char[w][];
            for (int i = 0; i < w; i++)
            {
                char[] column = new char[h];
                for (int j = 0; j < h; j++)
                    column[j] = grid[j][i];
                int index = order[i];
                result[index] = column;
            }
            return result;
        }

        private static int[] GenerateOrder(string codeWord)
        {
            int n = codeWord.Length, index = 0;
            int[] order = new int[codeWord.Length];
            for (int i = 0; i < n; i++)
                order[i] = -1;

            foreach (var c in codeWord.OrderBy(c => c))
            {
                for (int i = 0; i < n; i++)
                {
                    if (codeWord[i] == c && order[i] == -1)
                    {
                        order[i] = index++;
                        break;
                    }
                }
            }
            return order;
        }

        private static char[][] GenerateGrid(string text, string codeWord)
        {
            int h = text.Length / codeWord.Length, w = codeWord.Length, index = 0;
            char[][] grid = new char[h][];
            for (int i = 0; i < h; i++)
            {
                grid[i] = new char[w];
                for (int j = 0; j < w; j++)
                    grid[i][j] = text[index++];
            }
            return grid;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _text = File.ReadAllLines(path).First().ToLower();
            string wordListPath = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\codeWords.txt";
            _codeWords = useInput ? File.ReadAllLines(wordListPath).ToList() : new List<string>() { "GLASS", "LEVER" };
            string wordsPath = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\englishWords.txt";
            _englishWords = File.ReadAllLines(wordsPath).Select(s => s.ToLower()).ToHashSet();
        }
    }
}
