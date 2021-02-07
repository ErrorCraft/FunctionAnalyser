using CommandParser.Minecraft;

namespace CommandParser.Results.Arguments
{
    public class MobEffect
    {
        public ResourceLocation Effect { get; }

        public MobEffect(ResourceLocation effect)
        {
            Effect = effect;
        }
    }
}
