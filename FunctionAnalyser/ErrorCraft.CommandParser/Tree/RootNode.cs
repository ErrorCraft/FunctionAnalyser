using ErrorCraft.CommandParser.Results;
using System;

namespace ErrorCraft.CommandParser.Tree {
    public class RootNode : Node {
        public override string GetName() {
            return "";
        }

        public override ParseResults Parse(IStringReader reader) {
            throw new NotSupportedException();
        }
    }
}
