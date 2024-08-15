using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;
using Object = System.Object;

public class BasicPathfindingSystem : MonoBehaviour
{ 

    [SerializeField] private SimpleSquareGridView m_gridVisualiser;
    private Plane m_plane;
    private bool m_startTileHasBeenSelected;
    private TileBaseView m_startTile;
    private TileBaseView m_endTile;

    private Grid<SimpleSquareNode> m_grid;
    private List<NodeCoordinates> m_nextNodesToEvaluate = new List<NodeCoordinates>();
    private INode m_parentNode;
    private List<NodeCoordinates> m_listOfAlreadyEvaluatedNode = new List<NodeCoordinates>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
                    return;
                }
            }
            var possibleEndTile = m_gridVisualiser.SelectTileAtPosition(Input.mousePosition);
            if (possibleEndTile != null)
            {
                m_endTile = possibleEndTile;
                if (m_endTile == m_startTile) return;
                m_endTile.SelectAsPathEnd();
                Initialize();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (m_startTile != null && m_endTile != null)
            {
                Next();
            }
                
        }
        
    }

    private void Next()
    {
        var currentNode = m_nextNodesToEvaluate[0];
        Debug.Log("Evaluating : "+currentNode.ColumnIndex+"-"+currentNode.LineIndex);

        if (currentNode.Equals(m_endTile.GetNode().Coordinates))
        {
            Debug.Log("Found");
        }
        
        m_nextNodesToEvaluate.RemoveAt(0);
        var n = m_grid.GetNodeAtIndexes(currentNode.ColumnIndex, currentNode.LineIndex);

        var neighbourNodeCoordinates = n.GetNeighboursCoordinates();
        foreach (var VARIABLE in neighbourNodeCoordinates)
        {
            if (!ThisNodeCoordinateIsAlreadyInTheList(VARIABLE,m_listOfAlreadyEvaluatedNode) && !ThisNodeCoordinateIsAlreadyInTheList(VARIABLE,m_nextNodesToEvaluate) )
            {
                var node = m_grid.GetNodeAtIndexes(VARIABLE.ColumnIndex, VARIABLE.LineIndex);
                node.parentCoordinates = currentNode;
                if(node.IsAccessible) m_nextNodesToEvaluate.Add(VARIABLE);
            }
        }

        m_listOfAlreadyEvaluatedNode.Add(currentNode);
        
        foreach (var VARIABLE in m_nextNodesToEvaluate)
        {
            m_gridVisualiser.EvaluateTileAt(VARIABLE);
        }
        
        foreach (var VARIABLE in m_listOfAlreadyEvaluatedNode)
        {
            m_gridVisualiser.HighlightTileAt(VARIABLE);
        }
    }

    private bool ThisNodeCoordinateIsAlreadyInTheList(NodeCoordinates _variable, List<NodeCoordinates> _listOfAlreadyEvaluatedNode)
    {
        return _listOfAlreadyEvaluatedNode.Any(VARIABLE => VARIABLE.ColumnIndex == _variable.ColumnIndex && VARIABLE.LineIndex == _variable.LineIndex);
    }



    private void Initialize()
    {
        m_nextNodesToEvaluate.Add(m_startTile.GetNode().Coordinates);
        m_grid = m_gridVisualiser.GetGrid(); 

    }
    
}


