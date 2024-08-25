using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            var absoluteDeltaOnColumn = Mathf.Abs(_destination.ColumnIndex - ColumnIndex);
            var absoluteDeltaOnLine = Mathf.Abs(_destination.LineIndex - LineIndex);

            
        
            return absoluteDeltaOnColumn+absoluteDeltaOnLine;

        }



        public int GetTotalCost(INode _destination)
        {
            HCost = DistanceToDestination(_destination);
            GCost = DistanceToStart();
            Cost = HCost + GCost;
            return Cost;
        }

        public int DistanceToStart()
        {
            GCost = ParentNode == null ? GCost = 0: GCost = ParentNode.GCost + DistanceToDestination(ParentNode);
            return GCost;
        }
    }

}