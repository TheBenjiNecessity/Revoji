using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi
{
	public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ILogger logger;

        public ResourceOwnerPasswordValidator(ILogger<ResourceOwnerPasswordValidator> logger)
        {
            this.logger = logger;
        }

        Task IResourceOwnerPasswordValidator.ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            logger.LogInformation("ValidateAsync");

            using (var dbctx = new RevojiDataContext())
            {
                if (context.UserName == null || context.Password == null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "missing_arguments_error");
                    return Task.FromResult<object>(null);
                }

                DBAppUser dbAppUser = dbctx.AppUsers.FirstOrDefault(au => au.Handle == context.UserName);
                if (dbAppUser == null || !dbAppUser.isPasswordCorrect(context.Password))
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid_handle_password_error");
                    return Task.FromResult<object>(null);
                }

                var claim = new Claim(ClaimTypes.Name, dbAppUser.Handle);
                var claims = new List<Claim>();
                claims.Add(claim);
                context.Result = new GrantValidationResult(dbAppUser.Handle, "access_token", claims);
                return Task.FromResult<object>(null);
            }
        }
    }
}