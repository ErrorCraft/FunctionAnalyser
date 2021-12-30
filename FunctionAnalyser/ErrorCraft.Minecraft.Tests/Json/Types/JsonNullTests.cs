using ErrorCraft.Minecraft.Json.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Types;

[TestClass]
public class JsonNullTests {
    [TestMethod]
    public void GetElementType_ReturnsCorrectValue() {
        Assert.AreEqual(JsonElementType.NULL, JsonNull.INSTANCE.GetElementType());
    }
}
