using System.Collections.Generic;
using GridSystem;

internal interface IPathfindingLogic
{
    void Next();
    void SetStartNode(INode _node);
    
    bool IsDone { get; set; }
    bool IsReady { get; set; }
    bool HasReachTarget { get; set; }
    List<INode> ListOfEvaluatedNodes { get; set; }
    void FindPath();
    void ResetEndNode();
    void SetEndNode(INode _getNode);
    void Initialize();
    IEnumerable<INode> GetPath();
}