using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class MobEffectArgument : IArgument<MobEffect>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out MobEffect result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation mobEffect);
            if (!readResults.Successful) return readResults;
            if (!resources.MobEffects.Contains(mobEffect))
            {
                return ReadResults.Failure(CommandError.UnknownEffect(mobEffect));
            }
            result = new MobEffect(mobEffect);
            return ReadResults.Success();
        }
    }
}
