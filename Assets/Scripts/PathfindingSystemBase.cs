using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public abstract class PathfindingSystemBase : MonoBehaviour
{ 
    [SerializeField] protected SimpleSquareGridView m_gridVisualiser;
    protected IPathfindingLogic m_pathfindingSystem;

    private void Awake()
    {
        m_gridVisualiser = GetComponent<SimpleSquareGridView>();
        Init();
    }

    protected virtual void Init()
    {
        m_pathfindingSystem = new BruteForcePathfindingSystem();
    }
    
    // Update is called once per frame
    private void Update()
    {
        Tick();
        if(m_pathfindingSystem.IsDone) DrawPath();
    }

    protected virtual void Tick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_pathfindingSystem.Reset();
            m_gridVisualiser.ResetAllTiles();
            m_gridVisualiser.DrawObstacles();
        }
    }
    
    protected bool HasFoundANewEndNode(out TileBaseView _o)
    {
        _o = null;

        if (m_gridVisualiser.SelectTileAtPosition(Input.mousePosition, out var possibleEndTile) == false) return false;
        
        if (possibleEndTile.GetNode() == m_pathfindingSystem.GetStartNode()) return false;
        if (possibleEndTile.GetNode() == m_pathfindingSystem.GetEndNode()) return false;
        
        _o = possibleEndTile;
        return true;
    }

    private void DrawPath()
    {
        m_gridVisualiser.ResetAllTiles();
        m_gridVisualiser.DrawObstacles();
        m_gridVisualiser.DrawEvaluatedTiles(m_pathfindingSystem.ListOfEvaluatedNodes);

        var path = m_pathfindingSystem.GetPath();
        foreach (var node in path)
        {
            m_gridVisualiser.HighlightTileAt(node);
        }
    }
}