using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class BiomeArgumentTests
    {
        [TestMethod]
        public void BiomeArgument_ParseShouldSucceed()
        {
            // Arrange
            BiomeArgument argument = new BiomeArgument();
            IStringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BiomeArgument_ParseShouldFail()
        {
            // Arrange
            BiomeArgument argument = new BiomeArgument();
            IStringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
