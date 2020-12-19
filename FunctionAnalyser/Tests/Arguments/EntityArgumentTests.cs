using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class EntityArgumentTests
    {
        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithPlayername()
        {
            // Arrange
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("Steve");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(reader.CanRead());
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithUuid()
        {
            // Arrange
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("1-2-3-4-5");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(reader.CanRead());
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithSelector()
        {
            // Arrange
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@a");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithSelector_BecauseMissingSelectorType()
        {
            // Arrange
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithSelector_BecauseInvalidSelectorType()
        {
            // Arrange
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@m");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithSelector_WithArguments()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"foo\":{\"contents\":{\"type\":\"argument\",\"parser\":\"string\",\"properties\":{\"type\":\"word\"}}}, \"baz\":{\"contents\":{\"type\":\"argument\",\"parser\":\"integer\"}}}");
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@a[foo=bar, baz=3]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithSelector_WithArguments_BecauseInvalidArgument()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"foo\":{\"contents\":{\"type\":\"argument\",\"parser\":\"string\",\"properties\":{\"type\":\"word\"}}}, \"baz\":{\"contents\":{\"type\":\"argument\",\"parser\":\"integer\"}}}");
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@a[hello=false]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithPlayername_WhenLimitedToOneEntity()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(true, false);
            IStringReader reader = new StringReader("Steve");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithPlayername_WhenLimitedToPlayers()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(false, true);
            IStringReader reader = new StringReader("Steve");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithUuid_WhenLimitedToOneEntity()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(true, false);
            IStringReader reader = new StringReader("1-2-3-4-5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithUuid_WhenLimitedToPlayers()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(false, true);
            IStringReader reader = new StringReader("1-2-3-4-5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithAllPlayersSelector_WhenLimitedToOneEntity()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(true, false);
            IStringReader reader = new StringReader("@a");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithAllPlayersSelector_WhenLimitedToPlayers()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(false, true);
            IStringReader reader = new StringReader("@a");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithAllEntitiesSelector_WhenLimitedToOneEntity()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(true, false);
            IStringReader reader = new StringReader("@e");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithAllEntitiesSelector_WithLimit_WhenLimitedToOneEntity()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"limit\":{\"predicate\":\"set_limit\"}}");
            EntityArgument argument = new EntityArgument(true, false);
            IStringReader reader = new StringReader("@e[limit=1]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithAllEntitiesSelector_WhenLimitedToPlayers()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(false, true);
            IStringReader reader = new StringReader("@e");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithSelfSelector_WhenLimitedToOneEntity()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(true, false);
            IStringReader reader = new StringReader("@s");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithSelfSelector_WhenLimitedToPlayers()
        {
            // Arrange
            EntityArgument argument = new EntityArgument(false, true);
            IStringReader reader = new StringReader("@s");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithDuplicateArguments()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"foo\":{\"contents\":{\"type\":\"argument\",\"parser\":\"string\",\"properties\":{\"type\":\"word\"}}}}");
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@a[foo=bar, foo=baz]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithDuplicateArguments_IfReapplicationIsAllowed()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"foo\":{\"reapplication_type\":\"always\",\"contents\":{\"type\":\"argument\",\"parser\":\"string\",\"properties\":{\"type\":\"word\"}}}}");
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@a[foo=bar, foo=baz]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldSucceed_WithDuplicateArguments_IfReapplicationIsAllowedButOnlyIfInverted_AndIsInverted()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"foo\":{\"reapplication_type\":\"only_if_inverted\",\"allow_inverse\":true,\"contents\":{\"type\":\"argument\",\"parser\":\"string\",\"properties\":{\"type\":\"word\"}}}}");
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@a[foo=!bar, foo=!baz]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityArgument_ParseShouldFail_WithDuplicateArguments_IfReapplicationIsAllowedButOnlyIfInverted_AndIsNotInverted()
        {
            // Arrange
            EntitySelectorOptions.Set("{\"foo\":{\"reapplication_type\":\"only_if_inverted\",\"allow_inverse\":true,\"contents\":{\"type\":\"argument\",\"parser\":\"string\",\"properties\":{\"type\":\"word\"}}}}");
            EntityArgument argument = new EntityArgument();
            IStringReader reader = new StringReader("@a[foo=bar, foo=!baz]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
