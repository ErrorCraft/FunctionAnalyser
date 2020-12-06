using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ObjectiveArgumentTests
    {
        [TestMethod]
        public void ObjectiveArgument_ParseShouldSucceed()
        {
            // Arrange
            ObjectiveArgument argument = new ObjectiveArgument();
            StringReader reader = new StringReader("fooBar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ObjectiveArgument_ParseShouldFail_NameTooLong()
        {
            // Arrange
            ObjectiveArgument argument = new ObjectiveArgument();
            StringReader reader = new StringReader("fooBarBaz123456789");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
