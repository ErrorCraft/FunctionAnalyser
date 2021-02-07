using CommandParser.Minecraft;
using CommandParser.Parsers.NbtParser.NbtArguments;
using System.Text;

namespace CommandParser.Results.Arguments
{
    public class Item
    {
        public ResourceLocation Resource { get; }
        public NbtCompound Nbt { get; }
        public bool IsTag { get; }

        public Item(ResourceLocation item, NbtCompound nbt, bool isTag)
        {
            Resource = item;
            Nbt = nbt;
            IsTag = isTag;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (IsTag) stringBuilder.Append('#');
            stringBuilder.Append(Resource.ToString());
            if (Nbt != null) stringBuilder.Append(Nbt.ToSnbt());
            return stringBuilder.ToString();
        }
    }
}
