using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class BlockArgumentTests
    {
        [TestMethod]
        public void BlockArgument_ParseShouldSucceed()
        {
            // Arrange
            Blocks.Set("{\"foo\":{},\"bar\":{\"baz\":[\"true\",\"false\"]}}");
            BlockArgument argument = new BlockArgument();
            IStringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockArgument_ParseShouldFail_BecauseBlockDoesNotExist()
        {
            // Arrange
            Blocks.Set("{\"foo\":{},\"bar\":{\"baz\":[\"true\",\"false\"]}}");
            BlockArgument argument = new BlockArgument();
            IStringReader reader = new StringReader("baz");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void BlockArgument_ParseShouldSucceed_WithNamespace()
        {
            // Arrange
            Blocks.Set("{\"foo\":{},\"bar\":{\"baz\":[\"true\",\"false\"]}}");
            BlockArgument argument = new BlockArgument();
            IStringReader reader = new StringReader("minecraft:foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockArgument_ParseShouldSucceed_WithBlockStates()
        {
            // Arrange
            Blocks.Set("{\"foo\":{},\"bar\":{\"baz\":[\"true\",\"false\"]}}");
            BlockArgument argument = new BlockArgument();
            IStringReader reader = new StringReader("bar[baz=true]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockArgument_ParseShouldFail_BecauseUnknownBlockState()
        {
            // Arrange
            Blocks.Set("{\"foo\":{},\"bar\":{\"baz\":[\"true\",\"false\"]}}");
            BlockArgument argument = new BlockArgument();
            IStringReader reader = new StringReader("foo[baz=true]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void BlockArgument_ParseShouldSucceed_WithNbt()
        {
            // Arrange
            Blocks.Set("{\"foo\":{},\"bar\":{\"baz\":[\"true\",\"false\"]}}");
            BlockArgument argument = new BlockArgument();
            IStringReader reader = new StringReader("foo{bar: 'baz'}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockArgument_ParseShouldSucceed_WithBlockStatesAndNbt()
        {
            // Arrange
            Blocks.Set("{\"foo\":{},\"bar\":{\"baz\":[\"true\",\"false\"]}}");
            BlockArgument argument = new BlockArgument();
            IStringReader reader = new StringReader("bar[baz=true]{hello: 'world'}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
