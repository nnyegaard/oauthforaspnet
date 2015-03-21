---
layout: provider
title:  Microsoft
logo: microsoft.png
links:
  - name: Microsoft Account Developer Center Applications
    url: https://account.live.com/developers/applications
  - name: Scopes and Permissions
    url: https://msdn.microsoft.com/en-us/library/hh243646.aspx
guide: /providers/microsoft/guides/aspnet-mvc5
---
## 1. Register an app in the Microsot Account Developer Center

Go to the [Microsoft Account Developer Center](https://account.live.com/developers/applications) and create a new application. After you have registered the application take note of the App ID and App Secret:

![](/images/microsoft-client-id-and-secret.png)

## 2. Install the Nuget Package

Install the Nuget Package which contains the Microsoft OAuth provider.

{% highlight bash %}
Install-Package Microsoft.Owin.Security.MicrosoftAccount
{% endhighlight %}

## 3. Register Provider

Locate the file in your project called `\App_Start\Startup.Auth.cs`. Ensure that you have imported the `Owin` namespace:

{% highlight csharp %}
using Owin;
{% endhighlight %}

In the `ConfigureAuth` method add the following lines of code:

{% highlight csharp %}
app.UseMicrosoftAccountAuthentication(
    clientId: "Your client ID", 
    clientSecret: "Your client secret");
{% endhighlight %}

## 4. Advanced Configuration

To use the advanced configuration options, be sure to use the `Microsoft.Owin.Security.MicrosoftAccount` namespace:

{% highlight csharp %}
using Microsoft.Owin.Security.MicrosoftAccount;
{% endhighlight %}

### Request extra permissions

If no scope is specified, the Microsoft OAuth provider will request permissions for the `wl.basic` scope. If you would like to request any other scopes, your will need to pass these scopes in the `Scope` property. For example, to request the `wl.calendars` permission, you can register the Microsoft provider as per the following example:

{% highlight csharp %}
var options = new MicrosoftAccountAuthenticationOptions
{
    ClientId = "Your client ID",
    ClientSecret = "Your client secret",
};
options.Scope.Add("wl.calendars");
app.UseMicrosoftAccountAuthentication(options);
{% endhighlight %}

> For the full list of available permissions, see [Scopes and permissions](https://msdn.microsoft.com/en-us/library/hh243646.aspx) on the MSDN.

### Specify an alternative callback path

By default the Microsoft provider will request Microsoft to redirect to the path `/signin-microsoft` after the user has signed in and granted permissions on Microsoft. You can specify an alternative callback path:

{% highlight csharp %}
var options = new MicrosoftAccountAuthenticationOptions
{
    ClientId = "Your client ID",
    ClientSecret = "Your client secret",
    CallbackPath = new PathString("/oauth-redirect/microsoft")
};
app.UseMicrosoftAccountAuthentication(options);
{% endhighlight %}

You need to also make sure that the **Redirect URI** of your application in the Microsoft Account Developer Center matches this new callback path.

### Retrieve access token and other user information returned from Microsoft

You can retrieve the access token and other user information returned from Microsoft in the `OnAuthenticated` callback function which gets invoked after the user has authenticated with Microsoft:

{% highlight csharp %}
var options = new MicrosoftAccountAuthenticationOptions
{
    ClientId = "Your client ID",
    ClientSecret = "Your client secret",
    Provider = new MicrosoftAccountAuthenticationProvider
    {
        OnAuthenticated = async context =>
        {
            // Retrieve the OAuth access token to store for subsequent API calls
            string accessToken = context.AccessToken;

            // Retrieve the user ID
            string microsoftUserId = context.Id;

            // Retrieve the user's full name
            string microsoftFullName = context.Name;

            // You can even retrieve the full JSON-serialized user
            var serializedUser = context.User;
        }
    }
};
app.UseMicrosoftAccountAuthentication(options);
{% endhighlight %}
