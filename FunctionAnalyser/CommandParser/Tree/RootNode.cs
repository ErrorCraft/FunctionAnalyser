using CommandParser.Context;
using CommandParser.Results;
using System;

namespace CommandParser.Tree
{
    public class RootNode : Node
    {
        public RootNode() : base(false, null) { }

        public override string GetName()
        {
            return "";
        }

        public override ReadResults Parse(IStringReader reader, CommandContext builder)
        {
            throw new NotImplementedException();
        }
    }
}
