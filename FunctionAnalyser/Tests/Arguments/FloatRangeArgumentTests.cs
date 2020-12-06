using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class FloatRangeArgumentTests
    {
        [TestMethod]
        public void FloatRangeArgument_ParseShouldSucceed_WithSingleNumber()
        {
            // Arrange
            FloatRangeArgument argument = new FloatRangeArgument();
            StringReader reader = new StringReader("1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void FloatRangeArgument_ParseShouldSucceed_WithSimpleRange()
        {
            // Arrange
            FloatRangeArgument argument = new FloatRangeArgument();
            StringReader reader = new StringReader("1.0..10.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void FloatRangeArgument_ParseShouldSucceed_WithLoopable()
        {
            // Arrange
            FloatRangeArgument argument = new FloatRangeArgument(true);
            StringReader reader = new StringReader("10.0..-10.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void FloatRangeResult_RangeResultShouldBeCorrect()
        {
            // Arrange
            FloatRangeArgument argument = new FloatRangeArgument();
            StringReader reader = new StringReader("1.0..10.0");

            // Act
            argument.Parse(reader, out Range<float> result);

            // Assert
            Assert.AreEqual(result, new Range<float>(1.0f, 10.0f));
        }

        [TestMethod]
        public void FloatRangeArgument_ParseShouldFail_BecauseEmpty()
        {
            // Arrange
            FloatRangeArgument argument = new FloatRangeArgument();
            StringReader reader = new StringReader("..");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void FloatRangeResult_RangeResultMinimumShouldBeCorrect()
        {
            // Arrange
            FloatRangeArgument argument = new FloatRangeArgument();
            StringReader reader = new StringReader("1.0..");

            // Act
            argument.Parse(reader, out Range<float> result);

            // Assert
            Assert.AreEqual(result.Minimum, 1.0f);
        }

        [TestMethod]
        public void FloatRangeResult_RangeResultMaximumShouldBeCorrect()
        {
            // Arrange
            FloatRangeArgument argument = new FloatRangeArgument();
            StringReader reader = new StringReader("..10.0");

            // Act
            argument.Parse(reader, out Range<float> result);

            // Assert
            Assert.AreEqual(result.Maximum, 10.0f);
        }
    }
}
