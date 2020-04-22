using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Golf_Results_MVC;
using Golf_Results_MVC.Providers;
using Golf_Results_MVC.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Golf_Results_MVC.Models;

[assembly: OwinStartup(typeof(Golf_Results_MVC.Startup))]

namespace Golf_Results_MVC
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAppProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(200), // Sets the days until token expires for API
                AllowInsecureHttp = true
            };
        }
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            ConfigureAuth(app);
            createRolesandUsers();
        }

        //If want add an Admin automatically can use this. 
        private void createRolesandUsers()
        {
            GolfContext context = new GolfContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            //In Startup I am creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("admin"))
            {

                //first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website

                var user = new ApplicationUser();
                user.UserName = "x00152190@mytudublin.ie"; // Username has to match email
                user.Email = "x00152190@mytudublin.ie";
                string userPWD = "TestTallaght55!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "admin");
                }
            }
        }
    }
}

