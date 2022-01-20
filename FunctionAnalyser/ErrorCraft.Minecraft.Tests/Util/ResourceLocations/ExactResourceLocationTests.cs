using ErrorCraft.Minecraft.Util.ResourceLocations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Util.ResourceLocations;

[TestClass]
public class ExactResourceLocationTests {
    [TestMethod]
    public void TryParse_ReturnsCorrectValue() {
        _ = ExactResourceLocation.TryParse("namespace:path/to/item", out ExactResourceLocation? resourceLocation);
        Assert.AreEqual(new ExactResourceLocation("namespace", "path/to/item"), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithNoNamespace_UsesDefaultNamespace() {
        _ = ExactResourceLocation.TryParse("path/to/item", out ExactResourceLocation? resourceLocation);
        Assert.AreEqual(new ExactResourceLocation("minecraft", "path/to/item"), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithEmptyNamespace_UsesDefaultNamespace() {
        _ = ExactResourceLocation.TryParse(":path/to/item", out ExactResourceLocation? resourceLocation);
        Assert.AreEqual(new ExactResourceLocation("minecraft", "path/to/item"), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithEmptyNamespaceAndPath_UsesDefaultNamespaceAndEmptyPath() {
        _ = ExactResourceLocation.TryParse(":", out ExactResourceLocation? resourceLocation);
        Assert.AreEqual(new ExactResourceLocation("minecraft", ""), resourceLocation);
    }

    [TestMethod]
    public void TryParse_WithEmptyPath_UsesEmptyPath() {
        _ = ExactResourceLocation.TryParse("namespace:", out ExactResourceLocation? resourceLocation);
        Assert.AreEqual(new ExactResourceLocation("namespace", ""), resourceLocation);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecauseNamespaceIsInvalid() {
        bool successful = ExactResourceLocation.TryParse("INVALID:path/to/item", out _);
        Assert.IsFalse(successful);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecausePathIsInvalid() {
        bool successful = ExactResourceLocation.TryParse("namespace:INVALID", out _);
        Assert.IsFalse(successful);
    }

    [TestMethod]
    public void TryParse_ReturnsFalse_BecauseNamespaceContainsPathCharacters() {
        bool successful = ExactResourceLocation.TryParse("name/space:path/to/item", out _);
        Assert.IsFalse(successful);
    }
}
