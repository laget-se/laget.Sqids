using laget.Sqids.Tests.Abstractions;
using Xunit;

namespace laget.Sqids.Tests
{
    public class EqualityTests : TestBase
    {
        [Fact]
        public void SqidEqualSqid()
        {
            const int id1 = 1;
            const int id2 = 2;

            var sqid1 = Sqid.FromInt(id1);
            var sqid2 = Sqid.FromInt(id2);

            Assert.Equal(sqid1, sqid1);
            Assert.Equal(sqid2, sqid2);
            Assert.True(sqid1 == sqid1);
            Assert.True(sqid2 == sqid2);
        }

        [Fact]
        public void SqidNotEqualSqid()
        {
            const int id1 = 3;
            const int id2 = 4;

            var sqid1 = Sqid.FromInt(id1);
            var sqid2 = Sqid.FromInt(id2);

            Assert.NotEqual(sqid1, sqid2);
            Assert.NotEqual(sqid2, sqid1);
            Assert.False(sqid1 == sqid2);
            Assert.False(sqid2 == sqid1);
            Assert.True(sqid1 != sqid2);
            Assert.True(sqid2 != sqid1);
        }

        [Fact]
        public void SqidNotEqualNullableHash()
        {
            const int id2 = 4;

            Sqid sqid1 = null;
            var sqid2 = Sqid.FromInt(id2);

            Assert.False(sqid1 == sqid2);
            Assert.False(sqid2 == sqid1);
        }

        [Fact]
        public void SqidGeneratedWithDifferentAlphabetsShouldNotBeEqual()
        {
            var squid0x = Sqid.FromInt(1, AlphabetVersion0X);
            var squid1x = Sqid.FromInt(1, AlphabetVersion1X);

            Assert.NotEqual(squid1x, squid0x);
        }
    }
}
