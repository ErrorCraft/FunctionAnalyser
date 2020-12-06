using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class IntegerRangeArgumentTests
    {
        [TestMethod]
        public void IntegerRangeArgument_ParseShouldSucceed_WithSingleNumber()
        {
            // Arrange
            IntegerRangeArgument argument = new IntegerRangeArgument();
            StringReader reader = new StringReader("1");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void IntegerRangeArgument_ParseShouldSucceed_WithSimpleRange()
        {
            // Arrange
            IntegerRangeArgument argument = new IntegerRangeArgument();
            StringReader reader = new StringReader("1..10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void IntegerRangeArgument_ParseShouldSucceed_WithLoopable()
        {
            // Arrange
            IntegerRangeArgument argument = new IntegerRangeArgument(true);
            StringReader reader = new StringReader("10..-10");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void IntegerRangeResult_RangeResultShouldBeCorrect()
        {
            // Arrange
            IntegerRangeArgument argument = new IntegerRangeArgument();
            StringReader reader = new StringReader("1..10");

            // Act
            argument.Parse(reader, out Range<int> result);

            // Assert
            Assert.AreEqual(result, new Range<int>(1, 10));
        }

        [TestMethod]
        public void IntegerRangeArgument_ParseShouldFail_BecauseEmpty()
        {
            // Arrange
            IntegerRangeArgument argument = new IntegerRangeArgument();
            StringReader reader = new StringReader("..");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void IntegerRangeResult_RangeResultMinimumShouldBeCorrect()
        {
            // Arrange
            IntegerRangeArgument argument = new IntegerRangeArgument();
            StringReader reader = new StringReader("1..");

            // Act
            argument.Parse(reader, out Range<int> result);

            // Assert
            Assert.AreEqual(result.Minimum, 1);
        }

        [TestMethod]
        public void IntegerRangeResult_RangeResultMaximumShouldBeCorrect()
        {
            // Arrange
            IntegerRangeArgument argument = new IntegerRangeArgument();
            StringReader reader = new StringReader("..10");

            // Act
            argument.Parse(reader, out Range<int> result);

            // Assert
            Assert.AreEqual(result.Maximum, 10);
        }
    }
}
