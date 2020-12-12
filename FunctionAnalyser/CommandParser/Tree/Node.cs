using CommandParser.Context;
using CommandParser.Converters;
using CommandParser.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommandParser.Tree
{
    public abstract class Node
    {
        protected Node(bool executable, string[] redirect)
        {
            Executable = executable;
            Redirect = redirect;
        }

        [JsonProperty("children"), JsonConverter(typeof(NodeContainerConverter))]
        public Dictionary<string, Node> Children { get; private set; } = new Dictionary<string, Node>();
        private readonly Dictionary<string, LiteralNode> Literals = new Dictionary<string, LiteralNode>();
        private readonly Dictionary<string, ArgumentNode> Arguments = new Dictionary<string, ArgumentNode>();

        [OnDeserialized]
        internal void DivideChildren(StreamingContext context)
        {
            foreach (Node node in Children.Values)
            {
                AddSpecific(node);
            }
        }

        [JsonProperty("executable")]
        private readonly bool Executable;

        public bool GetExecutable()
        {
            return Executable;
        }

        [JsonProperty("redirect")]
        private readonly string[] Redirect;

        public string[] GetRedirect()
        {
            return Redirect;
        }

        public abstract string GetName();

        public void AddChild(Node node)
        {
            Children.Add(node.GetName(), node);
            AddSpecific(node);
        }

        private void AddSpecific(Node node)
        {
            if (node is LiteralNode literalNode)
            {
                Literals.Add(literalNode.GetName(), literalNode);
            }
            else if (node is ArgumentNode argumentNode)
            {
                Arguments.Add(argumentNode.GetName(), argumentNode);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add a {node.GetType().Name} as a child to any other {nameof(Node)}");
            }
        }

        public abstract ReadResults Parse(IStringReader reader, CommandContext builder);
        public IEnumerable<Node> GetRelevantNodes(IStringReader reader)
        {
            if (Literals.Count > 0)
            {
                int start = reader.GetCursor();
                while (!reader.AtEndOfArgument())
                {
                    reader.Skip();
                }
                string value = reader.GetString()[start..reader.GetCursor()];
                reader.SetCursor(start);
                if (Literals.TryGetValue(value, out LiteralNode literal))
                {
                    return new List<LiteralNode>(1) { literal };
                }
            }
            return Arguments.Values;
        }
    }
}
