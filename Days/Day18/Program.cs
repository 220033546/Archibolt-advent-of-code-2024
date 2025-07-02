using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputEighteen.txt";
            var lines = File.ReadAllLines(filePath);

            var deltaMap = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

            var nodes = new Node[73, 73];
            var start = new Node(0, 0, true, false);
            var end = new Node(0, 0, false, true);

            var memorySpace = new bool[71, 71];
            for (var i = 0; i < 1024; i++)
            {
                var bytePosition = lines[i].Split(',').Select(int.Parse).ToArray();
                memorySpace[bytePosition[1], bytePosition[0]] = true;
            }

            for (var y = 0; y < memorySpace.GetLength(0); y++)
                for (var x = 0; x < memorySpace.GetLength(1); x++)
                    if (!memorySpace[y, x])
                    {
                        var node = new Node(x + 1, y + 1, y == 0 && x == 0, y == memorySpace.GetLength(0) - 1 && x == memorySpace.GetLength(1) - 1);
                        nodes[y + 1, x + 1] = node;

                        if (node.start)
                            start = node;
                        else if (node.end)
                            end = node;
                    }

            Search(nodes, start, end, deltaMap);

            var path = new List<Node>();
            BuildPath(path, end);

            Console.WriteLine(path.Count);
        }

        static void Search(Node[,] nodes, Node start, Node end, int[,] deltaMap)
        {
            start.minCostToStart = 0;
            var priorityQueue = new List<Node> { start };
            do
            {
                priorityQueue = priorityQueue.OrderBy(x => x.minCostToStart).ToList();
                var node = priorityQueue.First();
                priorityQueue.Remove(node);
                for (var dir = 0; dir < 4; dir++)
                {
                    var neighbor = nodes[node.y + deltaMap[dir, 0], node.x + deltaMap[dir, 1]];

                    if (neighbor == null || neighbor.visited)
                        continue;

                    var cost = node.minCostToStart + 1;
                    if (neighbor.minCostToStart == null || cost < neighbor.minCostToStart)
                    {
                        neighbor.minCostToStart = cost;
                        neighbor.nearestToStart = node;

                        if (!priorityQueue.Contains(neighbor))
                            priorityQueue.Add(neighbor);
                    }

                    node.visited = true;
                }
                if (node.end)
                    return;
            }
            while (priorityQueue.Count > 0);
        }

        static void BuildPath(List<Node> path, Node node)
        {
            if (node.nearestToStart == null)
                return;

            path.Add(node.nearestToStart);
            BuildPath(path, node.nearestToStart);
        }

    }
    internal class Node
    {
        public int x;
        public int y;
        public bool start;
        public bool end;
        public int? minCostToStart;
        public bool visited;
        public Node nearestToStart;

        public Node(int x, int y, bool start, bool end)
        {
            this.x = x;
            this.y = y;
            this.start = start;
            this.end = end;
            this.minCostToStart = null;
            this.visited = false;
            this.nearestToStart = null;
        }
    }
}
