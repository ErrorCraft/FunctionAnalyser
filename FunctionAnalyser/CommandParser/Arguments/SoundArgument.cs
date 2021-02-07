using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class SoundArgument : IArgument<Sound>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Sound result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation sound);
            if (readResults.Successful) result = new Sound(sound);
            return readResults;
        }
    }
}
