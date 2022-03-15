// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IdentityTenant.Api
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
            string JSUrl = Configuration["JSUrl"];
            return new List<Client>
            {
                new Client
                    {
                        ClientId = "tenantjs",
                        ClientName = "JavaScript Client",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        RequireClientSecret = false,

                        //RedirectUris =           { JSUrl+"/#/logincallback" },
                        //PostLogoutRedirectUris = { JSUrl+"/#/login" },
                        //AllowedCorsOrigins =     { JSUrl },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "orderapi",
                            "memberapi",
                            "productapi",
                            "tenantapi",
                            "payapi",
                        }
                    },
            };
        }

    }
}