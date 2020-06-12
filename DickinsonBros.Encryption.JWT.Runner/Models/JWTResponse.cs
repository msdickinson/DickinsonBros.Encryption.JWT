using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DickinsonBros.Encryption.JWT.Runner.Models
{
    [ExcludeFromCodeCoverage]
    public class JWTResponse
    {
        public string AccessToken { get; set; }
        public int AccessTokenExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
