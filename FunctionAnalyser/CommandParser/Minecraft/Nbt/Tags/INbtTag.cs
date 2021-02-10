namespace CommandParser.Minecraft.Nbt.Tags {
    public interface INbtTag {
        string ToSnbt();
        string GetName();
        sbyte GetId();
    }
}
