using UnityEngine;

public class StepByStepBruteforcePathfinding : PathfindingSystemBase
{
    protected override void Init()
    {
        m_pathfindingSystem = new BruteForcePathfindingSystem();
    }

    protected override void Tick()
    {
        base.Tick();
        
        if (Input.GetMouseButtonDown(0))
        {
            if (m_pathfindingSystem.GetStartNode() == default)
            {
                if (!m_gridVisualiser.SelectTileAtPosition(Input.mousePosition, out var tile)) return;
                tile.SelectAsPathStart();
                m_pathfindingSystem.SetStartNode(tile.GetNode());
                return;
            }
            
            if (HasFoundANewEndNode(out var hit))
            {
                m_pathfindingSystem.ResetEndNode();
                hit.SelectAsPathEnd();
                m_pathfindingSystem.SetEndNode(hit.GetNode());
                m_pathfindingSystem.Initialize();
            }
            
        }

        if (!Input.GetKeyDown(KeyCode.Return)) return;
        m_pathfindingSystem.Next();
        UpdateTiles();
        
        
    }

    private void UpdateTiles()
    {
        foreach (var node in m_pathfindingSystem.ListOfNextNodesToEvaluate)
        {
            m_gridVisualiser.EvaluateTileAt(node);
        }
        
        foreach (var node in m_pathfindingSystem.ListOfEvaluatedNodes)
        {
            m_gridVisualiser.HighlightTileAt(node);
        }
        
        
        
        m_gridVisualiser.SetAsPathStart(m_pathfindingSystem.GetStartNode());
        m_gridVisualiser.SetAsPathEnd(m_pathfindingSystem.GetEndNode());
        
        m_gridVisualiser.SetVeryNextNodeToBeEvaluated(m_pathfindingSystem.GetVeryNextNode());
    }
}