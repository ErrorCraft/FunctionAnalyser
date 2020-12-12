using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ScoreHolderArgumentTests
    {
        [TestMethod]
        public void ScoreHolderArgument_ParseShouldSucceed()
        {
            // Arrange
            ScoreHolderArgument argument = new ScoreHolderArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ScoreHolderArgument_ParseShouldSucceed_WithSelector()
        {
            // Arrange
            ScoreHolderArgument argument = new ScoreHolderArgument();
            IStringReader reader = new IStringReader("@a");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ScoreHolderArgument_ParseShouldSucceed_WithSpecialCharacters()
        {
            // Arrange
            ScoreHolderArgument argument = new ScoreHolderArgument();
            IStringReader reader = new IStringReader("!foo-bar@baz#");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ScoreHolderArgument_ShouldNotRead_SpaceCharacter()
        {
            // Arrange
            ScoreHolderArgument argument = new ScoreHolderArgument();
            IStringReader reader = new IStringReader("foo bar");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(reader.CanRead(4));
        }

        [TestMethod]
        public void ScoreHolderArgument_ParseShouldSucceed_WithSingleTarget_WhenLimitedToOneTarget()
        {
            // Arrange
            ScoreHolderArgument argument = new ScoreHolderArgument(false);
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ScoreHolderArgument_ParseShouldFail_WithAllPlayersSelector_WhenLimitedToOneTarget()
        {
            // Arrange
            ScoreHolderArgument argument = new ScoreHolderArgument(false);
            IStringReader reader = new IStringReader("@a");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ScoreHolderArgument_ParseShouldSucceeds_WithAllPlayersSelector_WhenLimitedToOneEntity_WhenLimitedToOneTarget()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"limit\":{\"predicate\":\"set_limit\"}}");
            ScoreHolderArgument argument = new ScoreHolderArgument(false);
            IStringReader reader = new IStringReader("@a[limit=1]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
