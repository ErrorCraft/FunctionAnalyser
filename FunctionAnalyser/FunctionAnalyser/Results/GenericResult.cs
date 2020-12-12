using AdvancedText;
using System;

namespace FunctionAnalyser.Results
{
    public class GenericResult : IGenericResult
    {
        private int Total;

        public GenericResult()
            : this(0) { }

        private GenericResult(int total)
        {
            Total = total;
        }

        public void Increase()
        {
            Total++;
        }

        public TextComponent ToTextComponent()
        {
            return new TextComponent(Total.ToString()).WithColour(Colour.BuiltinColours.WHITE);
        }

        public override bool Equals(object obj)
        {
            return obj is GenericResult result &&
                   Total == result.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Total);
        }

        public static GenericResult operator +(GenericResult a, GenericResult b)
        {
            return new GenericResult(a.Total + b.Total);
        }

        public static bool operator ==(GenericResult a, int b)
        {
            return a.Total == b;
        }

        public static bool operator !=(GenericResult a, int b)
        {
            return a.Total != b;
        }

        public static bool operator >(GenericResult a, int b)
        {
            return a.Total > b;
        }

        public static bool operator <(GenericResult a, int b)
        {
            return a.Total < b;
        }

        public static double operator /(double a, GenericResult b)
        {
            return a / b.Total;
        }
    }
}
