using System.Collections;
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
                tile.transform.parent = transform;
                column[j] = tile;
            }

            m_tileGrid[i] = column;
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*var pointerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (m_plane.Raycast(pointerRay, out var hit))
        {
            var position = pointerRay.GetPoint(hit);

            m_pointer.position = position;
            var node = m_grid.GetNodeAtPosition(position);
            if (node == default)
            {
                if (m_currentGridNode != default)
                {
                    var previousTile = m_tileGrid[m_currentGridNode.ColumnIndex][m_currentGridNode.LineIndex];
                    previousTile.GetComponent<TileBaseView>().PrintNodeInfo();
                    previousTile.GetComponent<TileBaseView>().Reset();

                    StopHighlightNeighbours();
                }

                m_currentGridNode = default;
            }
            else
            {
                if (node != m_currentGridNode)
                {
                    if (m_currentGridNode != default)
                    {
                        var previousTile = m_tileGrid[m_currentGridNode.ColumnIndex][m_currentGridNode.LineIndex];
                        previousTile.GetComponent<TileBaseView>().PrintNodeInfo();
                        previousTile.GetComponent<TileBaseView>().Reset();
                        StopHighlightNeighbours();
                    }

                    m_currentTile = m_tileGrid[node.ColumnIndex][node.LineIndex];
                    m_currentTile.GetComponent<TileBaseView>().PrintNodeInfo();
                    m_currentTile.GetComponent<TileBaseView>().Highlight();
                    m_currentGridNode = node;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (m_currentGridNode != default)
                    {
                        HighlightNeighbours();
                    }
                }

            }
        }*/
    }

    private void HighlightNeighbours()
    {
        var neighbours = m_currentGridNode.GetNeighboursCoordinates();
        foreach (var coordinates in neighbours)
        {
            var tile = m_tileGrid[coordinates.ColumnIndex][coordinates.LineIndex];
            tile.GetComponent<TileBaseView>().Interact();
        }
    }

    private void StopHighlightNeighbours()
    {
        var neighbours = m_currentGridNode.GetNeighboursCoordinates();
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

    public void EvaluateTileAt(NodeCoordinates _variable)
    {
        var tile = m_tileGrid[_variable.ColumnIndex][_variable.LineIndex];
        tile.SetToNextEvaluate();
    }

    public Grid<SimpleSquareNode> GetGrid()
    {
        return m_grid;
    }

    public void HighlightTileAt(NodeCoordinates _variable)
    {
        var tile = m_tileGrid[_variable.ColumnIndex][_variable.LineIndex];
        tile.Highlight();
    }
}