using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputThree.txt";
            string[] lines = File.ReadAllLines(filePath);

            var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");

            var answer = 0;

            foreach (var line in lines)
            {
                foreach (Match match in regex.Matches(line))
                {
                    // Extract the matched string: "mul(3,5)"
                    var str = match.Value;

                    // Slice off "mul(" and ")" using Substring
                    var sliced = str.Substring(4, str.Length - 5); // "3,5"

                    // Split and convert to numbers
                    var numbers = sliced.Split(',').Select(int.Parse).ToArray();

                    // Multiply and add to total
                    answer += numbers[0] * numbers[1];
                }
            }

            Console.WriteLine(answer);
        }
    }
}
