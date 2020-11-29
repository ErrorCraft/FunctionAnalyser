using AdvancedText;
using System.Collections.Generic;

namespace FunctionAnalyser
{
    public class FunctionData
    {
        public int Functions { get; set; }
        public int Commands { get; set; }
        public int Comments { get; set; }
        public int EmptyLines { get; set; }

        public int Literals { get; set; }

        public List<TextComponent> Messages { get; set; }

        public FunctionData()
        {
            Messages = new List<TextComponent>();
        }

        public static FunctionData operator +(FunctionData a, FunctionData b)
        {
            return new FunctionData()
            {
                Functions = a.Functions + b.Functions,
                Commands = a.Commands + b.Commands,
                Comments = a.Comments + b.Comments,
                EmptyLines = a.EmptyLines + b.EmptyLines,
                Literals = a.Literals + b.Literals
            };
        }
    }
}
