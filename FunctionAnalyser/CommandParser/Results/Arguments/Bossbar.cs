using CommandParser.Minecraft;

namespace CommandParser.Results.Arguments
{
    public class Bossbar
    {
        public ResourceLocation Value { get; }

        public Bossbar(ResourceLocation bossbar)
        {
            Value = bossbar;
        }
    }
}
