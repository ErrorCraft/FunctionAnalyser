using FunctionAnalyser.Results;

namespace UserInterface
{
    public class SortTypeViewModel
    {
        public SortType Value { get; }
        private readonly string DisplayName;

        public SortTypeViewModel(SortType value)
        {
            Value = value;
            DisplayName = value.GetDisplayName();
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
