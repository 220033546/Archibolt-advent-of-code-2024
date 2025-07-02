using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputTwentyThree.txt";
            var lines = File.ReadAllLines(filePath);

            var computers = new Dictionary<string, Computer>();

            foreach (var line in lines)
            {
                var names = line.Split('-');
                foreach (var name in names)
                {
                    if (!computers.ContainsKey(name))
                    {
                        computers.Add(name, new Computer(name));
                    }
                }

                computers[names[0]].connections.Add(computers[names[1]]);
                computers[names[1]].connections.Add(computers[names[0]]);
            }

            var sets = new HashSet<string>();
            foreach (var computerName in computers.Keys)
            {
                foreach (var computerConnection in computers[computerName].connections)
                {
                    foreach (var secondGradeConnection in computers[computerName].connections)
                    {
                        if (computerConnection != secondGradeConnection &&
                            secondGradeConnection.connections.Contains(computers[computerName]) &&
                            secondGradeConnection.connections.Contains(computerConnection) &&
                            (computerName.StartsWith("t") || computerConnection.name.StartsWith("t") || secondGradeConnection.name.StartsWith("t")))
                        {
                            var list = new List<string>
                        {
                            computerName,
                            computerConnection.name,
                            secondGradeConnection.name
                        };
                            list.Sort();
                            sets.Add(string.Format("{0},{1},{2}", list[0], list[1], list[2]));
                        }
                    }
                }
            }

            Console.WriteLine(sets.Count);
        }
    }
    class Computer
    {
        public string name;
        public List<Computer> connections;

        public Computer(string name)
        {
            this.name = name;
            this.connections = new List<Computer>();
        }
    }
}
