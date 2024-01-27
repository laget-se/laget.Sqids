using laget.Sqids.Exceptions;
using Sqids;
using System.Collections.Generic;
using System.Linq;

namespace laget.Sqids.Utilities
{
    public interface ISqidFactory
    {
        string GetHash(int id);
        string GetHash(int id, string alphabet);
        int GetId(string hash);
    }

    public class SqidFactory : ISqidFactory
    {
        private readonly string _defaultAlphabetVersion;

#if NET7_0_OR_GREATER
        private readonly Dictionary<string, SqidsEncoder<int>> _encoders = new();
#else
        private readonly Dictionary<string, SqidsEncoder> _encoders = new Dictionary<string, SqidsEncoder>();
#endif

        public SqidFactory(SqidOptions options)
        {
            foreach (var version in options.Alphabets)
            {
                _defaultAlphabetVersion = options.DefaultAlphabetVersion;
#if NET7_0_OR_GREATER
                _encoders.Add(version.Key, new SqidsEncoder<int>(new SqidsOptions
                {
                    Alphabet = options.DefaultAlphabet,
                    MinLength = options.MinLength
                }));
#else
                _encoders.Add(version.Key, new SqidsEncoder(new SqidsOptions
                {
                    Alphabet = options.DefaultAlphabet,
                    MinLength = options.MinLength
                }));
#endif
            }
        }

        public SqidFactory(SqidOptions options, HashSet<string> blockList)
        {
            foreach (var version in options.Alphabets)
            {
                _defaultAlphabetVersion = options.DefaultAlphabetVersion;
#if NET7_0_OR_GREATER
                _encoders.Add(version.Key, new SqidsEncoder<int>(new SqidsOptions
                {
                    Alphabet = options.DefaultAlphabet,
                    MinLength = options.MinLength,
                    BlockList = blockList
                }));
#else
                _encoders.Add(version.Key, new SqidsEncoder(new SqidsOptions
                {
                    Alphabet = options.DefaultAlphabet,
                    MinLength = options.MinLength,
                    BlockList = blockList
                }));
#endif
            }
        }

        public string GetHash(int id)
        {
            if (!_encoders.TryGetValue(_defaultAlphabetVersion, out var encoder))
                throw new SqidsInvalidVersionException(_defaultAlphabetVersion);

            return $"{_defaultAlphabetVersion}{encoder.Encode(id)}";
        }

        public string GetHash(int id, string alphabet)
        {
            if (!_encoders.TryGetValue(alphabet, out var encoder))
                throw new SqidsInvalidVersionException(alphabet);

            return $"{alphabet}{encoder.Encode(id)}";
        }

        public int GetId(string hash)
        {
            var version = hash.Substring(0, 2);

            if (!_encoders.TryGetValue(version, out var encoder))
                throw new SqidsInvalidVersionException(version);

            var hashString = hash.Substring(2);
            return encoder.Decode(hashString).Single();
        }
    }
}