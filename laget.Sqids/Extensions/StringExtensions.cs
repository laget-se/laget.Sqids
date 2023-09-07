namespace laget.Sqids.Extensions
{
    public static class StringExtensions
    {
        public static Sqid ToSqid(this string @string) => !string.IsNullOrWhiteSpace(@string) ? Sqid.FromString(@string) : null;
    }
}
