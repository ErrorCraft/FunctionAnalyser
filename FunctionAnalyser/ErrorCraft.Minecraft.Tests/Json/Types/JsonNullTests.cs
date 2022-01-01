using ErrorCraft.Minecraft.Json.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Types;

[TestClass]
public class JsonNullTests {
    [TestMethod]
    public void GetElementType_ReturnsCorrectValue() {
        Assert.AreEqual(JsonElementType.NULL, JsonNull.INSTANCE.GetElementType());
    }

    [TestMethod]
    public void TryParse_ReturnsTrue() {
        string s = "null";
        bool successful = JsonNull.TryParse(s, out _);
        Assert.IsTrue(successful);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecauseInputIsInvalid() {
        string s = "invalid";
        bool successful = JsonNull.TryParse(s, out _);
        Assert.IsFalse(successful);
    }
}
