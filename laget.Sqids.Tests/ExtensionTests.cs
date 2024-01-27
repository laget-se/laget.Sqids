using laget.Sqids.Extensions;
using laget.Sqids.Tests.Abstractions;
using Xunit;

namespace laget.Sqids.Tests
{
    public class ExtensionTests : TestBase
    {
        [Fact]
        public void ShouldConvertIntToSqid()
        {
            var id = 1;
            var sqid = id.ToSqid();

            var expected = "0x4OloBIN3OGzel";
            Assert.Equal(expected, sqid.Hash);
        }

        [Fact]
        public void ShouldConvertIntToSqidWithSpecifiedAlphabet()
        {
            var id = 1;
            var sqid = id.ToSqid(AlphabetVersion1X);

            var expected = "1x4OloBIN3OGzel";
            Assert.Equal(expected, sqid.Hash);
        }

        [Fact]
        public void ShouldConvertLongToSqid()
        {
            var id = (long)1;
            var sqid = id.ToSqid();

            var expected = "0x4OloBIN3OGzel";
            Assert.Equal(expected, sqid.Hash);
        }

        [Fact]
        public void ShouldConvertLongToSqidWithSpecifiedAlphabet()
        {
            var id = (long)1;
            var sqid = id.ToSqid(AlphabetVersion1X);

            var expected = "1x4OloBIN3OGzel";
            Assert.Equal(expected, sqid.Hash);
        }
    }
}
