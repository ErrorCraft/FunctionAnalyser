using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class FunctionArgumentTests
    {
        [TestMethod]
        public void FunctionArgument_ParseShouldSucceed()
        {
            // Arrange
            FunctionArgument argument = new FunctionArgument();
            StringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void FunctionArgument_ParseShouldFail()
        {
            // Arrange
            FunctionArgument argument = new FunctionArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
