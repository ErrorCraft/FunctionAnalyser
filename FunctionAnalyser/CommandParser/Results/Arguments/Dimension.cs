using CommandParser.Minecraft;

namespace CommandParser.Results.Arguments
{
    public class Dimension
    {
        public ResourceLocation Value { get; }

        public Dimension(ResourceLocation dimension)
        {
            Value = dimension;
        }
    }
}
