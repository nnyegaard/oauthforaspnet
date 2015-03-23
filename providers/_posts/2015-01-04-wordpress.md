---
layout: provider
title:  WordPress  
logo: wordpress.png
links:
  - name: WordPress Home Page
    url: https://www.wordpress.com
  - name: WordPress Developer Home
    url: https://developer.wordpress.com
  - name: Registered Applications
    url: https://developer.wordpress.com/apps
---

## 1. Register an application in WordPress

Go to the [WordPress Registered Applications page](https://developer.wordpress.com/apps) and click on the "Create New Application" button. For the **Redirect URL** use the URL of your application with the suffix `/signin-wordpress`, for example `http://localhost:4515/signin-wordpress`. After you completed the application registration, take note of the **Client ID** and **Client secret** as you will need those when configuring the WordPress provider in your ASP.NET application:

![](/images/wordpress-client-id-and-secret.png)

## 2. Install the Nuget Package

Install the Nuget Package which contains the WordPress OAuth provider.

{% highlight bash %}
Install-Package Install-Package Owin.Security.Providers
{% endhighlight %}

## 3. Register Provider
 
Locate the file in your project called `\App_Start\Startup.Auth.cs`. Ensure that you have imported the `Owin.Security.Providers.WordPress` namespace:

{% highlight csharp %}
using Owin.Security.Providers.WordPress;
{% endhighlight %}

In the `ConfigureAuth` method add the following line of code:

{% highlight csharp %}
app.UseWordPressAuthentication("Your client id", "Your client secret");
{% endhighlight %}

## 4. Advanced Configuration

### Specify an alternative callback path

By default the WordPress provider will request WordPress to redirect to the path `/signin-wordpress` after the user has signed in and granted permissions on WordPress. You can specify an alternative callback path:

{% highlight csharp %}
var options = new WordPressAuthenticationOptions
{
    ClientId = "Your client id", 
    ClientSecret = "Your client secret",
    CallbackPath = new PathString("/oauth-redirect/wordpress")
};
app.UseWordPressAuthentication(options);
{% endhighlight %}

> When you specify an alternative callback path you must ensure that the **Redirect URL** you specified when registering the application in WordPress matches this path.

### Retrieve access token and other user information returned from WordPress

You can retrieve the user information returned from WordPress in the `OnAuthenticated` callback function which gets invoked after the user has authenticated with WordPress:

{% highlight csharp %}
var options = new WordPressAuthenticationOptions
{
    ClientId = "Your client id", 
    ClientSecret = "Your client secret",
    Provider = new WordPressAuthenticationProvider
    {
        OnAuthenticated = async context =>
        {
            // Retrieve the OAuth access token to store for subsequent API calls
            string accessToken = context.AccessToken;

            // Retrieve the user's name
            string userName = context.Name;

            // Retrieve the user's email address

            // Retrieve the blog details
            var blogId = context.BlogId;
            var blogName = context.BlogName;
            var blogUrl = context.BlogUrl;

            // You can even retrieve the full JSON-serialized user
            var serializedUser = context.User;
        }
    }
};
app.UseWordPressAuthentication(options);
{% endhighlight %}
