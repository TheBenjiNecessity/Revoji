using System;
using System.Collections.Generic;
using IdentityModel.Client;
using IdentityServer4.Models;

namespace RevojiWebApi
{
    public class Config
	{
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("api", "Revoji Api")
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "com.revoji",
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
					AllowAccessTokensViaBrowser = true,
					AllowedScopes = { "api" }
				}
			};
		}
    }
}
