using AdvancedText;

namespace ErrorCraft.PackAnalyser.Results {
    public interface IResult {
        int GetTotal();
        TextComponent ToTextComponent();
    }
}
