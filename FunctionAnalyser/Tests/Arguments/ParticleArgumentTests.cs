using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ParticleArgumentTests
    {
        [TestMethod]
        public void ParticleArgument_ParseShouldSucceed()
        {
            // Arrange
            Particles.Set("{\"foo\":{},\"bar\":{\"children\":{\"baz\":{\"type\":\"argument\",\"parser\":\"double\"}}}}");
            ParticleArgument argument = new ParticleArgument();
            IStringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ParticleArgument_ParseShouldSucceed_WithNamespace()
        {
            // Arrange
            Particles.Set("{\"foo\":{},\"bar\":{\"children\":{\"baz\":{\"type\":\"argument\",\"parser\":\"double\"}}}}");
            ParticleArgument argument = new ParticleArgument();
            IStringReader reader = new StringReader("minecraft:foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ParticleArgument_ParseShouldFail_BecauseUnknownParticle()
        {
            // Arrange
            Particles.Set("{\"foo\":{},\"bar\":{\"children\":{\"baz\":{\"type\":\"argument\",\"parser\":\"double\"}}}}");
            ParticleArgument argument = new ParticleArgument();
            IStringReader reader = new StringReader("hello");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ParticleArgument_ParseShouldSucceed_WithAdditionalArguments()
        {
            // Arrange
            Particles.Set("{\"foo\":{},\"bar\":{\"children\":{\"baz\":{\"type\":\"argument\",\"parser\":\"double\"}}}}");
            ParticleArgument argument = new ParticleArgument();
            IStringReader reader = new StringReader("bar 1.5");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ParticleArgument_ParseShouldFail_WithIncompleteAdditionalArguments()
        {
            // Arrange
            Particles.Set("{\"foo\":{},\"bar\":{\"children\":{\"baz\":{\"type\":\"argument\",\"parser\":\"double\"}}}}");
            ParticleArgument argument = new ParticleArgument();
            IStringReader reader = new StringReader("bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
