using laget.Sqids.Extensions;
using laget.Sqids.Tests.Abstractions;
using Xunit;

namespace laget.Sqids.Tests
{
    public class ExtensionTests : TestBase
    {
        [Fact]
        public void ShouldConvertIntToHashId()
        {
            var id = 1;
            var sqid = id.ToSqid();

            var expected = "0xKwAcVqIY5sSnA";
            Assert.Equal(expected, sqid.Hash);
        }

        [Fact]
        public void ShouldConvertLongToHashId()
        {
            var id = (long)1;
            var sqid = id.ToSqid();

            var expected = "0xKwAcVqIY5sSnA";
            Assert.Equal(expected, sqid.Hash);
        }
    }
}
