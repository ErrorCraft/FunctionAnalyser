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

        public override ReadResults Parse(StringReader reader, CommandContext builder)
        {
            int start = reader.Cursor;
            int end = Parse(reader);
            if (end > -1)
            {
                ParsedArgument<Literal> parsed = new ParsedArgument<Literal>(new Literal(reader.Command[start..end]), builder.InRoot);
                builder.AddArgument(parsed);
                builder.EncompassRange(new Range(start, end));
                return new ReadResults(true, null);
            }
            return new ReadResults(false, CommandError.ExpectedLiteral(Literal).WithContext(reader));
        }

        private int Parse(StringReader reader)
        {
            int start = reader.Cursor;
            if (reader.CanRead(Literal.Length))
            {
                int end = reader.Cursor + Literal.Length;
                if (reader.Command[start..end].Equals(Literal))
                {
                    reader.Cursor = end;
                    if (reader.AtEndOfArgument())
                    {
                        return end;
                    } else
                    {
                        reader.Cursor = start;
                    }
                }
            }
            return -1;
        }
    }
}
