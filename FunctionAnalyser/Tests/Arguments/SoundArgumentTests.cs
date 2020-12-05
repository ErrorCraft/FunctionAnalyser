using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class SoundArgumentTests
    {
        [TestMethod]
        public void SoundArgument_ParseShouldSucceed()
        {
            // Arrange
            SoundArgument argument = new SoundArgument();
            StringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void SoundArgument_ParseShouldFail()
        {
            // Arrange
            SoundArgument argument = new SoundArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
