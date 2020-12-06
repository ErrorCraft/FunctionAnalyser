using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class NbtTagArgumentTests
    {
        [TestMethod]
        public void NbtTagArgument_ParseShouldSucceed()
        {
            // Arrange
            NbtTagArgument argument = new NbtTagArgument();
            StringReader reader = new StringReader("{foo: 'bar', baz: 3}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void NbtTagArgument_ParseShouldSucceed_WithDifferentType()
        {
            // Arrange
            NbtTagArgument argument = new NbtTagArgument();
            StringReader reader = new StringReader("[1, 2, 3]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
