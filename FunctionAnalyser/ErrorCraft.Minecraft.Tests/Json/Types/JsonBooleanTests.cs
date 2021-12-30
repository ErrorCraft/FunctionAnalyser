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
}
