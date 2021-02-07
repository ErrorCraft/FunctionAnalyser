using CommandParser.Minecraft;
using CommandParser.Parsers.NbtParser.NbtArguments;
using System.Collections.Generic;
using System.Text;

namespace CommandParser.Results.Arguments
{
    public class Block
    {
        public ResourceLocation Resource { get; }
        public Dictionary<string, string> BlockStates { get; }
        public NbtCompound Nbt { get; }
        public bool IsTag { get; }

        public Block(ResourceLocation block, Dictionary<string, string> blockStates, NbtCompound nbt, bool isTag)
        {
            Resource = block;
            BlockStates = blockStates;
            Nbt = nbt;
            IsTag = isTag;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (IsTag) stringBuilder.Append('#');
            stringBuilder.Append(Resource.ToString());
            if (BlockStates != null)
            {
                stringBuilder.Append('[');
                string s = "";
                foreach (KeyValuePair<string, string> BlockState in BlockStates)
                {
                    s += BlockState.Key + "=" + BlockState.Value + ", ";
                }
                stringBuilder.Append(s.Trim(',', ' '));
                stringBuilder.Append(']');
            }
            if (Nbt != null) stringBuilder.Append(Nbt.ToSnbt());
            return stringBuilder.ToString();
        }
    }
}
