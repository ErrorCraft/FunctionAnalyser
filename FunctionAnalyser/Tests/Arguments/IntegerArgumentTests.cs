using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class IntegerArgumentTests
    {
        [TestMethod]
        public void IntegerArgument_ParseShouldSucceed()
        {
            // Arrange
            IntegerArgument argument = new IntegerArgument();
            IStringReader reader = new IStringReader("123");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void IntegerArgument_ParseShouldFail()
        {
            // Arrange
            IntegerArgument argument = new IntegerArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void IntegerResult_ShouldHaveCorrectValue()
        {
            // Arrange
            IntegerArgument argument = new IntegerArgument();
            IStringReader reader = new IStringReader("123");

            // Act
            argument.Parse(reader, out int result);

            // Assert
            Assert.AreEqual(result, 123);
        }

        [TestMethod]
        public void IntegerArgument_ParseShouldFail_BecauseInvalidInteger()
        {
            // Arrange
            IntegerArgument argument = new IntegerArgument();
            IStringReader reader = new IStringReader("1.5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void IntegerArgument_ParseShouldFail_BecauseNumberIsTooHigh()
        {
            // Arrange
            IntegerArgument argument = new IntegerArgument(maximum: 0);
            IStringReader reader = new IStringReader("10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void IntegerArgument_ParseShouldFail_BecauseNumberIsTooLow()
        {
            // Arrange
            IntegerArgument argument = new IntegerArgument(minimum: 0);
            IStringReader reader = new IStringReader("-10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
