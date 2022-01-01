using ErrorCraft.Minecraft.Json;
using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Tests.Util;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json;

[TestClass]
public class JsonReaderTests {
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
    public void Read_IsUnsuccessful_BecauseInputIsInvalid() {
        IStringReader stringReader = new StringReaderMock("INVALID");
        JsonReader jsonReader = new JsonReader(stringReader);
        Result<IJsonElement> result = jsonReader.Read();
        Assert.IsFalse(result.Successful);
    }
}
