using System;
using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;

public class AStarPathfindingSystem : IPathfindingLogic
{
    private INode m_startNode;
    private IEnumerable<INode> m_nodePath;
    private INode m_endNode;
    private int m_numberOfTest;

    public void Next()
    {
        m_numberOfTest++;
        var currentNode = FindTheBestNodeToEvaluate();
        if (currentNode.Equals(m_endNode))
        {
            IsDone = true;
            m_nodePath = ReverseNodePath(currentNode);
            Debug.Log(m_numberOfTest);
            return;
        }
        var neighbours = currentNode.GetNeighbourNodes();
        foreach (var node in neighbours)
        {
            if (ThisNodeCoordinateIsAlreadyInTheList(node, ListOfNextNodesToEvaluate)) continue;
            if (ThisNodeCoordinateIsAlreadyInTheList(node, ListOfEvaluatedNodes)) continue;
            
            if (!node.IsAccessible) continue;
            node.ParentNode = currentNode;
            node.GetTotalCost(m_endNode);
            
            ListOfNextNodesToEvaluate.Add(node);
        }
        
        ListOfNextNodesToEvaluate.Remove(currentNode);
        ListOfEvaluatedNodes.Add(currentNode);
    }

    private INode FindTheBestNodeToEvaluate()
    {
        var potentialNextNodes = new List<INode>();
        var bestCost = float.PositiveInfinity;
        
        foreach (var VARIABLE in ListOfNextNodesToEvaluate)
        {
            if (VARIABLE.Cost > bestCost) continue;
            if (VARIABLE.Cost < bestCost)
            {
                potentialNextNodes.Clear();
                bestCost = VARIABLE.Cost;
                potentialNextNodes.Add(VARIABLE);
            }
            else
            {
                potentialNextNodes.Add(VARIABLE);
            }
            
        }

        if (potentialNextNodes.Count == 1) return potentialNextNodes[0];

        INode bestNodeToEvaluate = default;
        var bestHCost = float.PositiveInfinity;
        
        foreach (var nodeToEvaluate in potentialNextNodes)
        {
            if (nodeToEvaluate.HCost > bestHCost) continue;
            {
                bestHCost = nodeToEvaluate.HCost;
                bestNodeToEvaluate = nodeToEvaluate;
            }
        }

        return bestNodeToEvaluate;
    }

    public void SetStartNode(INode _node)
    {
        m_startNode = _node;
    }
    
    private static bool ThisNodeCoordinateIsAlreadyInTheList(INode _variable, IEnumerable<INode> _listOfAlreadyEvaluatedNode)
    {
        return _listOfAlreadyEvaluatedNode.Any(_node => _node.ColumnIndex == _variable.ColumnIndex && _node.LineIndex == _variable.LineIndex);
    }

    public bool IsDone { get; set; }
    public bool IsReady { get; set; }
    public bool HasReachTarget { get; set; }
    public List<INode> ListOfEvaluatedNodes { get; set; }
    public List<INode> ListOfNextNodesToEvaluate { get; set; }
    public void FindPath()
    {
        while (IsDone == false && ListOfNextNodesToEvaluate.Count>0)
        {
            Next();
        }
    }

    public void ResetEndNode()
    {
        m_endNode = default;
    }

    public void SetEndNode(INode _getNode)
    {
        m_endNode = _getNode;
    }

    public void Initialize()
    {
        ListOfNextNodesToEvaluate = new List<INode>();
        ListOfEvaluatedNodes = new List<INode>();
        ListOfNextNodesToEvaluate.Add(m_startNode);
        IsDone = false;
    }

    public IEnumerable<INode> GetPath()
    {
        return m_nodePath;
    }

    public INode GetStartNode()
    {
        return m_startNode;
    }

    public INode GetEndNode()
    {
        return m_endNode;
    }

    public INode GetVeryNextNode()
    {
        return FindTheBestNodeToEvaluate();
    }

    public void Reset()
    {
        m_endNode = default;
        m_startNode = default;
        IsDone = false;
    }
    
    private static IEnumerable<INode> ReverseNodePath(INode _currentNode)
    {
        var path = new List<INode>();
        var stepNode = _currentNode;
        while (stepNode.ParentNode != null)
        {
            path.Insert(0,stepNode);
            stepNode = stepNode.ParentNode;
        }
        path.Insert(0,stepNode);
        return path;
    }
}