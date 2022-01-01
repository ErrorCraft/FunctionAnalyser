using ErrorCraft.Minecraft.Json.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Types;

[TestClass]
public class JsonNumberTests {
    [TestMethod]
    public void GetElementType_ReturnsCorrectValue() {
        JsonNumber json = new JsonNumber(1.0d);
        Assert.AreEqual(JsonElementType.NUMBER, json.GetElementType());
    }

    [TestMethod]
    public void TryParse_ReturnsCorrectValue() {
        string s = "-1.5";
        _ = JsonNumber.TryParse(s, out JsonNumber? result);
        Assert.AreEqual(-1.5d, (double)result);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecauseInputIsInvalid() {
        string s = "invalid";
        bool successful = JsonNumber.TryParse(s, out _);
        Assert.IsFalse(successful);
    }
}
