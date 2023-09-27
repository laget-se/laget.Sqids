using laget.Sqids.Extensions;
using laget.Sqids.Tests.Abstractions;
using Xunit;

namespace laget.Sqids.Tests
{
    public class ConversionTests : TestBase
    {
        [Fact]
        public void ShouldBeCastToInt()
        {
            const int id = 1;
            var sqid = Sqid.FromInt(id);
            var model = new IntModel { Value = (int)sqid };

            Assert.Equal(1, sqid.ToInt());
            Assert.Equal(1, (int)sqid);
            Assert.Equal(1, model.Value);
        }

        [Fact]
        public void ShouldBeCastNullableInt()
        {
            var model = new NullableIntModel { Value = null };
            var sqid = model.Value.ToSqid();

            Assert.Null(sqid);
        }

        [Fact]
        public void ShouldBeCastToLong()
        {
            const int id = 1;
            var sqid = Sqid.FromInt(id);
            var model = new LongModel { Value = (long)sqid };

            Assert.Equal(1, sqid.ToLong());
            Assert.Equal(1, (long)sqid);
            Assert.Equal(1, model.Value);
        }

        [Fact]
        public void ShouldBeCastNullableLong()
        {
            var model = new NullableLongModel { Value = null };
            var sqid = model.Value.ToSqid();

            Assert.Null(sqid);
        }

        [Fact]
        public void ShouldBeCastToString()
        {
            const int id = 1;
            var sqid = Sqid.FromInt(id);
            var model = new StringModel { Value = (string)sqid };

            Assert.Equal("0x4OloBIN3OGzel", sqid.ToString());
            Assert.Equal("0x4OloBIN3OGzel", (string)sqid);
            Assert.Equal("0x4OloBIN3OGzel", model.Value);
        }

        [Fact]
        public void ShouldBeCastNullableString()
        {
            var model = new StringModel { Value = null };
            var sqid = model.Value.ToSqid();

            Assert.Null(sqid);
        }

        internal class IntModel
        {
            public int Value { get; set; }
        }

        internal class NullableIntModel
        {
            public int? Value { get; set; }
        }

        internal class LongModel
        {
            public long Value { get; set; }
        }

        internal class NullableLongModel
        {
            public long? Value { get; set; }
        }

        internal class StringModel
        {
            public string Value { get; set; }
        }
    }
}
