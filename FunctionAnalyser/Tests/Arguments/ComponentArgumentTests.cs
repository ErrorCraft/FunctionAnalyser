using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ComponentArgumentTests
    {
        [TestMethod]
        public void ComponentArgument_ParseShouldSucceed_WithSimpleInput()
        {
            // Arrange
            ComponentArgument argument = new ComponentArgument();
            IStringReader reader = new IStringReader("\"foo\"");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ComponentArgument_ParseShouldSucceed_WithComplexInput()
        {
            // Arrange
            Components.Set("{\"content\":{\"foo\":{\"type\":\"string\"}},\"formatting\":{\"bar\":{\"type\":\"number\"}}}");
            ComponentArgument argument = new ComponentArgument();
            IStringReader reader = new IStringReader("{\"foo\":\"Hello world!\",\"bar\":3}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
