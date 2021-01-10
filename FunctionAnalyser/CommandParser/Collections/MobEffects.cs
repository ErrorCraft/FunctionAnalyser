using CommandParser.Results.Arguments;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class MobEffects
    {
        private readonly HashSet<string> Values;

        public MobEffects(HashSet<string> values)
        {
            Values = values;
        }

        public bool Contains(ResourceLocation effect)
        {
            return effect.IsDefaultNamespace() && Values.Contains(effect.Path);
        }
    }
}
