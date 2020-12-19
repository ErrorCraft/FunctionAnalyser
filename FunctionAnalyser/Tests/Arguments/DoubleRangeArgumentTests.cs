using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class DoubleRangeArgumentTests
    {
        [TestMethod]
        public void DoubleRangeArgument_ParseShouldSucceed_WithSingleNumber()
        {
            // Arrange
            DoubleRangeArgument argument = new DoubleRangeArgument();
            IStringReader reader = new StringReader("1.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void DoubleRangeArgument_ParseShouldSucceed_WithSimpleRange()
        {
            // Arrange
            DoubleRangeArgument argument = new DoubleRangeArgument();
            IStringReader reader = new StringReader("1.0..10.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void DoubleRangeArgument_ParseShouldSucceed_WithLoopable()
        {
            // Arrange
            DoubleRangeArgument argument = new DoubleRangeArgument(true);
            IStringReader reader = new StringReader("10.0..-10.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void DoubleRangeResult_RangeResultShouldBeCorrect()
        {
            // Arrange
            DoubleRangeArgument argument = new DoubleRangeArgument();
            IStringReader reader = new StringReader("1.0..10.0");

            // Act
            argument.Parse(reader, out Range<double> result);

            // Assert
            Assert.AreEqual(result, new Range<double>(1.0d, 10.0d));
        }

        [TestMethod]
        public void DoubleRangeArgument_ParseShouldFail_BecauseEmpty()
        {
            // Arrange
            DoubleRangeArgument argument = new DoubleRangeArgument();
            IStringReader reader = new StringReader("..");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void DoubleRangeResult_RangeResultMinimumShouldBeCorrect()
        {
            // Arrange
            DoubleRangeArgument argument = new DoubleRangeArgument();
            IStringReader reader = new StringReader("1.0..");

            // Act
            argument.Parse(reader, out Range<double> result);

            // Assert
            Assert.AreEqual(result.Minimum, 1.0d);
        }

        [TestMethod]
        public void DoubleRangeResult_RangeResultMaximumShouldBeCorrect()
        {
            // Arrange
            DoubleRangeArgument argument = new DoubleRangeArgument();
            IStringReader reader = new StringReader("..10.0");

            // Act
            argument.Parse(reader, out Range<double> result);

            // Assert
            Assert.AreEqual(result.Maximum, 10.0d);
        }
    }
}
