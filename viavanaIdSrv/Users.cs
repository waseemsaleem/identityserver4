﻿using IdentityModel;
using IdentityServer4.Test;
//using IdentityServer4.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace viavanaIdSrv
{
    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> {
            new TestUser {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "waseem",
                Password = "123456",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "mws@quantumcph.com"),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            }
        };
        }
    }
}
