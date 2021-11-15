using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge23
{
    public class Challenge23 : BaseChallenge<string>
    {
        private const bool SHOULD_USE_PADDING_FOR_DECRYPTING = false; //AquaQ Challenge Hub requires this true for correct answer to the challenge
        private const int MATRIX_SIZE = 5;
        private string _keyword;
        private string _text;

        protected override string SolveChallenge()
        {
            Console.WriteLine($"Before encrypting: {_text}");
            var encrypted = Encrypt(_text, _keyword);
            Console.WriteLine($"Encrypted: {encrypted}");
            var decrypted = Decrypt(!SHOULD_USE_PADDING_FOR_DECRYPTING ? encrypted : _text, _keyword);
            return decrypted;
        }

        private static string Decrypt(string encryptedText, string keyword)
        {
            Debug.Assert(encryptedText.Length % 2 == 0);
            char[][] grid = ConvertKeywordToMatrix(keyword, MATRIX_SIZE);
            List<string> pairs = GeneratePairs(encryptedText);

            string decrypted = string.Empty;
            foreach (var twoLetters in pairs)
            {
                if (IsSameRow(grid, ref decrypted, twoLetters, false)) continue;
                if (IsSameColumn(grid, ref decrypted, twoLetters, false)) continue;
                decrypted = Box(grid, decrypted, twoLetters);
            }

            string copy = decrypted;
            if (!SHOULD_USE_PADDING_FOR_DECRYPTING)
            {
                if (copy.Last() == 'x') 
                    copy = copy.Remove(copy.Length - 1);

                for (int i = 1; i < copy.Length - 1; i++)
                {
                    char prev = copy[i - 1], next = copy[i + 1];
                    if (prev == next)
                        copy = copy.Remove(i, 1);
                }
            }

            return copy;
        }

        /*
         * 1. If the two letters are in the same row, the new letters are the letters directly to the right of the input letters (wrapping around if necessary)
         * 2. If the two letters are in the same column, the new letters are the letters directly below the input letters (wrapping as above)
         * 3. If the letters are separated diagonally, they form the corners of a "box". The encrypted letters are the letters on the laterally opposite end of this box to the input (i.e. look to the far left or right of the input while staying within the box)
         */
        private static string Encrypt(string plainText, string keyword)
        {
            char[][] grid = ConvertKeywordToMatrix(keyword, MATRIX_SIZE);
            List<string> prepared = PrepareLookup(plainText);

            string encrypted = string.Empty;
            foreach (var twoLetters in prepared)
            {
                if (IsSameRow(grid, ref encrypted, twoLetters, true)) continue;
                if (IsSameColumn(grid, ref encrypted, twoLetters, true)) continue;
                encrypted = Box(grid, encrypted, twoLetters);
            }

            return encrypted;
        }

        private static bool IsSameRow(char[][] grid, ref string text, string twoLetters, bool isEncrypt)
        {
            int h = grid.Length, w = grid.First().Length;
            for (int i = 0; i < h; i++)
            {
                if (twoLetters.All(grid[i].Contains))
                {
                    string temp = new string(grid[i]);
                    foreach (var c in twoLetters)
                    {
                        int index = temp.IndexOf(c);
                        int dy = isEncrypt ? Mod(index + 1, w) : Mod(index - 1, w);
                        text += grid[i][dy];
                    }
                    return true;
                }
            }

            return false;
        }

        private static bool IsSameColumn(char[][] grid, ref string text, string twoLetters, bool isEncrypt)
        {
            int h = grid.Length, w = grid.First().Length;
            for (int i = 0; i < h; i++)
            {
                List<(char c, int x, int y)> temp = new();
                for (int j = w - 1; j >= 0 && temp.Count < 2; j--)
                    if (twoLetters.Any(c => c == grid[j][i]))
                        temp.Add((twoLetters.First(c => c == grid[j][i]), j, i));

                if (temp.Count == 2)
                {
                    if (temp.First().c != twoLetters.First())
                        temp.Reverse();

                    foreach (var (c, x, y) in temp)
                    {
                        int dx = isEncrypt ? Mod(x + 1, h) : Mod(x - 1, h);
                        text += grid[dx][y];
                    }

                    return true;
                }
            }

            return false;
        }

        private static int Mod(int value, int mod) //To handle negative numbers with mod
        {
            return (value % mod + mod) % mod;
        }

        private static string Box(char[][] grid, string text, string twoLetters)
        {
            int h = grid.Length, w = grid.First().Length;
            List<(char c, int x, int y)> box = new();
            foreach (var c in twoLetters)
            {
                bool isFound = false;
                for (int i = 0; i < h && !isFound; i++)
                {
                    for (int j = 0; j < w && !isFound; j++)
                        if (grid[i][j] == c)
                            box.Add((c, i, j));
                }
            }

            if (box.First().c != twoLetters.First())
                box.Reverse();

            var first = box.First();
            var last = box.Last();
            Debug.Assert(first.x != last.x);
            Debug.Assert(first.y != last.y);
            text += grid[first.x][last.y];
            text += grid[last.x][first.y];
            return text;
        }

        private static List<string> GeneratePairs(string word)
        {
            List<string> pairs = new();
            for (int i = 1; i < word.Length; i += 2)
            {
                char prev = word[i - 1], current = word[i];
                pairs.Add($"{prev}{current}");
            }
            return pairs;
        }

        private static List<string> PrepareLookup(string text)
        {
            string lookup = text.Replace("j", string.Empty).Replace(" ", string.Empty);
            string copy = lookup;
            for (int i = 1; i < lookup.Length; i++)
            {
                char prev = lookup[i - 1], current = lookup[i];
                if (prev == current) copy = copy.Insert(i, "x");
            }
            if (copy.Length % 2 == 1) copy = copy.PadRight(copy.Length + 1, 'x');

            List<string> result = GeneratePairs(copy);
            return result;
        }

        private static char[][] ConvertKeywordToMatrix(string keyword, int size)
        {
            string distinct = new string(keyword.Distinct().ToArray()).Replace("j", string.Empty).Replace(" ", string.Empty);
            HashSet<char> alphabet = distinct.ToHashSet();
            for (char i = 'a'; i <= 'z'; i++)
                if (i != 'j' && !alphabet.Contains(i))
                    distinct += i;

            int index = 0;
            char[][] result = new char[size][];
            for (int i = 0; i < size; i++)
            {
                result[i] = new char[size];
                for (int j = 0; j < size; j++)
                    result[i][j] = distinct[index++];
            }

            return result;
        }

        protected override void ReadData()
        {
            bool useInput = false;
            _keyword = useInput ? "power plant" : "playfair";
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample3")}.txt";
            _text = File.ReadAllLines(path).First();
        }
    }
}
