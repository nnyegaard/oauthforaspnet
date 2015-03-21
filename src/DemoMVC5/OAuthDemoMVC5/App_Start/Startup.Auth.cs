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
using Owin.Security.Providers.ArcGISOnline;
using Owin.Security.Providers.Asana;
using Owin.Security.Providers.BattleNet;
using Owin.Security.Providers.Buffer;
using Owin.Security.Providers.Dropbox;
using Owin.Security.Providers.Instagram;
using Owin.Security.Providers.Instagram.Provider;
using Owin.Security.Providers.OpenID;
using Owin.Security.Providers.Reddit;
using Owin.Security.Providers.Reddit.Provider;
using Owin.Security.Providers.Salesforce;
using Owin.Security.Providers.StackExchange;
using Owin.Security.Providers.TripIt;
using Owin.Security.Providers.TripIt.Provider;
using Owin.Security.Providers.Twitch;
using Owin.Security.Providers.Yahoo;

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
            
            // Configure ArcGIS
            ConfigureArcGIS(app);

            // Configure Asana
            ConfigureAsana(app);

            // Configure Battle.NET
            ConfigureBattleNet(app);

            // Configure Buffer
            ConfigureBuffer(app);

            // Configure Dropbox
            ConfigureDropbox(app);

            // Configure Facebook
            ConfigureFacebook(app);

            // Configure Foursquare
            ConfigureFoursquare(app);

            // Configure Google
            ConfigureGoogle(app);

            // Configure GitHub
            ConfigureGitHub(app);

            // Configure Instagram
            ConfigureInstagram(app);

            // Configure LinkedIn
            ConfigureLinkedIn(app);

            // Configure Microsoft
            ConfigureMicrosoft(app);

            // Configure Reddit
            ConfigureReddit(app);

            // Configure Salesforce
            ConfigureSalesforce(app);

            // Configure Stack Exchange
            ConfigureStackExchange(app);

            // Configure TripIt
            ConfigureTripIt(app);

            // Configure Twitter
            ConfigureTwitter(app);

            // Configure Twitch
            ConfigureTwitch(app);

            // Configure Yahoo
            ConfigureYahoo(app);
        }

        private void ConfigureArcGIS(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseArcGISOnlineAuthentication("Your client id", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            // ArcGIS has no concept of scopes. To see which information is available, please
            // refer to this document:
            //
            // https://developers.arcgis.com/authentication/limitations-of-application-authentication/

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify under the Security tab of the application matches
             * this redirect URI
             * ------------------------------------------------------------------------------- */

            //var options = new ArcGISOnlineAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/arcgis")
            //};
            //app.UseArcGISOnlineAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new ArcGISOnlineAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Provider = new ArcGISOnlineAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user's full name
            //            string userFullName = context.Name;

            //            // Retrieve the user's email address
            //            string userEmail = context.Email;
            //        }
            //    }
            //};
            //app.UseArcGISOnlineAuthentication(options);
        }

        private void ConfigureAsana(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseAsanaAuthentication("Your client id", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in Facebook
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new AsanaAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/asana")
            //};
            //app.UseAsanaAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new AsanaAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Provider = new AsanaAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user's full name
            //            string userFullName = context.Name;

            //            // Retrieve the user's email address
            //            string userEmail = context.Email;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseAsanaAuthentication(options);

        }

        private void ConfigureBattleNet(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //var options = new BattleNetAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Region = Region.US
            //};
            //app.UseBattleNetAuthentication(options);
        }

        private void ConfigureBuffer(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseBufferAuthentication("Your client id", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in Buffer
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new BufferAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/buffer")
            //};
            //app.UseBufferAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new BufferAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Provider = new BufferAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user's account ID on buffer
            //            string userId = context.Id;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseBufferAuthentication(options);

        }

        private void ConfigureDropbox(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseDropboxAuthentication("Your app key", "Your app secret");

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in Dropbox
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new DropboxAuthenticationOptions
            //{
            //    AppKey = "Your app key",
            //    AppSecret = "Your app secret",
            //    CallbackPath = new PathString("/oauth-redirect/dropbox")
            //};
            //app.UseDropboxAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new DropboxAuthenticationOptions
            //{
            //    AppKey = "Your app key",
            //    AppSecret = "Your app secret",
            //    Provider = new DropboxAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user's full name
            //            string userFullName = context.Name;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseDropboxAuthentication(options);

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

        private void ConfigureFoursquare(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseFoursquareAuthentication("Your App ID", "Your App Secret");

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

        public void ConfigureInstagram(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseInstagramInAuthentication("Your client id", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new InstagramAuthenticationOptions()
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret"
            //};
            //options.Scope.Add("likes");
            //app.UseInstagramInAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in Instagram
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new InstagramAuthenticationOptions()
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/instagram")
            //};
            //app.UseInstagramInAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new InstagramAuthenticationOptions()
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Provider = new InstagramAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the username
            //            string userName = context.UserName;

            //            // Retrieve the user's full name
            //            string fullName = context.Name;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseInstagramInAuthentication(options);

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

        private void ConfigureReddit(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseRedditAuthentication("Your client id", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new RedditAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret"
            //};
            //options.Scope.Clear();
            //options.Scope.Add("identity");
            //app.UseRedditAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. In this case you need to make sure that
             * the redirect URI you specify when registering the application in Microsoft
             * matches this exactly
             * ------------------------------------------------------------------------------- */

            //var options = new RedditAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/reddit")
            //};
            //app.UseRedditAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new RedditAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Provider = new RedditAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user's Reddit ID
            //            string userId = context.Id;

            //            // Retrieve the user's Reddit username
            //            string userName = context.UserName;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseRedditAuthentication(options);
        }

        private void ConfigureTripIt(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseTripItAuthentication("Your api key", "Your api secret");

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. 
             * ------------------------------------------------------------------------------- */

            //var options = new TripItAuthenticationOptions
            //{
            //    ConsumerKey = "Your API Key", 
            //    ConsumerSecret = "Your API Secret",
            //    CallbackPath = new PathString("/oauth-redirect/tripit")
            //};
            //app.UseTripItAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new TripItAuthenticationOptions
            //{
            //    ConsumerKey = "Your API Key", 
            //    ConsumerSecret = "Your API Secret",
            //    Provider = new TripItAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;
            //            string accessTokenSecret = context.AccessTokenSecret;

            //            // Retrieve the user ID
            //            string userDisplayName = context.DisplayName;

            //            // Retrieve the user's full name
            //            string userEmail = context.Email;

            //            // You can even retrieve the full JSON-serialized user profile 
            //            var serializedUser = context.Profile;
            //        }
            //    }
            //};
            //app.UseTripItAuthentication(options);
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

        private void ConfigureStackExchange(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseStackExchangeAuthentication("Your client id", "Your client secret", "Your key");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new StackExchangeAuthenticationOptions
            //{
            //    ClientId = "Your client Id",
            //    ClientSecret = "Your client secret",
            //    Key = "Your client key"
            //};
            //options.Scope.Add("read_inbox");
            //app.UseStackExchangeAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path. 
             * ------------------------------------------------------------------------------- */

            //var options = new StackExchangeAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Key = "Your key",
            //    CallbackPath = new PathString("/oauth-redirect/stackexchange")
            //};
            //app.UseStackExchangeAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new StackExchangeAuthenticationOptions
            //{
            //    ClientId = "Your client id",
            //    ClientSecret = "Your client secret",
            //    Key = "Your key",
            //    Provider = new StackExchangeAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user ID
            //            string stackExchangeUserId = context.Id;

            //            // Retrieve the user name
            //            string stackExchangeUserName = context.UserName;

            //            // Retrieve the user's profile image URL
            //            string stackExchangeProfileImage = context.ProfileImage;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseStackExchangeAuthentication(options);
        }

        private void ConfigureTwitch(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseTwitchAuthentication("Your client id", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Request extra permissions
             * ------------------------------------------------------------------------------- */

            //var options = new TwitchAuthenticationOptions
            //{
            //    ClientId = "Your client id", 
            //    ClientSecret = "Your client secret"
            //};
            //options.Scope.Add("user_subscriptions");
            //app.UseTwitchAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path
             * ------------------------------------------------------------------------------- */

            //var options = new TwitchAuthenticationOptions
            //{
            //    ClientId = "Your client id", 
            //    ClientSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/twitch")
            //};
            //app.UseTwitchAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new TwitchAuthenticationOptions
            //{
            //    ClientId = "Your client id", 
            //    ClientSecret = "Your client secret",
            //    Provider = new TwitchAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;

            //            // Retrieve the user's name
            //            string userDisplayName = context.Name;

            //            // Retrieve the user's email address
            //            var userEmail = context.Email;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseTwitchAuthentication(options);

        }

        private void ConfigureTwitter(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
             * Normal configuration
             * ------------------------------------------------------------------------------- */

            //app.UseTwitterAuthentication("Your consumer key", "Your consumer secret");

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path
             * ------------------------------------------------------------------------------- */

            //var options = new TwitterAuthenticationOptions
            //{
            //    ConsumerKey = "Your consumer key",
            //    ConsumerSecret = "Your consumer secret",
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

        private void ConfigureYahoo(IAppBuilder app)
        {
            /* -------------------------------------------------------------------------------
            * Normal configuration
            * ------------------------------------------------------------------------------- */

            //app.UseYahooAuthentication("Your client ID", "Your client secret");

            /* -------------------------------------------------------------------------------
             * Specify an alternate callback path.
             * ------------------------------------------------------------------------------- */

            //var options = new YahooAuthenticationOptions
            //{
            //    ConsumerKey = "Your client ID",
            //    ConsumerSecret = "Your client secret",
            //    CallbackPath = new PathString("/oauth-redirect/yahoo")
            //};
            //app.UseYahooAuthentication(options);

            /* -------------------------------------------------------------------------------
             * Retrieve the access token and other user information
             * ------------------------------------------------------------------------------- */

            //var options = new YahooAuthenticationOptions
            //{
            //    ConsumerKey = "Your client ID",
            //    ConsumerSecret = "Your client secret",
            //    Provider = new YahooAuthenticationProvider
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            // Retrieve the OAuth access token to store for subsequent API calls
            //            string accessToken = context.AccessToken;
            //            string accessTokenSecret = context.AccessTokenSecret;

            //            // Retrieve the user ID
            //            string yahooUserName = context.UserId;

            //            // You can even retrieve the full JSON-serialized user
            //            var serializedUser = context.User;
            //        }
            //    }
            //};
            //app.UseYahooAuthentication(options);
        }
    }
}