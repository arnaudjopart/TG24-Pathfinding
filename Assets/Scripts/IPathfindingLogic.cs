using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public interface IPathfindingLogic
{
    void Next();
    void SetStartNode(INode _node);
    bool IsDone { get; set; }
    bool IsReady { get; set; }
    bool HasReachTarget { get; set; }
    List<INode> ListOfEvaluatedNodes { get; set; }
    List<INode> ListOfNextNodesToEvaluate { get; set; }
    void FindPath();
    void ResetEndNode();
    void SetEndNode(INode _getNode);
    void Initialize();
    IEnumerable<INode> GetPath();
    INode GetStartNode();
    INode GetEndNode();
    INode GetVeryNextNode();
    void Reset();
}