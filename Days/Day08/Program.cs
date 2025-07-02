using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputEight.txt";
            var lines = File.ReadAllLines(filePath);

            var antennas = new Dictionary<char, List<Tuple<int, int>>>();
            var antinodes = new HashSet<Tuple<int, int>>();

            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    char symbol = lines[y][x];
                    if (!symbol.Equals('.'))
                    {
                        List<Tuple<int, int>> value;
                        if (!antennas.TryGetValue(symbol, out value))
                        {
                            antennas[symbol] = new List<Tuple<int, int>> { new Tuple<int, int>(y, x) };
                        }
                        else
                        {
                            foreach (var antenna in value)
                            {
                                var dY = y - antenna.Item1;
                                var dX = x - antenna.Item2;

                                PlaceAntinode(y + dY, x + dX, antinodes, lines);
                                PlaceAntinode(antenna.Item1 - dY, antenna.Item2 - dX, antinodes, lines);
                            }

                            value.Add(new Tuple<int, int>(y, x));
                        }
                    }
                }
            }

            Console.WriteLine(antinodes.Count);
        }
        static void PlaceAntinode(int y, int x, HashSet<Tuple<int, int>> antinodes, string[] lines)
        {
            if (y >= 0 && y < lines.Length && x >= 0 && x < lines[y].Length)
            {
                antinodes.Add(new Tuple<int, int>(y, x));
            }
        }
    }
}

