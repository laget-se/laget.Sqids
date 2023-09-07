namespace laget.Sqids.Extensions
{
    public static class LongExtensions
    {
        public static Sqid ToSqid(this long id) => Sqid.FromLong(id);

        public static Sqid ToSqid(this long? id) => id.HasValue ? Sqid.FromLong(id.Value) : null;
    }
}