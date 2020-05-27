using Ds.Generic.Tree;
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
    }
}
