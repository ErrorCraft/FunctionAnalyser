using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class TimeArgumentTests
    {
        [TestMethod]
        public void TimeArgument_ParseShouldSucceed_WithoutScalar()
        {
            // Arrange
            TimeScalars.Set("{\"t\":1,\"s\":20,\"d\":24000}");
            TimeArgument argument = new TimeArgument();
            IStringReader reader = new StringReader("100.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void TimeArgument_ParseShouldSucceed_WithScalar()
        {
            // Arrange
            TimeScalars.Set("{\"t\":1,\"s\":20,\"d\":24000}");
            TimeArgument argument = new TimeArgument();
            IStringReader reader = new StringReader("100.0t");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void TimeArgument_ShouldScaleCorrectly()
        {
            // Arrange
            TimeScalars.Set("{\"t\":1,\"s\":20,\"d\":24000}");
            TimeArgument argument = new TimeArgument();
            IStringReader reader = new StringReader("100.0s");

            // Act
            argument.Parse(reader, out Time result);

            // Assert
            Assert.AreEqual(result.Value, 2000);
        }

        [TestMethod]
        public void TimeArgument_ParseShouldFail_BecauseInvalidUnit()
        {
            // Arrange
            TimeScalars.Set("{\"t\":1,\"s\":20,\"d\":24000}");
            TimeArgument argument = new TimeArgument();
            IStringReader reader = new StringReader("100.0m");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void TimeArgument_ParseShouldFail_BecauseNegativeTime()
        {
            // Arrange
            TimeScalars.Set("{\"t\":1,\"s\":20,\"d\":24000}");
            TimeArgument argument = new TimeArgument();
            IStringReader reader = new StringReader("-100.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
