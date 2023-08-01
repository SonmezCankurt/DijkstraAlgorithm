using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraAlgorithmProject
{
    class DijkstraAlgorithm
    {
        public Point FindUnexplored(List<Point> points)
        {
            Point p = null;

            foreach (var point in points)
            {
                if (!point.explored)
                {
                    p = point;
                    break;
                }
            }

            return p;
        }

        public string PrintShortestPath(Point end)
        {
            Point p = end;
            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Insert(0, p.name);

                if (p.parent != null)
                    sb.Insert(0, $" --{p.neighbors[p.parent]}--> ");

                p = p.parent;
            }
            while (p != null);

            return sb.ToString();
        }

        public void FindShortestPath(Point start, Point end, List<Point> points)
        {
            start.pathDistance = 0;

            for (int i = 0; i < points.Count; i++)
            {

                Point point = FindUnexplored(points);

                if (point != null)
                {
                    foreach (var neighbor in point.neighbors.Keys)
                    {
                        int altDistance = point.neighbors[neighbor] + point.pathDistance;

                        if (altDistance < neighbor.pathDistance)
                        {
                            neighbor.pathDistance = altDistance;
                            neighbor.parent = point;
                        }
                    }

                    point.explored = true;

                }
                else
                {
                    break;
                }
            }


            Console.WriteLine("Shortest Path : " + PrintShortestPath(end));

        }

        public void DoExercise()
        {
            Point A = new Point("A");
            Point B = new Point("B");
            Point C = new Point("C");
            Point D = new Point("D");
            Point E = new Point("E");

            List<Point> points = new List<Point>(5);
            points.Add(A);
            points.Add(B);
            points.Add(C);
            points.Add(D);
            points.Add(E);

            A.AddNeighbor(B, 2);
            A.AddNeighbor(C, 4);
            B.AddNeighbor(C, 1);
            B.AddNeighbor(D, 7);
            C.AddNeighbor(D, 4);
            C.AddNeighbor(E, 5);
            D.AddNeighbor(E, 3);

            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm();
            dijkstra.FindShortestPath(A, E, points);
        }
    }

    class Point
    {
        public string name;
        public Dictionary<Point, int> neighbors = new Dictionary<Point, int>();
        public bool explored;
        public int pathDistance;
        public Point parent;

        public Point(string name)
        {
            this.name = name;
            explored = false;
            pathDistance = int.MaxValue;
            parent = null;
        }

        public void AddNeighbor(Point neighbor, int distance)
        {
            neighbors[neighbor] = distance;
            neighbor.neighbors[this] = distance;
        }
    }
}
