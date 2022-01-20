namespace ErrorCraft.Minecraft.Util.ResourceLocations;

public class LazyResourceLocation : ResourceLocation {
    public LazyResourceLocation(string path) : base("", path) { }

    public override string ToString() {
        return Path;
    }
}
