//using Azure.Core;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace CommentService
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtAuthenticationMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers[HeaderNames.Authorization].Count != 0 ? httpContext.Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1] : "";

            if (token == "") 
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Provide Token.");
                return;
            }

            var myTenant = _config["AzureAd:TenantId"]; 
            var myAudience = _config["AzureAd:ClientId"];

            var myIssuer = String.Format(CultureInfo.InvariantCulture, "https://sts.windows.net/{0}/", myTenant);
            var mySecret = _config["AzureAd:ClientSecret"]; 
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var stsDiscoveryEndpoint = String.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/{0}/v2.0/.well-known/openid-configuration", myTenant);
            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
            var config = await configManager.GetConfigurationAsync();

            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidAudience = myAudience,
                ValidIssuer = myIssuer,
                IssuerSigningKeys = config.SigningKeys,
                ValidateLifetime = false,

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true
            };

            try
            {
                // Validate the token and set the user identity
                var handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(token, validationParameters, out var validatedToken);

                // Set the user identity in the context
                httpContext.User = new ClaimsPrincipal(user);
            }
            catch (Exception)
            {
                // Return unauthorized if the token is invalid
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Not Authenticated");
                return;
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthenticationMiddleware>();
        }
    }
}
