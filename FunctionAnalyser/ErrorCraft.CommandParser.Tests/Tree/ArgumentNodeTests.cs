using ErrorCraft.CommandParser.Arguments;
using ErrorCraft.CommandParser.Results;
using ErrorCraft.CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests.Tree {
    [TestClass]
    public class ArgumentNodeTests {
        [TestMethod]
        public void GetName_ReturnsCorrectValue() {
            ArgumentNode<int> argumentNode = new ArgumentNode<int>("foo", new IntegerArgument());
            string name = argumentNode.GetName();
            Assert.AreEqual("foo", name);
        }

        [TestMethod]
        public void Parse_IsSuccessful() {
            ArgumentNode<int> argumentNode = new ArgumentNode<int>("foo", new IntegerArgument());
            IStringReader stringReader = new StringReader("123");
            ParseResults parseResults = argumentNode.Parse(stringReader);
            Assert.IsTrue(parseResults.Successful);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful() {
            ArgumentNode<int> argumentNode = new ArgumentNode<int>("foo", new IntegerArgument());
            IStringReader stringReader = new StringReader("");
            ParseResults parseResults = argumentNode.Parse(stringReader);
            Assert.IsFalse(parseResults.Successful);
        }
    }
}
