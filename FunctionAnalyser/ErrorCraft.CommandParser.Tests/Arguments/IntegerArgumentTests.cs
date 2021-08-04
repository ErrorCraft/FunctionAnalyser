using ErrorCraft.CommandParser.Arguments;
using ErrorCraft.CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests.Arguments {
    [TestClass]
    public class IntegerArgumentTests {
        [TestMethod]
        public void Parse_ReturnsCorrectValue() {
            IntegerArgument integerArgument = new IntegerArgument();
            StringReader stringReader = new StringReader("100");
            integerArgument.Parse(stringReader, out int result);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful_BecauseStringIsEmpty() {
            IntegerArgument integerArgument = new IntegerArgument();
            StringReader stringReader = new StringReader("");
            ParseResults parseResults = integerArgument.Parse(stringReader, out _);
            Assert.IsFalse(parseResults.Successful);
        }

        [TestMethod]
        public void Parse_IsUnsuccessful_BecauseIntegerIsInvalid() {
            IntegerArgument integerArgument = new IntegerArgument();
            StringReader stringReader = new StringReader("--1");
            ParseResults parseResults = integerArgument.Parse(stringReader, out _);
            Assert.IsFalse(parseResults.Successful);
        }
    }
}
