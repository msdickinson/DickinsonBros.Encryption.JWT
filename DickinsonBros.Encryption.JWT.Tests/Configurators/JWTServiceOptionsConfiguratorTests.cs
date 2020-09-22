using DickinsonBros.Encryption.JWT.Configurators;
using DickinsonBros.Encryption.JWT.Models;
using DickinsonBros.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DickinsonBros.Encryption.JWT.Tests.Configurators
{
    [TestClass]
    public class CertificateEncryptionServiceOptionsConfiguratorTests : BaseTest
    {
        public class TestJWTServiceOptions : JWTServiceOptions
        {
        }

        [TestMethod]
        public async Task Configure_Runs_OptionsReturned()
        {
            var jwtServiceOptions = new JWTServiceOptions<TestJWTServiceOptions>
            {
                AccessExpiresAfterMinutes = 1,
                AccessStoreLocation = "SampleAccessStoreLocation",
                AccessThumbPrint = "SampleAccessThumbPrint",
                Audience = "SampleAudience",
                RefershExpiresAfterMinutes = 2,
                Issuer = "SampleIssuer",
                RefershStoreLocation = "SampleRefershStoreLocation",
                RefershThumbPrint = "SampleRefershThumbPrint"
            };
            var configurationRoot = BuildConfigurationRoot(jwtServiceOptions);

            await RunDependencyInjectedTestAsync
            (
                async (serviceProvider) =>
                {
                    //Setup

                    //Act
                    var options = serviceProvider.GetRequiredService<IOptions<JWTServiceOptions<TestJWTServiceOptions>>>().Value;

                    //Assert
                    Assert.IsNotNull(options);

                    Assert.AreEqual(jwtServiceOptions.AccessExpiresAfterMinutes, options.AccessExpiresAfterMinutes);
                    Assert.AreEqual(jwtServiceOptions.AccessStoreLocation, options.AccessStoreLocation);
                    Assert.AreEqual(jwtServiceOptions.AccessThumbPrint, options.AccessThumbPrint);
                    Assert.AreEqual(jwtServiceOptions.Audience, options.Audience);
                    Assert.AreEqual(jwtServiceOptions.RefershExpiresAfterMinutes, options.RefershExpiresAfterMinutes);
                    Assert.AreEqual(jwtServiceOptions.Issuer, options.Issuer);
                    Assert.AreEqual(jwtServiceOptions.RefershStoreLocation, options.RefershStoreLocation);
                    Assert.AreEqual(jwtServiceOptions.RefershStoreLocation, options.RefershStoreLocation);
                    Assert.AreEqual(jwtServiceOptions.RefershThumbPrint, options.RefershThumbPrint);

                    await Task.CompletedTask.ConfigureAwait(false);

                },
                serviceCollection => ConfigureServices(serviceCollection, configurationRoot)
            );
        }

        #region Helpers

        private IServiceCollection ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<IConfigureOptions<JWTServiceOptions<TestJWTServiceOptions>>, JWTServiceOptionsConfigurator<TestJWTServiceOptions>>();

            return serviceCollection;
        }

        #endregion
    }
}
