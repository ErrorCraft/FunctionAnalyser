using ErrorCraft.CommandParser.Arguments;
using ErrorCraft.CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests.Arguments {
    [TestClass]
    public class BooleanArgumentTests {
        [TestMethod]
        public void Parse_ReturnsCorrectValue() {
            BooleanArgument booleanArgument = new BooleanArgument();
            StringReader stringReader = new StringReader("true");
            booleanArgument.Parse(stringReader, out bool result);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Parse_ReturnsCorrectValue_WithAllCapitals() {
            BooleanArgument booleanArgument = new BooleanArgument();
            StringReader stringReader = new StringReader("TRUE");
            booleanArgument.Parse(stringReader, out bool result);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful_BecauseValueReadIsNotABoolean() {
            BooleanArgument booleanArgument = new BooleanArgument();
            StringReader stringReader = new StringReader("foo");
            ParseResults parseResults = booleanArgument.Parse(stringReader, out _);
            Assert.IsFalse(parseResults.Successful);
        }
    }
}
