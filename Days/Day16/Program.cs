using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day16
{
    internal class Program
    {
        static int[,] deltaMap = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputSixteen.txt";
            string[] lines = File.ReadAllLines(filePath);

            Node[,] nodes = new Node[lines.Length, lines[0].Length];
            Node start = null;
            Node end = null;

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] != '#')
                    {
                        Node node = new Node(x, y, lines[y][x] == 'S', lines[y][x] == 'E');
                        nodes[y, x] = node;

                        if (node.start)
                            start = node;
                        else if (node.end)
                            end = node;
                    }
                }
            }

            Search(nodes, start, end);
            Console.WriteLine(end.minCostToStart.HasValue ? end.minCostToStart.Value : -1);
        }

        static void Search(Node[,] nodes, Node start, Node end)
        {
            start.minCostToStart = 0;
            List<Tuple<Node, int>> priorityQueue = new List<Tuple<Node, int>>();
            priorityQueue.Add(new Tuple<Node, int>(start, 1));

            while (priorityQueue.Count > 0)
            {
                // Sort by cost
                priorityQueue.Sort((a, b) =>
                    a.Item1.minCostToStart.GetValueOrDefault().CompareTo(b.Item1.minCostToStart.GetValueOrDefault()));

                Tuple<Node, int> nodeWithDirection = priorityQueue[0];
                priorityQueue.RemoveAt(0);

                Node current = nodeWithDirection.Item1;
                int fromDir = nodeWithDirection.Item2;

                if (current.end)
                    return;

                for (int dir = 0; dir < 4; dir++)
                {
                    int newY = current.y + deltaMap[dir, 0];
                    int newX = current.x + deltaMap[dir, 1];

                    if (newY < 0 || newY >= nodes.GetLength(0) || newX < 0 || newX >= nodes.GetLength(1))
                        continue;

                    Node neighbor = nodes[newY, newX];
                    if (neighbor == null || neighbor.visited)
                        continue;

                    int cost = current.minCostToStart.GetValueOrDefault() + 1;
                    if (dir != fromDir)
                        cost += 1000;

                    if (!neighbor.minCostToStart.HasValue || cost < neighbor.minCostToStart.Value)
                    {
                        neighbor.minCostToStart = cost;
                        neighbor.nearestToStart = current;

                        // Avoid duplicates (by reference and direction)
                        bool exists = false;
                        for (int i = 0; i < priorityQueue.Count; i++)
                        {
                            if (priorityQueue[i].Item1 == neighbor && priorityQueue[i].Item2 == dir)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                            priorityQueue.Add(new Tuple<Node, int>(neighbor, dir));
                    }
                }

                current.visited = true;
            }
        }
    }

    class Node
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

