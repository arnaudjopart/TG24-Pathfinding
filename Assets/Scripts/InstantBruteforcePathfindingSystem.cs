using UnityEngine;

public class InstantBruteforcePathfindingSystem : PathfindingSystemBase
{
    private bool m_startTileHasBeenSelected;
    
    protected override void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_startTileHasBeenSelected == false)
            {
                if (m_gridVisualiser.SelectTileAtPosition(Input.mousePosition, out var tile))
                {
                    tile.SelectAsPathStart();
                    m_startTileHasBeenSelected = true;
                    m_pathfindingSystem.SetStartNode(tile.GetNode());
                    return;
                }
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_startTileHasBeenSelected = false;
            m_pathfindingSystem.Reset();
            m_gridVisualiser.ResetAllTiles();
            m_gridVisualiser.DrawObstacles();
        }

        if (m_pathfindingSystem.IsReady == false) return;
        if (!HasFoundANewEndNode(out var hit)) return;
        m_pathfindingSystem.ResetEndNode();
        
        m_pathfindingSystem.SetEndNode(hit.GetNode());
        m_pathfindingSystem.Initialize();
        m_pathfindingSystem.FindPath();
    }
}