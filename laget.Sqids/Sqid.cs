using laget.Sqids.Exceptions;
using laget.Sqids.Utilities;

namespace laget.Sqids
{
    [Newtonsoft.Json.JsonConverter(typeof(Serialization.Newtonsoft.Json.SqidConverter))]
    public class Sqid
    {
        #region SquidsFactory
        private static ISquidFactory _squidFactory;
        public static void SetFactory(ISquidFactory factory) => _squidFactory = factory;

        private static ISquidFactory SquidFactory
        {
            get
            {
                if (_squidFactory == null)
                    throw new SqidsNotRegisteredException();
                return _squidFactory;
            }
        }
        #endregion

        public string Hash { get; }

        private Sqid(string hash)
        {
            Hash = hash;
        }

        public long ToLong() => (long)SquidFactory.GetId(Hash);
        public int ToInt() => (int)SquidFactory.GetId(Hash);
        public static Sqid FromLong(long id) => new Sqid(SquidFactory.GetHash((int)id));
        public static Sqid FromInt(int id) => new Sqid(SquidFactory.GetHash(id));
        public static Sqid FromString(string hash) => new Sqid(hash);


        #region Overrides & Operators
        public static explicit operator long(Sqid hashId)
        {
            return hashId.ToLong();
        }

        public static explicit operator int(Sqid hashId)
        {
            return hashId.ToInt();
        }

        public static explicit operator string(Sqid hashId)
        {
            return hashId.ToString();
        }

        public static explicit operator Sqid(string hash)
        {
            return new Sqid(hash);
        }

        public static explicit operator Sqid(long value)
        {
            return Sqid.FromLong(value);
        }

        public static explicit operator Sqid(long? value)
        {
            return value.HasValue ? Sqid.FromLong(value.Value) : null;
        }

        public static explicit operator Sqid(int value)
        {
            return Sqid.FromInt(value);
        }

        public static explicit operator Sqid(int? value)
        {
            return value.HasValue ? Sqid.FromLong(value.Value) : null;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Sqid))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            var rhs = (Sqid)obj;

            return Hash.Equals(rhs.Hash);
        }

        public override int GetHashCode() => Hash.GetHashCode();

        public override string ToString() => Hash;

        public static bool operator ==(Sqid lhs, Sqid rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return ReferenceEquals(rhs, null);
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Sqid lhs, Sqid rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
