using ErrorCraft.Minecraft.Json.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Types;

[TestClass]
public class JsonBooleanTests {
    [TestMethod]
    public void GetElementType_ReturnsCorrectValue() {
        JsonBoolean json = new JsonBoolean(false);
        Assert.AreEqual(JsonElementType.BOOLEAN, json.GetElementType());
    }

    [TestMethod]
    public void TryParse_WithTrue_ReturnsCorrectValue() {
        string s = "true";
        _ = JsonBoolean.TryParse(s, out JsonBoolean? result);
        Assert.AreEqual(true, (bool)result);
    }

    [TestMethod]
    public void TryParse_WithFalse_ReturnsCorrectValue() {
        string s = "false";
        _ = JsonBoolean.TryParse(s, out JsonBoolean? result);
        Assert.AreEqual(false, (bool)result);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecauseInputIsInvalid() {
        string s = "invalid";
        bool successful = JsonBoolean.TryParse(s, out _);
        Assert.IsFalse(successful);
    }
}
