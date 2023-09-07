using System;

namespace laget.Sqids.Exceptions
{
    public class SqidsInvalidVersionException : Exception
    {
        public SqidsInvalidVersionException(string version)
            : base($"No encoder available for sqid of version {version}")
        {
        }
    }
}