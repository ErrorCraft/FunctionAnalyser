using CommandVerifier;

namespace FunctionAnalyser
{
    public class FunctionInformation
    {
        public int Functions { get; set; }
        public int Commands { get; set; }
        public int EmptyLines { get; set; }
        public int Comments { get; set; }
        public int EntitySelectors { get; set; }
        public int NbtAccess { get; set; }
        public int PredicateCalls { get; set; }

        public void Reset()
        {
            Functions = 0;
            Commands = 0;
            EmptyLines = 0;
            Comments = 0;
            EntitySelectors = 0;
            NbtAccess = 0;
            PredicateCalls = 0;
        }

        public static FunctionInformation operator +(FunctionInformation a, FunctionInformation b) => new FunctionInformation()
        {
            Functions = a.Functions + b.Functions,
            Commands = a.Commands + b.Commands,
            EmptyLines = a.EmptyLines + b.EmptyLines,
            Comments = a.Comments + b.Comments,
            EntitySelectors = a.EntitySelectors + b.EntitySelectors,
            NbtAccess = a.NbtAccess + b.NbtAccess,
            PredicateCalls = a.PredicateCalls + b.PredicateCalls
        };

        public static FunctionInformation operator +(FunctionInformation a, CommandInformation b) => new FunctionInformation()
        {
            Functions = a.Functions,
            Commands = a.Commands,
            EmptyLines = a.EmptyLines,
            Comments = a.Comments,
            EntitySelectors = a.EntitySelectors + b.EntitySelectors,
            NbtAccess = a.NbtAccess + b.NbtAccess,
            PredicateCalls = a.PredicateCalls + b.PredicateCalls
        };
    }
}
