using CommandParser.Minecraft;
using CommandParser.Results;

namespace CommandParser.Arguments
{
    public class GamemodeArgument : IArgument<GameType>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out GameType result)
        {
            result = default;
            int start = reader.GetCursor();
            ReadResults readResults = reader.ReadUnquotedString(out string gamemode);
            if (!readResults.Successful) return readResults;
            if (!resources.Gamemodes.TryGet(gamemode, out result))
            {
                reader.SetCursor(start);
                return new ReadResults(false, CommandError.UnknownGamemode(gamemode).WithContext(reader));
            }

            return new ReadResults(true, null);
        }
    }
}
