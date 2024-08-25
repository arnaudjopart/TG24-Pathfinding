using GridSystem;
using TMPro;
using UnityEngine;

public class AStarTile : TileBaseView
{
    [SerializeField] private TMP_Text m_coordinates;
    [SerializeField] private TMP_Text m_gCost;
    [SerializeField] private TMP_Text m_hCost;
    [SerializeField] private TMP_Text m_totalCost;
    
    public override void ConnectToGrid(INode _connectedNode)
    {
        base.ConnectToGrid(_connectedNode);
        m_coordinates.SetText(m_node.ColumnIndex+" - "+m_node.LineIndex);
    }
    
    public override void SetToNextEvaluate()
    {
        base.SetToNextEvaluate();
        m_gCost.SetText(m_node.GCost.ToString());
        m_hCost.SetText(m_node.HCost.ToString());
        m_totalCost.SetText(m_node.Cost.ToString());

    }
}