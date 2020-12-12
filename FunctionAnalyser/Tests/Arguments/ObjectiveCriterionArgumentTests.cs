using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ObjectiveCriterionArgumentTests
    {
        [TestMethod]
        public void ObjectiveCriterionArgument_ParseShouldSucceed_WithNormalCriterion()
        {
            // Arrange
            ObjectiveCriteria.Set("{\"normal\":{\"foo\":{}}}");
            ObjectiveCriterionArgument argument = new ObjectiveCriterionArgument();
            IStringReader reader = new IStringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ObjectiveCriterionArgument_ParseShouldFail_InvalidCriterion()
        {
            // Arrange
            ObjectiveCriteria.Set("{\"normal\":{\"foo\":{}}}");
            ObjectiveCriterionArgument argument = new ObjectiveCriterionArgument();
            IStringReader reader = new IStringReader("bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ObjectiveCriterionArgument_ParseShouldSucceed_WithNamespacedCriterion()
        {
            // Arrange
            ObjectiveCriteria.Set("{\"namespaced\":{\"foo\":{\"criterion_type\":\"statistic\"}},\"custom\":[\"bar\",\"baz\"]}");
            ObjectiveCriterionArgument argument = new ObjectiveCriterionArgument();
            IStringReader reader = new IStringReader("foo:bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ObjectiveCriterionArgument_ParseShouldSucceed_WithExpandedNamespacedCriterion()
        {
            // Arrange
            ObjectiveCriteria.Set("{\"namespaced\":{\"foo\":{\"criterion_type\":\"statistic\"}},\"custom\":[\"bar\",\"baz\"]}");
            ObjectiveCriterionArgument argument = new ObjectiveCriterionArgument();
            IStringReader reader = new IStringReader("minecraft.foo:minecraft.bar");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
