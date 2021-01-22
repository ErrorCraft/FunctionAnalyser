using FunctionAnalyser.Results;
using Utilities;

namespace UserInterface.ViewModels
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
