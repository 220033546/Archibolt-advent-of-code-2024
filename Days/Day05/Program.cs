﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputFive.txt";
            string[] lines = File.ReadAllLines(filePath);
            var rules = new List<int[]>();

            var answer = 0;
            foreach (var line in lines)
                if (line.Contains('|'))
                    rules.Add(line.Split('|').Select(int.Parse).ToArray());
                else if (!string.IsNullOrEmpty(line))
                    CheckUpdate(line.Split(',').Select(int.Parse).ToList());

            Console.WriteLine(answer);

            void CheckUpdate(List<int> numbers)
            {
                foreach (var rule in rules)
                    if (numbers.Contains(rule[0]) && numbers.Contains(rule[1]))
                        if (numbers.IndexOf(rule[0]) > numbers.IndexOf(rule[1]))
                            return;

                answer += numbers[numbers.Count / 2];
            }
        }
    }
}
