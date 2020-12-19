using CommandParser;
using CommandParser.Arguments;
using CommandParser.Context;
using CommandParser.Results.Arguments;
using CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CommandParser.Providers.NodeBuilder;

namespace Tests.Tree
{
    [TestClass]
    public class LiteralNodeTests
    {
        [TestMethod]
        public void LiteralNode_ShouldParse()
        {
            // Arrange
            Node node = Literal("foo", true, null);
            IStringReader reader = new StringReader("foo");
            CommandContext context = new CommandContext(0);

            // Act
            node.Parse(reader, context);

            // Assert
            Assert.AreEqual(context.Results[0].GetResult(), new Literal("foo"));
        }
    }
}
