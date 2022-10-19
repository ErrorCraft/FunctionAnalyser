using AdvancedText;

namespace ErrorCraft.PackAnalyser.Results {
    public class SelectorResult : IResult {
        private int Total;
        private int InSelectors;

        public SelectorResult() : this(0, 0) { }

        private SelectorResult(int total, int inSelectors) {
            Total = total;
            InSelectors = inSelectors;
        }

        public void Increase(bool inSelector) {
            Total++;
            if (inSelector) InSelectors++;
        }

        public int GetTotal() {
            return Total;
        }

        public TextComponent ToTextComponent() {
            return new TextComponent(Total.ToString()).With(
                InSelectors > 0 ? new TextComponent($" ({InSelectors} in selectors)").WithColour(Colour.BuiltinColours.GREY) : null
            );
        }

        public static SelectorResult operator +(SelectorResult a, SelectorResult b) {
            return new SelectorResult(a.Total + b.Total, a.InSelectors + b.InSelectors);
        }
    }
}
