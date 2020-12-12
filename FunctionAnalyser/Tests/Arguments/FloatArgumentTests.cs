using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class FloatArgumentTests
    {
        [TestMethod]
        public void FloatArgument_ParseShouldSucceed()
        {
            // Arrange
            FloatArgument argument = new FloatArgument();
            IStringReader reader = new IStringReader("123");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void FloatArgument_ParseShouldFail()
        {
            // Arrange
            FloatArgument argument = new FloatArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void FloatResult_ShouldHaveCorrectValue()
        {
            // Arrange
            FloatArgument argument = new FloatArgument();
            IStringReader reader = new IStringReader("123");

            // Act
            argument.Parse(reader, out float result);

            // Assert
            Assert.AreEqual(result, 123.0f);
        }

        [TestMethod]
        public void FloatArgument_ParseShouldFail_BecauseInvalidFloat()
        {
            // Arrange
            FloatArgument argument = new FloatArgument();
            IStringReader reader = new IStringReader("1..5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void FloatArgument_ParseShouldFail_BecauseNumberIsTooHigh()
        {
            // Arrange
            FloatArgument argument = new FloatArgument(maximum: 0.0f);
            IStringReader reader = new IStringReader("10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void FloatArgument_ParseShouldFail_BecauseNumberIsTooLow()
        {
            // Arrange
            FloatArgument argument = new FloatArgument(minimum: 0.0f);
            IStringReader reader = new IStringReader("-10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
