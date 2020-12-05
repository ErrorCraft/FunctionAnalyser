using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class PredicateArgumentTests
    {
        [TestMethod]
        public void PredicateArgument_ParseShouldSucceed()
        {
            // Arrange
            PredicateArgument argument = new PredicateArgument();
            StringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void PredicateArgument_ParseShouldFail()
        {
            // Arrange
            PredicateArgument argument = new PredicateArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
