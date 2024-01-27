using laget.Sqids.Utilities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace laget.Sqids.Tests.Utilities
{
    public class SqidFactoryTest
    {
        [Fact]
        public void ProperlyParsesConfig()
        {
            const string defaultAlphabetVersion = "a0";

            const string alphabetVersion2 = "1x";

            const string version0XAlphabet = "hG25pLJeYHEcnyzwi0IaOuFVb47o68U3SxT1vWdNKlfsqmtZkMDBAXQPR9rCjg";
            const string version1XAlphabet = "WTXozyU3DcI5C4LKinBJZOH6a2mfGewpP8ltV7ES0qu1kAYjvhgrbxNR9dFsQM";

            var config = new Dictionary<string, string>
            {
                { $"Alphabets:{defaultAlphabetVersion}", version0XAlphabet },
                { $"Alphabets:{alphabetVersion2}", version1XAlphabet }
            };

            var options = new ConfigurationBuilder()
                .AddInMemoryCollection(config)
                .Build()
                .Get<SqidOptions>();

            Assert.Equal(defaultAlphabetVersion, options.DefaultAlphabetVersion);
            Assert.Equal(version0XAlphabet, options.Alphabets[defaultAlphabetVersion]);
            Assert.Equal(version1XAlphabet, options.Alphabets[alphabetVersion2]);
        }

        [Fact]
        public void ProperlyParsesConfigWithOverrides()
        {
            const string defaultAlphabetVersion = "0x";

            const string alphabetVersion2 = "1x";

            const string version0XAlphabet = "hG25pLJeYHEcnyzwi0IaOuFVb47o68U3SxT1vWdNKlfsqmtZkMDBAXQPR9rCjg";
            const string version1XAlphabet = "WTXozyU3DcI5C4LKinBJZOH6a2mfGewpP8ltV7ES0qu1kAYjvhgrbxNR9dFsQM";

            var config = new Dictionary<string, string>
            {
                { "DefaultAlphabetVersion", defaultAlphabetVersion },
                { $"Alphabets:{defaultAlphabetVersion}", version0XAlphabet },
                { $"Alphabets:{alphabetVersion2}", version1XAlphabet }
            };

            var options = new ConfigurationBuilder()
                .AddInMemoryCollection(config)
                .Build()
                .Get<SqidOptions>();

            Assert.Equal(defaultAlphabetVersion, options.DefaultAlphabetVersion);
            Assert.Equal(version0XAlphabet, options.Alphabets[defaultAlphabetVersion]);
            Assert.Equal(version1XAlphabet, options.Alphabets[alphabetVersion2]);
        }
    }
}