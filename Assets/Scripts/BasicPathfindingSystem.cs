using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;

public class BasicPathfindingSystem : MonoBehaviour
{ 

    [SerializeField] private SimpleSquareGridView m_gridVisualiser;
    private Plane m_plane;
    private bool m_startTileHasBeenSelected;
    private TileBaseView m_startTile;
    private TileBaseView m_endTile;
    
    private List<INode> m_nextNodesToEvaluate;
    private List<INode> m_listOfAlreadyEvaluatedNodes;
    private bool m_targetReached;
    private List<INode> m_nodePath;

    private IPathfindingLogic m_pathfindingSystem;
    [SerializeField] private bool m_automaticPathDetection;


    // Start is called before the first frame update
    void Start()
    {
        m_pathfindingSystem = new BruteForcePathfindingSystem(m_gridVisualiser);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_automaticPathDetection)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_startTileHasBeenSelected == false)
                {
                    m_startTile = m_gridVisualiser.SelectTileAtPosition(Input.mousePosition);
                    if (m_startTile != null)
                    {
                        m_startTile.SelectAsPathStart();
                        m_startTileHasBeenSelected = true;
                        m_pathfindingSystem.SetStartNode(m_startTile.GetNode());
                        return;
                    }
                }
            }
            
            if (m_pathfindingSystem.IsReady && m_automaticPathDetection)
            {
                var possibleEndTile = m_gridVisualiser.SelectTileAtPosition(Input.mousePosition);
                if (possibleEndTile == null) return;
                if (possibleEndTile == m_startTile) return;
                if (possibleEndTile != m_endTile)
                {
                    if (m_endTile != null) m_endTile.Reset();
                    m_pathfindingSystem.ResetEndNode();
                    m_endTile = possibleEndTile;
                    m_endTile.SelectAsPathEnd();
                    m_pathfindingSystem.SetEndNode(m_endTile.GetNode());
                    m_pathfindingSystem.Initialize();
            
                    m_pathfindingSystem.FindPath();
                }
            }
            
            if (m_pathfindingSystem.IsDone)
            {
                DrawPath();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                var selectedTile = m_gridVisualiser.SelectTileAtPosition(Input.mousePosition);
                if (m_startTileHasBeenSelected == false)
                {
                    m_startTile = selectedTile;
                    
                    if (m_startTile != null)
                    {
                        m_startTile.SelectAsPathStart();
                        m_startTileHasBeenSelected = true;
                        m_pathfindingSystem.SetStartNode(m_startTile.GetNode());
                        return;
                    }

                    return;
                }


                var possibleEndTile = selectedTile;
                if (possibleEndTile == null) return;
                if (possibleEndTile == m_startTile) return;
                if (possibleEndTile != m_endTile)
                {
                    if (m_endTile != null) m_endTile.Reset();
                    m_pathfindingSystem.ResetEndNode();
                    m_endTile = possibleEndTile;
                    m_endTile.SelectAsPathEnd();
                    m_pathfindingSystem.SetEndNode(m_endTile.GetNode());
                    m_pathfindingSystem.Initialize();
                
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (m_startTile != null && m_endTile != null) 
                {
                    m_pathfindingSystem.Next();
                }
            }
        }
        

        if (m_pathfindingSystem.IsReady && m_automaticPathDetection)
        {
            
            var possibleEndTile = m_gridVisualiser.SelectTileAtPosition(Input.mousePosition);
            if (possibleEndTile == null) return;
            if (possibleEndTile == m_startTile) return;
            if (possibleEndTile != m_endTile)
            {
                if (m_endTile != null) m_endTile.Reset();
                m_pathfindingSystem.ResetEndNode();
                m_endTile = possibleEndTile;
                m_endTile.SelectAsPathEnd();
                m_pathfindingSystem.SetEndNode(m_endTile.GetNode());
                m_pathfindingSystem.Initialize();
            
                m_pathfindingSystem.FindPath();
            }
        }
        
        
        if (m_pathfindingSystem.IsDone)
        {
            DrawPath();
                
        }
        
    }

    private void DrawPath()
    {
        m_gridVisualiser.ResetAllTiles();
        m_gridVisualiser.DrawObstacles();
        m_gridVisualiser.DrawEvaluatedTiles(m_pathfindingSystem.ListOfEvaluatedNodes);

        var path = m_pathfindingSystem.GetPath();
        foreach (var VARIABLE in path)
        {
            m_gridVisualiser.HighlightTileAt(VARIABLE);
        }
    }
}

internal class BruteForcePathfindingSystem : IPathfindingLogic
{
    private INode m_startNode;
    private INode m_endNode;
    
    private List<INode> m_nextNodesToEvaluate;

    private List<INode> m_nodePath;
    private readonly SimpleSquareGridView m_view;

    public BruteForcePathfindingSystem(SimpleSquareGridView _view)
    {
        m_view = _view;
    }
    public void Next()
    {
        var currentNode = m_nextNodesToEvaluate[0];
        Debug.Log("Evaluating : "+currentNode.ColumnIndex+"-"+currentNode.LineIndex);

        if (currentNode.Equals(m_endNode))
        {
            IsDone = true;
            Debug.Log("Found");
            m_nodePath = ReverseNodePath(currentNode);
            return;
        }
        
        m_nextNodesToEvaluate.RemoveAt(0);

        var neighbourNodes = currentNode.GetNeighbourNodes();
        foreach (var neighbour in neighbourNodes)
        {
            if (neighbour.IsAccessible == false) continue;
            if (!ThisNodeCoordinateIsAlreadyInTheList(neighbour,ListOfEvaluatedNodes) && !ThisNodeCoordinateIsAlreadyInTheList(neighbour,m_nextNodesToEvaluate) )
            {
                neighbour.ParentNode = currentNode;
                if(neighbour.IsAccessible) m_nextNodesToEvaluate.Add(neighbour);
            }
        }

        ListOfEvaluatedNodes.Add(currentNode);
        
        foreach (var VARIABLE in m_nextNodesToEvaluate)
        {
            m_view.EvaluateTileAt(VARIABLE);
        }
        
        foreach (var VARIABLE in ListOfEvaluatedNodes)
        {
            m_view.HighlightTileAt(VARIABLE);
        }
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
    public void FindPath()
    {
        while (IsDone == false && m_nextNodesToEvaluate.Count>0)
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
        m_nextNodesToEvaluate = new List<INode>();
        ListOfEvaluatedNodes = new List<INode>();
        m_nextNodesToEvaluate.Add(m_startNode);
        IsDone = false;
    }

    public IEnumerable<INode> GetPath()
    {
        return m_nodePath;
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

        return path;
    }
}