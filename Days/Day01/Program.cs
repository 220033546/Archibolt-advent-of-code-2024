using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\input.txt";
            string[] lines = File.ReadAllLines(filePath);

            var answer = 0;
            var lists = new List<int>[2];
            for (var i = 0; i < 2; i++)
                lists[i] = new List<int>();

            foreach (var line in lines)
            {
                var numbers = line.Split(',');
                for (var i = 0; i < 2; i++)
                    lists[i].Add(int.Parse(numbers[i]));
            }

            for (var i = 0; i < 2; i++)
                lists[i].Sort();

            for (var i = 0; i < lists[0].Count; i++)
                answer += Math.Abs(lists[0][i] - lists[1][i]);

            Console.WriteLine(answer);
        }
    }
}
