---
layout: guide
title:  "Walkthrough: Configuring an OpenID Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

A lot of applications these days allow users to sign in using their existing login credentials from a social networking service such as Facebook or Twitter.  This simplifies the login process as users do not have to remember multiple login credentials for various websites, and it also provides the application developer in turn access to certain demographical information from the user.

ASP.NET MVC 5 does not support signing in with OpenID as part of the standard external authentication services, but support for OpenID authentication is available through an open source NuGet package. This guide will help you through the process of enabling OpenID signin in your application in a step-by-step manner.

[OpenID](http://openid.net/) is an authentication protocol implemented by many organizations. In this guide we will use Orange as the OpenID authentication provider, so you will need to have a Orange account. If you do not have an account (accounts are reserved for clients) then you can use any other OpenID authentication provider.

> With the OpenID protocol you don't have to register your application and you can use any OpenID authentication provider.

## Enabling Orange OpenID authentication in your ASP.NET MVC Application

The next step is to add the OpenID login to your ASP.NET MVC application.  For this we will create a new ASP.NET MVC application using Visual Studio. Go to File > New > Project and select the template for a new "ASP.NET Web Application" and click "OK".

![](/images/guides/openid/mvc5/New_Project.png)

Next, select the MVC project template and ensure that the **authentication** method is set to "Individual User Accounts".  Click "OK".

![](/images/guides/openid/mvc5/New_ASP.NET_Project_WebApplication1.png)

> After the project wizard has completed I would advise you to update your NuGet packages before you proceed.  To do this you can right click on the solution file and select "Manage NuGet Packages for Solution...".  In the "Manage Nuget Packages" dialog you can navigate to the Updates node and ensure that you install any updates.

Next we need to install the **Owin.Security.Providers** Nuget package which will give us access to the OpenID authentication provider.  Right click on your web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/openid/mvc5/Manage_NuGet_Packages.png)

> The **Owin.Security.Providers** Nuget package was developed by Jerrie Pelser with contributions from others, myself included. All contributions are appreciated, if you want to add extra functionality to any of the providers or add new providers for other services. Please fork the repository located at [https://github.com/owin-middleware/OwinOAuthProviders](https://github.com/owin-middleware/OwinOAuthProviders) and send a pull request.

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/openid/mvc5/SolutionExplorer.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.OpenID;
{% endhighlight %}

Enable the Orange OpenID provider by making a call to the `app.UseOpenIDAuthentication` method:

{% highlight csharp %}
app.UseOpenIDAuthentication("http://orange.fr", "Orange");
{% endhighlight %}

The first parameter is the url of the OpenID authentication provider, usually the home page. The second is the name displayed on the "Login" page of the application.
If the OpenID authentication provider have issues in the discovery part of the protocol you can directly specified the OpenID login url instead of the discovery one:

{% highlight csharp %}
app.UseOpenIDAuthentication("http://openid.orange.fr/server", "Orange", true);
{% endhighlight %}

## Testing the application

You have now enabled the Orange OpenID authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/github/mvc5/GoToLoginPage.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Orange.  Click the "Orange" button.

![](/images/guides/openid/mvc5/LoginWithOrangeOpenID.png)

You will be redirected to the Orange website.  If you are not logged in to Orange yet you will be prompted to do so.
You can see which account is used to authenticate as well as the data send to your application.
Click on the "Validate" button.  
![](/images/guides/openid/mvc5/OrangeOpenID_Sign_In.png)

You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/openid/mvc5/Complete_Registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Orange account in the future.
