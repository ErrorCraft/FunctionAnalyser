using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class DoubleArgumentTests
    {
        [TestMethod]
        public void DoubleArgument_ParseShouldSucceed()
        {
            // Arrange
            DoubleArgument argument = new DoubleArgument();
            StringReader reader = new StringReader("123");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void DoubleArgument_ParseShouldFail()
        {
            // Arrange
            DoubleArgument argument = new DoubleArgument();
            StringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void DoubleResult_ShouldHaveCorrectValue()
        {
            // Arrange
            DoubleArgument argument = new DoubleArgument();
            StringReader reader = new StringReader("123");

            // Act
            argument.Parse(reader, out double result);

            // Assert
            Assert.AreEqual(result, 123.0d);
        }

        [TestMethod]
        public void DoubleArgument_ParseShouldFail_BecauseInvalidDouble()
        {
            // Arrange
            DoubleArgument argument = new DoubleArgument();
            StringReader reader = new StringReader("1..5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void DoubleArgument_ParseShouldFail_BecauseNumberIsTooHigh()
        {
            // Arrange
            DoubleArgument argument = new DoubleArgument(maximum: 0.0d);
            StringReader reader = new StringReader("10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void DoubleArgument_ParseShouldFail_BecauseNumberIsTooLow()
        {
            // Arrange
            DoubleArgument argument = new DoubleArgument(minimum: 0.0d);
            StringReader reader = new StringReader("-10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
