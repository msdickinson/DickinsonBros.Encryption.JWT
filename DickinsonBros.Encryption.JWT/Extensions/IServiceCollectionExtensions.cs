using DickinsonBros.Encryption.JWT.Abstractions;
using DickinsonBros.Encryption.JWT.Configurators;
using DickinsonBros.Encryption.JWT.Models;
using DickinsonBros.Encryption.JWT.RollerCoaster.Acccount.API.Infrastructure.JWT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace DickinsonBros.Encryption.JWT.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJWTService<T>(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton(typeof(IJWTService<T>), typeof(JWTService<T>));
            serviceCollection.TryAddSingleton(typeof(IConfigureOptions<JWTServiceOptions<T>>), typeof(JWTServiceOptionsConfigurator<T>));
            return serviceCollection;
        }
    }
}
