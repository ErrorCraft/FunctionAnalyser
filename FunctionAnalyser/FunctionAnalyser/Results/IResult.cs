using AdvancedText;

namespace FunctionAnalyser.Results
{
    public interface IResult
    {
        int GetTotal();
        TextComponent ToTextComponent();
    }
}
