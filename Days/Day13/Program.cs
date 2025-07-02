using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputThirty.txt";
            var lines = File.ReadAllLines(filePath);

            long answer = 0;
            for (int i = 0; i < lines.Length; i += 4)
            {
                string lineA = lines[i].Substring(12).Replace(" Y+", string.Empty);
                string lineB = lines[i + 1].Substring(12).Replace(" Y+", string.Empty);
                string linePrize = lines[i + 2].Substring(9).Replace(" Y=", string.Empty);

                double[] A = Array.ConvertAll(lineA.Split(','), double.Parse);
                double[] B = Array.ConvertAll(lineB.Split(','), double.Parse);
                double[] prize = Array.ConvertAll(linePrize.Split(','), double.Parse);

                double a = (prize[1] - (B[1] * prize[0] / B[0])) / (A[1] - (B[1] * A[0] / B[0]));
                double b = (prize[0] - (a * A[0])) / B[0];

                if (a > 0 && b > 0 &&
                    Math.Round(a, 2) == Math.Round(a, 0) &&
                    Math.Round(b, 2) == Math.Round(b, 0))
                {
                    answer += (long)Math.Round(a, 0) * 3 + (long)Math.Round(b, 0);
                }
            }

            Console.WriteLine(answer);
        }
    }
}
