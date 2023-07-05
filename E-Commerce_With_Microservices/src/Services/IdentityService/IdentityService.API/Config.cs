// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityService.API
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_read" , "catalog_write"}},
            new ApiResource("resource_basket"){Scopes = {"basket_read" , "basket_write"}},
            new ApiResource("resource_stock"){Scopes = {"stock_read" , "stock_write"}},
            new ApiResource("resource_payment"){Scopes = {"payment_read" , "payment_write"}},
                new ApiResource("resource_order"){Scopes = {"order_read","order_read_admin" , "order_write"}},
                new ApiResource("resource_complaint"){Scopes = {"complaint_read" , "complaint_write"}},
                 new ApiResource("resource_gateway"){Scopes = {"gateway_full"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource {
                                   Name = "Roles",
                                   DisplayName = "Roles",
                                   UserClaims = { JwtClaimTypes.Role } }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_read","Allowing to read for catalog api"),
                new ApiScope("catalog_write","Allowing to write for catalog api"),
                  new ApiScope("basket_read","Allowing to read for basket api"),
                new ApiScope("basket_write","Allowing to write for basket api"),
                  new ApiScope("stock_read","Allowing to read for stock api"),
                new ApiScope("stock_write","Allowing to write for stock api"),
                      new ApiScope("payment_read","Allowing to read for stock api"),
                new ApiScope("payment_write","Allowing to write for stock api"),
                    new ApiScope("order_read","Allowing to read for order api"),
                new ApiScope("order_write","Allowing to write for order api"),
                new ApiScope("order_read_admin","Allowing to write for order api"),
                 new ApiScope("gateway_full","Allowing to full for gateway api"),
                   new ApiScope("complaint_read","Allowing to read for complaint api"),
                new ApiScope("complaint_write","Allowing to write for complaint api"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                   ClientName="Asp.Net Core MVC",
                    ClientId="MobileClient",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={ "catalog_read","gateway_full",IdentityServerConstants.LocalApi.ScopeName }
                },
                   new Client
                {
                   ClientName="Asp.Net Core MVC",
                    ClientId="MobileClientForUser",
                    AllowOfflineAccess=true,
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"Roles" ,
                       "basket_read" , "basket_write" , "stock_read" , "payment_read" , "payment_write" , "order_write","order_read",
                       "catalog_read","gateway_full","complaint_read","complaint_write"},
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                },
                    new Client
                {
                   ClientName="Asp.Net Core MVC",
                    ClientId="MobileClientForAdmin",
                    AllowOfflineAccess=true,
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"Roles" ,
                        "catalog_write" , "stock_read" , "stock_write","order_read_admin",
                        "catalog_read","gateway_full","complaint_read"

                        },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                }
            };
    }
}