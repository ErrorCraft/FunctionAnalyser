using System.Collections.Generic;
using System.Text;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class ListNbtTag : INbtCollectionTag {
        private readonly List<INbtTag> Data;
        private sbyte? Type;

        public ListNbtTag() {
            Data = new List<INbtTag>();
            Type = null;
        }

        public string GetName() {
            return "TAG_List";
        }

        public string ToSnbt() {
            StringBuilder stringBuilder = new StringBuilder("[");
            stringBuilder.Append(NbtUtilities.Join(", ", Data, n => n.ToSnbt()));
            return stringBuilder.Append(']').ToString();
        }

        public sbyte GetId() {
            return 9;
        }

        public bool Add(INbtTag tag) {
            sbyte id = tag.GetId();
            if (Type == null) Type = id;
            else if (Type != id) return false;

            Data.Add(tag);
            return true;
        }
    }
}
