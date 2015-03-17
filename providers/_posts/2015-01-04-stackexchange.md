---
layout: provider
title:  Stack Exchange
logo: stackexchange.png
links:
  - name: StackExchange Home Page
    url: http://stackexchange.com/
  - name: StackExchange API Website
    url: http://api.stackexchange.com/
  - name: Registered OAuth applications
    url: https://stackapps.com/apps/oauth
guide: /stackexchange/guides/aspnet-mvc5
---

## 1. Register an application in Stack Exchange

Go to the [StackExchange API Website](http://api.stackexchange.com/) and click on the "Register for An App Key" link. Register a new application and be sure to leave the "Enable Client Side OAuth Flow" unchecked. When you are finished take note of the Client Id, Client Secret and Key:

![](/images/stackexchange-keys.png)

## 2. Install the Nuget Package

Install the Nuget Package which contains the StackExchange OAuth provider.

{% highlight bash %}
Install-Package Install-Package Owin.Security.Providers
{% endhighlight %}

## 3. Register Provider
 
Locate the file in your project called `\App_Start\Startup.Auth.cs`. Ensure that you have imported the `Owin.Security.Providers.StackExchange` namespace:

{% highlight csharp %}
using Owin.Security.Providers.StackExchange;
{% endhighlight %}

In the `ConfigureAuth` method add the following line of code:

{% highlight csharp %}
app.UseStackExchangeAuthentication("Your client id", "Your client secret", "Your key");
{% endhighlight %}

## 4. Advanced Configuration

### Request extra permissions

By default the StackExchange provider will not request any scope, which only gives it access to the `/me` endpoint of the API. If you want to request more permissions, you will need to add these to the `Scope` property:

{% highlight csharp %}
var options = new StackExchangeAuthenticationOptions
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    Key = "Your key"
};
options.Scope.Add("read_inbox");
app.UseStackExchangeAuthentication(options);
{% endhighlight %}

The full list of scopes are available under the Scopes heading in the [StackExchange API Authentication documentation](https://api.stackexchange.com/docs/authentication)

### Specify an alternative callback path

By default the StackExchange provider will request StackExchange to redirect to the path `/signin-stackexchange` after the user has signed in and granted permissions. You can specify an alternative redirect URL:

{% highlight csharp %}
var options = new StackExchangeAuthenticationOptions
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    Key = "Your key",
    CallbackPath = new PathString("/oauth-redirect/stackexchange")
};
app.UseStackExchangeAuthentication(options);
{% endhighlight %}

### Retrieve access token and other user information returned from StackExchange

You can retrieve the user information returned from StackExchange in the `OnAuthenticated` callback function which gets invoked after the user has authenticated with StackExchange:

{% highlight csharp %}
var options = new StackExchangeAuthenticationOptions
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    Key = "Your key",
    Provider = new StackExchangeAuthenticationProvider
    {
        OnAuthenticated = async context =>
        {
            // Retrieve the OAuth access token to store for subsequent API calls
            string accessToken = context.AccessToken;

            // Retrieve the user ID
            string stackExchangeUserId = context.Id;

            // Retrieve the user name
            string stackExchangeUserName = context.UserName;

            // Retrieve the user's profile image URL
            string stackExchangeProfileImage = context.ProfileImage;

            // You can even retrieve the full JSON-serialized user
            var serializedUser = context.User;
        }
    }
};
app.UseStackExchangeAuthentication(options);
{% endhighlight %}
