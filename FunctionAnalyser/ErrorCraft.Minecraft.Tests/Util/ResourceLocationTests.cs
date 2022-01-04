using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Util;

[TestClass]
public class ResourceLocationTests {
    [TestMethod]
    public void TryParse_ReturnsCorrectValue() {
        _ = ResourceLocation.TryParse("namespace:path/to/item", out ResourceLocation? resourceLocation);
        Assert.AreEqual(new ResourceLocation("namespace", "path/to/item"), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithNoNamespace_UsesDefaultNamespace() {
        _ = ResourceLocation.TryParse("path/to/item", out ResourceLocation? resourceLocation);
        Assert.AreEqual(new ResourceLocation("minecraft", "path/to/item"), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithEmptyNamespace_UsesDefaultNamespace() {
        _ = ResourceLocation.TryParse(":path/to/item", out ResourceLocation? resourceLocation);
        Assert.AreEqual(new ResourceLocation("minecraft", "path/to/item"), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithEmptyNamespaceAndPath_UsesDefaultNamespaceAndEmptyPath() {
        _ = ResourceLocation.TryParse(":", out ResourceLocation? resourceLocation);
        Assert.AreEqual(new ResourceLocation("minecraft", ""), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithEmptyPath_UsesEmptyPath() {
        _ = ResourceLocation.TryParse("namespace:", out ResourceLocation? resourceLocation);
        Assert.AreEqual(new ResourceLocation("namespace", ""), resourceLocation);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecauseNamespaceIsInvalid() {
        bool successful = ResourceLocation.TryParse("INVALID:path/to/item", out _);
        Assert.IsFalse(successful);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecausePathIsInvalid() {
        bool successful = ResourceLocation.TryParse("namespace:INVALID", out _);
        Assert.IsFalse(successful);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecauseNamespaceContainsPathCharacters() {
        bool successful = ResourceLocation.TryParse("name/space:path/to/item", out _);
        Assert.IsFalse(successful);
    }
}
