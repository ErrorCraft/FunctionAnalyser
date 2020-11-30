using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class BossbarArgument : IArgument<Bossbar>
    {
        public ReadResults Parse(StringReader reader, out Bossbar result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation bossbar);
            if (readResults.Successful) result = new Bossbar(bossbar);
            return readResults;
        }
    }
}
