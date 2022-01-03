using ErrorCraft.Minecraft.Util.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Tests.Util.Json;

[TestClass]
public class JsonHelperTests {
    [TestMethod]
    public void GetString_ReturnsCorrectValue() {
        JObject json = GetJson();
        string result = json.GetString("example_string");
        Assert.AreEqual("text", result);
    }

    [TestMethod]
    public void GetString_ThrowsException_BecauseItemDoesNotExist() {
        JObject json = GetJson();
        Assert.ThrowsException<JsonException>(() => json.GetString("invalid"));
    }

    [TestMethod]
    public void GetString_ThrowsException_BecauseItemTypeIsIncorrect() {
        JObject json = GetJson();
        Assert.ThrowsException<JsonException>(() => json.GetString("example_integer"));
    }

    [TestMethod]
    public void AsString_ReturnsCorrectValue() {
        JToken json = "text";
        string result = json.AsString("");
        Assert.AreEqual("text", result);
    }

    [TestMethod]
    public void AsString_ThrowsException_BecauseTypeIsIncorrect() {
        JToken json = 10;
        Assert.ThrowsException<JsonException>(() => json.AsString(""));
    }

    [TestMethod]
    public void GetBoolean_ReturnsCorrectValue() {
        JObject json = GetJson();
        bool result = json.GetBoolean("example_boolean");
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void GetBoolean_ThrowsException_BecauseItemDoesNotExist() {
        JObject json = GetJson();
        Assert.ThrowsException<JsonException>(() => json.GetBoolean("invalid"));
    }

    [TestMethod]
    public void GetBoolean_ThrowsException_BecauseItemTypeIsIncorrect() {
        JObject json = GetJson();
        Assert.ThrowsException<JsonException>(() => json.GetBoolean("example_integer"));
    }

    [TestMethod]
    public void GetBoolean_WithDefaultValue_ReturnsCorrectValue() {
        JObject json = GetJson();
        bool result = json.GetBoolean("example_boolean", false);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void GetBoolean_WithDefaultValue_ReturnsDefaultValue_BecauseItemDoesNotExist() {
        JObject json = GetJson();
        bool result = json.GetBoolean("invalid", false);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void GetBoolean_WithDefaultValue_ThrowsException_BecauseItemTypeIsIncorrect() {
        JObject json = GetJson();
        Assert.ThrowsException<JsonException>(() => json.GetBoolean("example_integer"));
    }

    [TestMethod]
    public void AsBoolean_ReturnsCorrectValue() {
        JToken json = true;
        bool result = json.AsBoolean("");
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void AsBoolean_ThrowsException_BecauseTypeIsIncorrect() {
        JToken json = 10;
        Assert.ThrowsException<JsonException>(() => json.AsString(""));
    }

    private static JObject GetJson() {
        return new JObject() {
            { "example_string", "text" },
            { "example_boolean", true },
            { "example_integer", 10 }
        };
    }
}
