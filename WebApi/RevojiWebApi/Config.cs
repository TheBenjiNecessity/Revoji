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
                    AccessTokenLifetime = 31536000,
                    AllowedScopes = { "api" },
                    AllowedCorsOrigins = { "http://localhost:3000", "http://revoke-web.com.s3-website-us-west-2.amazonaws.com" }//TODO needs to be in config
                }
            };
        }
    }
}
