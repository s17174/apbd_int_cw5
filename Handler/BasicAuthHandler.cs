using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace apbd_int_cw5.Handler
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
     
        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            // this.service = service;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Brak headera"));

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(":");

            if (credentials.Length != 2)
                return Task.FromResult(AuthenticateResult.Fail("Incorrect authoriziation header value"));

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1"),
                 new Claim(ClaimTypes.Name, "jan123"),
                 new Claim(ClaimTypes.Role, "admin"),
                 new Claim(ClaimTypes.Role, "student")
             };
            var identify = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identify);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);


            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

}
