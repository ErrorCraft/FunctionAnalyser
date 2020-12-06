using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ColumnPosArgumentTests
    {
        [TestMethod]
        public void ColumnPosArgument_ParseShouldSucceed()
        {
            // Arrange
            ColumnPosArgument argument = new ColumnPosArgument();
            StringReader reader = new StringReader("1 2");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ColumnPosArgument_ParseShouldFail_BecauseInvalidInteger()
        {
            // Arrange
            ColumnPosArgument argument = new ColumnPosArgument();
            StringReader reader = new StringReader("1.0 2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ColumnPosArgument_ParseShouldSucceed_WithRelativeWorldCoordinates()
        {
            // Arrange
            ColumnPosArgument argument = new ColumnPosArgument();
            StringReader reader = new StringReader("~1.0 ~2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ColumnPosArgument_ParseShouldSucceed_WithEmptyRelativeWorldCoordinates()
        {
            // Arrange
            ColumnPosArgument argument = new ColumnPosArgument();
            StringReader reader = new StringReader("~ ~");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ColumnPosArgument_ParseShouldFail_WithLocalCoordinates_BecauseMixedCoordinateTypes()
        {
            // Arrange
            ColumnPosArgument argument = new ColumnPosArgument();
            StringReader reader = new StringReader("^1.0 ^2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ColumnPosArgument_ParseShouldFail_BecauseIncomplete()
        {
            // Arrange
            ColumnPosArgument argument = new ColumnPosArgument();
            StringReader reader = new StringReader("1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
