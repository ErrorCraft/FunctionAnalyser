using System.Collections.Generic;

namespace AdvancedText
{
    public interface ILogger
    {
        void Log(TextComponent component);
        void Log(List<TextComponent> component);
        void Clear();
        string GetFlatString();
    }
}
