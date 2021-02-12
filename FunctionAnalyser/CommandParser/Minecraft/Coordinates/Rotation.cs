namespace CommandParser.Minecraft.Coordinates {
    public class Rotation {
        public Angle YRotation { get; }
        public Angle XRotation { get; }

        public Rotation(Angle yRotation, Angle xRotation) {
            YRotation = yRotation;
            XRotation = xRotation;
        }

        public override string ToString() {
            return YRotation.ToString() + " " + XRotation.ToString();
        }
    }
}
