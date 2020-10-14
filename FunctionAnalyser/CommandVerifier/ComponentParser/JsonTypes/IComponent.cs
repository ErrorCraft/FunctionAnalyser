namespace CommandVerifier.ComponentParser.JsonTypes
{
    public interface IComponent
    {
        bool Validate(StringReader reader, int start, bool mayThrow);
        string GetName();
        string AsJson();
    }
}
