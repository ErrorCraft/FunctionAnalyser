using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class NbtPathArgumentTests
    {
        [TestMethod]
        public void NbtPathArgument_ParseShouldSucceed()
        {
            // Arrange
            NbtPathArgument argument = new NbtPathArgument();
            IStringReader reader = new IStringReader("foo.bar[0].baz{hello: 'world'}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void NbtPathArgument_ParseShouldSucceed_WithQuotedValue()
        {
            // Arrange
            NbtPathArgument argument = new NbtPathArgument();
            IStringReader reader = new IStringReader("foo.\"bar with \\\"weird\\\" characters\"[0].baz{hello: 'world'}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void NbtPathArgument_ParseShouldSucceed_WithCompoundSelector()
        {
            // Arrange
            NbtPathArgument argument = new NbtPathArgument();
            IStringReader reader = new IStringReader("foo[{bar: 'baz'}]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
