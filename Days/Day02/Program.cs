using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputTwo.txt";

            string[] lines = File.ReadAllLines(filePath);

            var answer = 0;
            foreach (var line in lines)
            {
                var report = line.Split(' ').Select(int.Parse).ToArray();
                if (isSafe(report))
                    answer++;
            }

            Console.WriteLine(answer);
        }

        private static bool isSafe(int[] report)
        {
            var direction = 0;

            for (int i = 1; i < report.Length; i++)
            {
                var delta = report[i] - report[i - 1];

                if (delta == 0 || delta > 3 || delta < -3)
                    return false;

                if (direction == 0)
                    direction = delta;
                else
                    if ((direction > 0 && delta < 0) || (direction < 0 && delta > 0))
                    return false;
            }

            return true;
        }

    }
}
