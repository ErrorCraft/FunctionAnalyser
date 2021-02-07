using CommandParser.Context;
using CommandParser.Minecraft;
using System.Collections.Generic;

namespace CommandParser.Results.Arguments
{
    public class Particle
    {
        public ResourceLocation Name { get; }
        public Dictionary<string, ParsedArgument> Arguments { get; }

        public Particle(ResourceLocation name, Dictionary<string, ParsedArgument> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public override string ToString()
        {
            string s = Name.ToString();

            foreach (ParsedArgument argument in Arguments.Values)
            {
                s += $" {argument.GetResult()}";
            }

            return s;
        }
    }
}
