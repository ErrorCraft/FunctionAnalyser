using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class UuidArgumentTests
    {
        [TestMethod]
        public void UuidArgument_ParseShouldSucceed()
        {
            // Arrange
            UuidArgument argument = new UuidArgument();
            StringReader reader = new StringReader("1-2-3-4-5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void UuidArgument_ParseShouldFail_IncompleteUuid()
        {
            // Arrange
            UuidArgument argument = new UuidArgument();
            StringReader reader = new StringReader("1-2-3");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void UuidArgument_ShouldHaveTrailingCharacters()
        {
            // Arrange
            UuidArgument argument = new UuidArgument();
            StringReader reader = new StringReader("1-2-3-4-5!!!");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(reader.CanRead(3));
        }

        [TestMethod]
        public void UuidArgument_ParseShouldSucceed_WithDifferentCapitalisation()
        {
            // Arrange
            UuidArgument argument = new UuidArgument();
            StringReader reader = new StringReader("1-2-3-aA-fF");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
