using Aloji.AspNetCore.HATEOAS.Options;
using Aloji.AspNetCore.HATEOAS.Services.Contracts;
using Aloji.AspNetCore.HATEOAS.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class HATEOASServiceCollectionExtensions
    {
        public static IServiceCollection AddHATEOAS(this IServiceCollection services, Action<ResourceOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<ResourceOptions>(configureOptions);
            services.AddSingleton<IResourceService, ResourceService>();

            return services;
        }
    }
}
