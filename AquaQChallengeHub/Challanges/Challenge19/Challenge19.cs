using AquaQChallengeHub.Bases;
using AquaQChallengeHub.SharedClasses;
using AquaQChallengeHub.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge19
{
    public class Challenge19 : BaseChallenge<int>
    {
        private class Game
        {
            public int Steps { get; set; }
            public int Size { get; set; }
            public List<(int x, int y)> Initial { get; set; } = new();
        }

        private readonly List<Game> _games = new();

        protected override int SolveChallenge()
        {
            int ans = 0;

            /*
             * Solved but could use memorization to find cycles for faster execution. Took me 18 min and 30 seconds to bruteforce
             * 
             * 30 answer: 16
             * 45 answer: 34
             * 99 answer: 44
             * 204 answer: 154
             * 429 answer: 625
             * 940 answer: 159
             * 1293 answer: 59
             * 8487 answer: 224
             * 23853 answer: 978
             * 97409 answer: 188
            */
            Parallel.ForEach(_games, game =>
            {
                Stopwatch watch = Stopwatch.StartNew();
                char[][] grid = GenerateGrid(game.Size, true);
                foreach (var (x, y) in game.Initial)
                    grid[x][y] = '#';

                for (int i = 0; i < game.Steps; i++)
                {
                    char[][] clone = GenerateGrid(game.Size, false);
                    for (int j = 0; j < game.Size; j++)
                    {
                        for (int k = 0; k < game.Size; k++)
                        {
                            int count = CountOn(grid, j, k);
                            clone[j][k] = count % 2 == 0 ? '.' : '#';
                        }
                    }

                    grid = clone;
                    if (i > 0 && i % game.Size == 0) Console.WriteLine($"{game.Steps} {game.Size} {string.Join(", ", game.Initial)} -- {watch.GetEta(i, game.Steps)} time remaining");
                }

                int cnt = grid.Sum(row => row.Count(c => c == '#'));
                Console.WriteLine($"{game.Steps} finished. Lights on count: {cnt}. This part took: {watch.ElapsedMilliseconds} ms");
                ans += cnt;
            });

            return ans;
        }

        private int CountOn(char[][] grid, int x, int y)
        {
            int count = 0;
            foreach ((int x, int y) item in Direction.WALK)
            {
                var dx = item.x + x;
                var dy = item.y + y;
                if (Direction.Inbounds(grid, dx, dy) && grid[dx][dy] == '#')
                    count++;
            }
            return count;
        }

        private void Print(char[][] grid)
        {
            foreach (var item in grid)
            {
                Console.WriteLine(string.Join("", item));
            }
            Console.WriteLine();
        }

        private char[][] GenerateGrid(int size, bool shouldPrintDots)
        {
            char[][] result = new char[size][];
            for (int i = 0; i < size; i++)
            {
                result[i] = new char[size];
                if (shouldPrintDots)
                {
                    for (int j = 0; j < size; j++)
                        result[i][j] = '.';
                }
            }
            return result;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var lines = File.ReadAllLines(path).Select(s => s.Split(' ').Select(int.Parse).ToList());
            foreach (var array in lines)
            {
                var steps = array[0];
                var size = array[1];
                Game game = new()
                {
                    Steps = steps,
                    Size = size
                };
                for (int i = 3; i < array.Count; i += 2)
                {
                    int x = array[i - 1], y = array[i];
                    game.Initial.Add((x, y));
                }
                _games.Add(game);
            }
        }
    }
}
