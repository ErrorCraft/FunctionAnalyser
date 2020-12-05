using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class EntitySummonArgumentTests
    {
        [TestMethod]
        public void EntitySummonArgument_ParseShouldSucceed()
        {
            // Arrange
            EntitySummonArgument argument = new EntitySummonArgument();
            Entities.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntitySummonArgument_ParseShouldFail()
        {
            // Arrange
            EntitySummonArgument argument = new EntitySummonArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void EntitySummonArgument_ParseShouldSucceed_WithDefaultNamespace()
        {
            // Arrange
            EntitySummonArgument argument = new EntitySummonArgument();
            Entities.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("minecraft:foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Entities_ShouldNotContainEntity()
        {
            // Arrange
            EntitySummonArgument argument = new EntitySummonArgument();
            Entities.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("baz");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
