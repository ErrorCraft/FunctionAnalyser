using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandVerifier.Types
{
    public class Uuid
    {
        private static readonly Regex UUID_REGEX = new Regex("^[0-9a-fA-F]{1,8}-([0-9a-fA-F]{1,4}-){3}[0-9a-fA-F]{1,12}$");
        public static bool TryParse(string s)
        {
            // Currently no "out" parameter
            return UUID_REGEX.IsMatch(s);
        }
    }
}
