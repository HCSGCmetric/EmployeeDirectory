using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using Google.Apis.Auth.OAuth2.Responses;
using System.Security.Claims;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;
using EmployeeDirectory.Helper;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartup(typeof(EmployeeDirectory.App_Start.Startup))]

namespace EmployeeDirectory.App_Start
{
    public class Startup
    {
        private IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //    Provider = new CookieAuthenticationProvider
            //    {
            //        // Enables the application to validate the security stamp when the user logs in.
            //        // This is a security feature which is used when you change a password or add an external login
            //        // to your account.  
            //        OnValidateIdentity =
            //            SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //                validateInterval: TimeSpan.FromMinutes(30),
            //                regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            //    }
            //});
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var google = new GoogleOAuth2AuthenticationOptions()
            {
                AccessType = "offline",     // Request a refresh token.
                ClientId = AppConstants.ClientId,
                ClientSecret = AppConstants.ClientSecret,
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        var userId = context.Id;
                        context.Identity.AddClaim(new Claim("GoogleUserId", userId));
                        context.Identity.AddClaim(new System.Security.Claims.Claim("Google_AccessToken", context.AccessToken));

                        if (context.RefreshToken != null)
                        {
                            context.Identity.AddClaim(new Claim("GoogleRefreshToken", context.RefreshToken));
                        }
                        context.Identity.AddClaim(new Claim("GoogleUserId", context.Id));
                        context.Identity.AddClaim(new Claim("GoogleTokenIssuedAt", DateTime.Now.ToBinary().ToString()));
                        var expiresInSec = (long)(context.ExpiresIn.Value.TotalSeconds);
                        context.Identity.AddClaim(new Claim("GoogleTokenExpiresIn", expiresInSec.ToString()));


                        //if (context.RefreshToken != null)
                        //{
                        //    context.Identity.AddClaim(new Claim("GoogleRefreshToken", context.RefreshToken));
                        //}

                        //var tokenResponse = new TokenResponse()
                        //{
                        //    AccessToken = context.AccessToken,
                        //    RefreshToken = context.RefreshToken,
                        //    ExpiresInSeconds = (long)context.ExpiresIn.Value.TotalSeconds,
                        //    Issued = DateTime.Now,
                        //};

                        //await dataStore.StoreAsync(userId, tokenResponse);
                        return Task.FromResult(0);
                    },
                },
            };

            foreach (var scope in MyRequestedScopes.Scopes)
            {
                google.Scope.Add(scope);
            }

            app.UseGoogleAuthentication(google);
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = AppConstants.ClientId,
            //    ClientSecret = AppConstants.ClientSecret,
            //    Provider = new GoogleAuthProvider(),
            //});
        }
    }
}
