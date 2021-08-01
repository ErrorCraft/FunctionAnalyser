using ErrorCraft.CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests.Tree {
    [TestClass]
    public class LiteralNodeTests {
        [TestMethod]
        public void GetName_ReturnsLiteral() {
            LiteralNode literalNode = new LiteralNode("foo");
            string name = literalNode.GetName();
            Assert.AreEqual("foo", name);
        }
    }
}
