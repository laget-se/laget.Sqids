namespace laget.Sqids.Extensions
{
    public static class IntExtensions
    {
        public static Sqid ToSqid(this int id) => Sqid.FromInt(id);

        public static Sqid ToSqid(this int? id) => id.HasValue ? Sqid.FromInt(id.Value) : null;
    }
}