using CommandParser.Results.Arguments;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class Entities
    {
        private readonly HashSet<string> Values;

        public Entities(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(ResourceLocation entity)
        {
            return entity.IsDefaultNamespace() && Values.Contains(entity.Path);
        }
    }
}
