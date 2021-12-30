using ErrorCraft.Minecraft.Json.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Types;

[TestClass]
public class JsonArrayTests {
    [TestMethod]
    public void GetElementType_ReturnsCorrectValue() {
        JsonArray json = new JsonArray();
        Assert.AreEqual(JsonElementType.ARRAY, json.GetElementType());
    }
}
