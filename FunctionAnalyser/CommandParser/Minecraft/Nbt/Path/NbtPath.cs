namespace CommandParser.Minecraft.Nbt.Path {
    public class NbtPath {
        private readonly string Path;

        public NbtPath(string path) {
            Path = path;
        }

        public string GetPath() {
            return Path;
        }
    }
}
