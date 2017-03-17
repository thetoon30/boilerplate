using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using InternalModule.Boilerplate.Providers;
using InternalModule.Boilerplate.Models;
using InternalModeule.Boilerplate.EF;
using Microsoft.Owin.Security.Facebook;
using System.Configuration;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace InternalModule.Boilerplate
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true // NOTE : When don't need SSL
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            var options = new TwitterAuthenticationOptions
            {
                ConsumerKey = ConfigurationManager.AppSettings["TwiiterConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["TwiiterConsumerSecret"],
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(
                new[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
	                "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
	                "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
	                "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
	                "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
	                "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server CA
	                "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
                }),
                Provider = new TwitterAuthenticationProvider
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesstoken", context.AccessToken));
                        context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesssecret", context.AccessTokenSecret));
                        // Retrieve the OAuth access token to store for subsequent API calls
                        string accessToken = context.AccessToken;

                        // Retrieve the screen name (e.g. @jerriepelser)
                        string twitterScreenName = context.ScreenName;

                        // Retrieve the user ID
                        var twitterUserId = context.UserId;
                    }
                }
            };
            app.UseTwitterAuthentication(options);

            var fbObtions = new FacebookAuthenticationOptions();
            fbObtions.AppId = ConfigurationManager.AppSettings["FacebookAppId"];
            fbObtions.AppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];
            fbObtions.Scope.Add("email");
            fbObtions.Scope.Add("publish_actions");
            fbObtions.Provider = new FacebookAuthenticationProvider
            {
                OnAuthenticated = async (context) =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));

                    foreach (var claim in context.User)
                    {
                        var claimType = string.Format("urn:facebook:{0}", claim.Key);
                        var claimValue = claim.Value.ToString();
                        if (!context.Identity.HasClaim(claimType, claimValue))
                            context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));
                    }
                }
            };

            app.UseFacebookAuthentication(fbObtions);
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
