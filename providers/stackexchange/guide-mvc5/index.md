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
---
## Introduction
A lot of applications these days allow users to sign in using their existing login credentials from a social networking service such as Facebook and Twitter.  This simplifies the login process as users do not have to remember multiple login credentials for various websites, and it also provides the application developer in turn access to certain demographical information from the user.

ASP.NET MVC 5 has support for social logins built in, but as an app developer you will still need to go trough a few steps to enable this on your application.  This guide will help you through the process of allowing users to log in with their StackExchange account in a step-by-step manner.

To follow this guide you will need to have a StackExchange account.  If you do not have an account then head on over to the [StackExchange Homepage](http://stackexchange.com/) and register before you continue any further.

## Registering an application in StackExchange
In order for your application to use StackExchange as a login mechanism you will need to create an application on the StackExchange API (Stack Apps) website.  To do this head over to the [StackExchange API Website](http://api.stackexchange.com/). 

Click on the "Register for An App Key" link.

![](/images/guides/stackexchange/stackexchange-api-website.png)

If you are not yet signed in to StackExhange network you will be prompted to sign in:

![](/images/guides/stackexchange/stackapps-auth.png)

If this is the first time you register on the Stack Apps website, you will be asked to confirm the creation of your account on Stack Apps. Click the "Confirm And Create This Account" button.

![](/images/guides/stackexchange/stackapps-create-account.png)

The next screen will prompt you to supply the details of your application.

![](/images/guides/stackexchange/register-app.png)

Enter the name of your application as well as a description. The OAuth domain needs to be the domain to which the post-authentication OAuth callback is made, so for local development this should be `localhost`. Once you deploy your application it is suggested that you register a separate application on the Stack Apps website with the correct domain for where you application is being deployed.

Finally ensure that the "Enable Client Side OAuth Flow" checkbox is left unticked and click on the "Register Your Application" button.

After the application has been created a screen will be displayed with the details your need when enabling the StackExhange authentication in ASP.NET MVC. Take note of the "Client Id", "Client Secret" and "Key" fields.

![](/images/guides/stackexchange/register-app-success.png)

## Enabling StackExchange authentication in your ASP.NET MVC Application
The next step is to add the StackExchange login to your ASP.NET MVC application.  For this we will create a new ASP.NET MVC application using Visual Studio. Go to File > New > Project and select the template for a new "ASP.NET Web Application" and click "OK".

![](/images/guides/stackexchange/new_project.png)

Next, select the MVC project template and ensure that the **authentication** method is set to "Individual User Accounts".  Click "OK".

![](/images/guides/stackexchange/new_project_mvc.png)

> After the project wizard has completed I would advise you to update your NuGet packages before you proceed.  To do this you can right click on the solution file and select "Manage NuGet Packages for Solution...".  In the "Manage Nuget Packages" dialog you can navigate to the Updates node and ensure that you install any updates.

Next we need to install the **Owin.Security.Providers** Nuget package which will give us access to the StackExchange authentication provider.  Right click on your web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/stackexchange/nuget_package_dialog.png)

> The **Owin.Security.Providers** Nuget package was developed by myself with contributions from others.  If you want to add extra functionality to any of the providers or add new providers for other services I would appreciate the contributions.  Please fork the repository located at [https://github.com/owin-middleware/OwinOAuthProviders](https://github.com/owin-middleware/OwinOAuthProviders) and send a pull request.

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/stackexchange/navigate_startup_auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.StackExchange;
{% endhighlight %}

Enable the StackExchange provider by making a call to the `app.UseStackExchangeAuthentication` method passing in the Client ID of your StackExchange application as the `clientId` parameter, the Client Secret as the `clientSecret` parameter and the Key as the `key` parameter.

{% highlight csharp %}
app.UseStackExchangeAuthentication(
    clientId: "3272",
    clientSecret: "Zxcsu0KwTsDizsv72AZdZA((",
    key: "r8LkgkG)rRAuCEit1jWwPw((");
{% endhighlight %}

It is important to ensure that these parameters match the values from StackExchange exactly, otherwise the authentication for your application will fail.


![](/images/guides/stackexchange/keys-matchup.png)

## Testing the application
You have now created an application in StackExchange and enabled the StackExchange authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/stackexchange/application_start_screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with StackExchange.  Click the "StackExchange" button.

![](/images/guides/stackexchange/application_login_screen.png)

You will be redirected to the StackExchange website.  If you are not logged in to StackExchange yet you will be prompted to do so.  StackExchange will then prompt you to give the application access to your account.

![](/images/guides/stackexchange/stackexchange_auth_screen.png)

Click on the "Approve" button. You will be redirected back to your application and will need to supply your username to complete the registration process.

![](/images/guides/stackexchange/complete_registration.png)

Once you have filled in your username and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your StackExchange account in the future.

