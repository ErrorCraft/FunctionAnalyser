using CommandParser.Context;
using CommandParser.Minecraft;
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

            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation particle);
            if (!readResults.Successful) return readResults;

            if (!resources.Particles.TryGetNodes(particle, out Dictionary<string, Node> nodes))
            {
                return ReadResults.Failure(CommandError.UnknownParticle(particle));
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
            return ReadResults.Success();
        }
    }
}
