using CommandParser.Context;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Tree
{
    public class LiteralNode : Node
    {
        private readonly string Literal;

        public LiteralNode(string literal, bool executable, string[] redirect) : base(executable, redirect)
        {
            Literal = literal;
        }

        public override string GetName()
        {
            return Literal;
        }

        public override ReadResults Parse(IStringReader reader, CommandContext builder, DispatcherResources resources)
        {
            int start = reader.GetCursor();
            int end = Parse(reader);
            if (end > -1)
            {
                ParsedArgument<Literal> parsed = new ParsedArgument<Literal>(new Literal(reader.GetString()[start..end]), builder.InRoot);
                builder.AddArgument(parsed);
                builder.EncompassRange(new Range(start, end));
                return ReadResults.Success();
            }
            return ReadResults.Failure(CommandError.ExpectedLiteral(Literal).WithContext(reader));
        }

        private int Parse(IStringReader reader)
        {
            int start = reader.GetCursor();
            if (reader.CanRead(Literal.Length))
            {
                int end = reader.GetCursor() + Literal.Length;
                if (reader.GetString()[start..end].Equals(Literal))
                {
                    reader.SetCursor(end);
                    if (reader.AtEndOfArgument())
                    {
                        return end;
                    } else
                    {
                        reader.SetCursor(start);
                    }
                }
            }
            return -1;
        }
    }
}
