using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ItemModifierArgumentTests
    {
        [TestMethod]
        public void ItemModifierArgument_ParseShouldSucceed()
        {
            // Arrange
            ItemModifierArgument argument = new ItemModifierArgument();
            IStringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ItemModifierArgument_ParseShouldFail()
        {
            // Arrange
            ItemModifierArgument argument = new ItemModifierArgument();
            IStringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
