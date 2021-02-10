using CommandParser.Minecraft.Nbt;
using CommandParser.Minecraft.Nbt.Tags;
using CommandParser.Results;

namespace CommandParser.Arguments {
    public class CompoundTagArgument : IArgument<INbtTag> {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out INbtTag result) {
            return new NbtParser(reader).ReadCompound(out result);
        }
    }
}
