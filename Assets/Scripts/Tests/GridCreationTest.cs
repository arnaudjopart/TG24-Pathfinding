using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GridSystem;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridCreationTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void has_a_2_by_2_grid_a_number_of_lines_equals_to_2()
        {
            var grid = new Grid<SimpleSquareNode>(2, 2);
            grid.Fill();
            int nbOfLines = grid.NbOfLines;
            Assert.AreEqual(nbOfLines,2);
        }
        [Test]
        public void has_the_node_at_0_0_only_2_neighbours()
        {
            var grid = new Grid<SimpleSquareNode>(2, 2);
            grid.Fill();
            var firstCell = grid.GetNodeAtIndexes(0, 0);
            Assert.AreEqual(2,firstCell.GetNumberOfNeighbours());
            var result = firstCell.GetNeighbourNodes();
            int[] test = {0, 1};
            var containsThisElement = CheckForElement(test, result);
            Assert.AreEqual(true,containsThisElement);
        }

        [Test]
        public void does_the_node_at_0_0_contains_0_1_coords()
        {
            var grid = new Grid<SimpleSquareNode>(2, 2);
            grid.Fill();
            var firstCell = grid.GetNodeAtIndexes(0, 0);
            Assert.AreEqual(2,firstCell.GetNumberOfNeighbours());
            var result = firstCell.GetNeighbourNodes();
            int[] test = {0, 1};
            var containsThisElement = CheckForElement(test, result);
            Assert.AreEqual(true,containsThisElement);
        }
        
        [Test]
        public void does_the_node_at_0_0_contains_1_0_coords()
        {
            var grid = new Grid<SimpleSquareNode>(2, 2);
            grid.Fill();
            var firstCell = grid.GetNodeAtIndexes(0, 0);
            Assert.AreEqual(2,firstCell.GetNumberOfNeighbours());
            var result = firstCell.GetNeighbourNodes();
            int[] test = {1, 0};
            var containsThisElement = CheckForElement(test, result);
            Assert.AreEqual(true,containsThisElement);
        }
        
        
        private bool CheckForElement(int[] _test, List<INode> _result)
        {
            return _result.Any(element => element.ColumnIndex == _test[0] && element.LineIndex == _test[1]);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GridCreationTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
