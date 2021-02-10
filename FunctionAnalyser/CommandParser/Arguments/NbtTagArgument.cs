using CommandParser.Minecraft.Nbt;
using CommandParser.Minecraft.Nbt.Tags;
using CommandParser.Results;

namespace CommandParser.Arguments {
    public class NbtTagArgument : IArgument<INbtTag> {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out INbtTag result) {
            return new NbtParser(reader).ReadValue(out result);
        }
    }
}
