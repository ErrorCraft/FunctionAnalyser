using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class AttributeArgumentTests
    {
        [TestMethod]
        public void AttributeArgument_ParseShouldSucceed()
        {
            // Arrange
            AttributeArgument argument = new AttributeArgument();
            StringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void AttributeArgument_ParseShouldFail()
        {
            // Arrange
            AttributeArgument argument = new AttributeArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
