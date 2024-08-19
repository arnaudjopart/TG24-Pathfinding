using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using UnityEngine.WSA;

public class SimpleSquareGridView : MonoBehaviour
{
    [Tooltip("The number of columns")] [SerializeField]
    private int m_width = 10;

    [Tooltip("The number of lines")] [SerializeField]
    private int m_height = 10;

    [SerializeField] private TileBaseView m_prefab;
    private TileBaseView[][] m_gridOfTiles;

    private Grid<SimpleSquareNode> m_grid;

    private Plane m_plane;
    [SerializeField] Transform m_pointer;
    private SimpleSquareNode m_currentGridNode;
    private TileBaseView[][] m_tileGrid;
    private TileBaseView m_currentTile;

    // Start is called before the first frame update
    void Start()
    {
        m_grid = new Grid<SimpleSquareNode>(m_width, m_height);
        m_grid.Fill();
        m_grid.ConnectNodes();
        
        DrawGrid();
        m_plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void DrawGrid()
    {

        m_tileGrid = new TileBaseView[m_grid.NbOfColumns][];

        for (var i = 0; i < m_grid.NbOfColumns; i++)
        {
            var column = new TileBaseView[m_grid.NbOfLines];
            for (var j = 0; j < m_grid.NbOfLines; j++)
            {
                var tileStartPosition = new Vector3(i * 1, 0, j * 1);
                var tile = Instantiate(m_prefab, tileStartPosition, Quaternion.identity);
                tile.name = i + "-" + j;
                tile.GetComponent<TileBaseView>().ConnectToGrid(m_grid.GetNodeAtIndexes(i,j));
                if (m_grid.GetNodeAtIndexes(i, j).IsAccessible == false)
                    tile.GetComponent<TileBaseView>().SetAsObstacle();
                tile.transform.parent = transform;
                column[j] = tile;
            }

            m_tileGrid[i] = column;
        }
    }


    private void HighlightNeighbours()
    {
        var neighbours = m_currentGridNode.GetNeighbourNodes();
        foreach (var coordinates in neighbours)
        {
            var tile = m_tileGrid[coordinates.ColumnIndex][coordinates.LineIndex];
            tile.GetComponent<TileBaseView>().Interact();
        }
    }

    private void StopHighlightNeighbours()
    {
        var neighbours = m_currentGridNode.GetNeighbourNodes();
        foreach (var coordinates in neighbours)
        {
            var tile = m_tileGrid[coordinates.ColumnIndex][coordinates.LineIndex];
            tile.GetComponent<TileBaseView>().Reset();
        }
    }

    public bool DoesFindATileAtPosition(Vector3 _mousePosition)
    {
        var pointerRay = Camera.main.ScreenPointToRay(_mousePosition);
        if (!m_plane.Raycast(pointerRay, out var hit)) return false;
        var position = pointerRay.GetPoint(hit);

        m_pointer.position = position;
        var node = m_grid.GetNodeAtPosition(position);
        return node == default;
    }

    public TileBaseView SelectTileAtPosition(Vector3 _mousePosition)
    {
        var pointerRay = Camera.main.ScreenPointToRay(_mousePosition);
        if (!m_plane.Raycast(pointerRay, out var hit)) return null;
        var position = pointerRay.GetPoint(hit);

        m_pointer.position = position;
        var node = m_grid.GetNodeAtPosition(position);
        return node == default? null : m_tileGrid[node.ColumnIndex][node.LineIndex];
    }

    public void EvaluateTileAt(INode _variable)
    {
        var tile = m_tileGrid[_variable.ColumnIndex][_variable.LineIndex];
        tile.SetToNextEvaluate();
    }
    
    public void HighlightTileAt(INode _variable)
    {
        var tile = m_tileGrid[_variable.ColumnIndex][_variable.LineIndex];
        tile.Highlight();
    }

    public void ResetAllTiles()
    {
        for (var i = 0; i < m_grid.NbOfColumns; i++)
        {
            
            for (var j = 0; j < m_grid.NbOfLines; j++)
            {
                m_tileGrid[i][j].Reset();
            }
        }
    }

    public void DrawObstacles()
    {
        for (var i = 0; i < m_grid.NbOfColumns; i++)
        {
            
            for (var j = 0; j < m_grid.NbOfLines; j++)
            {
                if(m_tileGrid[i][j].GetNode().IsAccessible == false) m_tileGrid[i][j].SetAsObstacle();
            }
        }
    }

    public void DrawEvaluatedTiles(List<INode> _listOfAlreadyEvaluatedNodes)
    {
        foreach (var VARIABLE in _listOfAlreadyEvaluatedNodes)
        {
            EvaluateTileAt(VARIABLE);
        }
    }
}