---
layout: guide
title:  "Walkthrough: Configuring Stack Exchange Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with StackExchange and allow users of your application to sign in with their StackExchange account, you will need to register an application in StackExchange. After you have registered the application you can use the **Client Id**, **Client Secret** and **Key** supplied by StackExchange to register the StackExchange authentication provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced StackExchange integration topics, but only covers OAuth signin with StackExchange.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/stackexchange/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/stackexchange/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual User Accounts". Click OK.

![](/images/guides/stackexchange/mvc5/aspnet-project-type-dialog.png)

## Registering your application in StackExchange

In order for your application to use StackExchange as a login mechanism you will need to create an application on the StackExchange API (Stack Apps) website. Go to the [StackExchange API Website](http://api.stackexchange.com/). 

Click on the "Register for An App Key" link.

![](/images/guides/stackexchange/mvc5/stackexchange-api-website.png)

If you are not yet signed in to StackExhange network you will be prompted to sign in. If this is the first time you register on the Stack Apps you will be asked to confirm the creation of your account on Stack Apps.

Next you will need to supply the details of your application. Enter the name of your application as well as a description. The OAuth domain needs to be the domain to which the post-authentication OAuth callback is made, so for local development this should be `localhost`.

Finally ensure that the "Enable Client Side OAuth Flow" checkbox is left unticked and click on the "Register Your Application" button.

![](/images/guides/stackexchange/mvc5/register-app.png)

After the application has been created a screen will be displayed with the details your need when enabling the StackExhange authentication in ASP.NET MVC. Take note of the "Client Id", "Client Secret" and "Key" fields.

![](/images/guides/stackexchange/mvc5/register-app-success.png)

## Enabling StackExchange authentication in your ASP.NET MVC Application

You need to install the **Owin.Security.Providers** Nuget package which contains the StackExchange authentication provider.  Right click on your ASP.NET web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/stackexchange/mvc5/nuget-package-dialog.png)

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/stackexchange/mvc5/solution-explorer-startup-auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.StackExchange;
{% endhighlight %}

Enable the StackExchange provider by making a call to the `app.UseStackExchangeAuthentication` method passing in the Client ID of your StackExchange application as the `clientId` parameter, the Client Secret as the `clientSecret` parameter and the Key as the `key` parameter.

{% highlight csharp %}
app.UseStackExchangeAuthentication(
    clientId: "4457", 
    clientSecret: "WMAEuXft*cxT61E)exUtWw((", 
    key: "fwdkzo)jTjT)dYP5IiwULg((");
{% endhighlight %}

Make sure that the values you pass in for the `clientId`, `clientSecret` and `key` parameters are **exactly** the same as the values which were supplied by StackExchange when registering the application.

## Testing the application

You have now created an application in StackExchange and enabled the StackExchange authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/stackexchange/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with StackExchange.  Click the "StackExchange" button.

![](/images/guides/stackexchange/mvc5/application-login-screen.png)

You will be redirected to the StackExchange website.  If you are not logged in to StackExchange yet you will be prompted to do so.  StackExchange will then prompt you to give the application permissions to access your data.

![](/images/guides/stackexchange/mvc5/authorize-application.png)

Click on the "Approve" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/stackexchange/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your StackExchange account in the future.

## Launching to Production

When launching your application into production, you will need to create a new application in Stack Apps, with the OAuth Domain set to the domain for your production application.