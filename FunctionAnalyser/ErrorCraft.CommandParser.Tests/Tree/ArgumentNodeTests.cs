using ErrorCraft.CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests.Tree {
    [TestClass]
    public class ArgumentNodeTests {
        [TestMethod]
        public void GetName_ReturnsCorrectValue() {
            ArgumentNode<int> argumentNode = new ArgumentNode<int>("foo");
            string name = argumentNode.GetName();
            Assert.AreEqual("foo", name);
        }
    }
}
