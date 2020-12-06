using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class LongArgumentTests
    {
        [TestMethod]
        public void LongArgument_ParseShouldSucceed()
        {
            // Arrange
            LongArgument argument = new LongArgument();
            StringReader reader = new StringReader("123");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void LongArgument_ParseShouldFail()
        {
            // Arrange
            LongArgument argument = new LongArgument();
            StringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void LongResult_ShouldHaveCorrectValue()
        {
            // Arrange
            LongArgument argument = new LongArgument();
            StringReader reader = new StringReader("123");

            // Act
            argument.Parse(reader, out long result);

            // Assert
            Assert.AreEqual(result, 123L);
        }

        [TestMethod]
        public void LongArgument_ParseShouldFail_BecauseInvalidLong()
        {
            // Arrange
            LongArgument argument = new LongArgument();
            StringReader reader = new StringReader("1.5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void LongArgument_ParseShouldFail_BecauseNumberIsTooHigh()
        {
            // Arrange
            LongArgument argument = new LongArgument(maximum: 0L);
            StringReader reader = new StringReader("10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void LongArgument_ParseShouldFail_BecauseNumberIsTooLow()
        {
            // Arrange
            LongArgument argument = new LongArgument(minimum: 0L);
            StringReader reader = new StringReader("-10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
