using CommandParser;
using CommandParser.Minecraft;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Parsers
{
    [TestClass]
    public class ResourceLocationParserTests
    {
        [TestMethod]
        public void ResourceLocationParser_ReadFromStringReader()
        {
            // Arrange
            IStringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = ResourceLocation.TryRead(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationParser_TryParse()
        {
            // Arrange
            string s = "foo:bar";

            // Act
            bool successful = ResourceLocation.TryParse(s, out _);

            // Assert
            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void ResourceLocationParser_ParseShouldFail_BecauseDuplicateSeparator()
        {
            // Arrange
            IStringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = ResourceLocation.TryRead(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationParser_ParseShouldFail_BecauseInvalidCharactersInNamespace()
        {
            // Arrange
            IStringReader reader = new StringReader("foo/bar:baz");

            // Act
            ReadResults readResults = ResourceLocation.TryRead(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationParser_ResultShouldUseDefaultNamespace()
        {
            // Arrange
            IStringReader reader = new StringReader("foo");

            // Act
            ResourceLocation.TryRead(reader, out ResourceLocation result);

            // Assert
            Assert.IsTrue(result.IsDefaultNamespace());
        }
    }
}
