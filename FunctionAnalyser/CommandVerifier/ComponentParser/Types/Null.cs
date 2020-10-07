using CommandVerifier.Commands;

namespace CommandVerifier.ComponentParser.Types
{
    class Null : IComponent
    {
        public override string ToString() => "null";
        public bool Validate(StringReader reader, int start, bool mayThrow)
        {
            reader.SetCursor(start);
            if (mayThrow) CommandError.InvalidChatComponent("Don't know how to turn " + ToString() + " into a component").AddWithContext(reader);
            return false;
        }
    }
}
