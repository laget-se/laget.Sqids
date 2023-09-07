using laget.Sqids.Exceptions;
using laget.Sqids.Tests.Abstractions;
using laget.Sqids.Tests.Models;
using Newtonsoft.Json;
using Xunit;

namespace laget.Sqids.Tests
{
    public class SquidTest : TestBase
    {
        [Fact]
        public void ThrowsErrorOnUnknownHashVersion()
        {
            var sqid = Sqid.FromString($"{AlphabetVersion2X}bogusHash");
            Assert.Throws<SqidsInvalidVersionException>(() =>
            {
                var id = sqid.ToLong();
            });
        }

        [Fact]
        public void ThrowsErrorIfNotRegistered()
        {
            Sqid.SetFactory(null);

            Assert.Throws<SqidsNotRegisteredException>(() => Sqid.FromLong(1));

            Sqid.SetFactory(SquidFactory);
        }

        [Fact]
        public void IsProperlySerializedWithNewtonsoft()
        {
            const long id = 1234;
            var model = new Model { Id = Sqid.FromLong(id) };

            var expectedJson = $"{{\"Id\":\"{model.Id.Hash}\"}}";
            var json = JsonConvert.SerializeObject(model);

            Assert.Equal(expectedJson, json);
        }

        [Fact]
        public void IsProperlyDeserializedWithNewtonsoft()
        {
            const long id = 1234;
            var model = new Model { Id = Sqid.FromLong(id) };
            var json = JsonConvert.SerializeObject(model);

            var deserializedDto = JsonConvert.DeserializeObject<Model>(json);

            Assert.Equal(model.Id, deserializedDto.Id);
        }

        [Fact]
        public void NumericIdIsProperlyExtractedFromHashWithNewtonsoft()
        {
            const long id = 1234;
            var model = new Model { Id = Sqid.FromLong(id) };
            var json = JsonConvert.SerializeObject(model);

            var deserializedDto = JsonConvert.DeserializeObject<Model>(json);
            var deserializedId = deserializedDto.Id.ToLong();

            Assert.Equal(id, deserializedId);
        }

        [Fact]
        public void NumericIdIntIsProperlyExtractedFromHashWithNewtonsoft()
        {
            const int id = 1234;
            var model = new Model { Id = Sqid.FromInt(id) };
            var json = JsonConvert.SerializeObject(model);

            var deserializedDto = JsonConvert.DeserializeObject<Model>(json);
            var deserializedId = deserializedDto.Id.ToInt();

            Assert.Equal(id, deserializedId);
        }

        [Fact]
        public void ShouldHandleExplicitOperator()
        {
            const string expected = "0xR4reL0zL3Xgq8";
            var actual = (Sqid)"0xR4reL0zL3Xgq8";

            Assert.Equal(expected, actual.Hash);
        }
    }
}