---
layout: provider
title:  Foursquare
logo: foursquare.png
links:
  - name: Foursquare Home Page
    url: https://www.foursquare.com/
  - name: Foursquare Developer Home
    url: https://developer.foursquare.com/
  - name: Your Apps
    url: https://foursquare.com/developers/apps
sdks:
  - name: SharpSquare
    url: https://github.com/TICLAB/SharpSquare
---

## 1. Register an app in Foursquare

Go to the [Foursquare Apps page](https://foursquare.com/developers/apps) and click on the "Create a new app" button. Complete the information as required, and for the **Redirect URI** use the URL of your application with the suffix `/signin-foursuare`, e.g. `http://localhost:4515/signin-foursquare`. Once you have completed all of the information click on the Save Changes button and take note of the  **Client Id** and **Client secret** as your will need these when enabling the Foursquare OAuth provider in your ASP.NET application.

![](/images/foursquare-app-key-and-secret.png)

## 2. Install the Nuget Package

Install the Nuget Package which contains the Foursquare OAuth provider.

{% highlight bash %}
Install-Package Install-Package Owin.Security.Providers
{% endhighlight %}

## 3. Register Provider

Locate the file in your project called `\App_Start\Startup.Auth.cs`. Ensure that you have imported the `Owin.Security.Providers.Foursquare` namespace:

{% highlight csharp %}
using Owin.Security.Providers.Foursquare;
{% endhighlight %}

In the `ConfigureAuth` method add the following line of code:

{% highlight csharp %}
app.UseFoursquareAuthentication("Your client id", "Your client secret");
{% endhighlight %}

## 4. Advanced Configuration

### Request extra permissions

The FourSquare API does not give the ability to request any extra permissions. 

### Specify an alternative callback path

By default the Foursquare provider will request Foursquare to redirect to the path `/signin-foursquare` after the user has signed in and granted permissions on Foursquare. You can specify an alternative redirect URL:

{% highlight csharp %}
var options = new FoursquareAuthenticationOptions()
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    CallbackPath = "/oauth-redirect/foursquare"
};
app.UseFoursquareAuthentication(options);
{% endhighlight %}

### Retrieve access token and other user information returned from Foursquare

You can retrieve the user information returned from Foursquare in the `OnAuthenticated` callback function which gets invoked after the user has authenticated with Foursquare:

{% highlight csharp %}
var options = new FoursquareAuthenticationOptions()
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    Provider = new FoursquareAuthenticationProvider
    {
        OnAuthenticated = async context =>
        {
            // Retrieve the OAuth access token to store for subsequent API calls
            string accessToken = context.AccessToken;

            // Retrieve the user's name
            string name = context.Name;

            // Retrieve the user's email address
            string emailAddress = context.Email;

            // You can even retrieve the full JSON-serialized user
            var serializedUser = context.User;
        }
    }
};
app.UseFoursquareAuthentication(options);
{% endhighlight %}
