using CommandParser.Minecraft;

namespace CommandParser.Results.Arguments
{
    public class Entity
    {
        public ResourceLocation Value { get; }
        public bool IsTag { get; }

        public Entity(ResourceLocation value) : this(value, false) { }

        public Entity(ResourceLocation value, bool isTag)
        {
            Value = value;
            IsTag = isTag;
        }
    }
}
