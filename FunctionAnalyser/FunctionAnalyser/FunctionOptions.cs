using FunctionAnalyser.Results;

namespace FunctionAnalyser
{
    public class FunctionOptions
    {
        public bool SkipFunctionOnError { get; init; }
        public bool ShowCommandErrors { get; init; }
        public bool ShowEmptyFunctions { get; init; }
        public SortType CommandSort { get; init; }
    }
}
