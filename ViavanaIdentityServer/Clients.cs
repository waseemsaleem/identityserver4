using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace ViavanaIdentityServer
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client> {
            new Client {
                ClientId = "oauthClient",
                ClientName = "Example Client Credentials Client Application",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {
                    new Secret("superSecretPassword".Sha256())},
                AllowedScopes = new List<string> {StandardScopes.OpenId,
                               StandardScopes.OpenId,
                    StandardScopes.Profile,
                        StandardScopes.Email,
                    //    StandardScopes.OfflineAccess,
                    "customAPI.read" }
            },
            new Client {
                ClientId = "lenderclient",
                ClientName = "Lender Client Credentials Client Application",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {
                    new Secret("superSecretPassword".Sha256())},
                AllowedScopes = new List<string> {
                           StandardScopes.OpenId,
                    "accountAPI.read",
                    "accountAPI.write" }
            },
            new Client
                {
                    ClientId = "test",
                    ClientName = "Test client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = false,
                    AlwaysSendClientClaims = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    ClientSecrets =new List<Secret>
                    {
                        new Secret("test")
                    },
                    RedirectUris = new List<string>
                    {
                       "http://localhost:3000/test/calback"
                    },
                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        //StandardScopes.OfflineAccess,
                        "role",
                        "api1",
                    },
                },
            new Client {
    ClientId = "openIdConnectClient",
    ClientName = "Example Implicit Client Application",
    AllowedGrantTypes = GrantTypes.Implicit,
    AllowedScopes = new List<string>
    {
        StandardScopes.OpenId,
        StandardScopes.Profile,
        StandardScopes.Email,
        "role",
        "customAPI.write"
    },
    RedirectUris = new List<string> {"https://localhost:59384/signin-oidc"},
    PostLogoutRedirectUris = new List<string> {"https://localhost:59384"}
}
        };
        }
    }
}
