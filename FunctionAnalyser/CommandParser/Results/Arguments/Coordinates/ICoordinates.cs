namespace CommandParser.Results.Arguments.Coordinates
{
    public interface ICoordinates
    {
        bool IsXRelative();
        bool IsYRelative();
        bool IsZRelative();
    }
}
