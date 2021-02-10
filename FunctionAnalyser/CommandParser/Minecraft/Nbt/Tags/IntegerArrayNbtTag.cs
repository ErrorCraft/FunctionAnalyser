using System.Collections.Generic;
using System.Text;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class IntegerArrayNbtTag : INbtCollectionTag {
        private readonly List<IntegerNbtTag> Data;

        public IntegerArrayNbtTag() {
            Data = new List<IntegerNbtTag>();
        }

        public string GetName() {
            return "TAG_Integer_Array";
        }

        public string ToSnbt() {
            StringBuilder stringBuilder = new StringBuilder("[I;");
            stringBuilder.Append(NbtUtilities.Join(", ", Data, n => n.ToSnbt()));
            return stringBuilder.Append(']').ToString();
        }

        public sbyte GetId() {
            return 11;
        }

        public bool Add(INbtTag tag) {
            if (tag is IntegerNbtTag i) {
                Data.Add(i);
                return true;
            }
            return false;
        }
    }
}
