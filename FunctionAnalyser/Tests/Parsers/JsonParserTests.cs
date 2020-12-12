using CommandParser;
using CommandParser.Parsers.JsonParser;
using CommandParser.Parsers.JsonParser.JsonArguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Parsers
{
    [TestClass]
    public class JsonParserTests
    {
        [TestMethod]
        public void JsonParser_ResultsShouldBeObject()
        {
            // Arrange
            IStringReader reader = new IStringReader("{\"foo\":\"bar\"}");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            jsonReader.ReadAny(out IJsonArgument result);

            // Assert
            Assert.IsTrue(result is JsonObject);
        }

        [TestMethod]
        public void JsonParser_ResultsShouldBeArray()
        {
            // Arrange
            IStringReader reader = new IStringReader("[\"foo\", \"bar\", \"baz\"]");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            jsonReader.ReadAny(out IJsonArgument result);

            // Assert
            Assert.IsTrue(result is JsonArray);
        }

        [TestMethod]
        public void JsonParser_ResultsShouldBeString()
        {
            // Arrange
            IStringReader reader = new IStringReader("\"foo bar baz\"");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            jsonReader.ReadAny(out IJsonArgument result);

            // Assert
            Assert.IsTrue(result is JsonString);
        }

        [TestMethod]
        public void JsonParser_ResultsShouldBeNumber()
        {
            // Arrange
            IStringReader reader = new IStringReader("1.23");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            jsonReader.ReadAny(out IJsonArgument result);

            // Assert
            Assert.IsTrue(result is JsonNumber);
        }

        [TestMethod]
        public void JsonParser_ResultsShouldBeBoolean()
        {
            // Arrange
            IStringReader reader = new IStringReader("true");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            jsonReader.ReadAny(out IJsonArgument result);

            // Assert
            Assert.IsTrue(result is JsonBoolean);
        }

        [TestMethod]
        public void JsonParser_ResultsShouldBeNull()
        {
            // Arrange
            IStringReader reader = new IStringReader("null");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            jsonReader.ReadAny(out IJsonArgument result);

            // Assert
            Assert.IsTrue(result is JsonNull);
        }

        [TestMethod]
        public void JsonParser_ParseObjectShouldFail_BecauseExpectedOpenObjectCharacter()
        {
            // Arrange
            IStringReader reader = new IStringReader("\"foo\":\"bar\"}");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadObject(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseObjectShouldFail_BecauseExpectedNameValueSeparator()
        {
            // Arrange
            IStringReader reader = new IStringReader("{\"foo\"}");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadObject(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseObjectShouldFail_BecauseExpectedCloseObjectCharacter()
        {
            // Arrange
            IStringReader reader = new IStringReader("{\"foo\":\"bar\"");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadObject(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseArrayShouldFail_BecauseExpectedOpenArrayCharacter()
        {
            // Arrange
            IStringReader reader = new IStringReader("\"foo\"]");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadArray(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseArrayShouldFail_BecauseExpectedCloseArrayCharacter()
        {
            // Arrange
            IStringReader reader = new IStringReader("[\"foo\"");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadArray(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseStringShouldFail_BecauseExpectedOpenStringCharacter()
        {
            // Arrange
            IStringReader reader = new IStringReader("foo\"");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadString(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseStringShouldFail_BecauseInvalidEscapeSequence()
        {
            // Arrange
            IStringReader reader = new IStringReader("\"foo\\\"");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadString(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseStringShouldFail_BecauseUnterminatedEscapeSequence()
        {
            // Arrange
            IStringReader reader = new IStringReader("\"foo\\u00");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadString(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseStringShouldFail_BecauseInvalidUnicodeCharacter()
        {
            // Arrange
            IStringReader reader = new IStringReader("\"foo\\uZZZZ\"");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadString(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void JsonParser_ParseStringShouldFail_BecauseExpectedCloseStringCharacter()
        {
            // Arrange
            IStringReader reader = new IStringReader("\"foo");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadString(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
