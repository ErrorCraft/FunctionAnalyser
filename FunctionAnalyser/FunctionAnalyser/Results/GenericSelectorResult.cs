using AdvancedText;

namespace FunctionAnalyser.Results
{
    public class GenericSelectorResult : IGenericResult
    {
        private int Total;
        private int InSelectors;

        public void Increase(bool inSelector)
        {
            Total++;
            if (inSelector) InSelectors++;
        }

        public TextComponent ToTextComponent()
        {
            return new TextComponent(Total.ToString()).WithColour(Colour.BuiltinColours.WHITE).With(
                InSelectors > 0 ? new TextComponent($" ({InSelectors} in selectors)").WithColour(Colour.BuiltinColours.AQUA) : null
            );
        }

        public static GenericSelectorResult operator +(GenericSelectorResult a, GenericSelectorResult b)
        {
            return new GenericSelectorResult()
            {
                Total = a.Total + b.Total,
                InSelectors = a.InSelectors + b.InSelectors
            };
        }
    }
}
