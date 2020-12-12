using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class TeamArgumentTests
    {
        [TestMethod]
        public void TeamArgument_ParseShouldSucceed()
        {
            // Arrange
            TeamArgument argument = new TeamArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void TeamArgument_ParseShouldSucceed_WhenEmpty()
        {
            // Arrange
            TeamArgument argument = new TeamArgument();
            IStringReader reader = new IStringReader("");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void TeamArgument_StringReaderShouldHaveTrailingCharacters()
        {
            // Arrange
            TeamArgument argument = new TeamArgument();
            IStringReader reader = new IStringReader("foo!@#");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(reader.CanRead(3));
        }
    }
}
