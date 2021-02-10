namespace CommandParser.Minecraft.Nbt.Tags {
    public interface INbtCollectionTag : INbtTag {
        bool Add(INbtTag tag);
    }
}
