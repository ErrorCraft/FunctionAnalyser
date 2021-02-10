using System.Collections.Generic;
using System.Text;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class CompoundNbtTag : INbtTag {
        private readonly Dictionary<string, INbtTag> Data;

        public CompoundNbtTag() {
            Data = new Dictionary<string, INbtTag>();
        }

        public string ToSnbt() {
            StringBuilder stringBuilder = new StringBuilder("{");
            stringBuilder.Append(NbtUtilities.Join(", ", Data, n => n.Key + ": " + n.Value.ToSnbt()));
            return stringBuilder.Append('}').ToString();
        }

        public string GetName() {
            return "TAG_Compound";
        }

        public sbyte GetId() {
            return 10;
        }

        public void Add(string key, INbtTag value) {
            Data[key] = value;
        }
    }
}
