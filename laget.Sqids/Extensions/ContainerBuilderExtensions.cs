using Autofac;
using laget.Sqids.Exceptions;
using laget.Sqids.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace laget.Sqids.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterSqids(this ContainerBuilder builder, string sectionKey = "Sqids")
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.RegisterBuildCallback(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var options = configuration.GetSection(sectionKey).Get<SqidOptions>();
                if (options == null)
                    throw new SqidsMissingConfigurationException();

                Sqid.SetFactory(new SqidFactory(options));
            });
        }

        public static void RegisterSqids(this ContainerBuilder builder, HashSet<string> blockList, string sectionKey = "Sqids")
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.RegisterBuildCallback(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var options = configuration.GetSection(sectionKey).Get<SqidOptions>();
                if (options == null)
                    throw new SqidsMissingConfigurationException();

                Sqid.SetFactory(new SqidFactory(options, blockList));
            });
        }
    }
}