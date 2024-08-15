using System.Collections.Generic;

namespace GridSystem
{
    public interface INode
    {
        void SetNeighbours(int _lineIndex, int _column, int _nbLines, int _nbColumns);
        public HashSet<NodeCoordinates> GetNeighboursCoordinates();
        public int GetNumberOfNeighbours();
        
        bool IsAccessible { get; set; }
        
        public int LineIndex { get; }
        public int ColumnIndex { get; }
        NodeCoordinates Coordinates { get; set; }
    }
}