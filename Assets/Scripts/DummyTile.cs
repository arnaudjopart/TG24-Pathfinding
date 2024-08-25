using System.Collections;
using System.Collections.Generic;
using GridSystem;
using TMPro;
using UnityEngine;

public class DummyTile : TileBaseView
{
    [SerializeField] private TMP_Text m_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ConnectToGrid(INode _connectedNode)
    {
        base.ConnectToGrid(_connectedNode);
        m_text.SetText(m_node.ColumnIndex+" - "+m_node.LineIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}