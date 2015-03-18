---
layout: provider
title:  SalesForce
logo: salesforce.png
links: 
  - name: SalesForce Home Page
    url: http://www.salesforce.com/
  - name: SalesForce Developer Page
    url: https://developer.salesforce.com/
sdks:
  - name: Web Services API
    url: https://developer.salesforce.com/page/Web_Services_API
  - name: Force.com Toolkit for Azure
    url: https://developer.salesforce.com/page/Getting_Started_with_the_Force.com_Toolkit_for_Azure
guide: /salesforce/guides/aspnet-mvc5
---

## 1. Register an application in Salesforce

Log in to Salesforce and go the the Setup area. In the navigation pane on the left hand side, navigate to Build > Create and click on the Apps link. Under the Connected Apps section, create a new application. Complete the application registration information and when you are finished take note of the Consumer Key and Consumer Secret:

![](/images/salesforce-consumer-key-and-secret.png)

## 2. Install the Nuget Package

Install the Nuget Package which contains the Salesforce OAuth provider.

{% highlight bash %}
Install-Package Install-Package Owin.Security.Providers
{% endhighlight %}

## 3. Register Provider
 
Locate the file in your project called `\App_Start\Startup.Auth.cs`. Ensure that you have imported the `Owin.Security.Providers.Salesforce` namespace:

{% highlight csharp %}
using Owin.Security.Providers.Salesforce;
{% endhighlight %}

When registering the Salesforce provider, you will need to specify the authorizaton and token endpoints yourself, as these will differ based on the instance which you are running on. The Salesforce instance name is the first part of the URL of your Salesforce application:

![](/images/salesforce-instance.png)

When specifying the endpoints they need to be in the following format:

{% highlight csharp %}
AuthorizationEndpoint = "https://<instance name>.salesforce.com/services/oauth2/authorize"
TokenEndpoint = "https://<instance name>.salesforce.com/services/oauth2/token"
{% endhighlight %}

Once you have the instance name, you can register the provider by adding the following lines of code in the `ConfigureAuth` method:

{% highlight csharp %}
var options = new SalesforceAuthenticationOptions
{
    ClientId = "Your consumer key",
    ClientSecret = "Your consumer secret",
    Endpoints = new SalesforceAuthenticationOptions.SalesforceAuthenticationEndpoints
    {
        AuthorizationEndpoint = "https://ap1.salesforce.com/services/oauth2/authorize",
        TokenEndpoint = "https://ap1.salesforce.com/services/oauth2/token"
    }
};
app.UseSalesforceAuthentication(options);
{% endhighlight %}

In the example above the registration was done for a Salesforce application running on the instance `ap1`, but the instance name will most probably be different for you.

## 4. Advanced Configuration

### Request extra permissions

Requesting extra permissions cannot be done through runtime via code. You will notice that the `SalesforceAuthenticationOptions` class does have a `Scope` property, but setting this property has no effect.

Permissions requested by your application needs the be specified when registering the application in Salesforce:

![](/images/salesforce-scopes.png)

### Retrieve access token and other user information returned from Salesforce

You can retrieve the user information returned from Salesforce in the `OnAuthenticated` callback function which gets invoked after the user has authenticated with Salesforce:

{% highlight csharp %}
var options = new SalesforceAuthenticationOptions
{
    ClientId = "Your consumer key",
    ClientSecret = "Your consumer secret",
    Endpoints = new SalesforceAuthenticationOptions.SalesforceAuthenticationEndpoints
    {
        AuthorizationEndpoint = "https://<instance name>.salesforce.com/services/oauth2/authorize",
        TokenEndpoint = "https://<instance name>.salesforce.com/services/oauth2/token"
    },
    Provider = new SalesforceAuthenticationProvider
    {
        OnAuthenticated = async context =>
        {
            // Retrieve the OAuth access token to store for subsequent API calls
            string accessToken = context.AccessToken;

            // Retrieve the user ID
            string salesforceUserId = context.UserId;

            // Retrieve the user's Organization ID
            string salesforceOrganizationId = context.OrganizationId;

            // Retrieve the user's full name
            string salesforceFullName = context.DisplayName;

            // You can even retrieve the full JSON-serialized user
            var serializedUser = context.User;
        }
    }
};
app.UseSalesforceAuthentication(options);
{% endhighlight %}
