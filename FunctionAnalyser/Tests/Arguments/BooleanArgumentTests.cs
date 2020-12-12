using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class BooleanArgumentTests
    {
        [TestMethod]
        public void BooleanArgument_ParseShouldSucceed()
        {
            // Arrange
            BooleanArgument argument = new BooleanArgument();
            IStringReader reader = new IStringReader("true");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BooleanArgument_ParseShouldFail()
        {
            // Arrange
            BooleanArgument argument = new BooleanArgument();
            IStringReader reader = new IStringReader("hello");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void BooleanArgument_UseQuotedText()
        {
            // Arrange
            BooleanArgument argument = new BooleanArgument();
            IStringReader reader = new IStringReader("'true'");

            // Act
            argument.Parse(reader, out bool result);

            // Assert
            Assert.AreEqual(result, true);
        }
    }
}
