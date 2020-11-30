using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAnalyser
{
    public class CommandUsage
    {
        public int Commands { get; set; }
        public int BehindExecute { get; set; }

        public static CommandUsage operator +(CommandUsage a, CommandUsage b)
        {
            return new CommandUsage()
            {
                Commands = a.Commands + b.Commands,
                BehindExecute = a.BehindExecute + b.BehindExecute
            };
        }
    }
}
