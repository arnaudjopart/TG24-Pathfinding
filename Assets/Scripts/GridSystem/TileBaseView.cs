using System;
using TMPro;
using UnityEngine;

namespace GridSystem
{
    public abstract class TileBaseView : MonoBehaviour
    {
        protected INode m_node;
        
        [SerializeField] private MeshRenderer m_renderer;
        [SerializeField] private Material m_highlightMaterial;
        private Material m_defaultMaterial;
        [SerializeField] private Material m_interactMaterial;
        [SerializeField] private Material m_obstacleMaterial;
        [SerializeField] private Material m_startMaterial;
        [SerializeField] private Material m_veryNextToBeEvaluatedMaterial;
        [SerializeField] private Material m_endMaterial;

        private void Awake()
        {
            m_defaultMaterial = m_renderer.material;
        }

        public virtual void ConnectToGrid(INode _connectedNode)
        {
            m_node = _connectedNode;
        }

        public void PrintNodeInfo()
        {
            Debug.Log("Column: "+m_node.ColumnIndex+ " Line: "+m_node.LineIndex);
        }
        

        public INode GetNode()
        {
            return m_node;
        }


        public void SetAsObstacle()
        {
            m_renderer.material = m_obstacleMaterial;
        }
        
        public void Highlight()
        {
            m_renderer.material = m_highlightMaterial;
        }

        public void Reset()
        {
            m_renderer.material = m_defaultMaterial;
        }

        public void Interact()
        {
            m_renderer.material = m_interactMaterial;
        }

        public void SelectAsPathStart()
        {
            m_renderer.material = m_startMaterial;
        }

        public void SelectAsPathEnd()
        {
            m_renderer.material = m_endMaterial;
        }

        public virtual void SetToNextEvaluate()
        {
            m_renderer.material = m_interactMaterial;
        }
    
        public void SetToVeryNextEvaluate()
        {
            m_renderer.material = m_veryNextToBeEvaluatedMaterial;
        }
    }
}