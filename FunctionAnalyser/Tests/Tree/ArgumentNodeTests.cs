using CommandParser;
using CommandParser.Arguments;
using CommandParser.Context;
using CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CommandParser.Providers.NodeBuilder;

namespace Tests.Tree
{
    [TestClass]
    public class ArgumentNodeTests
    {
        [TestMethod]
        public void ArgumentNode_ShouldParse()
        {
            // Arrange
            Node node = Argument("foo", new IntegerArgument(), true, null);
            IStringReader reader = new IStringReader("123 456");
            CommandContext context = new CommandContext(0);

            // Act
            node.Parse(reader, context);

            // Assert
            Assert.AreEqual(context.Results[0].GetResult(), 123);
        }
    }
}
