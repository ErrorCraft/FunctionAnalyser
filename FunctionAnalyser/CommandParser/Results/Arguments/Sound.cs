using CommandParser.Minecraft;

namespace CommandParser.Results.Arguments
{
    public class Sound
    {
        public ResourceLocation Value { get; }

        public Sound(ResourceLocation sound)
        {
            Value = sound;
        }
    }
}
