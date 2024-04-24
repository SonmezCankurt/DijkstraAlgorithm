using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraAlgorithmProject
{
    class DijkstraAlgorithm
    {
        public Node FindUndiscoveredMinValue(List<Node> nodes)
        {
            Node p = null;

            int val = int.MaxValue;

            foreach (var node in nodes)
            {
                if (!node.discorvered)
                {
                    if(node.pathDistance <= val)
                    {
                        val = node.pathDistance;
                        p = node;
                    }
                }
            }

            return p;
        }

        public string PrintShortestPath(Node end)
        {
            Node p = end;
            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Insert(0, p.name);

                if (p.parent != null)
                    sb.Insert(0, $" --({p.neighbors[p.parent]})--> ");

                p = p.parent;
            }
            while (p != null);

            return sb.ToString();
        }


        public void FindShortestPath(Node start, Node end, List<Node> nodes)
        {
            start.pathDistance = 0;

            for (int i = 0; i < nodes.Count; i++)
            {

                Node node = FindUndiscoveredMinValue(nodes);

                if (node != null)
                {
                    if (node == end)
                        break;

                    foreach (var neighbor in node.neighbors.Keys)
                    {
                        if (node.parent == neighbor)
                            continue;

                        int altDistance = node.neighbors[neighbor] + node.pathDistance;

                        if (altDistance < neighbor.pathDistance)
                        {
                            neighbor.pathDistance = altDistance;
                            neighbor.parent = node;
                        }
                    }

                    node.discorvered = true;

                }
                else
                {
                    break;
                }
            }


            Console.WriteLine("Shortest Path : " + PrintShortestPath(end));
            Console.WriteLine("Total Distance : " + end.pathDistance);

        }

        public void DoExercise()
        {
            Node A = new Node("A");
            Node B = new Node("B");
            Node C = new Node("C");
            Node D = new Node("D");
            Node E = new Node("E");

            List<Node> nodes = new List<Node>(5);


            nodes.Add(D);
            nodes.Add(B);
            nodes.Add(A);
            nodes.Add(E);
            nodes.Add(C);


            A.AddNeighbor(B, 2);
            A.AddNeighbor(C, 4);
            B.AddNeighbor(C, 1);
            B.AddNeighbor(D, 7);
            C.AddNeighbor(D, 4);
            C.AddNeighbor(E, 5);
            D.AddNeighbor(E, 3);

            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm();
            dijkstra.FindShortestPath(A, E, nodes);
        }
    }

    class Node
    {
        public string name;
        public Dictionary<Node, int> neighbors = new Dictionary<Node, int>();
        public bool discorvered;
        public int pathDistance;
        public Node parent;

        public Node(string name)
        {
            this.name = name;
            discorvered = false;
            pathDistance = int.MaxValue;
            parent = null;
        }

        public void AddNeighbor(Node neighbor, int distance)
        {
            neighbors[neighbor] = distance;
            neighbor.neighbors[this] = distance;
        }
    }
}
