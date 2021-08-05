using ErrorCraft.CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ErrorCraft.CommandParser.Tests.Tree {
    [TestClass]
    public class RootNodeTests {
        [TestMethod]
        public void GetName_ReturnsEmptyString() {
            RootNode rootNode = new RootNode();
            string name = rootNode.GetName();
            Assert.AreEqual("", name);
        }

        [TestMethod]
        public void Parse_ThrowsException() {
            RootNode rootNode = new RootNode();
            IStringReader stringReader = new StringReader("");
            Assert.ThrowsException<NotSupportedException>(() => { rootNode.Parse(stringReader); });
        }
    }
}
