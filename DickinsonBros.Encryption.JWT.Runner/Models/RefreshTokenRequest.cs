using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DickinsonBros.Encryption.JWT.Runner.Models
{
    [ExcludeFromCodeCoverage]
    public class RefreshTokenRequest
    {
        [Required]
        [MinLength(1)]
        public string AccessToken { get; set; }

        [Required]
        [MinLength(1)]
        public string RefreshToken { get; set; }
    }
}
