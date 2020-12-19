using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class AngleArgumentTests
    {
        [TestMethod]
        public void AngleArgument_ParseShouldSucceed()
        {
            // Arrange
            AngleArgument argument = new AngleArgument();
            IStringReader reader = new StringReader("1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void AngleArgument_ParseShouldSucceed_WithRelativeWorldCoordinates()
        {
            // Arrange
            AngleArgument argument = new AngleArgument();
            IStringReader reader = new StringReader("~1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void AngleArgument_ParseShouldSucceed_WithEmptyRelativeWorldCoordinates()
        {
            // Arrange
            AngleArgument argument = new AngleArgument();
            IStringReader reader = new StringReader("~");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void AngleArgument_ParseShouldFail_WithLocalCoordinates_BecauseMixedCoordinateTypes()
        {
            // Arrange
            AngleArgument argument = new AngleArgument();
            IStringReader reader = new StringReader("^1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
