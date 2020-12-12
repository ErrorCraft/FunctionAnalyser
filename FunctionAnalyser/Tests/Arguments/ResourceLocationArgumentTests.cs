using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ResourceLocationArgumentTests
    {
        [TestMethod]
        public void ResourceLocationArgument_ParseShouldSucceed()
        {
            // Arrange
            ResourceLocationArgument argument = new ResourceLocationArgument();
            IStringReader reader = new IStringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ResourceLocationArgument_ParseShouldFail()
        {
            // Arrange
            ResourceLocationArgument argument = new ResourceLocationArgument();
            IStringReader reader = new IStringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void StringReader_ShouldHaveTrailingCharacters()
        {
            // Arrange
            ResourceLocationArgument argument = new ResourceLocationArgument();
            IStringReader reader = new IStringReader("foo:barBAZ");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(reader.CanRead());
        }

        [TestMethod]
        public void ResourceLocationResult_ShouldUseDefaultNamespace()
        {
            // Arrange
            ResourceLocationArgument argument = new ResourceLocationArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            argument.Parse(reader, out ResourceLocation result);

            // Assert
            Assert.IsTrue(result.IsDefaultNamespace());
        }
    }
}
