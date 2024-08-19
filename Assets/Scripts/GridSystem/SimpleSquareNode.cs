using System.Collections;
using System.Collections.Generic;

namespace GridSystem
{
    public class SimpleSquareNode : INode
    {
       
        private readonly int[][] m_possibleNeighborsCoordinates;
        private readonly List<INode> m_listOfNeighbours = new();

        public SimpleSquareNode()
        {
            m_possibleNeighborsCoordinates = new[]
            {
                new[] {1, 0},//RIGHT
                new[] {-1, 0}, //LEFT
                new[] {0, -1}, //DOWN
                new[] {0, 1},//TOP
                
                
                
            };
        }
        public void SetIndexes(int _i, int _j)
        {
            IsAccessible = true;
            ColumnIndex = _i;
            LineIndex = _j;
        }

        
        public int GetNumberOfNeighbours()
        {
            return m_listOfNeighbours.Count;
        }

        public bool IsAccessible { get; set; }
        public int LineIndex { get; private set; }

        public int ColumnIndex { get; private set; }

        public void AddNeighbour(INode _node)
        {
            m_listOfNeighbours.Add(_node);
        }

        public INode ParentNode { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int Cost { get; set; }

        public int[][] GetNeighboursRelativeCoordinates()
        {
            return m_possibleNeighborsCoordinates;
        }

        public List<INode> GetNeighbourNodes()
        {
            return m_listOfNeighbours;
        }
        
        public bool Equals(INode other)
        {
            return ColumnIndex == other.ColumnIndex && LineIndex == other.LineIndex;
        }

        public int DistanceToDestination(INode _destination)
        {
            return 1;
        }

        public int DistanceToStart()
        {
            return 1;
        }
    }

}