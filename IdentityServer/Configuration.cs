﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "Gallery.Name",
                    UserClaims =
                    {
                        "Gallery.Claim"
                    }
                }
            };
        public static IEnumerable<ApiResource> GetApis() =>  
            new List<ApiResource> { new ApiResource("ApiOne") };
        public static IEnumerable<Client> GetClients() =>
            new List<Client> { new  Client {  
                ClientId = "client_id",
                ClientSecrets = {new Secret("client_secret".ToSha256())}, 
                AllowedGrantTypes = {GrantType.ClientCredentials },  
                RedirectUris = { "https://localhost:44337/signin-oidc" },
                AllowedScopes = {"ApiOne", "ApiTwo", "Gallery.Name" }
            } , new  Client {
                ClientId = "client_id_mvc",
                ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},
                AllowedGrantTypes = {GrantType.ClientCredentials, GrantType.AuthorizationCode },
                RedirectUris = { "https://localhost:44337/signin-oidc" },
                AllowedScopes = { 
                    "ApiOne", 
                    "ApiTwo", 
                    IdentityServerConstants.StandardScopes.OpenId, 
                    IdentityServerConstants.StandardScopes.Profile,
                    "Gallery.Name"
                }, 
                RequireConsent = false,   
                AllowOfflineAccess = true
                //commented out because ID Token can turn out to be very big 
                //AlwaysIncludeUserClaimsInIdToken =  true
            }
            };
    }
}
