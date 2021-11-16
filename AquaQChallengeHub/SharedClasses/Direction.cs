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

            if (x < 0 || x >= h || y < 0 || y >= w)
                return false;
            else
                return true;
        }
    }
}
