using CommandParser.Collections;
using CommandParser.Context;
using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using CommandParser.Tree;
using System.Collections.Generic;

namespace CommandParser.Arguments
{
    public class ParticleArgument : IArgument<Particle>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Particle result)
        {
            result = default;

            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation particle);
            if (!readResults.Successful) return readResults;

            if (!Particles.TryGetNodes(particle, out Dictionary<string, Node> nodes))
            {
                return new ReadResults(false, CommandError.UnknownParticle(particle));
            }

            Dictionary<string, ParsedArgument> arguments = new Dictionary<string, ParsedArgument>();
            CommandContext context;
            foreach (KeyValuePair<string, Node> pair in nodes)
            {
                readResults = reader.Expect(' ');
                if (!readResults.Successful) return readResults;

                context = new CommandContext(0);
                readResults = pair.Value.Parse(reader, context, resources);
                if (!readResults.Successful) return readResults;

                arguments.Add(pair.Key, context.Results[0]);
            }

            result = new Particle(particle, arguments);
            return new ReadResults(true, null);
        }
    }
}
