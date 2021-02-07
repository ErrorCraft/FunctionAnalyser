using CommandParser.Minecraft;

namespace CommandParser.Results.Arguments
{
    public class Advancement
    {
        public ResourceLocation Value { get; }

        public Advancement(ResourceLocation advancement)
        {
            Value = advancement;
        }
    }
}
