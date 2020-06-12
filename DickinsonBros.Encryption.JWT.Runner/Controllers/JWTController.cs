using System.Security.Claims;
using System.Threading.Tasks;
using DickinsonBros.Encryption.JWT.Abstractions;
using DickinsonBros.Encryption.JWT.Runner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DickinsonBros.Encryption.JWT.Runner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JWTController : ControllerBase
    {

        internal readonly IJWTService<WebsiteJWTServiceOptions> _websiteJWTService;
        internal readonly IJWTService<AdministrationWebSiteJWTServiceOptions> _administrationJWTService;
        public JWTController
        (
            IJWTService<WebsiteJWTServiceOptions> websiteJWTService,
            IJWTService<AdministrationWebSiteJWTServiceOptions> administrationJWTService
        )
        {
            _websiteJWTService = websiteJWTService;
            _administrationJWTService = administrationJWTService;
        }

        [AllowAnonymous]
        [HttpGet("LoginWebsite")]
        public async Task<ActionResult> LoginWebsiteAsync()
        {
            await Task.CompletedTask;

            var accountId = "DemoUser";

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, accountId),
                new Claim(ClaimTypes.Role, "User")
            };

            var generateTokensDescriptor = _websiteJWTService.GenerateTokens(claims);

            if(generateTokensDescriptor.Authorized == false)
            {
                return Unauthorized();
            }

            return Ok(generateTokensDescriptor.Tokens);
        }

        //You can add a list of allowed roles Or use [Authorize] for any authenticated role
        [Authorize(Roles = "User")]
        [HttpPost("CheckAuthWebsite")]
        public async Task<ActionResult> CheckAuthWebsiteAsync()
        {
            await Task.CompletedTask;

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("RefreshTokensWebsite")]
        public async Task<ActionResult> RefreshTokensWebsiteAsync(RefreshTokenRequest request)
        {
            await Task.CompletedTask;

            var generateTokensDescriptor = _websiteJWTService.GenerateTokens(request.AccessToken, request.RefreshToken);

            if (generateTokensDescriptor.Authorized == false)
            {
                return Unauthorized();
            }

            return Ok(generateTokensDescriptor.Tokens);
        }

        [AllowAnonymous]
        [HttpGet("LoginAdministrationWebsite")]
        public async Task<ActionResult> LoginAdministrationWebsiteAsync()
        {
            await Task.CompletedTask;

            var accountId = "DemoUser";

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, accountId),
                new Claim(ClaimTypes.Role, "User")
            };

            var generateTokensDescriptor = _administrationJWTService.GenerateTokens(claims);

            if (generateTokensDescriptor.Authorized == false)
            {
                return Unauthorized();
            }

            return Ok(generateTokensDescriptor.Tokens);
        }

        [AllowAnonymous]
        [HttpPost("RefreshTokensAdministrationWebsite")]
        public async Task<ActionResult> RefreshTokensAdministrationWebsiteAsync(RefreshTokenRequest request)
        {
            await Task.CompletedTask;

            var generateTokensDescriptor = _administrationJWTService.GenerateTokens(request.AccessToken, request.RefreshToken);

            if (generateTokensDescriptor.Authorized == false)
            {
                return Unauthorized();
            }

            return Ok(generateTokensDescriptor.Tokens);
        }
    }
}
