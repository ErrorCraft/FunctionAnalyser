using AdvancedText;

namespace FunctionAnalyser.Results
{
    public class SimpleResult : IResult
    {
        private int Total;

        public SimpleResult() : this(0) { }

        private SimpleResult(int total)
        {
            Total = total;
        }

        public void Increase()
        {
            Total++;
        }

        public int GetTotal()
        {
            return Total;
        }

        public TextComponent ToTextComponent()
        {
            return new TextComponent(Total.ToString());
        }

        public static SimpleResult operator +(SimpleResult a, SimpleResult b)
        {
            return new SimpleResult(a.Total + b.Total);
        }
    }
}
