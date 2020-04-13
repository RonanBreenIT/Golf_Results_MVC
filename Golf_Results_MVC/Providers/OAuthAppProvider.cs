using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Golf_Results_MVC.Providers
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                if (context.UserName == "admin" && context.Password == "admin")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                    identity.AddClaim(new Claim("username", "admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "Powerful administrator"));


                    context.Validated(identity);
                }
                else if (context.UserName == "user" && context.Password == "user")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                    identity.AddClaim(new Claim("username", "user"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "Humble user"));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}