using CommandParser.Minecraft;

namespace CommandParser.Results.Arguments
{
    public class Function
    {
        public ResourceLocation Location { get; }
        public bool IsTag { get; }

        public Function(ResourceLocation location, bool isTag)
        {
            Location = location;
            IsTag = isTag;
        }
    }
}
