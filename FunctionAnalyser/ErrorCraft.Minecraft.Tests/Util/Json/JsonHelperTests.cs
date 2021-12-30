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

    private static JObject GetJson() {
        return new JObject() {
            { "example_string", "text" },
            { "example_integer", 10 }
        };
    }
}
