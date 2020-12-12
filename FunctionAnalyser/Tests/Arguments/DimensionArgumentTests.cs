using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class DimensionArgumentTests
    {
        [TestMethod]
        public void DimensionArgument_ParseShouldSucceed()
        {
            // Arrange
            DimensionArgument argument = new DimensionArgument();
            IStringReader reader = new IStringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void DimensionArgument_ParseShouldFail()
        {
            // Arrange
            DimensionArgument argument = new DimensionArgument();
            IStringReader reader = new IStringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
