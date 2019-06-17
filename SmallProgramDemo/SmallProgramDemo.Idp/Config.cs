// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace SmallProgramDemo.Idp
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("RESTApi", "My REST API #1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "mvc hybrid client",
                    ClientName = "MVC 客户端",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "https://localhost:7001/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:7001/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:7001/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    //AllowedScopes = { "openid", "profile", "api1" }
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "RESTApi"
                    }
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "AngularClient",
                    ClientName = "SPA Client",
                    ClientUri = "http://localhost:4200",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true, //是否需要用户点击同意
                    AccessTokenLifetime = 60 *5, //AccessToken过期时间 单位是秒

                    RedirectUris =
                    {
                        "http://localhost:4200/signin-oidc",
                        "http://localhost:4200/redirect-silentrenew",
                    },

                    PostLogoutRedirectUris = { "http://localhost:4200/" },
                    

                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "RESTApi"
                    }
                }
            };
        }
    }
}