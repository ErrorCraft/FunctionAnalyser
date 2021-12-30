using ErrorCraft.Minecraft.Json.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Types;

[TestClass]
public class JsonObjectTests {
    [TestMethod]
    public void GetElementType_ReturnsCorrectValue() {
        JsonObject json = new JsonObject();
        Assert.AreEqual(JsonElementType.OBJECT, json.GetElementType());
    }
}
