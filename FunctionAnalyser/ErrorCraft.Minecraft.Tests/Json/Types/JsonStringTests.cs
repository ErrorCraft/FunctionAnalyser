using ErrorCraft.Minecraft.Json.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Types;

[TestClass]
public class JsonStringTests {
    [TestMethod]
    public void GetElementType_ReturnsCorrectValue() {
        JsonString json = new JsonString("");
        Assert.AreEqual(JsonElementType.STRING, json.GetElementType());
    }
}
