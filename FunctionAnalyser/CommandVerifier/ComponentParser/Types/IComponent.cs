namespace CommandVerifier.ComponentParser.Types
{
    interface IComponent
    {
        bool Validate(StringReader reader, int start, bool mayThrow);
    }
}
