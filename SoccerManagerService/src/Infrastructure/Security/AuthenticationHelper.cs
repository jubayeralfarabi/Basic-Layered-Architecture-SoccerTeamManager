namespace Soccer.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Framework.Core;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class AuthenticationHelper
    {
        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthentication(authenticationOptions =>
            {
                authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = "*",
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuers = new List<string>() { "self.com"},
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Thisisasecretkey")),
                    ClockSkew = TimeSpan.Zero,
                    LifetimeValidator = GetLifeTimeValidator(),
                };

                var jwtSecurityTokenHandler = options.SecurityTokenValidators.First() as JwtSecurityTokenHandler;

                jwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = MessageReceived,
                    OnTokenValidated = TokenValidated,
                };
            });
            return serviceCollection;
        }

        private static LifetimeValidator GetLifeTimeValidator()
        {
            LifetimeValidator lifetimeValidator = (
                DateTime? notBefore,
                DateTime? expires,
                SecurityToken securityToken,
                TokenValidationParameters validationParameters) =>
            {
                TokenValidationParameters clonedParameters = validationParameters.Clone();
                clonedParameters.LifetimeValidator = null;
                try
                {
                    Validators.ValidateLifetime(
                        notBefore,
                        expires,
                        securityToken,
                        clonedParameters);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            };

            return lifetimeValidator;
        }

        private static Task TokenValidated(TokenValidatedContext context)
        {
            ClaimsIdentity claimsIdentity = context.Principal.Identity as ClaimsIdentity;

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var claims = claimsPrincipal.Claims.ToList();

            ISecurityContext existingUserContext = context.HttpContext.RequestServices.GetRequiredService<ISecurityContext>();

            var userId = claims.FirstOrDefault(c => c.Type == "userId").Value;
            var firstName = claims.FirstOrDefault(c => c.Type == "firstName").Value;
            var lastName = claims.FirstOrDefault(c => c.Type == "lastName").Value;
            var email = claims.FirstOrDefault(c => c.Type == "email").Value;

            existingUserContext.UserContext = new UserContext(int.Parse(userId),firstName,lastName,email);

            return Task.CompletedTask;
        }

        private static Task MessageReceived(MessageReceivedContext context)
        {
            var text = context.Request.Headers["Authorization"].ToString();
            context.Token = string.IsNullOrEmpty(text) ? string.Empty : text.Substring(7);
            return Task.CompletedTask;
        }
    }
}
