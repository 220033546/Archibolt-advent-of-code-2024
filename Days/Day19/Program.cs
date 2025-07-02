using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputNineteen.txt";
            var lines = File.ReadAllLines(filePath);

            var answer = 0L;
            var availablePatterns = lines[0].Split(new[] { ", " }, StringSplitOptions.None);

            for (var i = 2; i < lines.Length; i++)
            {
                if (SortTowels(lines[i], availablePatterns))
                {
                    answer++;
                }
            }

            Console.WriteLine(answer);
        }

        static bool SortTowels(string design, string[] availablePatterns)
        {
            var towels = new List<string>[design.Length];
            for (var i = 0; i < design.Length; i++)
            {
                towels[i] = new List<string>();
            }

            foreach (var pattern in availablePatterns)
            {
                if (design.Contains(pattern))
                {
                    var lastIndex = 0;
                    var indexes = new List<int>();

                    while (lastIndex <= design.Length - pattern.Length &&
                           design.IndexOf(pattern, lastIndex) >= 0)
                    {
                        var index = design.IndexOf(pattern, lastIndex);
                        indexes.Add(index);
                        towels[index].Add(pattern);
                        lastIndex = index + 1;
                    }
                }
            }

            var possibilities = new long[design.Length];
            for (var i = towels.Length - 1; i >= 0; i--)
            {
                foreach (var towel in towels[i])
                {
                    if (i + towel.Length < design.Length)
                    {
                        possibilities[i] += possibilities[i + towel.Length];
                    }
                    else
                    {
                        possibilities[i]++;
                    }
                }
            }

            return possibilities[0] > 0;
        }
    }
}
