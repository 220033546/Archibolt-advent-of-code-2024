using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    internal class Program
    {
        private static char[,] map;
        private static Robot robot;
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputFifteen.txt";
            string[] lines = File.ReadAllLines(filePath);

            var directions = "^>v<";
            var deltaMap = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

            var mapList = new List<string>();
            var moves = new List<string>();
            robot = new Robot(0, 0);

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("#"))
                    mapList.Add(lines[i]);
                else if (!string.IsNullOrEmpty(lines[i]))
                    moves.Add(lines[i]);

                if (lines[i].Contains('@'))
                    robot = new Robot(lines[i].IndexOf('@'), i);
            }

            map = new char[mapList.Count, mapList[0].Length];
            for (var y = 0; y < mapList.Count; y++)
                for (var x = 0; x < mapList[y].Length; x++)
                    map[y, x] = mapList[y][x];

            foreach (var line in moves)
                foreach (var move in line)
                    AttemptMove(directions.IndexOf(move), deltaMap);

            var answer = 0;
            for (var y = 0; y < map.GetLength(0); y++)
                for (var x = 0; x < map.GetLength(1); x++)
                    if (map[y, x].Equals('O'))
                        answer += 100 * y + x;

            Console.WriteLine(answer);
        }

        private static void AttemptMove(int direction, int[,] deltaMap)
        {
            var dY = robot.y + deltaMap[direction, 0];
            var dX = robot.x + deltaMap[direction, 1];

            if (map[dY, dX].Equals('.'))
            {
                map[robot.y, robot.x] = '.';
                map[dY, dX] = '@';
                robot.y = dY;
                robot.x = dX;
            }

            if (map[dY, dX].Equals('O'))
            {
                int nextY = dY, nextX = dX, steps = 0;
                bool wall = false, empty = false;
                while (!wall && !empty)
                {
                    nextY += deltaMap[direction, 0];
                    nextX += deltaMap[direction, 1];
                    steps++;

                    if (map[nextY, nextX].Equals('.'))
                        empty = true;
                    else if (map[nextY, nextX].Equals('#'))
                        wall = true;
                }

                if (empty)
                {
                    int previousY = 0, previousX = 0;
                    for (var i = 0; i <= steps; i++)
                    {
                        previousY = nextY - deltaMap[direction, 0];
                        previousX = nextX - deltaMap[direction, 1];

                        map[nextY, nextX] = map[previousY, previousX];

                        if (map[previousY, previousX].Equals('@'))
                        {
                            map[robot.y, robot.x] = '.';
                            robot.y = nextY;
                            robot.x = nextX;
                        }

                        nextY = previousY;
                        nextX = previousX;
                    }
                }
            }
        }
    }

    internal class Robot
    {
        public int x;
        public int y;

        public Robot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
