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
                new ApiScope("orderapi", "My API"),
                new ApiScope("memberapi", "My API"),
                new ApiScope("payapi", "My API"),
                new ApiScope("productapi", "My API"),
                new ApiScope("tenantapi", "My API"),
            };

        public static IEnumerable<Client> Clients(IConfiguration Configuration)
        {
            string MemberWebUrl = Configuration["MemberWebUrl"];
            string MemberMauiUrl = Configuration["MemberMauiUrl"];
            string MemberIdentityServerUrl = Configuration["MemberIdentityServerUrl"];
            return new List<Client>
            {
                new Client
                    {
                        ClientId = "productjs",
                        ClientName = "JavaScript Client",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequireClientSecret = false,
                        AllowOfflineAccess=true,
                        RedirectUris =           { MemberWebUrl + "/#/logincallback" },
                        PostLogoutRedirectUris = { MemberWebUrl + "/#/login" },
                        AllowedCorsOrigins =     { MemberMauiUrl },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "orderapi",
                            "memberapi",
                            "productapi",
                            "payapi",
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
                        RedirectUris =           { MemberMauiUrl + "/membermauicallback" },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "orderapi",
                            "memberapi",
                            "productapi",
                            "payapi",
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
                            "orderapi",
                            "memberapi",
                            "productapi",
                            "payapi",
                        }
                    },
            };
        }

    }
}