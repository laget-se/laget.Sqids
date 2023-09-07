using System;

namespace laget.Sqids.Exceptions
{
    public class SqidsMissingConfigurationException : Exception
    {
        public SqidsMissingConfigurationException()
            : base("Failed to load configuration for Sqids\n Make sure your config contains a section named 'Sqids' that contains all necessary configuration for using hashed ids")
        {
        }
    }
}