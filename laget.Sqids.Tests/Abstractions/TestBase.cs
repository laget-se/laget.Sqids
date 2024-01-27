using laget.Sqids.Utilities;
using System.Collections.Generic;

namespace laget.Sqids.Tests.Abstractions
{
    public abstract class TestBase
    {
        protected const string AlphabetVersion0X = "0x";
        protected const string Version0XAlphabet = "yCfG1QRsD6Jw70e4PLzKZb89vBuEHohMOaxkcArjqtmYlXnpNIg2i5WFSUT3Vd";
        protected const string AlphabetVersion1X = "1x";
        protected const string Version1XAlphabet = "kafeJOLsDjwdFVT8G4RmSgtB7H9XWI3ZpolrhA15yizuNQcPCEqxMvn2b06YKU";
        protected const string AlphabetVersion2X = "2x";

        protected readonly ISqidFactory SqidFactory;

        protected TestBase()
        {
            var options = new SqidOptions
            {
                DefaultAlphabetVersion = AlphabetVersion0X,
                DefaultAlphabet = Version0XAlphabet,
                Alphabets = new Dictionary<string, string>
                {
                    { AlphabetVersion0X, Version0XAlphabet },
                    { AlphabetVersion1X, Version1XAlphabet }
                }
            };
            SqidFactory = new SqidFactory(options);
            Sqid.SetFactory(SqidFactory);
        }
    }
}
