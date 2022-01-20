using ErrorCraft.Minecraft.Util.ResourceLocations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Util.ResourceLocations;

[TestClass]
public class LazyResourceLocationTests {
    [TestMethod]
    public void Constructor_UsesEmptyNamespace() {
        LazyResourceLocation resourceLocation = new LazyResourceLocation("path/to/item");
        Assert.AreEqual("", resourceLocation.Namespace);
    }
}
