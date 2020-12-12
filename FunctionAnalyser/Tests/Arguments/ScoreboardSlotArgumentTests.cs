using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ScoreboardSlotArgumentTests
    {
        [TestMethod]
        public void ScoreboardSlotArgument_ParseShouldSucceed()
        {
            // Arrange
            ScoreboardSlots.Set("{\"foo\":{}}");
            ScoreboardSlotArgument argument = new ScoreboardSlotArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ScoreboardSlotArgument_ParseShouldFail_BecauseInvalidScoreboardSlot()
        {
            // Arrange
            ScoreboardSlots.Set("{\"foo\":{}}");
            ScoreboardSlotArgument argument = new ScoreboardSlotArgument();
            IStringReader reader = new IStringReader("bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ScoreboardSlotArgument_ParseShouldSucceed_WithSlotType()
        {
            // Arrange
            Colours.Set("[\"red\",\"green\"]");
            ScoreboardSlots.Set("{\"foo\":{\"slot_type\":\"colour\"}}");
            ScoreboardSlotArgument argument = new ScoreboardSlotArgument();
            IStringReader reader = new IStringReader("foo.team.red");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ScoreboardSlotArgument_ParseShouldSucceed_WithOptionalSlotType()
        {
            // Arrange
            Colours.Set("[\"red\",\"green\"]");
            ScoreboardSlots.Set("{\"foo\":{\"contents_optional\":true,\"slot_type\":\"colour\"}}");
            ScoreboardSlotArgument argument = new ScoreboardSlotArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
