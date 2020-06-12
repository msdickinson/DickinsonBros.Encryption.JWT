using DickinsonBros.Encryption.JWT.Abstractions;
using DickinsonBros.Encryption.JWT.RollerCoaster.Acccount.API.Infrastructure.JWT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DickinsonBros.Encryption.JWT.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJWTService<T>(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton(typeof(IJWTService<>), typeof(JWTService<>));
            return serviceCollection;
        }
    }
}
