using System;
using UnityEngine;

namespace GridSystem
{
    public class Grid<T> where T: INode, new()
    {
        private readonly int m_nbLines;
        private readonly int m_nbColumns;

        private T[][] m_nodes;

        public Grid(int _nbLines, int _nbColumns)
        {
            m_nbLines = _nbLines;
            m_nbColumns = _nbColumns;
        
        }

        public int NbOfLines => m_nodes.Length;
        public int NbOfColumns => m_nbColumns;

        public void Fill()
        {
            m_nodes = new T[m_nbColumns][];
            for (var i = 0; i < m_nbColumns; i++)
            {
                var lineOfNode = new T[m_nbLines];
                for (var j = 0; j < m_nbLines; j++)
                {
                    var newNode = new T();
                    newNode.SetNeighbours(i,j, m_nbLines,m_nbColumns);
                    lineOfNode[j] = newNode;
                }
                m_nodes[i] = lineOfNode;
            }
        }

        /*public void Connect()
        {
            for (var i = 0; i < m_nbColumns; i++)
            {
                
                for (var j = 0; j < m_nbLines; j++)
                {
                    m_nodes[i][j].SetNeighbours(i,j, m_nbLines,m_nbColumns);
                }
            }
        }*/

        public T GetNodeAtIndexes(int _column,int _line)
        {
            if (_line < 0 || _line >= m_nbLines) return default(T);
            if (_column < 0 || _column >= m_nbColumns) return default(T);
            return m_nodes[_column][_line];
        }
        

        public T GetNodeAtPosition(Vector3 _position)
        {
            var columnIndex = Mathf.FloorToInt(_position.x);
            var lineIndex = Mathf.FloorToInt(_position.z);

            var selectedNode = GetNodeAtIndexes(columnIndex,lineIndex);
            return selectedNode;
        }
    }
}