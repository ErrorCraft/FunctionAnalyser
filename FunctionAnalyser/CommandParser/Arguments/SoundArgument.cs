using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class SoundArgument : IArgument<Sound>
    {
        public ReadResults Parse(IStringReader reader, out Sound result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation sound);
            if (readResults.Successful) result = new Sound(sound);
            return readResults;
        }
    }
}
