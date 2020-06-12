# DickinsonBros.Encryption.JWT

<a href="https://www.nuget.org/packages/DickinsonBros.Encryption.JWT/">
    <img src="https://img.shields.io/nuget/v/DickinsonBros.Encryption.JWT">
</a>

JWT Service

Features
* Read Tokens
* Create Tokens With Cliams
* Generate AccessToken and RefreshTokens
* Configured With Certificates

<a href="https://dev.azure.com/marksamdickinson/DickinsonBros/_build?definitionScope=%5CDickinsonBros.Encryption.JWT">Builds</a>

<h2>Example Usage - Generates Access Token And Refresh Token</h2>

```C#

...
var claims = new Claim[]
{
    new Claim(ClaimTypes.NameIdentifier, accountId),
    new Claim(ClaimTypes.Role, "User")
};

var generateTokensDescriptor = _websiteJWTService.GenerateTokens(claims);

if (generateTokensDescriptor.Authorized == false)
{
    return Unauthorized();
}

return Ok(generateTokensDescriptor.Tokens);

...

var generateTokensDescriptor = _websiteJWTService.GenerateTokens(request.AccessToken, request.RefreshToken);

if (generateTokensDescriptor.Authorized == false)
{
    return Unauthorized();
}

return Ok(generateTokensDescriptor.Tokens);

```
    
    {
        "accessToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjcyRTU0M0JDRjU0RTE0M0VFMzhGM0U3OERFQzNGMTQ1Q0E3ODI1QjIiLCJ0eXAiOiJKV1QifQ.eyJuYW1laWQiOiJEZW1vVXNlciIsInJvbGUiOiJVc2VyIiwibmJmIjoxNTkxOTgxNjg0LCJleHAiOjE1OTE5ODM0ODQsImlhdCI6MTU5MTk4MTY4NCwiaXNzIjoiQWNjb3VudCJ9.axwb-ruMwg2deYwN5ORYbiswCmrIQsXCWUzsxCWea_dSTUVaLtdgUDrbqILXgyPZTMvl1XTkYriHik75s5QErriw_TiL0a2C8I56PNyGaCSE0JeBPh3sv5VYrtEcXyATT7npvCL61GBsPpanOL51FgjNHGbGNUhLDKdrHuspitjYQn2N_r_zpJwlObcAC8k9nAZlWCXyODyontgO_wUBJhDU_1HtT9wjEAmY844T25FwihAIUbszK1pLOhNBEU7yw1H3RHGAJlvLI5HnMwZukEgbvP4u8_GT228l8XHBtsWJRJWazEtR4W8JH_FC9KkoSpDi4lroAoZo6zetJ07P_w",
        "accessTokenExpiresIn": "2020-06-12T17:38:04.6767643Z",
        "refreshToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjcyRTU0M0JDRjU0RTE0M0VFMzhGM0U3OERFQzNGMTQ1Q0E3ODI1QjIiLCJ0eXAiOiJKV1QifQ.eyJuYW1laWQiOiJEZW1vVXNlciIsInJvbGUiOiJVc2VyIiwibmJmIjoxNTkxOTgxNjg0LCJleHAiOjE1OTE5ODcwODQsImlhdCI6MTU5MTk4MTY4NCwiaXNzIjoiQWNjb3VudCJ9.R0sJpt5zG624S41L2neAMqPkztzD703zwcqNgoGGZ5O8PtxNdROvr4hG6S_YqevSlZfuhu1M0tawCqm5UpGwEfVFK8jTNT7EkqqmNF28ggXte9j5uUAkk49HTv8J6m6ZihwcuQXm2kswom718YKajEDT4yqEdP0QfDB76IDk_6iFuKXUhGcXJCm2_H06_tWuorcNJfEfuO5zEAm5WLaHTmI4-3LAQ6amuowRsSxs9h7TDKPVWU-iP0Fhyig5YYmmj-MhFFJTifEQgl2AySrZ1adJdBeNQ3HtX3I05VEAtYPgZNSQEnWv2Y2_MkzMqsVrGlnmC1jQXdmjB4nNcdh-sQ",
        "refreshTokenExpiresIn": "2020-06-12T18:38:04.6767739Z",
        "tokenType": "Bearer"
    }
    
 ...
 
    {
        "accessToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjcyRTU0M0JDRjU0RTE0M0VFMzhGM0U3OERFQzNGMTQ1Q0E3ODI1QjIiLCJ0eXAiOiJKV1QifQ.eyJuYW1laWQiOiJEZW1vVXNlciIsInJvbGUiOiJVc2VyIiwibmJmIjoxNTkxOTk0MTQyLCJleHAiOjE1OTE5OTU5NDIsImlhdCI6MTU5MTk5NDE0MiwiaXNzIjoiQWNjb3VudCIsImF1ZCI6IldlYnNpdGUifQ.Y2zUMMZaNZdMYZNuwespHSo-F_U21Aj75zKnttWJbI2Hy1mJEcFBCRBImh5dYp_AhSHoQ2H_oHKB7cdzXCTZYtS8uO4QolCF9ijsMPP_avRpV--UDHg9su8sOiynuX0gRGVOgbNSV2GEac8f0VRet_KPnKGs6CAuphc-Nh4KsFPvJcOtjI45J2IlBesZK4mEKvWoP4ypus99PbnLzZvk1bBdqrcm4Q1gco7t_liXPUOfjk7Q9WScfB5U6bzseWTIlDlrlXW5_9nqMfj_uHV3oZo9qHToTA_FVWbBMfGnmEF4dlV6OwAmPxMRUQIL6aSzr20EoPG2gsxAC2R-mvyzjg",
        "accessTokenExpiresIn": "2020-06-12T21:05:42.4701222Z",
        "refreshToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjcyRTU0M0JDRjU0RTE0M0VFMzhGM0U3OERFQzNGMTQ1Q0E3ODI1QjIiLCJ0eXAiOiJKV1QifQ.eyJuYW1laWQiOiJEZW1vVXNlciIsInJvbGUiOiJVc2VyIiwibmJmIjoxNTkxOTk0MTQyLCJleHAiOjE1OTE5OTk1NDIsImlhdCI6MTU5MTk5NDE0MiwiaXNzIjoiQWNjb3VudCIsImF1ZCI6IldlYnNpdGUifQ.YBmTeM_gUePnl-GNPzD_Jili5AuMpYNp8zHUBu0D_uOPGUaU8GAYnsAwRkOtkS5WzRHROMp4Scb3olt9iGz-9fPk3eQ_hMlA-Sh3P9UmUro5pa16wyg4KlL2O7J76U62-jjX3OGjuLVC7mJrhNyGX-wJojcDPGDzltsvKa0cLxf6CRipzBnp5m4ElfNB76dWcKbNTv-8tk9VAGT47i65P9fgIXoGY3vk3OfdiR49U1QxgO476kDD8oWxbQI2AyEgaGi6jaNxZ4Jng0FFdeKjmNEQv8QIQCVj4XkHiD25HWFoCOapVAs20CoCecUSjUBotaOg4nlXaV8Ff0AinuQ1UQ",
        "refreshTokenExpiresIn": "2020-06-12T22:05:42.4701296Z",
        "tokenType": "Bearer"
    }

Example Runner Included in folder "DickinsonBros.Encryption.JWT.Runner"

<h2>Setup</h2>

<h3>Install a windows certificate</h3>

Below will show you have to install a cert with the private key and without.
You can only decrypt if you have the cert with the private key.

<h4>Create powershell scripts</h3>

<h5>CreateCert.ps1</h5>
    
    $cert = New-SelfSignedCertificate -Type DocumentEncryptionCert -Subject "CN=DemoConfig" -KeyExportPolicy Exportable -KeySpec KeyExchange

    Export-Certificate -Cert $cert -FilePath ".\DemoConfig.cer"

    $mypwd = ConvertTo-SecureString -String "5cce8bf4-0cd7-4c22-9968-8793b9938db1" -Force -AsPlainText

    Export-PfxCertificate -Cert $cert -FilePath ".\DemoConfig.pfx" -Password $mypwd

    $cert

<h5>ImportCert.ps1</h5>

    Import-Certificate -FilePath ".\DemoConfig.cer" -CertStoreLocation Cert:\LocalMachine\My
    
<h5>ImportCertWithPrivateKey.ps1</h5>

    $mypwd = ConvertTo-SecureString -String "5cce8bf4-0cd7-4c22-9968-8793b9938db1" -Force -AsPlainText

    Import-PfxCertificate -FilePath ".\DemoConfig.pfx" -CertStoreLocation Cert:\LocalMachine\My -Password $mypwd
    
<h4>Run CreateCert.ps1</h3>

Running this will generate two files

    //Certificate
    DemoConfig.cer
    
    //Certificate With Private Key
    DemoConfig.pfx

<h4>Run importCert.ps1 OR ImportCertWithPrivateKey.ps1</h3>

This will install the Certificate and give you the ThumbPrint.

To verfiy certificate is installed (and where you can remove it)
* mmc.exe
* File -> Add/Remove Snap In
* Click Certificate and then Add
* Select computer account and press next
* Select local computer and press finsh
* Select ok to close the 
* Select the folder personal/Certificates
* Look for your your cert in the example above it would be "DemoConfig"

<h3>Add Nuget References</h3>

    https://www.nuget.org/packages/DickinsonBros.DateTime
    https://www.nuget.org/packages/DickinsonBros.Logger
    https://www.nuget.org/packages/DickinsonBros.Redactor
    https://www.nuget.org/packages/DickinsonBros.Encryption.Certificate

<h3>Create class with base of JWTServiceOptions</h3>

```c#
    public class WebsiteJWTServiceOptions : JWTServiceOptions
    {
    }
```
<h3>Create Instance</h3>

```c#
var runnerCertificateEncryptionServiceOptions = new RunnerCertificateEncryptionServiceOptions
{
    ThumbPrint = "...",
    StoreLocation = "LocalMachine"
};
var options = Options.Create(certificateEncryptionOptions);
var certificateEncryptionService = new CertificateEncryptionService<RunnerCertificateEncryptionServiceOptions>(options);

```

<h3>Create Instance (With Dependency Injection)</h3>

<h4>Add appsettings.json File With Contents (Example Options)</h4>

 ```json  
{
  "WebsiteJWTServiceOptions": {
    "Issuer": "",
    "Audience": "",
    "AccessExpiresAfterMinutes": 30,
    "AccessThumbPrint": "",
    "AccessStoreLocation": "",
    "RefershExpiresAfterMinutes": 90,
    "RefershThumbPrint": "",
    "RefershStoreLocation": ""
  }
}

 ```    
<h4>Code</h4>

```c#
{
    //Add Logging Service
    services.AddLoggingService();

    //Add Redactor Service
    services.AddRedactorService();
    services.Configure<RedactorServiceOptions>(Configuration.GetSection(nameof(RedactorServiceOptions)));

    //Add DateTime Service
    services.AddDateTimeService();

    //Add JWTService Website
    services.AddJWTService<WebsiteJWTServiceOptions>();
    services.Configure<JWTServiceOptions<WebsiteJWTServiceOptions>>(Configuration.GetSection(nameof(WebsiteJWTServiceOptions)));

}
```
