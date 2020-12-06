using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class CompoundTagArgumentTests
    {
        [TestMethod]
        public void CompoundTagArgument_ParseShouldSucceed()
        {
            // Arrange
            CompoundTagArgument argument = new CompoundTagArgument();
            StringReader reader = new StringReader("{foo: 'bar', baz: 3}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void CompoundTagArgument_ParseShouldFail_BecauseInvalidType()
        {
            // Arrange
            CompoundTagArgument argument = new CompoundTagArgument();
            StringReader reader = new StringReader("[1, 2, 3]");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
