using ErrorCraft.CommandParser.Results;
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

        [TestMethod]
        public void Parse_IsSuccessful() {
            ArgumentNode<int> argumentNode = new ArgumentNode<int>("foo");
            IStringReader stringReader = new StringReader("abc");
            ParseResults parseResults = argumentNode.Parse(stringReader);
            Assert.IsTrue(parseResults.Successful);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful() {
            ArgumentNode<int> argumentNode = new ArgumentNode<int>("foo");
            IStringReader stringReader = new StringReader("");
            ParseResults parseResults = argumentNode.Parse(stringReader);
            Assert.IsFalse(parseResults.Successful);
        }
    }
}
