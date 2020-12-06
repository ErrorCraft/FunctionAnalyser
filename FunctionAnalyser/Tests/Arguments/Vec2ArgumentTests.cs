using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class Vec2ArgumentTests
    {
        [TestMethod]
        public void Vec2Argument_ParseShouldSucceed()
        {
            // Arrange
            Vec2Argument argument = new Vec2Argument();
            StringReader reader = new StringReader("1.0 2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec2Argument_ParseShouldSucceed_WithRelativeWorldCoordinates()
        {
            // Arrange
            Vec2Argument argument = new Vec2Argument();
            StringReader reader = new StringReader("~1.0 ~2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec2Argument_ParseShouldSucceed_WithEmptyRelativeWorldCoordinates()
        {
            // Arrange
            Vec2Argument argument = new Vec2Argument();
            StringReader reader = new StringReader("~ ~");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec2Argument_ParseShouldFail_WithLocalCoordinates_BecauseMixedCoordinateTypes()
        {
            // Arrange
            Vec2Argument argument = new Vec2Argument();
            StringReader reader = new StringReader("^1.0 ^2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void Vec2Argument_ParseShouldFail_BecauseIncomplete()
        {
            // Arrange
            Vec2Argument argument = new Vec2Argument();
            StringReader reader = new StringReader("1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
