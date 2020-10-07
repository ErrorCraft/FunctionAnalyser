namespace CommandVerifier.NbtParser.Types
{
    interface INbtCollection : INbtArgument
    {
        public bool TryAdd(INbtArgument value);
    }
}
