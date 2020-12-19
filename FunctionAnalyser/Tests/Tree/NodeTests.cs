using CommandParser;
using CommandParser.Arguments;
using CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static CommandParser.Providers.NodeBuilder;

namespace Tests.Tree
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void Node_ShouldGetLiteralChildOnly_AsRelevantNodes()
        {
            // Arrange
            RootNode root = new RootNode();
            Node child1 = Literal("foo", true, null);
            Node child2 = Literal("bar", true, null);
            Node child3 = Argument("baz", new IntegerArgument(), true, null);
            Node child4 = Argument("qux", new BooleanArgument(), true, null);
            root.AddChild(child1);
            root.AddChild(child2);
            root.AddChild(child3);
            root.AddChild(child4);
            IStringReader reader = new StringReader("foo");

            // Act
            List<Node> relevantNodes = new List<Node>(root.GetRelevantNodes(reader));

            // Assert
            Assert.AreEqual(relevantNodes[0], child1);
        }

        [TestMethod]
        public void Node_ShouldGetArgumentChildren_AsRelevantNodes()
        {
            // Arrange
            RootNode root = new RootNode();
            Node child1 = Literal("foo", true, null);
            Node child2 = Literal("bar", true, null);
            Node child3 = Argument("baz", new IntegerArgument(), true, null);
            Node child4 = Argument("qux", new BooleanArgument(), true, null);
            root.AddChild(child1);
            root.AddChild(child2);
            root.AddChild(child3);
            root.AddChild(child4);
            IStringReader reader = new StringReader("123");

            // Act
            List<Node> relevantNodes = new List<Node>(root.GetRelevantNodes(reader));

            // Assert
            Assert.AreEqual(relevantNodes.Count, 2);
        }

        [TestMethod]
        public void Node_ShouldAddChildren()
        {
            // Arrange
            Node root = new RootNode();
            Node child1 = Argument("foo", new IntegerArgument(), true, null);
            Node child2 = Argument("bar", new IntegerArgument(), true, null);
            Node child3 = Argument("baz", new IntegerArgument(), true, null);

            // Act
            root.AddChild(child1);
            root.AddChild(child2);
            root.AddChild(child3);

            // Assert
            Assert.AreEqual(root.Children.Count, 3);
        }
    }
}
