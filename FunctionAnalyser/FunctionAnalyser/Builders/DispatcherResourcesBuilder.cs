using FunctionAnalyser.Builders.Collections;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders
{
    public class DispatcherResourcesBuilder
    {
        public Dictionary<string, ItemsBuilder> Items { get; init; }
        public Dictionary<string, CommandsBuilder> Commands { get; init; }
    }
}
