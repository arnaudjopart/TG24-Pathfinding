using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;

internal class BruteForcePathfindingSystem : IPathfindingLogic
{
    private INode m_startNode;
    private INode m_endNode;
    
    private List<INode> m_nodePath;
    private int m_nbOfTests;


    public BruteForcePathfindingSystem()
    {

    }
    public void Next()
    {
        m_nbOfTests++;
        var currentNode = ListOfNextNodesToEvaluate[0];
        

        if (currentNode.Equals(m_endNode))
        {
            IsDone = true;
            Debug.Log(m_nbOfTests);
            m_nodePath = ReverseNodePath(currentNode);
            return;
        }
        
        ListOfNextNodesToEvaluate.RemoveAt(0);

        var neighbourNodes = currentNode.GetNeighbourNodes();
        foreach (var neighbour in neighbourNodes)
        {
            if (neighbour.IsAccessible == false) continue;
            if (!ThisNodeCoordinateIsAlreadyInTheList(neighbour,ListOfEvaluatedNodes) && !ThisNodeCoordinateIsAlreadyInTheList(neighbour,ListOfNextNodesToEvaluate) )
            {
                neighbour.ParentNode = currentNode;
                if(neighbour.IsAccessible) ListOfNextNodesToEvaluate.Add(neighbour);
            }
        }

        ListOfEvaluatedNodes.Add(currentNode);
        
        
    }

    public void SetStartNode(INode _node)
    {
        m_startNode = _node;
    }

    public bool IsDone { get; set; }

    public bool IsReady
    {
        get => m_startNode != default;
        set { }
    }

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
        return ListOfNextNodesToEvaluate[0];
    }

    public void Reset()
    {
        m_endNode = default;
        m_startNode = default;
        IsDone = false;
    }

    private static bool ThisNodeCoordinateIsAlreadyInTheList(INode _variable, List<INode> _listOfAlreadyEvaluatedNode)
    {
        return _listOfAlreadyEvaluatedNode.Any(VARIABLE => VARIABLE.ColumnIndex == _variable.ColumnIndex && VARIABLE.LineIndex == _variable.LineIndex);
    }
    
    private static List<INode> ReverseNodePath(INode _currentNode)
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