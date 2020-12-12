using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ColourArgumentTests
    {
        [TestMethod]
        public void ColourArgument_ParseShouldSucceed()
        {
            // Arrange
            Colours.Set("[\"red\", \"green\"]");
            ColourArgument argument = new ColourArgument();
            IStringReader reader = new IStringReader("red");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ColourArgument_ParseShouldFail_BecauseInvalidColour()
        {
            // Arrange
            Colours.Set("[\"red\", \"green\"]");
            ColourArgument argument = new ColourArgument();
            IStringReader reader = new IStringReader("blue");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
