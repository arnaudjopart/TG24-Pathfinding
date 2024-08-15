using System;
using System.Collections.Generic;

namespace GridSystem
{
    public class SimpleSquareNode : INode
    {
        private HashSet<NodeCoordinates> m_neighbours;
        public NodeCoordinates parentCoordinates;
    
        public void SetNeighbours(int _i, int _j, int _nbLines, int _nbColumns)
        {
            IsAccessible = true;
            ColumnIndex = _i;
            LineIndex = _j;

            Coordinates = new NodeCoordinates(_i, _j);
            m_neighbours = new HashSet<NodeCoordinates>();
            var possibleNeighborsCoordinates = new[]
            {
                new[] {_i-1, _j}, //TOP
                new[] {_i, _j+1},//Right
                new[] {_i+1, _j},//BOTTOM
                new[] {_i, _j-1} //Left
            };

            foreach (var possibleNeighbor in possibleNeighborsCoordinates)
            {
                if(possibleNeighbor[0]<0 || possibleNeighbor[1]<0) continue;
                if(possibleNeighbor[0]>=_nbColumns || possibleNeighbor[1]>=_nbLines) continue;
                m_neighbours.Add(new NodeCoordinates(possibleNeighbor[0],possibleNeighbor[1]));
            }
        }

        
        public int GetNumberOfNeighbours()
        {
            return m_neighbours.Count;
        }

        public bool IsAccessible { get; set; }
        public int LineIndex { get; private set; }

        public int ColumnIndex { get; private set; }
        public NodeCoordinates Coordinates { get; set; }

        public HashSet<NodeCoordinates> GetNeighboursCoordinates()
        {
            return m_neighbours;
        }
    }

    [System.Serializable]
    public struct NodeCoordinates
    {

        public NodeCoordinates(int _columnsIndex, int _lineIndex)
        {
            ColumnIndex = _columnsIndex;
            LineIndex = _lineIndex;
        }

        public int ColumnIndex { get; }
        public int LineIndex { get; }
        
        public bool Equals(NodeCoordinates other)
        {
            return ColumnIndex == other.ColumnIndex && LineIndex == other.LineIndex;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ColumnIndex, LineIndex);
        }
    }
    
    
}