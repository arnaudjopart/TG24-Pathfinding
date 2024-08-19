using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class EightMovesSquareNode : INode
{
    private readonly int[][] m_possibleNeighborsCoordinates;
    private readonly List<INode> m_neighbours;
    private int m_hCost;

    public EightMovesSquareNode()
    {
        m_possibleNeighborsCoordinates = new[]
        {
            new[] {1, 0},//RIGHT
            new[] {-1, 0}, //LEFT
            new[] {0, -1}, //DOWN
            new[] {0, 1},//TOP
            new[] {1, 1},//TOP RIGHT
            new[] {-1, 1},//TOP LEFT
            new[] {1, -1},//DOWN RIGHT
            new[] {-1, -1},//DOWN LEFT

        };
        m_neighbours = new List<INode>();
    }
    public int[][] GetNeighboursRelativeCoordinates()
    {
        return m_possibleNeighborsCoordinates;
    }

    public void SetIndexes(int _i, int _j)
    {
        ColumnIndex = _i;
        LineIndex = _j;
    }
    
    public bool IsAccessible { get; set; }
    public int LineIndex { get; private set; }
    public int ColumnIndex { get; private set; }
    public void AddNeighbour(INode _node)
    {
        m_neighbours.Add(_node);
    }

    public INode ParentNode { get; set; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int Cost { get; set; }

    public List<INode> GetNeighbourNodes()
    {
        return m_neighbours;
    }

    public bool Equals(INode _node)
    {
        throw new NotImplementedException();
    }

    public int DistanceToDestination(INode _destination)
    {
        var absoluteDeltaOnColumn = Mathf.Abs(_destination.ColumnIndex - ColumnIndex);
        var absoluteDeltaOnLine = Mathf.Abs(_destination.LineIndex - LineIndex);

        var smallestDelta = Mathf.Min(absoluteDeltaOnColumn, absoluteDeltaOnLine);
        var biggestDelta = Mathf.Max(absoluteDeltaOnColumn, absoluteDeltaOnLine);
        var distance = smallestDelta * 14 + (biggestDelta - smallestDelta) * 10;

        HCost = distance;
        return distance;

    }



    public int GetTotalCost(INode _destination)
    {
        Cost = DistanceToDestination(_destination) + DistanceToStart();
        return Cost;
    }

    public int DistanceToStart()
    {
        GCost = ParentNode == null ? GCost = 0: GCost = ParentNode.GCost + DistanceToDestination(ParentNode);
        return GCost;
    }
}