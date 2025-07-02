using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputForthin.txt";
            var lines = File.ReadAllLines(filePath);

            List<Robot> robots = new List<Robot>();
            Tuple<int, int> dimensions = new Tuple<int, int>(101, 103);

            foreach (string line in lines)
            {
                string[] input = line.Split(' ');
                int[] pos = input[0].Replace("p=", string.Empty).Split(',').Select(int.Parse).ToArray();
                int[] vel = input[1].Replace("v=", string.Empty).Split(',').Select(int.Parse).ToArray();

                robots.Add(new Robot(new Tuple<int, int>(pos[0], pos[1]), new Tuple<int, int>(vel[0], vel[1])));
            }

            foreach (Robot robot in robots)
            {
                robot.Move(dimensions, 100);
            }

            int[] quadrants = new int[4];
            foreach (Robot robot in robots)
            {
                if (robot.Position.Item1 < dimensions.Item1 / 2 && robot.Position.Item2 < dimensions.Item2 / 2)
                    quadrants[0]++;
                else if (robot.Position.Item1 > dimensions.Item1 / 2 && robot.Position.Item2 < dimensions.Item2 / 2)
                    quadrants[1]++;
                else if (robot.Position.Item1 < dimensions.Item1 / 2 && robot.Position.Item2 > dimensions.Item2 / 2)
                    quadrants[2]++;
                else if (robot.Position.Item1 > dimensions.Item1 / 2 && robot.Position.Item2 > dimensions.Item2 / 2)
                    quadrants[3]++;
            }

            Console.WriteLine(quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3]);
        }
    }

    class Robot
    {
        public Tuple<int, int> Position;
        public Tuple<int, int> Velocity;

        public Robot(Tuple<int, int> position, Tuple<int, int> velocity)
        {
            this.Position = position;
            this.Velocity = velocity;
        }

        public void Move(Tuple<int, int> dimensions, int steps)
        {
            int pX = (Position.Item1 + (Velocity.Item1 * steps)) % dimensions.Item1;
            int pY = (Position.Item2 + (Velocity.Item2 * steps)) % dimensions.Item2;

            if (pX < 0)
                pX += dimensions.Item1;
            if (pX >= dimensions.Item1)
                pX -= dimensions.Item1;

            if (pY < 0)
                pY += dimensions.Item2;
            if (pY >= dimensions.Item2)
                pY -= dimensions.Item2;

            Position = new Tuple<int, int>(pX, pY);
        }
    }
}
