using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using viavanaIdSrv.Models;
using viavanaIdSrv.Services;

namespace viavanaIdSrv.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private ConfigurationDbContext _context;
        public ClientController(ConfigurationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetClient")]
        public IActionResult Get(string clientid)
        {
            return new JsonResult(from c in _context.Clients where c.ClientId==clientid select c);
        }
        //[HttpPost]
        //public IActionResult AddClient(Client client)
        //{
        //    addClient(client);
        //    return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        //}
        [HttpPost]
        public IActionResult AddClient()
        {
            Client client = new Client
            {
                ClientId = "mvc",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                ClientSecrets =
                {
                    new Secret(("secret").Sha256())
                },

                            RedirectUris = { "http://localhost:4200/signin-oidc" },
                            PostLogoutRedirectUris = { "http://localhost:4200/signout-callback-oidc" },

                            AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                },
                AllowOfflineAccess = true
            };
            client= new Client
            {
                ClientId = "js",
                ClientName = "JavaScript Client",
                ClientUri = "http://identityserver.io",

                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { "http://localhost:4200/index.html" },
                PostLogoutRedirectUris = { "http://localhost:4200/index.html" },
                AllowedCorsOrigins = { "http://localhost:55720" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,

                    "api1", "api2.read_only"
                }
            };
            addClient(client);
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
        private Client addClient(Client client)
        {
            //private void AddClient(IApplicationBuilder app)
            //{
            //    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //    {
            //        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            //        _context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            //        _context.Database.Migrate();
            if (!_context.Clients.Any(cl => cl.ClientId.ToLower().Trim() == client.ClientId.ToLower().Trim()))
            {
                _context.Clients.Add(client.ToEntity());

                _context.SaveChanges();
            }

            //if (!_context.IdentityResources.Any())
            //{
            //    foreach (var resource in Resources.GetIdentityResources())
            //    {
            //        _context.IdentityResources.Add(resource.ToEntity());
            //    }
            //    _context.SaveChanges();
            //}

            //if (!_context.ApiResources.Any())
            //{
            //    foreach (var resource in Resources.GetApiResources())
            //    {
            //        _context.ApiResources.Add(resource.ToEntity());
            //    }
            //    _context.SaveChanges();
            //}
            return client;
        }
    }

    public class SettingsController : Controller
    {
        private readonly SampleWebSettings _settings;

        public SettingsController(IOptions<SampleWebSettings> settingsOptions)
        {
            _settings = settingsOptions.Value;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = _settings.Title;
            ViewData["Updates"] = _settings.Updates;
            return View();
        }
        public IActionResult About([FromServices] IDateTime dateTime)
        {
            ViewData["Message"] = "Currently on the server the time is " + dateTime.Now;

            return View();
        }
    }

}
