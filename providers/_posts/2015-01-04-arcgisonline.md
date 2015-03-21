---
layout: provider
title:  ArcGIS Online
logo: arcgisonline.png
links:
  - name: ArcGIS Home Page
    url: http://www.arcgis.com/
  - name: ArcGIS Developer Home Page
    url: https://developers.arcgis.com
  - name: Registered Applications
    url: https://developers.arcgis.com/en/applications
sdks:
  - name: ArcGIS Runtime SDK for .NET
    url: https://developers.arcgis.com/net/
---

## 1. Register an application in ArcGIS

Go to the [ArcGIS Developer website](https://developers.arcgis.com) and create a new application. After creating the application you will need to specify a valid Redirect URI under the Authentication tab. For the Redirect URI, you need to use the `/signin-arcgis-online` endpoint, e.g. `http://localhost:4515/signin-arcgis-online`

After you have registered the application take note of the Client ID and Client Secret, as you will need these when registering the provider in your ASP.NET Application:

![](/images/arcgis-client-id-and-secret.png)

## 2. Install the Nuget Package

Install the Nuget Package which contains the ArcGIS OAuth provider.

{% highlight bash %}
Install-Package Install-Package Owin.Security.Providers
{% endhighlight %}

## 3. Register Provider

Locate the file in your project called `\App_Start\Startup.Auth.cs`. Ensure that you have imported the `Owin.Security.Providers.ArcGISOnline` namespace:

{% highlight csharp %}
using Owin.Security.Providers.ArcGISOnline;
{% endhighlight %}

In the `ConfigureAuth` method add the following lines of code:

{% highlight csharp %}
app.UseArcGISOnlineAuthentication("Your client id", "Your client secret");
{% endhighlight %}

## 4. Advanced Configuration

### Request extra permissions

The ArcGIS API does not have the concept of `scopes` like many other OAuth applictions. To understand what information can be accessed with an OAuth token, please refer to the ArcGIS document entitled [Limitations of Application Authentication](https://developers.arcgis.com/authentication/limitations-of-application-authentication/)

### Specify an alternative callback path

By default the ArcGIS provider will request ArcGIS to redirect to the path `/signin-arcgis-online` after the user has signed in and granted permissions on the ArcGIS website. You can specify an alternative callback path:

{% highlight csharp %}
var options = new ArcGISOnlineAuthenticationOptions
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    CallbackPath = new PathString("/oauth-redirect/arcgis")
};
app.UseArcGISOnlineAuthentication(options);
{% endhighlight %}

> You need to also make sure that the **Redirect URI** of your application in ArcGIS matches this new callback path.

### Retrieve access token and other user information returned from ArcGIS

You can retrieve the access token and other user information returned from ArcGIS in the `OnAuthenticated` callback function which gets invoked after the user has authenticated with ArcGIS:

{% highlight csharp %}
var options = new ArcGISOnlineAuthenticationOptions
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    Provider = new ArcGISOnlineAuthenticationProvider
    {
        OnAuthenticated = async context =>
        {
            // Retrieve the OAuth access token to store for subsequent API calls
            string accessToken = context.AccessToken;

            // Retrieve the user's full name
            string userFullName = context.Name;

            // Retrieve the user's email address
            string userEmail = context.Email;
        }
    }
};
app.UseArcGISOnlineAuthentication(options);
{% endhighlight %}
