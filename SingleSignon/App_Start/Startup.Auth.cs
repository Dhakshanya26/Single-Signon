using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;

namespace WebApplication1
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();

            // Google Client id & secret
            var googleClientId ="s.googleusercontent.com";
            var googleClientSecret ="2Bh";
            // Facebook app id & secret
            var facebookAppId = "Facebook.Api.AppId";
            var facebookAppSecret = "Facebook.Api.AppSecret";

            // Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                SlidingExpiration = true,
                //ExpireTimeSpan = 40.Minutes(),
                CookieName = "rldi",
                CookieDomain = "xxx.co.uk",
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    //OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                    //    validateInterval: TimeSpan.FromMinutes(30),
                    //    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret
            });
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = facebookAppId,
                AppSecret = facebookAppSecret,
                Scope = { "email" },
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name"
            });
        }
    }
}