using ErrorCraft.CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests.Tree {
    [TestClass]
    public class NodeTests {
        [TestMethod]
        public void AddChild_AddsChild() {
            Node node = new LiteralNode("");
            node.AddChild(new LiteralNode("foo"));
            Assert.AreEqual(1, node.ChildCount);
        }

        [TestMethod]
        public void IsExecutable_ReturnsFalse() {
            Node node = new LiteralNode("", false);
            bool executable = node.IsExecutable();
            Assert.IsFalse(executable);
        }

        [TestMethod]
        public void IsExecutable_ReturnsTrue() {
            Node node = new LiteralNode("", true);
            bool executable = node.IsExecutable();
            Assert.IsTrue(executable);
        }
    }
}
