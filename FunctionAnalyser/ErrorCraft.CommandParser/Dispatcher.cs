using ErrorCraft.CommandParser.Tree;

namespace ErrorCraft.CommandParser {
    public class Dispatcher {
        private readonly RootNode Root = new RootNode();

        public int CommandCount {
            get {
                return Root.ChildCount;
            }
        }

        public void Register(LiteralNode command) {
            Root.AddChild(command);
        }
    }
}
