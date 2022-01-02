using ErrorCraft.Minecraft.Json;
using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Tests.Util;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json;

[TestClass]
public class JsonReaderTests {
    [TestMethod]
    public void Read_ReturnsJsonNumber() {
        IStringReader stringReader = new StringReaderMock("-1.5");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonNumber));
    }

    [TestMethod]
    public void Read_WithExponent_ReturnsJsonNumber() {
        IStringReader stringReader = new StringReaderMock("-1.5e2");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonNumber));
    }

    [TestMethod]
    public void Read_WithExtraWhitespace_ReturnsJsonNumber() {
        IStringReader stringReader = new StringReaderMock("   -1.5   ");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonNumber));
    }

    [TestMethod]
    public void Read_ReturnsJsonBoolean() {
        IStringReader stringReader = new StringReaderMock("true");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonBoolean));
    }

    [TestMethod]
    public void Read_WithExtraWhitespace_ReturnsJsonBoolean() {
        IStringReader stringReader = new StringReaderMock("   true   ");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonBoolean));
    }

    [TestMethod]
    public void Read_ReturnsJsonNull() {
        IStringReader stringReader = new StringReaderMock("null");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonNull));
    }

    [TestMethod]
    public void Read_WithExtraWhitespace_ReturnsJsonNull() {
        IStringReader stringReader = new StringReaderMock("   null   ");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonNull));
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseInputIsInvalid() {
        IStringReader stringReader = new StringReaderMock("invalid");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_ReturnsJsonString() {
        IStringReader stringReader = new StringReaderMock("\"text\"");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonString));
    }

    [TestMethod]
    public void Read_WithExtraWhitespace_ReturnsJsonString() {
        IStringReader stringReader = new StringReaderMock("   \"text\"   ");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonString));
    }

    [TestMethod]
    public void Read_WithEscapedValues_ReturnsJsonString() {
        IStringReader stringReader = new StringReaderMock("\"text\\u0020and more\\ttext!\"");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        JsonString jsonString = (JsonString)result.Value!;
        Assert.AreEqual("text and more\ttext!", (string)jsonString);
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseStringIsUnterminated() {
        IStringReader stringReader = new StringReaderMock("\"text");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseStringEscapeSequenceIsInvalid() {
        IStringReader stringReader = new StringReaderMock("\"text\\ \"");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_ReturnsJsonObject() {
        IStringReader stringReader = new StringReaderMock("{\"example\":1,\"another_example\":true}");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonObject));
    }

    [TestMethod]
    public void Read_WithExtraWhitespace_ReturnsJsonObject() {
        IStringReader stringReader = new StringReaderMock("   {   \"example\"   :   1,   \"another_example\"   :   true   }   ");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonObject));
    }

    [TestMethod]
    public void Read_WithNoContents_ReturnsJsonObject() {
        IStringReader stringReader = new StringReaderMock("{}");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonObject));
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseObjectIsNotClosed() {
        IStringReader stringReader = new StringReaderMock("{");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseNameSeparatorIsNotPresent() {
        IStringReader stringReader = new StringReaderMock("{\"key\"");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseNameIsNotPresent() {
        IStringReader stringReader = new StringReaderMock("{:1}");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseValueIsNotPresent() {
        IStringReader stringReader = new StringReaderMock("{\"key\":");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseThereIsNoItemAfterValueSeparatorInObject() {
        IStringReader stringReader = new StringReaderMock("{\"key\":1,");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_ReturnsJsonArray() {
        IStringReader stringReader = new StringReaderMock("[1,2,3]");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonArray));
    }

    [TestMethod]
    public void Read_WithExtraWhitespace_ReturnsJsonArray() {
        IStringReader stringReader = new StringReaderMock("   [   1   ,   2   ,   3   ]   ");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonArray));
    }

    [TestMethod]
    public void Read_WithNoContents_ReturnsJsonArray() {
        IStringReader stringReader = new StringReaderMock("[]");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonArray));
    }

    [TestMethod]
    public void Read_WithMixedElements_ReturnsJsonArray() {
        IStringReader stringReader = new StringReaderMock("[1,true,[\"text\"]]");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsInstanceOfType(result.Value, typeof(JsonArray));
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseArrayIsNotClosed() {
        IStringReader stringReader = new StringReaderMock("[");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Read_IsUnsuccessful_BecauseThereIsNoItemAfterValueSeparatorInArray() {
        IStringReader stringReader = new StringReaderMock("[1,");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }
}
