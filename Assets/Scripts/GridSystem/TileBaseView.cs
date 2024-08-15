using UnityEngine;

namespace GridSystem
{
    public abstract class TileBaseView : MonoBehaviour
    {
        public float Width;
        public float Height;
        protected INode m_node;

        public void ConnectToGrid(INode _connectedNode)
        {
            m_node = _connectedNode;
        }

        public void PrintNodeInfo()
        {
            Debug.Log("Column: "+m_node.ColumnIndex+ " Line: "+m_node.LineIndex);
        }

        public abstract void Highlight();
        public abstract void Reset();
        public abstract void Interact();

        public abstract void SelectAsPathStart();

        public abstract void SelectAsPathEnd();

        public INode GetNode()
        {
            return m_node;
        }

        public abstract void SetToNextEvaluate();
    }
}