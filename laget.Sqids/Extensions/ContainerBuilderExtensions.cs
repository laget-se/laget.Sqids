using Autofac;
using laget.Sqids.Exceptions;
using laget.Sqids.Utilities;
using Microsoft.Extensions.Configuration;
using System;

namespace laget.Sqids.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterSqids(this ContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.RegisterBuildCallback(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var options = configuration.GetSection("Sqids").Get<SqidOptions>();
                if (options == null)
                    throw new SqidsMissingConfigurationException();

                Sqid.SetFactory(new SqidFactory(options));
            });
        }
    }
}