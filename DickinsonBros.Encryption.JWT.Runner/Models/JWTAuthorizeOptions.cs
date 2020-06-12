﻿namespace DickinsonBros.Encryption.JWT.Runner.Models
{
    public class JWTAuthorizeOptions
    {
        public string VaildIssuer { get; set; }
        public string VaildAudience { get; set; }
        public string StoreLocation { get; set; }
        public string ThumbPrint { get; set; }
    }
}
