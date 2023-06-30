using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Pathfinding : MonoBehaviour, IDependency<A_Grid>
{
    private A_Grid grid;

    public void Construct(A_Grid obj)
    {
        grid = obj;
    }

    void Update() 
    {
        
    }

    public void FindPath(Vector3 startPos, Vector3 targetPos) 
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        Node startNode = grid.NodeFromWorldPoint(startPos);

        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize); // Set of nodes to be evaluated

        HashSet<Node> closedSet = new HashSet<Node>(); // Set of nodes already evaluated

        openSet.Add(startNode);

        while (openSet.Count > 0) 
        {
            Node node = openSet.RemoveFirst(); // Optimized (3ms)

            closedSet.Add(node);

            if (node == targetNode) 
            { // Поиск пути

                sw.Stop();

                print("Path found! " + sw.ElapsedMilliseconds + " ms");

                RetracePath(startNode, targetNode);

                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node)) 
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) 
                {
                    continue;
                }

                int newMovementCostToNeighbour = node.gCost + GetDistance(node, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) 
                {
                    neighbour.gCost = newMovementCostToNeighbour;

                    neighbour.hCost = GetDistance(neighbour, targetNode);

                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour)) 
                    {
                        openSet.Add(neighbour);
                    } 
                    else 
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode) 
    {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);

            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }

    // Диагональный метод
    int GetDistance(Node nodeA, Node nodeB) 
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);

        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY) 
        {
            return 14 * distY + 10 * (distX - distY);
        } 
        else 
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }
}
