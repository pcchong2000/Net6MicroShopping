// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Shopping.Api.IdentityMember
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("memberapi", "My API"),
                new ApiScope("orderapi", "My API"),
                new ApiScope("payapi", "My API"),
                new ApiScope("productapi", "My API"),
                new ApiScope("ossapi", "My API"),
            };

        public static IEnumerable<Client> Clients(IConfiguration Configuration)
        {
            string MemberWebCallbackUrl = Configuration["MemberWebCallbackUrl"];
            string MemberMauiCallbackUrl = Configuration["MemberMauiCallbackUrl"];
            string TenantIdentityCallbackUrl = Configuration["TenantIdentityCallbackUrl"];
            return new List<Client>
            {
                new Client
                    {
                        ClientId = "productjs",
                        ClientName = "JavaScript Client",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequireClientSecret = false,
                        AllowOfflineAccess=true,
                        RedirectUris =           { MemberWebCallbackUrl },
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "memberapi",
                            "orderapi",
                            "productapi",
                            "payapi",
                            "ossapi",
                        }
                    },
                new Client
                    {
                        ClientId = "membermaui",
                        ClientSecrets={ new Secret("secret".Sha256()) },
                        ClientName = "maui Client",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequireClientSecret = false,
                        AllowOfflineAccess=true,
                        //RequireConsent=true, 要求确认同意
                        RedirectUris =           { MemberMauiCallbackUrl },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.LocalApi.ScopeName,
                            "orderapi",
                            "productapi",
                            "payapi",
                            "ossapi",
                        }
                    },
                new Client
                    {
                        ClientId = "membermauipassword",
                        ClientName = "maui Client",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        RequireClientSecret = false,
                        AllowOfflineAccess=true,

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.LocalApi.ScopeName,
                            "orderapi",
                            "productapi",
                            "payapi",
                            "ossapi",
                        }
                    },
                new Client
                    {
                        ClientId = "tenantAdmin",
                        ClientName = "tenantAdmin Client",
                        ClientSecrets={ new Secret("secret".Sha256()), },
                        AllowedGrantTypes = GrantTypes.Code,
                        AllowOfflineAccess=true,
                        RedirectUris =           { TenantIdentityCallbackUrl },
                        //RedirectUris = { "https://localhost:44302/signin-oidc" },
                        //FrontChannelLogoutUri = "https://localhost:44302/signout-oidc",
                        //PostLogoutRedirectUris = { "https://localhost:44302/signout-callback-oidc" },
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                        }
                    },
             new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:6002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:6002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "orderapi"
                    }
                }
            };
        }

    }
}