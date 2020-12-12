using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CommandParser.Arguments.StringArgument;

namespace Tests.Arguments
{
    [TestClass]
    public class StringArgumentTests
    {
        [TestMethod]
        public void StringArgument_ParseShouldSucceed_WithTypeWord()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.WORD);
            IStringReader reader = new IStringReader("hello");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void StringArgument_ParseShouldSucceed_WhenEmpty_WithTypeWord()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.WORD);
            IStringReader reader = new IStringReader("");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void StringArgument_WithTypeWord_HasTrailingCharacters()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.WORD);
            IStringReader reader = new IStringReader("foo!@#");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(reader.CanRead(3));
        }

        [TestMethod]
        public void StringArgument_WithTypeQuotable_ShouldSucceed()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.QUOTABLE);
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void StringArgument_ParseShouldSucceed_WithTypeQuotable_WithQuote()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.QUOTABLE);
            IStringReader reader = new IStringReader("'foo bar'");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void StringArgument_ParseShouldFail_WithTypeQuotable_BecauseUnclosedQuotedString()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.QUOTABLE);
            IStringReader reader = new IStringReader("'foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void StringArgument_ParseShouldFail_WithTypeQuotable_BecauseInvalidEscapeSequence()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.QUOTABLE);
            IStringReader reader = new IStringReader("\"foo\\bar\"");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void StringArgument_WithTypeQuotable_EscapesCorrectly()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.QUOTABLE);
            IStringReader reader = new IStringReader("\"\\\"foo bar\\\"\"");

            // Act
            argument.Parse(reader, out string result);

            // Assert
            Assert.AreEqual(result, "\"foo bar\"");
        }

        [TestMethod]
        public void StringArgument_ParseShouldSucceed_WithTypeGreedy()
        {
            // Arrange
            StringArgument argument = new StringArgument(StringType.GREEDY);
            IStringReader reader = new IStringReader("Hello world! This is a test.");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(reader.CanRead());
        }
    }
}
