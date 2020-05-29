using Ds.Generic.Tree;
using Ds.Helper;
using System.Runtime.InteropServices;
using Xunit;

namespace Ds.Test.Generic.Tree
{
    public class BinarySearchTreeUtc
    {
        [Fact]
        public void Add_AddsNewNode_WhenTheTreeIsEmpty()
        {
            var bt = new BinarySearchTree<int>();

            bt.Add(10);

            Assert.False(bt.IsEmpty);
            Assert.True(1 == bt.Count);
        }

        [Fact]
        public void Add_AddsNewNode_WhenTheTreeIsNotEmpty()
        {
            var bt = new BinarySearchTree<int>();

            bt.Add(5);
            bt.Add(4);
            bt.Add(3);
            bt.Add(2);
            bt.Add(1);
            bt.Add(6);
            bt.Add(7);
            bt.Add(8);
            bt.Add(9);

            Assert.False(bt.IsEmpty);
            Assert.True(9 == bt.Count);
        }

        [Theory]
        // When node is a leaf-node.
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 3, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 13, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 18, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 23, 11)]

        // When node has only one child(right or left).
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 16, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 10, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 6, 11)]

        // When node has 2 children. Node can a root-node.
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 15, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 5, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 12, 11)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 20, 11)]

        // When requested node does not exists.
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 100, 12)]
        public void Delete_PermanentlyDeletesTheNodeContainingSpecifiedData(
            string rawData
            , int itemToBeDeleted
            , int expectedCount)
        {
            var tree = new BinarySearchTree<int>(rawData.ConvertToInts());

            tree.Delete(itemToBeDeleted);

            Assert.False(tree.Contains(itemToBeDeleted));
            Assert.True(expectedCount == tree.Count);
        }

        [Theory]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 15, 12)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 23, 12)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 7, 12)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 13, 12)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 18, 12)]
        public void Contains_ReturnsTrue_WhenItemExists(
            string rawData
            , int itemToSearch
            , int expectedCount)
        {
            var tree = new BinarySearchTree<int>(rawData.ConvertToInts());

            var actualResult = tree.Contains(itemToSearch);

            Assert.True(actualResult);
            Assert.True(expectedCount == tree.Count);
        }

        [Theory]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", 100, 12)]
        [InlineData("15,5,3,12,10,13,6,7,16,20,18,23", -100, 12)]
        public void Contains_ReturnsFalse_WhenItemDoesNotExists(
            string rawData
            , int itemToSearch
            , int expectedCount)
        {
            var tree = new BinarySearchTree<int>(rawData.ConvertToInts());

            var actualResult = tree.Contains(itemToSearch);

            Assert.False(actualResult);
            Assert.True(expectedCount == tree.Count);
        }
    }
}
