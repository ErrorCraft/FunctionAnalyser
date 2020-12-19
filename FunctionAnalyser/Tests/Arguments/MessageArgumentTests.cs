using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class MessageArgumentTests
    {
        [TestMethod]
        public void MessageArgument_ParseShouldSucceed()
        {
            // Arrange
            MessageArgument argument = new MessageArgument();
            IStringReader reader = new StringReader("Hello world! This is a test.");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(reader.CanRead());
        }

        [TestMethod]
        public void MessageArgument_ParseShouldSucceed_WithSelector()
        {
            // Arrange
            MessageArgument argument = new MessageArgument();
            IStringReader reader = new StringReader("Hello @a! This is a test.");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(reader.CanRead());
        }

        [TestMethod]
        public void MessageArgument_ParseShouldSucceed_WithInvalidSelectorType()
        {
            // Arrange
            MessageArgument argument = new MessageArgument();
            IStringReader reader = new StringReader("Hello @m! This is a test.");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(reader.CanRead());
        }

        [TestMethod]
        public void MessageArgument_ParseShouldFail_WithInvalidSelector()
        {
            // Arrange
            MessageArgument argument = new MessageArgument();
            IStringReader reader = new StringReader("Hello @a[! This is a test.");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
