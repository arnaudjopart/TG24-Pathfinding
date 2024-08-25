using System.Collections;
using UnityEngine;

public class CoroutineBruteForcePathfinding : PathfindingSystemBase
{
    public float m_coroutineDelay = .5f;
    private WaitForSeconds m_waitCoroutine;
    private bool m_isCoroutineRunning;

    protected override void Init()
    {
        base.Init();
        m_waitCoroutine = new WaitForSeconds(m_coroutineDelay);
    }

    protected override void Tick()
    {
        if (m_isCoroutineRunning) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (m_pathfindingSystem.GetStartNode() == default)
            {
                if (!m_gridVisualiser.SelectTileAtPosition(Input.mousePosition, out var tile)) return;
                tile.SelectAsPathStart();
                m_pathfindingSystem.SetStartNode(tile.GetNode());
                return;
            }

            if (!HasFoundANewEndNode(out var hit)) return;
            m_pathfindingSystem.ResetEndNode();
            hit.SelectAsPathEnd();
            m_pathfindingSystem.SetEndNode(hit.GetNode());
            m_pathfindingSystem.Initialize();

            StartCoroutine(Run());

        }
    }

    private IEnumerator Run()
    {
        m_isCoroutineRunning = true;

        while (m_pathfindingSystem.IsDone == false && m_pathfindingSystem.ListOfNextNodesToEvaluate.Count > 0)
        {
            yield return m_waitCoroutine;
            m_pathfindingSystem.Next();
            UpdateTiles();
        }
        
        m_isCoroutineRunning = true;
        
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
        
        m_gridVisualiser.SetVeryNextNodeToBeEvaluated(m_pathfindingSystem.GetVeryNextNode());
        
        m_gridVisualiser.SetAsPathStart(m_pathfindingSystem.GetStartNode());
        m_gridVisualiser.SetAsPathEnd(m_pathfindingSystem.GetEndNode());
    }
}