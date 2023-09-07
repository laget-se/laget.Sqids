using laget.Sqids.Tests.Abstractions;
using Xunit;

namespace laget.Sqids.Tests
{
    public class EqualityTests : TestBase
    {
        [Fact]
        public void HashIdEqualHashId()
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
        public void HashIdNotEqualHashId()
        {
            const int hashId1 = 3;
            const int hashId2 = 4;

            var sqid1 = Sqid.FromInt(hashId1);
            var sqid2 = Sqid.FromInt(hashId2);

            Assert.NotEqual(sqid1, sqid2);
            Assert.NotEqual(sqid2, sqid1);
            Assert.False(sqid1 == sqid2);
            Assert.False(sqid2 == sqid1);
            Assert.True(sqid1 != sqid2);
            Assert.True(sqid2 != sqid1);
        }

        [Fact]
        public void HashIdNotEqualNullableHash()
        {
            const int hashId2 = 4;

            Sqid sqid1 = null;
            var sqid2 = Sqid.FromInt(hashId2);

            Assert.False(sqid1 == sqid2);
            Assert.False(sqid2 == sqid1);
        }
    }
}
