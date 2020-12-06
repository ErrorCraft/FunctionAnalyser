using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class OperationArgumentTests
    {
        [TestMethod]
        public void OperationArgument_ParseShouldSucceed()
        {
            // Arrange
            Operations.Set("[\"+=\",\"-=\"]");
            OperationArgument argument = new OperationArgument();
            StringReader reader = new StringReader("+=");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void OperationArgument_ParseShouldFail_BecauseInvalidOperation()
        {
            // Arrange
            Operations.Set("[\"+=\",\"-=\"]");
            OperationArgument argument = new OperationArgument();
            StringReader reader = new StringReader("!=");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
