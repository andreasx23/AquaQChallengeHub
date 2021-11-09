using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.SharedClasses
{
    public enum Dirs
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        UPPERLEFT = 4,
        UPPERRIGHT = 5,
        LOWERLEFT = 6,
        LOWERRIGHT = 7
    }

    public static class Direction
    {
        public static List<(int x, int y)> WALK { get; private set; } = new() { (-1, 0), (1, 0), (0, -1), (0, 1) };
        public static List<(int x, int y)> WALK_WITH_DIAGONAL { get; private set; } = new() { (-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1) };

        public static (int x, int y) GetCoordinateForDirection(Dirs direction)
        {
            return WALK[(int)direction];
        }

        public static (int x, int y) GetCoordinateForDirectionWithDiagonal(Dirs direction)
        {
            return WALK_WITH_DIAGONAL[(int)direction];
        }

        public static bool Inbounds(ICollection maze, int x, int y)
        {
            int h = maze.Count, w;
            if (maze.GetType() == typeof(int[][]))
            {
                int[][] cast = (int[][])maze;
                w = cast.First().Length;
            }
            else if (maze.GetType() == typeof(char[][]))
            {
                char[][] cast = (char[][])maze;
                w = cast.First().Length;
            }
            else
            {
                throw new Exception("INVALID TYPE TO VERIFY BOUNDS FOR!");
            }

            foreach ((int x, int y) item in WALK)
            {
                int dx = x + item.x, dy = y + item.y;
                if (dx < 0 || dx >= h || dy < 0 || dy >= w) return false;
            }

            return true;
        }

        public static bool InboundsWithDiagonal(ICollection maze, int x, int y)
        {
            int h = maze.Count, w;
            if (maze.GetType() == typeof(int[][]))
            {
                int[][] cast = (int[][])maze;
                w = cast.First().Length;
            }
            else if (maze.GetType() == typeof(char[][]))
            {
                char[][] cast = (char[][])maze;
                w = cast.First().Length;
            }
            else
            {
                throw new Exception("INVALID TYPE TO VERIFY BOUNDS FOR!");
            }
                        
            foreach ((int x, int y) item in WALK_WITH_DIAGONAL)
            {
                int dx = x + item.x, dy = y + item.y;
                if (dx < 0 || dx >= h || dy < 0 || dy >= w) return false;
            }

            return true;
        }
    }
}
