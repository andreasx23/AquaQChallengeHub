using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge31
{
    public class Challenge31 : BaseChallenge<int>
    {
        private enum Face
        {
            FRONT = 1,
            UP = 2,
            LEFT = 3,
            RIGHT = 4,
            DOWN = 5,
            BACK = 6,
        }

        private readonly List<string> _instructions = new();

        /* 
         * Cube directions: https://medium.com/@venkatesh20pillay/how-to-solve-a-3x3-rubiks-cube-beginners-method-6ae65d8b721
         * Actual 3D rubik cuke online: https://stackoverflow.com/a/62558531/15853751
         * 
         * No idea what's wrong. Either the challenge https://challenges.aquaq.co.uk/challenge/31 is wrong or my solution is.
         * Current solution gives answer: 8640 to input and I previously made a wrong solution (had mulitple errors) and got answer: 7200 which was accepted by the website
         */
        protected override int SolveChallenge()
        {
            int[][][] cube = GenerateInitialCube(3);

            foreach (var instruction in _instructions)
            {
                DoInstruction(cube, instruction);
                //Console.WriteLine(instruction);
                //Print(cube);
                //Console.WriteLine();
            }

            Console.WriteLine(_instructions.Last());
            Print(cube);

            int[][] front = cube[GetFaceIndex(Face.FRONT)];
            int ans = 1;
            for (int i = 0; i < front.Length; i++)
                for (int j = 0; j < front[i].Length; j++)
                    ans *= front[i][j];
            return ans;
        }

        private void DoInstruction(int[][][] cube, string instruction)
        {
            bool isClockwise = !instruction.Contains("\'");
            switch (instruction)
            {
                case "F":
                case "F'":
                    RotateFront(cube, isClockwise);
                    break;
                case "L":
                case "L'":
                    RotateLeft(cube, isClockwise);
                    break;
                case "R":
                case "R'":
                    RotateRight(cube, isClockwise);
                    break;
                case "U":
                case "U'":
                    RotateUp(cube, isClockwise);
                    break;
                case "D":
                case "D'":
                    RotateDown(cube, isClockwise);
                    break;
                case "B":
                case "B'":
                    RotateBack(cube, isClockwise);
                    break;
                default:
                    throw new Exception("Invalid instruction");
            }
        }

        private void Print(int[][][] cube)
        {
            var up = cube[GetFaceIndex(Face.UP)];
            string space = "    ";
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(space + string.Join("", up[i]));
            }
            Console.WriteLine(space + "(U)");
            Console.WriteLine();

            string top = string.Empty, middle = string.Empty, bottom = string.Empty;
            var left = cube[GetFaceIndex(Face.LEFT)];
            top += string.Join("", left[0]) + " ";
            middle += string.Join("", left[1]) + " ";
            bottom += string.Join("", left[2]) + " ";

            var front = cube[GetFaceIndex(Face.FRONT)];
            top += string.Join("", front[0]) + " ";
            middle += string.Join("", front[1]) + " ";
            bottom += string.Join("", front[2]) + " ";

            var right = cube[GetFaceIndex(Face.RIGHT)];
            top += string.Join("", right[0]);
            middle += string.Join("", right[1]);
            bottom += string.Join("", right[2]);
            Console.WriteLine(top);
            Console.WriteLine(middle);
            Console.WriteLine(bottom);
            Console.WriteLine("(L)" + " " + "(F)" + " " + "(R)");
            Console.WriteLine();

            var down = cube[GetFaceIndex(Face.DOWN)];
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(space + string.Join("", down[i]));
            }
            Console.WriteLine(space + "(D)");
            Console.WriteLine();

            var back = cube[GetFaceIndex(Face.BACK)];
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(space + string.Join("", back[i]));
            }
            Console.WriteLine(space + "(B)");
        }

        private void RotateFront(int[][][] cube, bool isClockwise)
        {
            int n = cube.First().Length;
            List<int> right = new(), left = new(), down = new(), up = new();
            for (int i = 0; i < n; i++)
            {
                right.Add(cube[GetFaceIndex(Face.RIGHT)][i][0]);
                left.Add(cube[GetFaceIndex(Face.LEFT)][i][2]);
                down.Add(cube[GetFaceIndex(Face.DOWN)][0][i]);
                up.Add(cube[GetFaceIndex(Face.UP)][2][i]);
            }
            int[][] face = cube[GetFaceIndex(Face.FRONT)];

            if (isClockwise)
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.RIGHT)][i][0] = up[i];
                    cube[GetFaceIndex(Face.LEFT)][i][2] = down[i];
                    cube[GetFaceIndex(Face.UP)][2][i] = left[i];
                    cube[GetFaceIndex(Face.DOWN)][0][i] = right[i];
                }
                cube[GetFaceIndex(Face.FRONT)] = RotateFaceClockwise(face);
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.RIGHT)][i][0] = down[i];
                    cube[GetFaceIndex(Face.LEFT)][i][2] = up[i];
                    cube[GetFaceIndex(Face.UP)][2][i] = right[i];
                    cube[GetFaceIndex(Face.DOWN)][0][i] = left[i];
                }
                cube[GetFaceIndex(Face.FRONT)] = RotateFaceCounterClockwise(face);
            }
        }

        private void RotateBack(int[][][] cube, bool isClockwise)
        {
            int n = cube.First().Length;
            List<int> right = new(), left = new(), down = new(), up = new();
            for (int i = 0; i < n; i++)
            {
                right.Add(cube[GetFaceIndex(Face.RIGHT)][i][2]);
                left.Add(cube[GetFaceIndex(Face.LEFT)][i][0]);
                down.Add(cube[GetFaceIndex(Face.DOWN)][2][i]);
                up.Add(cube[GetFaceIndex(Face.UP)][0][i]);
            }
            int[][] face = cube[GetFaceIndex(Face.BACK)];

            if (isClockwise)
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.RIGHT)][i][2] = down[i];
                    cube[GetFaceIndex(Face.LEFT)][i][0] = up[i];
                    cube[GetFaceIndex(Face.DOWN)][2][i] = left[i];
                    cube[GetFaceIndex(Face.UP)][0][i] = right[i];
                }
                cube[GetFaceIndex(Face.BACK)] = RotateFaceClockwise(face);
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.RIGHT)][i][2] = up[i];
                    cube[GetFaceIndex(Face.LEFT)][i][0] = down[i];
                    cube[GetFaceIndex(Face.DOWN)][2][i] = right[i];
                    cube[GetFaceIndex(Face.UP)][0][i] = left[i];
                }
                cube[GetFaceIndex(Face.BACK)] = RotateFaceCounterClockwise(face);
            }
        }

        private void RotateLeft(int[][][] cube, bool isClockwise)
        {
            int n = cube.First().Length;
            List<int> front = new(), up = new(), back = new(), down = new();
            for (int i = 0; i < n; i++)
            {
                down.Add(cube[GetFaceIndex(Face.DOWN)][i][0]);
                back.Add(cube[GetFaceIndex(Face.BACK)][i][0]);
                up.Add(cube[GetFaceIndex(Face.UP)][i][0]);
                front.Add(cube[GetFaceIndex(Face.FRONT)][i][0]);
            }
            int[][] face = cube[GetFaceIndex(Face.LEFT)];

            if (isClockwise)
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.DOWN)][i][0] = front[i];
                    cube[GetFaceIndex(Face.BACK)][i][0] = down[i];
                    cube[GetFaceIndex(Face.UP)][i][0] = back[i];
                    cube[GetFaceIndex(Face.FRONT)][i][0] = up[i];
                }
                cube[GetFaceIndex(Face.LEFT)] = RotateFaceClockwise(face);
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.DOWN)][i][0] = back[i];
                    cube[GetFaceIndex(Face.BACK)][i][0] = up[i];
                    cube[GetFaceIndex(Face.UP)][i][0] = front[i];
                    cube[GetFaceIndex(Face.FRONT)][i][0] = down[i];
                }
                cube[GetFaceIndex(Face.LEFT)] = RotateFaceCounterClockwise(face);
            }
        }

        private void RotateRight(int[][][] cube, bool isClockwise)
        {
            int n = cube.First().Length;
            List<int> front = new(), up = new(), back = new(), down = new();
            for (int i = 0; i < n; i++)
            {
                up.Add(cube[GetFaceIndex(Face.UP)][i][2]);
                back.Add(cube[GetFaceIndex(Face.BACK)][i][2]);
                down.Add(cube[GetFaceIndex(Face.DOWN)][i][2]);
                front.Add(cube[GetFaceIndex(Face.FRONT)][i][2]);
            }
            int[][] face = cube[GetFaceIndex(Face.RIGHT)];

            if (isClockwise)
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.UP)][i][2] = front[i];
                    cube[GetFaceIndex(Face.BACK)][i][2] = up[i];
                    cube[GetFaceIndex(Face.DOWN)][i][2] = back[i];
                    cube[GetFaceIndex(Face.FRONT)][i][2] = down[i];
                }
                cube[GetFaceIndex(Face.RIGHT)] = RotateFaceClockwise(face);
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    cube[GetFaceIndex(Face.UP)][i][2] = back[i];
                    cube[GetFaceIndex(Face.BACK)][i][2] = down[i];
                    cube[GetFaceIndex(Face.DOWN)][i][2] = front[i];
                    cube[GetFaceIndex(Face.FRONT)][i][2] = up[i];
                }
                cube[GetFaceIndex(Face.RIGHT)] = RotateFaceCounterClockwise(face);
            }
        }

        private void RotateUp(int[][][] cube, bool isClockwise)
        {
            int[][] face = cube[GetFaceIndex(Face.UP)];
            int[] front = cube[GetFaceIndex(Face.FRONT)][0];
            int[] right = cube[GetFaceIndex(Face.RIGHT)][0];
            int[] back = cube[GetFaceIndex(Face.BACK)][0];
            int[] left = cube[GetFaceIndex(Face.LEFT)][0];

            if (isClockwise)
            {
                cube[GetFaceIndex(Face.LEFT)][0] = front;
                cube[GetFaceIndex(Face.BACK)][0] = left;
                cube[GetFaceIndex(Face.RIGHT)][0] = back;
                cube[GetFaceIndex(Face.FRONT)][0] = right;
                cube[GetFaceIndex(Face.UP)] = RotateFaceClockwise(face);
            }
            else
            {
                cube[GetFaceIndex(Face.LEFT)][0] = back;
                cube[GetFaceIndex(Face.BACK)][0] = right;
                cube[GetFaceIndex(Face.RIGHT)][0] = front;
                cube[GetFaceIndex(Face.FRONT)][0] = left;
                cube[GetFaceIndex(Face.UP)] = RotateFaceCounterClockwise(face);
            }
        }

        private void RotateDown(int[][][] cube, bool isClockwise)
        {
            int[][] face = cube[GetFaceIndex(Face.DOWN)];
            int[] front = cube[GetFaceIndex(Face.FRONT)][2];
            int[] right = cube[GetFaceIndex(Face.RIGHT)][2];
            int[] back = cube[GetFaceIndex(Face.BACK)][2];
            int[] left = cube[GetFaceIndex(Face.LEFT)][2];

            if (isClockwise)
            {
                cube[GetFaceIndex(Face.RIGHT)][2] = front;
                cube[GetFaceIndex(Face.BACK)][2] = right;
                cube[GetFaceIndex(Face.LEFT)][2] = back;
                cube[GetFaceIndex(Face.FRONT)][2] = left;
                cube[GetFaceIndex(Face.DOWN)] = RotateFaceClockwise(face);
            }
            else
            {
                cube[GetFaceIndex(Face.RIGHT)][2] = back;
                cube[GetFaceIndex(Face.BACK)][2] = left;
                cube[GetFaceIndex(Face.LEFT)][2] = front;
                cube[GetFaceIndex(Face.FRONT)][2] = right;
                cube[GetFaceIndex(Face.DOWN)] = RotateFaceCounterClockwise(face);
            }
        }

        private int[][] RotateFaceClockwise(int[][] face)
        {
            int n = face.Length;
            int[][] rotatedFace = GenerateEmptyFace(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    rotatedFace[j][n - 1 - i] = face[i][j];
            return rotatedFace;
        }

        private int[][] RotateFaceCounterClockwise(int[][] face)
        {
            int n = face.Length;
            int[][] rotatedFace = GenerateEmptyFace(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    rotatedFace[j][i] = face[i][j];
            return rotatedFace;
        }

        private int GetFaceIndex(Face face)
        {
            return (int)face - 1;
        }

        private int[][][] GenerateInitialCube(int size)
        {
            int[][][] cube = new int[6][][];
            for (int i = 0; i < cube.Length; i++)
            {
                cube[i] = GenerateEmptyFace(size);
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        object obj = Enum.Parse(typeof(Face), (i + 1).ToString());
                        Face face = (Face)obj;
                        int value = (int)obj;
                        cube[GetFaceIndex(face)][j][k] = value;
                    }
                }
            }
            return cube;
        }

        private int[][] GenerateEmptyFace(int size)
        {
            int[][] face = new int[size][];
            for (int i = 0; i < size; i++)
                face[i] = new int[size];
            return face;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample2")}.txt";
            var lines = File.ReadAllLines(path).First();
            string instruction = lines.First().ToString();
            foreach (var c in lines.Skip(1))
            {
                if (char.IsLetter(c))
                {
                    _instructions.Add(instruction);
                    instruction = string.Empty;
                }
                instruction += c;
            }
            _instructions.Add(instruction);
        }
    }
}
