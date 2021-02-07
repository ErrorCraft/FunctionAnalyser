using CommandParser.Results;

namespace CommandParser.Minecraft
{
    public class ResourceLocation
    {
        public static readonly ResourceLocation PLAYER_ENTITY = new ResourceLocation("player");
        private const char NAMESPACE_SEPARATOR = ':';
        private const string DEFAULT_NAMESPACE = "minecraft";

        public string Namespace { get; }
        public string Path { get; }

        private ResourceLocation(string path) : this(DEFAULT_NAMESPACE, path) { }

        private ResourceLocation(string @namespace, string path)
        {
            Namespace = @namespace;
            Path = path;
        }

        public bool IsDefaultNamespace()
        {
            return Namespace == DEFAULT_NAMESPACE;
        }

        public override string ToString()
        {
            return Namespace + NAMESPACE_SEPARATOR + Path;
        }

        public static bool TryParse(string input, out ResourceLocation result)
        {
            result = null;
            string[] a = Decompose(input, NAMESPACE_SEPARATOR);
            if (!IsValidNamespace(a[0]) || !IsValidPath(a[1])) return false;

            result = new ResourceLocation(a[0], a[1]);
            return true;
        }

        public static ReadResults TryRead(IStringReader stringReader, out ResourceLocation result)
        {
            int start = stringReader.GetCursor();
            while (stringReader.CanRead() && IsAllowedInResourceLocation(stringReader.Peek())) stringReader.Skip();
            string input = stringReader.GetString()[start..stringReader.GetCursor()];

            if (!TryParse(input, out result))
            {
                stringReader.SetCursor(start);
                return ReadResults.Failure(CommandError.InvalidId());
            }
            return ReadResults.Success();
        }

        private static bool IsAllowedInResourceLocation(char c)
        {
            return c >= '0' && c <= '9' || c >= 'a' && c <= 'z' || c == '_' || c == ':' || c == '/' || c == '.' || c == '-';
        }

        private static string[] Decompose(string s, char c)
        {
            string[] result = new string[2] { DEFAULT_NAMESPACE, s };
            int n = s.IndexOf(c);
            if (n >= 0)
            {
                result[1] = s[(n + 1)..];
                if (n >= 1) result[0] = s[0..n];
            }
            return result;
        }

        private static bool IsValidNamespace(string s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (IsValidNamespaceCharacter(s[i])) continue;
                return false;
            }
            return true;
        }

        private static bool IsValidPath(string s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (IsValidPathCharacter(s[i])) continue;
                return false;
            }
            return true;
        }

        private static bool IsValidNamespaceCharacter(char c)
        {
            return c >= '0' && c <= '9' || c >= 'a' && c <= 'z' || c == '_' || c == '-' || c == '.';
        }

        private static bool IsValidPathCharacter(char c)
        {
            return c >= '0' && c <= '9' || c >= 'a' && c <= 'z' || c == '_' || c == '-' || c == '.' || c == '/';
        }
    }
}
