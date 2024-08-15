using System.Collections;
using System.Collections.Generic;
using GridSystem;
using TMPro;
using UnityEngine;

public class DummyTile : TileBaseView
{
    [SerializeField] private MeshRenderer m_renderer;
    [SerializeField] private Material m_highlightMaterial;
    private Material m_defaultMaterial;
    [SerializeField] private Material m_interactMaterial;
    [SerializeField] private TMP_Text m_text;

    // Start is called before the first frame update
    void Start()
    {
        m_defaultMaterial = m_renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        m_text.SetText(m_node.ColumnIndex+" - "+m_node.LineIndex);
    }

    public override void Highlight()
    {
        m_renderer.material = m_highlightMaterial;
    }

    public override void Reset()
    {
        m_renderer.material = m_defaultMaterial;
    }

    public override void Interact()
    {
        m_renderer.material = m_interactMaterial;
    }

    public override void SelectAsPathStart()
    {
        m_renderer.material = m_interactMaterial;
    }

    public override void SelectAsPathEnd()
    {
        m_renderer.material = m_highlightMaterial;
    }

    public override void SetToNextEvaluate()
    {
        m_renderer.material = m_interactMaterial;
    }
}
