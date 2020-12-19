using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ItemSlotArgumentTests
    {
        [TestMethod]
        public void ItemSlotArgument_ParseShouldSucceed()
        {
            // Arrange
            ItemSlots.Set("[\"container.0\", \"container.1\", \"weapon.mainhand\"]");
            ItemSlotArgument argument = new ItemSlotArgument();
            IStringReader reader = new StringReader("container.0");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ItemSlotArgument_ParseShouldFail_BecauseInvalidItemSlot()
        {
            // Arrange
            ItemSlots.Set("[\"container.0\", \"container.1\", \"weapon.mainhand\"]");
            ItemSlotArgument argument = new ItemSlotArgument();
            IStringReader reader = new StringReader("container.2");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
