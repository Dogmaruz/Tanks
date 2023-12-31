﻿using System.Collections.Generic;
using UnityEngine;

public class A_Grid : MonoBehaviour
{
    public bool onlyDisplayPathGizmos;

    public LayerMask unwalkableMask;

    public Vector2 gridWorldSize;

    public float nodeRadius;

    private Node[,] grid;
    public Node[,] Grid { get => grid; set => grid = value; }

    private float nodeDiameter;

    private int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);

        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }


    // Создаем сетку и заполняем ее с учетом слоев
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);

                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask);

                grid[x, y] = new Node(walkable, worldPoint, x, y);

                grid[x, y].IsActive = !walkable;
            }
        }
    }

    public List<Node> GetWalkableNodes()
    {
        List<Node> walkableNodes = new List<Node>();

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if(grid[x, y].walkable == true) walkableNodes.Add(grid[x, y]);
            }
        }

        return walkableNodes;
    }

    //Получаем соседние клетки
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;

                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    //Получаем кординаты в сетке из мировых координат.
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;

        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);

        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);

        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> path;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        //if (onlyDisplayPathGizmos)
        //{
        //    if (path != null)
        //    {
        //        foreach (Node n in path)
        //        {
        //            Gizmos.color = Color.black;

        //            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
        //        }
        //    }
        //}
        //else
        //{

        //    if (grid != null)
        //    {
        //        foreach (Node n in grid)
        //        {
        //            Gizmos.color = (n.walkable) ? Color.white : Color.red;

        //            //if (path != null)
        //            //{
        //            //    if (path.Contains(n))
        //            //    {
        //            //        Gizmos.color = Color.black;
        //            //    }
        //            //}

        //            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
        //        }
        //    }
        //}
    }
}

public class Node : IHeapItem<Node>
{
    public bool walkable;

    public bool IsActive;

    public Vector3 worldPosition;

    public int gridX;

    public int gridY;

    public int gCost;

    public int hCost;

    public Node parent;

    int heapIndex;

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;

        worldPosition = _worldPos;

        gridX = _gridX;

        gridY = _gridY;
    }


    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);

        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}
