using CommandParser.Parsers.NbtParser.NbtArguments;

namespace CommandParser.Results.Arguments
{
    public class Nbt
    {
        public INbtArgument Argument { get; }

        public Nbt(INbtArgument argument)
        {
            Argument = argument;
        }

        public string ToSnbt()
        {
            return Argument.ToSnbt();
        }
    }
}
