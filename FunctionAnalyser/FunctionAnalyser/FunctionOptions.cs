using ErrorCraft.PackAnalyser.Results;

namespace ErrorCraft.PackAnalyser {
    public class FunctionOptions {
        public bool SkipFunctionOnError { get; init; }
        public bool ShowCommandErrors { get; init; }
        public bool ShowEmptyFunctions { get; init; }
        public SortType CommandSortType { get; init; }
    }
}
