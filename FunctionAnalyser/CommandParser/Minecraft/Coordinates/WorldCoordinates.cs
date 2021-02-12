namespace CommandParser.Minecraft.Coordinates {
    public class WorldCoordinates : ICoordinates {
        public WorldCoordinate X { get; }
        public WorldCoordinate Y { get; }
        public WorldCoordinate Z { get; }

        public WorldCoordinates(WorldCoordinate x, WorldCoordinate y, WorldCoordinate z) {
            X = x;
            Y = y;
            Z = z;
        }

        public bool IsXRelative() {
            return X.IsRelative;
        }

        public bool IsYRelative() {
            return Y.IsRelative;
        }

        public bool IsZRelative() {
            return Z.IsRelative;
        }

        public override string ToString() {
            return X.ToString() + " " + Y.ToString() + " " + Z.ToString();
        }
    }
}
