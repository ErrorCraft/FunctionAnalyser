using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class LootTableArgumentTests
    {
        [TestMethod]
        public void LootTableArgument_ParseShouldSucceed()
        {
            // Arrange
            LootTableArgument argument = new LootTableArgument();
            StringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void LootTableArgument_ParseShouldFail()
        {
            // Arrange
            LootTableArgument argument = new LootTableArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
