using CommandParser.Collections;
using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class MobEffectArgument : IArgument<MobEffect>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out MobEffect result)
        {
            result = default;
            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation mobEffect);
            if (!readResults.Successful) return readResults;
            if (!MobEffects.Contains(mobEffect))
            {
                return new ReadResults(false, CommandError.UnknownEffect(mobEffect));
            }
            result = new MobEffect(mobEffect);
            return new ReadResults(true, null);
        }
    }
}
