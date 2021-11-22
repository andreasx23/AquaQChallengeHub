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

        private string _instructions;

        protected override int SolveChallenge() //https://stackoverflow.com/a/62558531/15853751 //Visual of a rubiks cube
        {
            var cube = GenerateCube(3);

            for (int i = 0; i < _instructions.Length - 1; i++) //Gave me right answer, but doesn't give right answer in the sample. So not sure if the sample is wrong or my solution is
            {
                bool isLastInstruction = false;
                char current = _instructions[i], next = _instructions[i + 1];
                string instruction = current.ToString();
                if (next == '\'')
                {
                    instruction += next;
                    i++;
                }
                else if (i + 1 == _instructions.Length - 1)
                    isLastInstruction = true;

                DoInstruction(cube, instruction);

                //Console.WriteLine(instruction);
                //Print(cube);

                if (isLastInstruction) DoInstruction(cube, next.ToString());
            }

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
            string top = string.Empty, middle = string.Empty, bottom = string.Empty;
            for (int i = 0; i < cube.Length; i++)
            {
                top += string.Join("", cube[i][0]) + " ";
                middle += string.Join("", cube[i][1]) + " ";
                bottom += string.Join("", cube[i][2]) + " ";
            }
            Console.WriteLine(top);
            Console.WriteLine(middle);
            Console.WriteLine(bottom);
        }

        private void RotateFront(int[][][] cube, bool isClockwise)
        {
            if (isClockwise)
            {
                var up = cube[GetFaceIndex(Face.UP)][2];
                List<int> rights = new();
                for (int i = 0; i < 3; i++)
                    rights.Add(cube[GetFaceIndex(Face.RIGHT)][i][0]);
                var down = cube[GetFaceIndex(Face.DOWN)][0];
                List<int> lefts = new();
                for (int i = 0; i < 3; i++)
                    lefts.Add(cube[GetFaceIndex(Face.LEFT)][i][2]);

                for (int i = 0; i < 3; i++)
                    cube[GetFaceIndex(Face.RIGHT)][i][0] = up[i];
                cube[GetFaceIndex(Face.DOWN)][0] = rights.ToArray();
                for (int i = 0; i < 3; i++)
                    cube[GetFaceIndex(Face.LEFT)][i][2] = down[i];
                cube[GetFaceIndex(Face.UP)][2] = lefts.ToArray();
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    RotateFront(cube, true);
            }
        }

        private void RotateBack(int[][][] cube, bool isClockwise)
        {
            if (isClockwise)
            {
                var up = cube[GetFaceIndex(Face.UP)][0];
                List<int> rights = new();
                for (int i = 0; i < 3; i++)
                    rights.Add(cube[GetFaceIndex(Face.RIGHT)][i][2]);
                var down = cube[GetFaceIndex(Face.DOWN)][2];
                List<int> lefts = new();
                for (int i = 0; i < 3; i++)
                    lefts.Add(cube[GetFaceIndex(Face.LEFT)][i][0]);

                for (int i = 0; i < 3; i++)
                    cube[GetFaceIndex(Face.RIGHT)][i][2] = up[i];
                cube[GetFaceIndex(Face.DOWN)][2] = rights.ToArray();
                for (int i = 0; i < 3; i++)
                    cube[GetFaceIndex(Face.LEFT)][i][0] = down[i];
                cube[GetFaceIndex(Face.UP)][0] = lefts.ToArray();
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    RotateBack(cube, true);
            }
        }

        private void RotateLeft(int[][][] cube, bool isClockwise)
        {
            if (isClockwise)
            {
                List<int> frontValues = new(), upValues = new(), backValues = new(), downValues = new();
                for (int i = 0; i < 3; i++)
                {
                    frontValues.Add(cube[GetFaceIndex(Face.FRONT)][i][0]);
                    upValues.Add(cube[GetFaceIndex(Face.UP)][i][0]);
                    downValues.Add(cube[GetFaceIndex(Face.DOWN)][i][0]);
                    backValues.Add(cube[GetFaceIndex(Face.BACK)][i][0]);
                }

                for (int i = 0; i < 3; i++)
                {
                    cube[GetFaceIndex(Face.FRONT)][i][0] = upValues[i];
                    cube[GetFaceIndex(Face.UP)][i][0] = backValues[i];
                    cube[GetFaceIndex(Face.BACK)][i][0] = downValues[i];
                    cube[GetFaceIndex(Face.DOWN)][i][0] = frontValues[i];
                }

                //List<int> frontValues = new(), upValues = new(), backValues = new(), downValues = new();
                //for (int i = 0; i < 3; i++)
                //{
                //    frontValues.Add(cube[GetFaceIndex(Face.FRONT)][i][0]);
                //    upValues.Add(cube[GetFaceIndex(Face.UP)][i][0]);
                //    downValues.Add(cube[GetFaceIndex(Face.DOWN)][i][2]);
                //    backValues.Add(cube[GetFaceIndex(Face.BACK)][i][2]);
                //}

                //for (int i = 0; i < 3; i++)
                //{
                //    cube[GetFaceIndex(Face.FRONT)][i][0] = upValues[i];
                //    cube[GetFaceIndex(Face.UP)][i][0] = backValues[i];
                //    cube[GetFaceIndex(Face.BACK)][i][2] = downValues[i];
                //    cube[GetFaceIndex(Face.DOWN)][i][2] = frontValues[i];
                //}
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    RotateLeft(cube, true);
            }
        }

        private void RotateRight(int[][][] cube, bool isClockwise)
        {
            if (isClockwise)
            {
                List<int> frontValues = new(), upValues = new(), backValues = new(), downValues = new();
                for (int i = 0; i < 3; i++)
                {
                    frontValues.Add(cube[GetFaceIndex(Face.FRONT)][i][2]);
                    upValues.Add(cube[GetFaceIndex(Face.UP)][i][2]);
                    backValues.Add(cube[GetFaceIndex(Face.BACK)][i][2]);
                    downValues.Add(cube[GetFaceIndex(Face.DOWN)][i][2]);
                }

                for (int i = 0; i < 3; i++)
                {
                    cube[GetFaceIndex(Face.FRONT)][i][2] = downValues[i];
                    cube[GetFaceIndex(Face.UP)][i][2] = frontValues[i];
                    cube[GetFaceIndex(Face.BACK)][i][2] = upValues[i];
                    cube[GetFaceIndex(Face.DOWN)][i][2] = backValues[i];
                }

                //List<int> frontValues = new(), upValues = new(), backValues = new(), downValues = new();
                //for (int i = 0; i < 3; i++)
                //{
                //    frontValues.Add(cube[GetFaceIndex(Face.FRONT)][i][2]);
                //    upValues.Add(cube[GetFaceIndex(Face.UP)][i][2]);
                //    backValues.Add(cube[GetFaceIndex(Face.BACK)][i][0]);
                //    downValues.Add(cube[GetFaceIndex(Face.DOWN)][i][0]);
                //}

                //for (int i = 0; i < 3; i++)
                //{
                //    cube[GetFaceIndex(Face.FRONT)][i][2] = downValues[i];
                //    cube[GetFaceIndex(Face.UP)][i][2] = frontValues[i];
                //    cube[GetFaceIndex(Face.BACK)][i][0] = upValues[i];
                //    cube[GetFaceIndex(Face.DOWN)][i][0] = backValues[i];
                //}
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    RotateRight(cube, true);
            }
        }

        private void RotateUp(int[][][] cube, bool isClockwise)
        {
            if (isClockwise)
            {
                var front = cube[GetFaceIndex(Face.FRONT)][0];
                var right = cube[GetFaceIndex(Face.RIGHT)][0];
                var back = cube[GetFaceIndex(Face.BACK)][0];
                var left = cube[GetFaceIndex(Face.LEFT)][0];
                cube[GetFaceIndex(Face.LEFT)][0] = front;
                cube[GetFaceIndex(Face.BACK)][0] = left;
                cube[GetFaceIndex(Face.RIGHT)][0] = back;
                cube[GetFaceIndex(Face.FRONT)][0] = right;

                //var front = cube[GetFaceIndex(Face.FRONT)][0];
                //var right = cube[GetFaceIndex(Face.RIGHT)][0];
                //var back = cube[GetFaceIndex(Face.BACK)][2];
                //var left = cube[GetFaceIndex(Face.LEFT)][2];
                //cube[GetFaceIndex(Face.LEFT)][2] = front;
                //cube[GetFaceIndex(Face.BACK)][2] = left;
                //cube[GetFaceIndex(Face.RIGHT)][0] = back;
                //cube[GetFaceIndex(Face.FRONT)][0] = right;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    RotateUp(cube, true);
            }
        }

        private void RotateDown(int[][][] cube, bool isClockwise)
        {
            if (isClockwise)
            {
                var front = cube[GetFaceIndex(Face.FRONT)][2];
                var right = cube[GetFaceIndex(Face.RIGHT)][2];
                var back = cube[GetFaceIndex(Face.BACK)][2];
                var left = cube[GetFaceIndex(Face.LEFT)][2];
                cube[GetFaceIndex(Face.LEFT)][2] = front;
                cube[GetFaceIndex(Face.BACK)][2] = left;
                cube[GetFaceIndex(Face.RIGHT)][2] = back;
                cube[GetFaceIndex(Face.FRONT)][2] = right;

                //var front = cube[GetFaceIndex(Face.FRONT)][2];
                //var right = cube[GetFaceIndex(Face.RIGHT)][2];
                //var back = cube[GetFaceIndex(Face.BACK)][0];
                //var left = cube[GetFaceIndex(Face.LEFT)][0];
                //cube[GetFaceIndex(Face.LEFT)][0] = front;
                //cube[GetFaceIndex(Face.BACK)][0] = left;
                //cube[GetFaceIndex(Face.RIGHT)][2] = back;
                //cube[GetFaceIndex(Face.FRONT)][2] = right;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    RotateDown(cube, true);
            }
        }

        private int GetFaceIndex(Face face)
        {
            return (int)face - 1;
        }

        private int[][][] GenerateCube(int size)
        {
            int[][][] cube = new int[6][][];
            for (int i = 0; i < cube.Length; i++)
            {
                cube[i] = new int[size][];
                for (int j = 0; j < cube[i].Length; j++)
                {
                    cube[i][j] = new int[size];
                    for (int k = 0; k < cube[i][j].Length; k++)
                    {
                        cube[i][j][k] = (int)Enum.Parse(typeof(Face), (i + 1).ToString());
                    }
                }
            }

            return cube;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            _instructions = File.ReadAllLines(path).First();
        }
    }
}
