using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using OAuthDemoMVC5.Models;
using Owin;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.LinkedIn;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin.Security.Providers.OpenID;
using Owin.Security.Providers.Salesforce;

namespace OAuthDemoMVC5
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(TimeSpan.FromMinutes(30),
                            (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);


            /*---------------------------------------------------------------------------------
             * Configuration of the various providers
             *--------------------------------------------------------------------------------- */

            // Configure Facebook
            ConfigureFacebook(app);

            // Configure Google
            ConfigureGoogle(app);

            // Configure GitHub
            ConfigureGitHub(app);

            // Configure LinkedIn
            ConfigureLinkedIn(app);

            // Configure Microsoft
            ConfigureMicrosoft(app);

            // Configure Salesforce
            ConfigureSalesforce(app);

            // Configure Twitter
            ConfigureTwitter(app);
        }

        private void ConfigureFacebook(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseFacebookAuthentication("Your App ID", "Your App Secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new FacebookAuthenticationOptions
            //{
            //    AppId = "Your App ID",
            //    AppSecret = "Your App Secret",
            //};
            //options.Scope.Add("user_friends");
            //app.UseFacebookAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in Facebook
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new FacebookAuthenticationOptions
            //{
            //    AppId = "Your App ID",
            //    AppSecret = "Your App Secret",
            //    CallbackPath = new PathString("/oauth-redirect/facebook")
            //};
            //app.UseFacebookAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new FacebookAuthenticationOptions
            //{
            //    AppId = "Your App ID",
            //    AppSecret = "Your App Secret",
            //    Provider = new FacebookAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the username
            //            string facebookUserName = context.UserName;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseFacebookAuthentication(options);
        }

        private void ConfigureGitHub(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseGitHubAuthentication("Your client ID", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new GitHubAuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //};
            //options.Scope.Add("user:email");
            //app.UseGitHubAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in GitHub
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new GitHubAuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/github")
            //};
            //app.UseGitHubAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new GitHubAuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //    Provider = new GitHubAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the username
            //            string gitHubUserName = context.UserName;

            //            // Retrieve the user's email address
            //            string gitHubEmailAddress = context.Email;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseGitHubAuthentication(options);
        }

        private void ConfigureGoogle(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseGoogleAuthentication("Your client ID", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new GoogleOAuth2AuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //};
            //options.Scope.Add("https://www.googleapis.com/auth/books");
            //app.UseGoogleAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in GitHub
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new GoogleOAuth2AuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/google")
            //};
            //app.UseGoogleAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new GoogleOAuth2AuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //    Provider = new GoogleOAuth2AuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the name of the user in Google
            //            string googleName = context.Name;

            //            // Retrieve the user's email address
            //            string googleEmailAddress = context.Email;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseGoogleAuthentication(options);
        }

        private void ConfigureLinkedIn(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseLinkedInAuthentication("Your API Key", "Your secret key");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new LinkedInAuthenticationOptions
            //{
            //    ClientId = "Your API Key",
            //    ClientSecret = "Your secret key",
            //};
            //options.Scope.Add("rw_groups");
            //app.UseLinkedInAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in GitHub
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new LinkedInAuthenticationOptions
            //{
            //    ClientId = "Your API Key",
            //    ClientSecret = "Your secret key",
            //    CallbackPath = new PathString("/oauth-redirect/linkedin")
            //};
            //app.UseLinkedInAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new LinkedInAuthenticationOptions
            //{
            //    ClientId = "Your API key",
            //    ClientSecret = "Your secret key",
            //    Provider = new LinkedInAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the username
            //            string linkedInUserName = context.UserName;

            //            // Retrieve the user's email address
            //            string linkedInEmailAddress = context.Email;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseLinkedInAuthentication(options);
        }

        private void ConfigureMicrosoft(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseMicrosoftAccountAuthentication("Your client ID", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new MicrosoftAccountAuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //};
            //options.Scope.Add("wl.calendars");
            //app.UseMicrosoftAccountAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in Microsoft
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new MicrosoftAccountAuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/microsoft")
            //};
            //app.UseMicrosoftAccountAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new MicrosoftAccountAuthenticationOptions
            //{
            //    ClientId = "Your client ID",
            //    ClientSecret = "Your client secret",
            //    Provider = new MicrosoftAccountAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user ID
            //            string microsoftUserId = context.Id;

            //            // Retrieve the user's full name
            //            string microsoftFullName = context.Name;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseMicrosoftAccountAuthentication(options);

        }

        private void ConfigureSalesforce(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------------------
             * IMPORTANT INFORMATION
             * 
             * For the Salesforce provider, you *must* always specify the correct Authorization
             * and Token endpoints as these are blank by default, and the authentication process
             * will not work if you do not specify them.
             * 
             * These are going to be dependent on which instance you are running. The instance is 
             * the first segement of your Salesforce URL (i.e. the subdomain) and will be something
             * line "na12", "ap1", etc.
             * 
             * The format for the endpoint are as follows:
             * 
             * AuthorizationEndpoint = "https://<instance_name>.salesforce.com/services/oauth2/authorize"
             * TokenEndpoint = "https://<instance_name>.salesforce.com/services/oauth2/token"
             * ------------------------------------------------------------------------------------------- */

            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //var options = new SalesforceAuthenticationOptions
            //{
            //    ClientId = "Your consumer key",
            //    ClientSecret = "Your consumer secret",
            //    Endpoints = new SalesforceAuthenticationOptions.SalesforceAuthenticationEndpoints
            //    {
            //        AuthorizationEndpoint = "https://ap1.salesforce.com/services/oauth2/authorize",
            //        TokenEndpoint = "https://ap1.salesforce.com/services/oauth2/token"
            //    }
            //};
            //app.UseSalesforceAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            // Specifying the scope has no effect. You will need to specify permissions requested
            // by your application through the Salesforce administration interface when you
            // register the application


            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new SalesforceAuthenticationOptions
            //{
            //    ClientId = "Your consumer key",
            //    ClientSecret = "Your consumer secret",
            //    Endpoints = new SalesforceAuthenticationOptions.SalesforceAuthenticationEndpoints
            //    {
            //        AuthorizationEndpoint = "https://ap1.salesforce.com/services/oauth2/authorize",
            //        TokenEndpoint = "https://ap1.salesforce.com/services/oauth2/token"
            //    },
            //    Provider = new SalesforceAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user ID
            //            string salesforceUserId = context.UserId;

            //            // Retrieve the user's Organization ID
            //            string salesforceOrganizationId = context.OrganizationId;

            //            // Retrieve the user's full name
            //            string salesforceFullName = context.DisplayName;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseSalesforceAuthentication(options);
        }

        private void ConfigureTwitter(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseTwitterAuthentication("Your Consumer Key", "Your Consumer Secret");

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path
             * ------------------------------------------------------------------------------- */

            //var options = new TwitterAuthenticationOptions
            //{
            //    ConsumerKey = "Your Consumer Key", 
            //    ConsumerSecret = "Your Consumer Secret",
            //    CallbackPath = new PathString("/oauth-redirect/twitter")
            //};
            //app.UseTwitterAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new TwitterAuthenticationOptions
            //{
            //    ConsumerKey = "Your Consumer Key",
            //    ConsumerSecret = "Your Consumer Secret",
            //    Provider = new TwitterAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the screen name (e.g. @jerriepelser)
            //            string twitterScreenName = context.ScreenName;

            //            // Retrieve the user ID
            //            var twitterUserId = context.UserId;
            //        }
            //    }
            //};
            //app.UseTwitterAuthentication(options);

        }
    }
}