using ErrorCraft.CommandParser.Results;
using System.Collections.Generic;

namespace ErrorCraft.CommandParser.Tree {
    public abstract class Node {
        private readonly Dictionary<string, Node> Children = new Dictionary<string, Node>();

        public abstract string GetName();
        public abstract ParseResults Parse(IStringReader reader);

        public void AddChild(Node node) {
            Children.Add(node.GetName(), node);
        }

        public IEnumerable<Node> GetChildren() {
            foreach (Node child in Children.Values) {
                yield return child;
            }
        }
    }
}
