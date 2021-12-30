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
}
