using CommandParser.Arguments;
using CommandParser.Tree;

namespace CommandParser.Providers
{
    public static class NodeBuilder
    {
        public static LiteralNode Literal(string literal, bool executable, string[] redirect)
        {
            return new LiteralNode(literal, executable, redirect);
        }

        public static ArgumentNode<T> Argument<T>(string name, IArgument<T> type, bool executable, string[] redirect)
        {
            return new ArgumentNode<T>(name, type, executable, redirect);
        }
    }
}
