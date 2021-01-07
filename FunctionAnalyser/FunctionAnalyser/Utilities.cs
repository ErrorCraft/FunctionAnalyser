namespace FunctionAnalyser
{
    public static class Utilities
    {
        public static string CombinePaths(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path1)) return path2;
            else if (string.IsNullOrEmpty(path2)) return path1;
            else if (path1.EndsWith('/')) return path1 + path2;
            else return path1 + '/' + path2;
        }
    }
}
