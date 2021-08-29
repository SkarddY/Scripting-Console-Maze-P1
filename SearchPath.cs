using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptingParcial1
{
    public class Node
    {
        public Node explored; public Vector2D location; public bool explore = false;
        public Vector2D GetPosition() {
            return location;
        }
        public Node (int x, int y) {
            location = new Vector2D(x, y);
        }
        public override string ToString() {
            return ("Explored: " + explore + "Ubication: " + location);
        }
    }
    public struct Vector2D
    {
        public int vX, vY;
        public Vector2D(int X, int Y) {
            vX = Y;
            vY = X;
        }
        public Vector2D CorrectionPos(Vector2D vector) {
            int eX = vX + vector.vX;
            int eY = vY + vector.vY;
            Vector2D solution = new Vector2D(eX, eY);
            return solution;
        }
        public override string ToString() {
            return ("( " + vX.ToString() + " , " + vY.ToString() + " )");
        }
    }
    class SearchPath
    {
        private Node First, End, pExploration;
        int xFirst = 0, yFirst = 0, xEnd = 0, yEnd = 0;

        private int mazeSize = 6;
        private Vector2D[] Dir = { new Vector2D(0, 1), new Vector2D(0, -1), new Vector2D(1, 0), new Vector2D(-1, 0) };
        private bool exploring = true;
        private Dictionary<Vector2D, Node> space = new Dictionary<Vector2D, Node>();
        private Dictionary<Vector2D, int> walls = new Dictionary<Vector2D, int>();
        private Dictionary<Vector2D, int> obj = new Dictionary<Vector2D, int>();
        private List<Node> path = new List<Node>();
        private List<Node> mainPath = new List<Node>();
        private Queue<Node> queue = new Queue<Node>();

        public void Maze()
        {
            for (int i = 0; i < mazeSize; i++) {
                for (int j = 0; j < mazeSize; j++) {
                    Vector2D nVector = new Vector2D(i, j);
                    Node space = new Node(i, j);
                    this.space.Add(nVector, space);
                }
            }
            do {
                Console.WriteLine("Please write the initial coordinate for X = "); xFirst = int.Parse(Console.ReadLine()); Console.WriteLine("Please write the initial coordinate for Y = "); yFirst = int.Parse(Console.ReadLine());
                Console.WriteLine("Please write the last coordinate for X = "); xEnd = int.Parse(Console.ReadLine()); Console.WriteLine("Please write the last coordinate for Y =  "); yEnd = int.Parse(Console.ReadLine());

                if (xFirst < 0 || xEnd < 0 || yFirst < 0 || yEnd < 0 || xFirst > mazeSize || yFirst > mazeSize || xEnd > mazeSize || yEnd > mazeSize) Console.WriteLine("Escriba valores dentro del limite de 25 Write values within the limit of 25");
            } while (xFirst < 0 || xEnd < 0 || yFirst < 0 || yEnd < 0 || xFirst > mazeSize || yFirst > mazeSize || xEnd > mazeSize || yEnd > mazeSize);

            Vector2D nInitial = new Vector2D(xFirst, yFirst);
            Vector2D nEnd = new Vector2D(xEnd, yEnd);
            obj.Add(nInitial, 1); obj.Add(nEnd, 1);
            First = space[nInitial]; End = space[nEnd];

            List<Vector2D> walls = new List<Vector2D>();
            Vector2D wall1 = new Vector2D(3, 4);
            Vector2D wall2 = new Vector2D(3, 8);
            Vector2D wall3 = new Vector2D(5, 4);
            Vector2D wall4 = new Vector2D(2, 1);
            Vector2D wall5 = new Vector2D(1, 2);
            walls.Add(wall1); walls.Add(wall2); walls.Add(wall3); walls.Add(wall4); walls.Add(wall5);

            foreach (Vector2D wall in walls) {
                if (space.ContainsKey(wall)) {
                    space.Remove(wall);
                    this.walls.Add(wall, 1);
                }
            }

            for (int i = 0; i < mazeSize; i++) {
                for (int j = 0; j < mazeSize; j++) {
                    Vector2D Pincel = new Vector2D(i, j);
                    if (obj.ContainsKey(Pincel)) {
                        Console.Write("F");
                    }
                    else if (space.ContainsKey(Pincel)) {
                        Console.Write("O");
                    }
                    else if (this.walls.ContainsKey(Pincel)) {
                        Console.Write("X");
                    }
                }
                Console.WriteLine("");
            }
        }
        public void BFS() {
            queue.Enqueue(First);
            while (queue.Count > 0 && exploring) {
                pExploration = queue.Dequeue();
                ReachindEnd(); ExploreCloseNodes();
            }
        }
        public void ReachindEnd() {
            if (pExploration == End) {
                exploring = false;
            }
            else {
                exploring = true;
            }
        }
        public void ExploreCloseNodes() {
            if (!exploring) {
                return;
            }
            foreach (var option in Dir) {
                Vector2D eLoc = pExploration.GetPosition().CorrectionPos(option);
                if (space.ContainsKey(eLoc)) {
                    Node nLoc = space[eLoc];
                    if (!nLoc.explore) {
                        nLoc.explored = pExploration;
                        queue.Enqueue(nLoc);
                        nLoc.explore = true;
                    }
                }
            }
        }
        public void CreatePath() {
            SetPath(End);
            Node lastPath = End.explored;
            while (lastPath != First) {
                SetPath(lastPath); mainPath.Add(lastPath); lastPath = lastPath.explored;
            }
            SetPath(First); path.Reverse();
        }
        public void SetPath(Node node) {
            path.Add(node);
        }
        public void ShowPath() {
            foreach (var item in mainPath) {
                Console.WriteLine(item.location);
            }
        }
    }
}
