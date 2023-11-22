using laget.Sqids.Exceptions;
using Sqids;
using System.Collections.Generic;
using System.Linq;

namespace laget.Sqids.Utilities
{
    public interface ISquidFactory
    {
        string GetHash(int id);
        int GetId(string hash);
    }

    public class SqidFactory : ISquidFactory
    {
        private const int DefaultHashLength = 13;


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
                    MinLength = DefaultHashLength
                }));
#else
                _encoders.Add(version.Key, new SqidsEncoder(new SqidsOptions
                {
                    Alphabet = options.DefaultAlphabet,
                    MinLength = DefaultHashLength
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
                    MinLength = DefaultHashLength,
                    BlockList = blockList
                }));
#else
                _encoders.Add(version.Key, new SqidsEncoder(new SqidsOptions
                {
                    Alphabet = options.DefaultAlphabet,
                    MinLength = DefaultHashLength,
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