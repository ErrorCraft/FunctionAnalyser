using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class StorageArgumentTests
    {
        [TestMethod]
        public void StorageArgument_ParseShouldSucceed()
        {
            // Arrange
            StorageArgument argument = new StorageArgument();
            IStringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void StorageArgument_ParseShouldFail()
        {
            // Arrange
            StorageArgument argument = new StorageArgument();
            IStringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
