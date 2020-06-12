using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using DickinsonBros.Encryption.JWT.Models;
using System.Linq;
using DickinsonBros.DateTime.Abstractions;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using DickinsonBros.Encryption.JWT.Abstractions;
using DickinsonBros.Encryption.JWT.Abstractions.Models;

namespace DickinsonBros.Encryption.JWT
{
    namespace RollerCoaster.Acccount.API.Infrastructure.JWT
    {
        [ExcludeFromCodeCoverage]
        public class JWTService<T> : IJWTService<T>
        {
            internal const string TOKEN_TYPE = "Bearer";
            internal const string LOCAL_MACHINE = "LocalMachine";

            internal readonly string _issuer;
            internal readonly string _audience;

            internal readonly int _accessExpiresAfterMinutes;
            internal readonly string _accessThumbPrint;
            internal readonly StoreLocation _accessStoreLocation = StoreLocation.LocalMachine;

            internal readonly int _refershExpiresAfterMinutes;
            internal readonly string _refershThumbPrint;
            internal readonly StoreLocation _refershStoreLocation = StoreLocation.LocalMachine;

            internal readonly IDateTimeService _dateTimeService;
            internal readonly ILogger<JWTService<T>> _logger;

            public JWTService(IOptions<JWTServiceOptions<T>> jwtServiceOptions, IDateTimeService dateTimeService, ILogger<JWTService<T>> logger)
            {
                _issuer = jwtServiceOptions.Value.Issuer;
                _audience = jwtServiceOptions.Value.Audience;

                _accessExpiresAfterMinutes = jwtServiceOptions.Value.AccessExpiresAfterMinutes;
                _accessThumbPrint = jwtServiceOptions.Value.AccessThumbPrint;
                _accessStoreLocation = jwtServiceOptions.Value.AccessStoreLocation == LOCAL_MACHINE
                                ? StoreLocation.LocalMachine : StoreLocation.CurrentUser;

                _refershExpiresAfterMinutes = jwtServiceOptions.Value.RefershExpiresAfterMinutes;
                _refershThumbPrint = jwtServiceOptions.Value.RefershThumbPrint;
                _refershStoreLocation = jwtServiceOptions.Value.RefershStoreLocation == LOCAL_MACHINE
                                ? StoreLocation.LocalMachine : StoreLocation.CurrentUser;

                _dateTimeService = dateTimeService;

                _logger = logger;
            }

            public GenerateTokensDescriptor GenerateTokens(string accessToken, string refershToken)
            {
                var accessTokenClaims = GetPrincipal(accessToken, false, TokenType.Access);
                var refreshTokenClaims = GetPrincipal(refershToken, true, TokenType.Refresh);


                if (accessTokenClaims == null ||
                     refreshTokenClaims == null ||
                     !accessTokenClaims.Identity.IsAuthenticated ||
                     !refreshTokenClaims.Identity.IsAuthenticated ||
                     accessTokenClaims.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value !=
                     refreshTokenClaims.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value)
                {
                    return new GenerateTokensDescriptor()
                    {
                        Authorized = false
                    };
                }

                return GenerateTokens(accessTokenClaims.Claims);
            }

            public GenerateTokensDescriptor GenerateTokens(IEnumerable<Claim> claims)
            {
                var accessTokenExpiresInDateTime = _dateTimeService.GetDateTimeUTC().AddMinutes(_accessExpiresAfterMinutes);
                var refreshTokenExpiresInDateTime = _dateTimeService.GetDateTimeUTC().AddMinutes(_refershExpiresAfterMinutes);

                return new GenerateTokensDescriptor()
                {
                    Tokens = new Tokens
                    {
                        AccessToken = GenerateJWT(claims, accessTokenExpiresInDateTime, TokenType.Access),
                        AccessTokenExpiresIn = accessTokenExpiresInDateTime,
                        RefreshToken = GenerateJWT(claims, refreshTokenExpiresInDateTime, TokenType.Refresh),
                        RefreshTokenExpiresIn = refreshTokenExpiresInDateTime,
                        TokenType = TOKEN_TYPE
                    },
                    Authorized = true
                };
            }

            public string GenerateJWT(IEnumerable<Claim> claims, System.DateTime expiresDateTime, TokenType tokenType)
            {
                var thumbPrint = TokenType.Access == tokenType ? _accessThumbPrint : _refershThumbPrint;
                var storeLocation = TokenType.Access == tokenType ? _accessStoreLocation : _refershStoreLocation;

                var X509Certificate2 = GetX509Certificate2(thumbPrint, storeLocation);
                var X509SecurityKey = new X509SecurityKey(X509Certificate2);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    NotBefore = _dateTimeService.GetDateTimeUTC(),
                    IssuedAt = _dateTimeService.GetDateTimeUTC(),
                    Issuer = _issuer,
                    Audience = _audience,
                    Subject = new ClaimsIdentity(claims),
                    Expires = expiresDateTime,
                    SigningCredentials = new SigningCredentials(X509SecurityKey, SecurityAlgorithms.RsaSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            public X509Certificate2 GetX509Certificate2(string thumbPrint, StoreLocation storeLocation)
            {
                try
                {
                    using var x509Store = new X509Store(StoreName.My, storeLocation);
                    x509Store.Open(OpenFlags.ReadOnly);
                    var certificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);
                    if (certificateCollection.Count > 0)
                    {
                        return certificateCollection[0];
                    }
                    else
                    {
                        throw new Exception($"No certificate found for Thumbprint {thumbPrint} in location {storeLocation}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unhandled exception. Thumbprint: {thumbPrint}, Location: {storeLocation}", ex);
                }
            }

            public ClaimsPrincipal GetPrincipal(string token, bool vaildateLifetime, TokenType tokenType)
            {
                var methodIdentifier = $"{nameof(JWTService<T>)}.{nameof(GetPrincipal)}";
                try
                {
                    var thumbPrint = TokenType.Access == tokenType ? _accessThumbPrint : _refershThumbPrint;
                    var storeLocation = TokenType.Access == tokenType ? _accessStoreLocation : _refershStoreLocation;

                    var X509Certificate2 = GetX509Certificate2(thumbPrint, storeLocation);
                    var X509SecurityKey = new X509SecurityKey(X509Certificate2);

                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                    if (jwtToken == null)
                        return null;

                    TokenValidationParameters parameters = new TokenValidationParameters()
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        IssuerSigningKey = X509SecurityKey,
                        ValidateLifetime = vaildateLifetime,
                        ValidAudience = _audience,
                        ValidIssuer = _issuer,
                    };
                    ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                          parameters, out SecurityToken securityToken);
                    return principal;
                }
                catch (Exception ex)
                {
                    _logger.LogError(methodIdentifier, ex);
                    return null;

                }
            }
        }
    }

}
