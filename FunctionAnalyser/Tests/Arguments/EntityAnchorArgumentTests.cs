using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class EntityAnchorArgumentTests
    {
        [TestMethod]
        public void EntityAnchorArgument_ParseShouldSucceed()
        {
            // Arrange
            Anchors.Set("[\"foo\",\"bar\"]");
            EntityAnchorArgument argument = new EntityAnchorArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void EntityAnchorArgument_ParseShouldFail_BecauseInvalidEntityAnchor()
        {
            // Arrange
            Anchors.Set("[\"foo\",\"bar\"]");
            EntityAnchorArgument argument = new EntityAnchorArgument();
            IStringReader reader = new IStringReader("baz");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
