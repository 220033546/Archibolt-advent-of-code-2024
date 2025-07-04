﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputNine.txt";
            var diskmap = File.ReadAllLines(filePath)[0];

            var intmap = new int[diskmap.Length];
            for (var i = 0; i < diskmap.Length; i++)
                intmap[i] = diskmap[i] - '0';

            var rightBoundary = diskmap.Length - 1;

            long checksum = 0;
            for (int i = 0, index = 0; i < diskmap.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (var j = 0; j < intmap[i]; j++)
                        checksum += (i / 2) * index++;
                }
                else
                {
                    for (var j = 0; i < intmap.Length && j < intmap[i]; j++)
                    {
                        var x = takeLast(i);
                        if (x >= 0)
                            checksum += x * index++;
                        else
                            i = diskmap.Length;
                    }
                }
            }

            Console.WriteLine(checksum);

            int takeLast(int leftBoundary)
            {
                for (int i = rightBoundary; i > leftBoundary; i -= 2)
                {
                    if (intmap[i] > 0)
                    {
                        if (intmap[i] == 1)
                            rightBoundary = i;
                        intmap[i]--;
                        return i / 2;
                    }
                }
                return -1;
            }
        }
    }
}
