using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.Commands.SubcommandTypes.Selector
{
    public class EntitySelector
    {
        public bool IncludesEntities { get; set; }
        public int MaxResults { get; set; }
        public bool CurrentEntity { get; set; }
    }
}
