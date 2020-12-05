using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ItemEnchantmentArgumentTests
    {
        [TestMethod]
        public void ItemEnchantmentArgument_ParseShouldSucceed()
        {
            // Arrange
            ItemEnchantmentArgument argument = new ItemEnchantmentArgument();
            Enchantments.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ItemEnchantmentArgument_ParseShouldFail()
        {
            // Arrange
            ItemEnchantmentArgument argument = new ItemEnchantmentArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ItemEnchantmentArgument_ParseShouldSucceed_WithDefaultNamespace()
        {
            // Arrange
            ItemEnchantmentArgument argument = new ItemEnchantmentArgument();
            Enchantments.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("minecraft:foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Enchantments_ShouldNotContainEnchantment()
        {
            // Arrange
            ItemEnchantmentArgument argument = new ItemEnchantmentArgument();
            Enchantments.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("baz");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
