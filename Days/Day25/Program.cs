﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputTwentyFive.txt";
            var lines = File.ReadAllLines(filePath);

            var locks = new List<int[]>();
            var keys = new List<int[]>();

            var answer = 0;
            for (var i = 0; i < lines.Length; i += 8)
            {
                var heights = new int[5];
                for (var j = i; j < i + 7; j++)
                {
                    for (var x = 0; x < 5; x++)
                    {
                        if (lines[j][x].Equals('#'))
                        {
                            heights[x]++;
                        }
                    }
                }

                if (!lines[i].Contains('.'))
                {
                    locks.Add(heights);
                }
                else
                {
                    keys.Add(heights);
                }
            }

            foreach (var l0ck in locks)
            {
                foreach (var key in keys)
                {
                    var fits = true;
                    for (var x = 0; x < 5; x++)
                    {
                        if (key[x] + l0ck[x] > 7)
                        {
                            fits = false;
                        }
                    }

                    if (fits)
                    {
                        answer++;
                    }
                }
            }

            Console.WriteLine(answer);
        }
    }
}
