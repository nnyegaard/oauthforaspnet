---
layout: provider
title:  Instagram
logo: instagram.png
links:
  - name: Instagram Home Page
    url: https://instagram.com/
  - name: Instagram Developer Home
    url: https://instagram.com/developer/
  - Registered Clients
    https://instagram.com/developer/clients/manage/
---

## 1. Register an application in Instagram

In the Instagram developer portal, [go to "Manage Clients" page](https://instagram.com/developer/clients/manage/) and click on the "Register a New Client" button. For the "OAuth redirect_uri" field you will need the specify the URL for your application with the `/signin-instagram` suffix, e.g. `http://localhost:4515/signin-instagram`. Complete the client registration information and when you are finished take note of the **Client ID** and **Client Secret** fields as you will need these when registering the Instagram OAuth provider in your ASP.NET application.

![](/images/instagram-client-id-and-secret.png)

## 2. Install the Nuget Package

Install the Nuget Package which contains the Instagram OAuth provider.

{% highlight bash %}
Install-Package Install-Package Owin.Security.Providers
{% endhighlight %}

## 3. Register Provider
 
Locate the file in your project called `\App_Start\Startup.Auth.cs`. Ensure that you have imported the `Owin.Security.Providers.Instagram` namespace:

{% highlight csharp %}
using Owin.Security.Providers.Instagram;
{% endhighlight %}

In the `ConfigureAuth` method add the following line of code:

{% highlight csharp %}
app.UseInstagramInAuthentication("Your client id", "Your client secret");
{% endhighlight %}

## 4. Advanced Configuration

### Request extra permissions

By default the Instagram provider will request the `basic` OAuth scope. If you want to request more permissions, you will need to add these to the `Scope` property:

{% highlight csharp %}
var options = new InstagramAuthenticationOptions()
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret"
};
options.Scope.Add("likes");
app.UseInstagramInAuthentication(options);
{% endhighlight %}

For more information on the available scopes, refer to the "Scope (Permissions)" section of the [Instagram Authentication documentation](https://instagram.com/developer/authentication/).

### Specify an alternative callback path

By default the Instagram provider will request Instagram to redirect to the path `/signin-instagram` after the user has signed in and granted permissions on Instagram. You can specify an alternative redirect URL:

{% highlight csharp %}
var options = new InstagramAuthenticationOptions()
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    CallbackPath = new PathString("/oauth-redirect/instagram")
};
app.UseInstagramInAuthentication(options);
{% endhighlight %}

You will also need to ensure that the Redirect URI you specified when registering your application in Instagram matches this value.

### Retrieve access token and other user information returned from Instagram

You can retrieve the user information returned from Instagram in the `OnAuthenticated` callback function which gets invoked after the user has authenticated with Instagram:

{% highlight csharp %}
var options = new InstagramAuthenticationOptions()
{
    ClientId = "Your client id",
    ClientSecret = "Your client secret",
    Provider = new InstagramAuthenticationProvider
    {
        OnAuthenticated = async context =>
        {
            // Retrieve the OAuth access token to store for subsequent API calls
            string accessToken = context.AccessToken;

            // Retrieve the username
            string userName = context.UserName;

            // Retrieve the user's full name
            string fullName = context.Name;

            // You can even retrieve the full JSON-serialized user
            var serializedUser = context.User;
        }
    }
};
app.UseInstagramInAuthentication(options);
{% endhighlight %}
