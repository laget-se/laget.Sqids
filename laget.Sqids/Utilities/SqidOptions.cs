using System.Collections.Generic;

namespace laget.Sqids.Utilities
{
    public class SqidOptions
    {
        public string DefaultAlphabetVersion { get; set; } = "a0";
        public int MinLength { get; set; } = 13;

        public Dictionary<string, string> Alphabets { get; set; }
    }
}