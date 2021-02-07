using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class BossbarArgument : IArgument<Bossbar>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Bossbar result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation bossbar);
            if (readResults.Successful) result = new Bossbar(bossbar);
            return readResults;
        }
    }
}
