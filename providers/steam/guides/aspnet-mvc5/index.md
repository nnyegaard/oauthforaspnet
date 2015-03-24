---
layout: guide
title:  "Walkthrough: Configuring a Steam Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

A lot of applications these days allow users to sign in using their existing login credentials from a social networking service such as Facebook or Twitter.  This simplifies the login process as users do not have to remember multiple login credentials for various websites, and it also provides the application developer in turn access to certain demographical information from the user.

ASP.NET MVC 5 has support for social logins built in, but as an app developer you will still need to go trough a few steps to enable this on your application.  If you develop an application which is geared towards gamers you may want to allow your users to sign in to your application using Steam.  

ASP.NET MVC 5 does not support signing in with Steam as part of the standard external authentication services, but support for Steam authentication is available through an open source NuGet package. This guide will help you through the process of enabling Steam signin in your application in a step-by-step manner.

To follow this guide you will need to have a Steam account.  If you do not have an account then head on over to the [Steam Login page](https://store.steampowered.com/join/) and register before you continue any further.

## Registering an application in Steam

In order for you to use Steam as an external authentication provider in your website, you will need to get an API key from Steam. Sign in to your Steam account and head over to [Steam Web API Key](https://steamcommunity.com/dev/apikey)


Complete the domain field for your application, check the "I agree to the Steam Web API Terms of Use" (and don't forget to read it) and click the "Register" button.

![](/images/guides/steam/mvc5/Steam_Web_API_Key_Register.png)

After the application registration is complete your will be redirected to the Web API Key page. Take note of the "Key" as it will be needed when enabling the Steam authentication in your ASP.NET MVC application.

![](/images/guides/steam/mvc5/Steam_Web_API_Key.png)

> You can only register one key, so be careful with it.

## Enabling Steam authentication in your ASP.NET MVC Application

The next step is to add the Steam login to your ASP.NET MVC application.  For this we will create a new ASP.NET MVC application using Visual Studio. Go to File > New > Project and select the template for a new ";ASP.NET Web Application" and click "OK".

![](/images/guides/steam/mvc5/New_Project.png)

Next, select the MVC project template and ensure that the **authentication** method is set to "Individual User Accounts". Click "OK".

![](/images/guides/steam/mvc5/New_ASP.NET_Project_WebApplication1.png)

> After the project wizard has completed I would advise you to update your NuGet packages before you proceed.  To do this you can right click on the solution file and select "Manage NuGet Packages for Solution...".  In the "Manage Nuget Packages" dialog you can navigate to the Updates node and ensure that you install any updates.

Next we need to install the **Owin.Security.Providers** Nuget package which will give us access to the Steam authentication provider.  Right click on your web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/steam/mvc5/Manage_NuGet_Packages.png)

> The **Owin.Security.Providers** Nuget package was developed by Jerrie Pelser with contributions from others, myself included. All contributions are appreciated, if you want to add extra functionality to any of the providers or add new providers for other services. Please fork the repository located at [https://github.com/owin-middleware/OwinOAuthProviders](https://github.com/owin-middleware/OwinOAuthProviders) and send a pull request.

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/steam/mvc5/SolutionExplorer.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.Steam;
{% endhighlight %}

Enable the Steam provider by making a call to the `app.UseSteamAuthentication` method passing in the Steam Web API Key.

{% highlight csharp %}
app.UseSteamAuthentication("E9BA9749873A4C3EA67B8CDB6BAC73A5");
{% endhighlight %}

It is important to ensure that the parameter match the values from Steam exactly, otherwise the Steam authentication for your application will fail.

## Testing the application

You have now registered a Web API Key in Steam and enabled the Steam authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/steam/mvc5/GoToLoginPage.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Steam.  Click the "Steam" button.

![](/images/guides/steam/mvc5/LoginWithSteam.png)

You will be redirected to the Steam website.  If you are not logged in to Steam yet you will be prompted to do so.
Click on the "Yes, Sign In" button.  
![](/images/guides/steam/mvc5/Steam_Sign_In.png)

You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/steam/mvc5/Complete_Registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Steam account in the future.
