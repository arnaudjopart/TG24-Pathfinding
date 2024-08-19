using System;
using UnityEngine;
using Random = System.Random;

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
                    newNode.SetIndexes(i,j);
                    newNode.IsAccessible = UnityEngine.Random.value > .3f;
                    lineOfNode[j] = newNode;
                }
                m_nodes[i] = lineOfNode;
            }
        }

        public void ConnectNodes()
        {
            for (var i = 0; i < m_nbColumns; i++)
            {
                for (var j = 0; j < m_nbLines; j++)
                {
                    var node = m_nodes[i][j];
                    ConnectNodeToTheirNeighbours(i,j,node);
                }
            }
        }

        private void ConnectNodeToTheirNeighbours(int _columnIndex, int _lineIndex, T _node)
        {
            var listOfNeighboursCoordinates = _node.GetNeighboursRelativeCoordinates();
            foreach (var coordinate in listOfNeighboursCoordinates)
            {
                var possibleColumnIndex = _columnIndex + coordinate[0];
                var possibleLineIndex = _lineIndex + coordinate[1];
                
                if(possibleColumnIndex<0 || possibleLineIndex<0) continue;
                if(possibleColumnIndex>=m_nbColumns || possibleLineIndex>=m_nbLines) continue;

                var neighbour = GetNodeAtIndexes(possibleColumnIndex, possibleLineIndex);
                _node.AddNeighbour(neighbour);
            }
        }

        public T GetNodeAtIndexes(int _column,int _line)
        {
            if (_line < 0 || _line >= m_nbLines) return default(T);
            if (_column < 0 || _column >= m_nbColumns) return default(T);
            return m_nodes[_column][_line];
        }
        

        public T GetNodeAtPosition(Vector3 _position)
        {
            var columnIndex = Mathf.FloorToInt(_position.x+.5f);
            var lineIndex = Mathf.FloorToInt(_position.z+.5f);

            var selectedNode = GetNodeAtIndexes(columnIndex,lineIndex);
            return selectedNode;
        }
    }
}