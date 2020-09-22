using DickinsonBros.Encryption.JWT.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DickinsonBros.Encryption.JWT.Configurators
{
    public class JWTServiceOptionsConfigurator<T> : IConfigureOptions<JWTServiceOptions<T>>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public JWTServiceOptionsConfigurator(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        void IConfigureOptions<JWTServiceOptions<T>>.Configure(JWTServiceOptions<T> options)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var provider = scope.ServiceProvider;
            var configuration = provider.GetRequiredService<IConfiguration>();
            var path = $"{nameof(JWTServiceOptions<T>)}:{typeof(T).Name}";
            var accountAPITestsOptions = configuration.GetSection(path).Get<JWTServiceOptions<T>>();
            configuration.Bind(path, options);
        }
    }
}
