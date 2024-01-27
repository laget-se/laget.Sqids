using System.Collections.Generic;

namespace laget.Sqids.Utilities
{
    public class SqidOptions
    {
        public string DefaultAlphabetVersion { get; set; } = "a0";
        public string DefaultAlphabet { get; set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public int MinLength { get; set; } = 13;

        public Dictionary<string, string> Alphabets { get; set; }
    }
}