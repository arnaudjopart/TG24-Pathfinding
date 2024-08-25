using System.Collections.Generic;

namespace GridSystem
{
    public interface INode
    {
        public int[][] GetNeighboursRelativeCoordinates();

        public void SetIndexes(int _i, int _j);
        
        bool IsAccessible { get; set; }
        
        public int LineIndex { get; }
        public int ColumnIndex { get; }

        void AddNeighbour(INode _node);
        
        public INode ParentNode { get; set; }
        int GCost { get; set; }
        int HCost { get; set; }
        int Cost { get; set; }

        public List<INode> GetNeighbourNodes();
        
        public bool Equals(INode _node);

        public int DistanceToDestination(INode _destination);
        public int DistanceToStart();

        public int GetTotalCost(INode _destination);

    }
}