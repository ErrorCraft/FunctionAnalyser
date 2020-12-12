using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class BlockPosArgumentTests
    {
        [TestMethod]
        public void BlockPosArgument_ParseShouldSucceed()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("1 2 3");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockPosArgument_ParseShouldFail_BecauseInvalidInteger()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("1.0 2.0 3.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void BlockPosArgument_ParseShouldSucceed_WithRelativeWorldCoordinates()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("~1.0 ~2.0 ~3.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockPosArgument_ParseShouldSucceed_WithEmptyRelativeWorldCoordinates()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("~ ~ ~");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockPosArgument_ParseShouldSucceed_WithLocalCoordinates()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("^1.0 ^2.0 ^3.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockPosArgument_ParseShouldSucceed_WithEmptyLocalCoordinates()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("^ ^ ^");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BlockPosArgument_ParseShouldFail_BecauseMixedCoordinateTypes()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("~1 ^2 3");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void BlockPosArgument_ParseShouldFail_BecauseIncomplete()
        {
            // Arrange
            BlockPosArgument argument = new BlockPosArgument();
            IStringReader reader = new IStringReader("1.0 2.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
