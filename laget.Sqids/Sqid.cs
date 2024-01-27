using laget.Sqids.Exceptions;
using laget.Sqids.Utilities;

namespace laget.Sqids
{
    [Newtonsoft.Json.JsonConverter(typeof(Serialization.Newtonsoft.Json.SqidConverter))]
    public class Sqid
    {
        #region SqidsFactory
        private static ISqidFactory _sqidFactory;
        public static void SetFactory(ISqidFactory factory) => _sqidFactory = factory;

        private static ISqidFactory SqidFactory
        {
            get
            {
                if (_sqidFactory == null)
                    throw new SqidsNotRegisteredException();
                return _sqidFactory;
            }
        }
        #endregion

        public string Hash { get; }

        private Sqid(string hash)
        {
            Hash = hash;
        }

        public long ToLong() => (long)SqidFactory.GetId(Hash);
        public int ToInt() => (int)SqidFactory.GetId(Hash);
        public static Sqid FromLong(long id) => new Sqid(SqidFactory.GetHash((int)id));
        public static Sqid FromLong(long id, string alphabet) => new Sqid(SqidFactory.GetHash((int)id, alphabet));
        public static Sqid FromInt(int id) => new Sqid(SqidFactory.GetHash(id));
        public static Sqid FromInt(int id, string alphabet) => new Sqid(SqidFactory.GetHash(id, alphabet));
        public static Sqid FromString(string hash) => new Sqid(hash);


        #region Overrides & Operators
        public static explicit operator long(Sqid sqid)
        {
            return sqid.ToLong();
        }

        public static explicit operator int(Sqid sqid)
        {
            return sqid.ToInt();
        }

        public static explicit operator string(Sqid sqid)
        {
            return sqid.ToString();
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
