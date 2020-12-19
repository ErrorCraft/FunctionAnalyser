using CommandParser;
using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
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
            ResourceLocationParser parser = new ResourceLocationParser(reader);

            // Act
            ReadResults readResults = parser.Read(out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationParser_ReadFromString()
        {
            // Arrange
            IStringReader reader = new StringReader("foo:bar");
            ResourceLocationParser parser = new ResourceLocationParser(reader);

            // Act
            ReadResults readResults = parser.ReadFromString(reader.GetString(), 0, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationParser_ParseShouldFail_BecauseDuplicateSeparator()
        {
            // Arrange
            IStringReader reader = new StringReader("foo::bar");
            ResourceLocationParser parser = new ResourceLocationParser(reader);

            // Act
            ReadResults readResults = parser.Read(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationParser_ParseShouldFail_BecauseInvalidCharactersInNamespace()
        {
            // Arrange
            IStringReader reader = new StringReader("foo/bar:baz");
            ResourceLocationParser parser = new ResourceLocationParser(reader);

            // Act
            ReadResults readResults = parser.Read(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationParser_ResultShouldUseDefaultNamespace()
        {
            // Arrange
            IStringReader reader = new StringReader("foo");
            ResourceLocationParser parser = new ResourceLocationParser(reader);

            // Act
            parser.Read(out ResourceLocation result);

            // Assert
            Assert.IsTrue(result.IsDefaultNamespace());
        }
    }
}
