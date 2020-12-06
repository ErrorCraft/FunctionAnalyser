using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class Vec3ArgumentTests
    {
        [TestMethod]
        public void Vec3Argument_ParseShouldSucceed()
        {
            // Arrange
            Vec3Argument argument = new Vec3Argument();
            StringReader reader = new StringReader("1.0 2.0 3.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec3Argument_ParseShouldSucceed_WithRelativeWorldCoordinates()
        {
            // Arrange
            Vec3Argument argument = new Vec3Argument();
            StringReader reader = new StringReader("~1.0 ~2.0 ~3.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec3Argument_ParseShouldSucceed_WithEmptyRelativeWorldCoordinates()
        {
            // Arrange
            Vec3Argument argument = new Vec3Argument();
            StringReader reader = new StringReader("~ ~ ~");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec3Argument_ParseShouldSucceed_WithLocalCoordinates()
        {
            // Arrange
            Vec3Argument argument = new Vec3Argument();
            StringReader reader = new StringReader("^1.0 ^2.0 ^3.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec3Argument_ParseShouldSucceed_WithEmptyLocalCoordinates()
        {
            // Arrange
            Vec3Argument argument = new Vec3Argument();
            StringReader reader = new StringReader("^ ^ ^");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Vec3Argument_ParseShouldFail_BecauseMixedCoordinateTypes()
        {
            // Arrange
            Vec3Argument argument = new Vec3Argument();
            StringReader reader = new StringReader("~1 ^2 3");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void Vec3Argument_ParseShouldFail_BecauseIncomplete()
        {
            // Arrange
            Vec3Argument argument = new Vec3Argument();
            StringReader reader = new StringReader("1.0 2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
