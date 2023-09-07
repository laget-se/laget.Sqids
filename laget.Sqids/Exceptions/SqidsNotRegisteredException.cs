using System;

namespace laget.Sqids.Exceptions
{
    public class SqidsNotRegisteredException : Exception
    {
        public SqidsNotRegisteredException()
            : base("Sqids has not been properly registered\nUse the ContainerBuilderExtension RegisterSquids in your Program.cs in order to user Sqids")
        {
        }
    }
}