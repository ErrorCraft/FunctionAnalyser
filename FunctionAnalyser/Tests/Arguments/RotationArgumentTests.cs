using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class RotationArgumentTests
    {
        [TestMethod]
        public void RotationArgument_ParseShouldSucceed()
        {
            // Arrange
            RotationArgument argument = new RotationArgument();
            IStringReader reader = new IStringReader("1.0 2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void RotationArgument_ParseShouldSucceed_WithRelativeWorldCoordinates()
        {
            // Arrange
            RotationArgument argument = new RotationArgument();
            IStringReader reader = new IStringReader("~1.0 ~2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void RotationArgument_ParseShouldSucceed_WithEmptyRelativeWorldCoordinates()
        {
            // Arrange
            RotationArgument argument = new RotationArgument();
            IStringReader reader = new IStringReader("~ ~");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void RotationArgument_ParseShouldFail_WithLocalCoordinates_BecauseMixedCoordinateTypes()
        {
            // Arrange
            RotationArgument argument = new RotationArgument();
            IStringReader reader = new IStringReader("^1.0 ^2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void RotationArgument_ParseShouldFail_BecauseIncomplete()
        {
            // Arrange
            RotationArgument argument = new RotationArgument();
            IStringReader reader = new IStringReader("1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
