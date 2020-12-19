using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class BossbarArgumentTests
    {
        [TestMethod]
        public void BossbarArgument_ParseShouldSucceed()
        {
            // Arrange
            BossbarArgument argument = new BossbarArgument();
            IStringReader reader = new StringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void BossbarArgument_ParseShouldFail()
        {
            // Arrange
            BossbarArgument argument = new BossbarArgument();
            IStringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
