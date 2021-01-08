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

        public override ReadResults Parse(IStringReader reader, CommandContext builder, DispatcherResources resources)
        {
            throw new NotImplementedException();
        }

        public void Merge(RootNode other)
        {
            foreach (Node child in other.Children.Values) AddChild(child);
        }
    }
}
