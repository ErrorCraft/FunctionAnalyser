using CommandParser.Arguments;
using CommandParser.Context;
using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Tree
{
    public abstract class ArgumentNode : Node
    {
        protected ArgumentNode(bool executable, string[] redirect) : base(executable, redirect) { }
    }

    public class ArgumentNode<T> : ArgumentNode
    {
        private readonly string Name;
        [JsonProperty("properties")]
        private readonly IArgument<T> Argument;

        public ArgumentNode(string name, IArgument<T> argument, bool executable, string[] redirect) : base(executable, redirect)
        {
            Name = name;
            Argument = argument;
        }

        public override string GetName()
        {
            return Name;
        }

        public override ReadResults Parse(IStringReader reader, CommandContext builder)
        {
            int start = reader.GetCursor();
            ReadResults parseResults = Argument.Parse(reader, out T result);
            if (parseResults.Successful)
            {
                ParsedArgument<T> parsed = new ParsedArgument<T>(result, builder.InRoot);
                builder.AddArgument(parsed);
                builder.EncompassRange(new Range(start, reader.GetCursor()));
            }

            return parseResults;
        }
    }
}
