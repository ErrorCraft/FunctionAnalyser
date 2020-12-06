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
            StringReader reader = new StringReader("{\"foo\":\"bar\"}");
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
            StringReader reader = new StringReader("[\"foo\", \"bar\", \"baz\"]");
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
            StringReader reader = new StringReader("\"foo bar baz\"");
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
            StringReader reader = new StringReader("1.23");
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
            StringReader reader = new StringReader("true");
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
            StringReader reader = new StringReader("null");
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
            StringReader reader = new StringReader("\"foo\":\"bar\"}");
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
            StringReader reader = new StringReader("{\"foo\"}");
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
            StringReader reader = new StringReader("{\"foo\":\"bar\"");
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
            StringReader reader = new StringReader("\"foo\"]");
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
            StringReader reader = new StringReader("[\"foo\"");
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
            StringReader reader = new StringReader("foo\"");
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
            StringReader reader = new StringReader("\"foo\\\"");
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
            StringReader reader = new StringReader("\"foo\\u00");
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
            StringReader reader = new StringReader("\"foo\\uZZZZ\"");
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
            StringReader reader = new StringReader("\"foo");
            JsonReader jsonReader = new JsonReader(reader);

            // Act
            ReadResults readResults = jsonReader.ReadString(out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }
    }
}
