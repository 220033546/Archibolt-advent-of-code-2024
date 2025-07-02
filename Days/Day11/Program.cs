using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    internal class Program
    {
        static Dictionary<long, List<long>> cache = new Dictionary<long, List<long>>();
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputEleven.txt";
            var lines = File.ReadAllLines(filePath);

            var stones = lines[0].Split(' ').Select(long.Parse).ToList();

            for (var i = 0; i < 25; i++)
            {
                var blink = new List<long>();
                for (var s = 0; s < stones.Count; s++)
                {
                    blink.AddRange(ChangeStones(stones[s]));
                }
                stones = blink;
            }

            Console.WriteLine(stones.Count);
        }

        static List<long> ChangeStones(long engraving)
        {
            List<long> value;
            if (cache.TryGetValue(engraving, out value))
                return value;

            var change = new List<long>();

            var str = engraving.ToString();
            if (engraving == 0)
            {
                change.Add(1);
            }
            else if (str.Length % 2 == 0)
            {
                int mid = str.Length / 2;
                string leftPart = str.Substring(0, mid);
                string rightPart = str.Substring(mid);

                change.Add(long.Parse(leftPart));
                change.Add(long.Parse(rightPart));
            }
            else
            {
                change.Add(engraving * 2024);
            }

            cache.Add(engraving, change);
            return change;
        }
    }
}
