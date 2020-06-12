﻿using DickinsonBros.Encryption.JWT.Abstractions;
using DickinsonBros.Encryption.JWT.Extensions;
using DickinsonBros.Encryption.JWT.Models;
using DickinsonBros.Encryption.JWT.RollerCoaster.Acccount.API.Infrastructure.JWT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DickinsonBros.Encryption.JWT.Tests.Extensions
{
    [TestClass]
    public class IServiceCollectionExtensionsTests
    {
        public class TestJWTServiceOptions : JWTServiceOptions
        {
        }
        [TestMethod]
        public void AddSQLService_Should_Succeed()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddJWTService<TestJWTServiceOptions>();

            // Assert

            Assert.IsTrue(serviceCollection.Any(serviceDefinition => serviceDefinition.ServiceType == typeof(IJWTService<>) &&
                                           serviceDefinition.ImplementationType == typeof(JWTService<>) &&
                                           serviceDefinition.Lifetime == ServiceLifetime.Singleton));
        }
    }
}
