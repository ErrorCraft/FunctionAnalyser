using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class MobEffectArgumentTests
    {
        [TestMethod]
        public void MobEffectArgument_ParseShouldSucceed()
        {
            // Arrange
            MobEffectArgument argument = new MobEffectArgument();
            MobEffects.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void MobEffectArgument_ParseShouldFail()
        {
            // Arrange
            MobEffectArgument argument = new MobEffectArgument();
            StringReader reader = new StringReader("foo::bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void MobEffectArgument_ParseShouldSucceed_WithDefaultNamespace()
        {
            // Arrange
            MobEffectArgument argument = new MobEffectArgument();
            MobEffects.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("minecraft:foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void Effects_ShouldNotContainEffect()
        {
            // Arrange
            MobEffectArgument argument = new MobEffectArgument();
            MobEffects.Set("[\"foo\", \"bar\"]");
            StringReader reader = new StringReader("baz");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
