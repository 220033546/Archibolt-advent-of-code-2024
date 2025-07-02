using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class Program
    {
        static string[] lines;
        static bool[,] visited;
        static int[,] deltaMap = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
        static int answer = 0;
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputTwelve.txt";
            lines = File.ReadAllLines(filePath);

            visited = new bool[lines.Length, lines[0].Length];

            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    if (!visited[y, x])
                    {
                        VisitRegion(y, x);
                    }
                }
            }

            Console.WriteLine(answer);
        }

        static void VisitRegion(int y, int x)
        {
            visited[y, x] = true;

            var plots = new List<Tuple<int, int>>();
            plots.Add(new Tuple<int, int>(y, x));
            int perimeter = 0;

            Discover(y, x, plots, ref perimeter);

            answer += (plots.Count * perimeter);
        }

        static void Discover(int y, int x, List<Tuple<int, int>> plots, ref int perimeter)
        {
            for (int i = 0; i < 4; i++)
            {
                int dY = y + deltaMap[i, 0];
                int dX = x + deltaMap[i, 1];

                if (dY >= 0 && dY < lines.Length && dX >= 0 && dX < lines[0].Length)
                {
                    if (lines[dY][dX] == lines[y][x])
                    {
                        if (!visited[dY, dX])
                        {
                            plots.Add(new Tuple<int, int>(dY, dX));
                            visited[dY, dX] = true;
                            Discover(dY, dX, plots, ref perimeter);
                        }
                    }
                    else
                    {
                        perimeter++;
                    }
                }
                else
                {
                    perimeter++;
                }
            }
        }
    }
}
