using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class AdvancementArgumentTests
    {
        [TestMethod]
        public void AdvancementArgument_ParseShouldSucceed()
        {
            // Arrange
            AdvancementArgument argument = new AdvancementArgument();
            IStringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void AdvancementArgument_ParseShouldFail()
        {
            // Arrange
            AdvancementArgument argument = new AdvancementArgument();
            IStringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
