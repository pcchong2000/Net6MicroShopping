// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Shopping.Api.IdentityTenant
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
                new ApiScope("orderapi", "order API"),
                new ApiScope("memberapi", "member API"),
                new ApiScope("payapi", "pay API"),
                new ApiScope("productapi", "product API"),
                new ApiScope("tenantapi", "tenant API"),
            };

        public static IEnumerable<Client> Clients(IConfiguration Configuration)
        {
            string TenantWebCallbackUrl = Configuration["TenantWebCallbackUrl"];
            return new List<Client>
            {
                 new Client
                    {
                        ClientId = "tenantjs",
                        ClientName = "JavaScript Client",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequireClientSecret = false,
                        RequireConsent=true, //要求确认同意
                        AllowOfflineAccess=true,
                        RedirectUris =           { TenantWebCallbackUrl},

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.OfflineAccess,
                            "orderapi",
                            "memberapi",
                            "productapi",
                            "tenantapi",
                            "payapi",
                        }
                    },
                //new Client
                //    {
                //        ClientId = "tenantjs",
                //        ClientName = "JavaScript Client",
                //        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                //        RequireClientSecret = false,
                //        AllowOfflineAccess=true,
                //        AllowedScopes = new List<string>
                //        {
                //            IdentityServerConstants.StandardScopes.OpenId,
                //            IdentityServerConstants.StandardScopes.Profile,
                //            "orderapi",
                //            "memberapi",
                //            "productapi",
                //            "tenantapi",
                //            "payapi",
                //        }
                //    },
            };
        }

    }
}