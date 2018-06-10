using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi
{
	public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
		Task IResourceOwnerPasswordValidator.ValidateAsync(ResourceOwnerPasswordValidationContext context)
		{
			using (var dbctx = new RevojiDataContext())
			{
				//TODO: check if username or password is null

				DBAppUser dbAppUser = dbctx.AppUsers.FirstOrDefault(u => u.Handle == context.UserName);
				if (dbAppUser != null && dbAppUser.isPasswordCorrect(context.Password))
				{
					var claim = new Claim(ClaimTypes.Name, dbAppUser.Handle);
					var claims = new List<Claim>();
					claims.Add(claim);
					context.Result = new GrantValidationResult(dbAppUser.Handle, "access_token", claims);
				}
			}
			return Task.FromResult<object>(null);
		}
	}
}
