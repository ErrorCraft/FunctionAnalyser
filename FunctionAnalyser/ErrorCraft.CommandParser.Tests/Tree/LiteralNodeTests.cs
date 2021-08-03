using ErrorCraft.CommandParser.Results;
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

        [TestMethod]
        public void Parse_IsSuccessful() {
            LiteralNode literalNode = new LiteralNode("abc");
            IStringReader stringReader = new StringReader("abc");
            ParseResults parseResults = literalNode.Parse(stringReader);
            Assert.IsTrue(parseResults.Successful);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful_BecauseStringLengthIsIncorrect() {
            LiteralNode literalNode = new LiteralNode("abc");
            IStringReader stringReader = new StringReader("ab");
            ParseResults parseResults = literalNode.Parse(stringReader);
            Assert.IsFalse(parseResults.Successful);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful_BecauseLiteralsDoNotMatch() {
            LiteralNode literalNode = new LiteralNode("abc");
            IStringReader stringReader = new StringReader("def");
            ParseResults parseResults = literalNode.Parse(stringReader);
            Assert.IsFalse(parseResults.Successful);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful_BecauseLiteralHasInvalidEnd() {
            LiteralNode literalNode = new LiteralNode("abc");
            IStringReader stringReader = new StringReader("abcd");
            ParseResults parseResults = literalNode.Parse(stringReader);
            Assert.IsFalse(parseResults.Successful);
        }
    }
}
