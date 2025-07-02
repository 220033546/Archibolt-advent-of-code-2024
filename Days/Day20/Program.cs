using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputTwenty.txt";
            var lines = File.ReadAllLines(filePath);

            var deltaMap = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

            var racetrack = new int[lines.Length, lines[0].Length];
            var start = new Point(0, 0);
            var end = new Point(0, 0);

            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x].Equals('#'))
                    {
                        racetrack[y, x] = -1;
                    }
                    else if (lines[y][x].Equals('S'))
                    {
                        start = new Point(x, y);
                    }
                    else if (lines[y][x].Equals('E'))
                    {
                        end = new Point(x, y);
                    }
                }
            }

            var pos = new Point(start.X, start.Y);
            int previousX = -1, previousY = -1;
            while (!(pos.X == end.X && pos.Y == end.Y))
            {
                int dY = 0, dX = 0;
                for (var i = 0; i < 4; i++)
                {
                    dY = pos.Y + deltaMap[i, 0];
                    dX = pos.X + deltaMap[i, 1];

                    if (dY >= 0 && dY < racetrack.GetLength(0) &&
                        dX >= 0 && dX < racetrack.GetLength(1) &&
                        racetrack[dY, dX] != -1 &&
                        !(dX == previousX && dY == previousY))
                    {
                        racetrack[dY, dX] = racetrack[pos.Y, pos.X] + 1;

                        previousX = pos.X;
                        previousY = pos.Y;

                        pos.X = dX;
                        pos.Y = dY;

                        break;
                    }
                }
            }

            var answer = 0;
            for (var y = 1; y < lines.Length - 1; y++)
            {
                for (var x = 1; x < lines[0].Length - 1; x++)
                {
                    if (racetrack[y, x] == -1)
                    {
                        if (racetrack[y - 1, x] != -1 && racetrack[y + 1, x] != -1)
                        {
                            Cheat(racetrack[y - 1, x], racetrack[y + 1, x], ref answer);
                        }
                        else if (racetrack[y, x - 1] != -1 && racetrack[y, x + 1] != -1)
                        {
                            Cheat(racetrack[y, x - 1], racetrack[y, x + 1], ref answer);
                        }
                    }
                }
            }

            Console.WriteLine(answer);
        }
        static void Cheat(int a, int b, ref int answer)
        {
            if (Saved(a, b, 2) >= 100)
            {
                answer++;
            }
        }

        static int Saved(int a, int b, int steps)
        {
            return a > b ? a - b - steps : b - a - steps;
        }
    }
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
