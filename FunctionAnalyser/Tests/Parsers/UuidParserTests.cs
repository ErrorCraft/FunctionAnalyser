using CommandParser.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Parsers
{
    [TestClass]
    public class UuidParserTests
    {
        [TestMethod]
        public void UuidParser_ParseShouldSucceed()
        {
            // Arrange
            UuidParser parser = new UuidParser("1-2-3-4-5");

            // Act
            bool successful = parser.Parse(out _);

            // Assert
            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void UuidParser_ParseShouldFail_BecauseInvalidLength()
        {
            // Arrange
            UuidParser parser = new UuidParser("1-2-3-4");

            // Act
            bool successful = parser.Parse(out _);

            // Assert
            Assert.IsFalse(successful);
        }

        [TestMethod]
        public void UuidParser_ParseShouldFail_BecauseInvalidCharacters()
        {
            // Arrange
            UuidParser parser = new UuidParser("1-2-xyz-4-5");

            // Act
            bool successful = parser.Parse(out _);

            // Assert
            Assert.IsFalse(successful);
        }
    }
}
