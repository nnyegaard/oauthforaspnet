---
layout: guide
title:  "Walkthrough: Configuring GitHub Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with GitHub and allow users of your application to sign in with their GitHub account, you will need to register an application in GitHub. After you have registered the application you can use the **Client ID** and **Client Secret** supplied by GitHub to register the GitHub social login provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced GitHub integration topics, but only covers OAuth signin with GitHub.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/github/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/github/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual Accounts". Click OK.

![](/images/guides/github/mvc5/aspnet-project-type-dialog.png)

After the project has been created, go to the web application's properties dialog and take note of the "Project Url", as you will need this when specifying the Authorization callback URL in GitHub.

![](/images/guides/github/mvc5/project-properties.png)

## Registering an application in GitHub
In order for you to use GitHub as an OAuth authentication provider in your website, you will need to create an application in GitHub.  Sign in to your GitHub account and navigate to your Account settings by clicking on the Settings icon on the top right of the GitHub website.

![](/images/guides/github/mvc5/account-settings-menu.png)

Navigate to the Applications section by selection "Applications" in the Personal Settings sidebar menu.

![](/images/guides/github/mvc5/account-settings-sidebar.png)

In the "Developer applications" section click on the "Register new application" button.

![](/images/guides/github/mvc5/developer-applications-section.png)

Complete the name and homepage URL fields for your application. The **Authorization callback URL** is optional, but if you specify it, it must be the URL for you application, with the `/signin-github` suffix. For you local development environment, this will be something like `http://localhost:4515/signin-github`. After you have completed all the information, click the "Register application" button.

![](/images/guides/github/mvc5/application-registration.png)

After the application registration is complete your will be redirected to the application page.  Take note of the "Client ID" and "Client Secret" as these will be needed when enabling the GitHub authentication in your ASP.NET MVC application.

![](/images/guides/github/mvc5/application-keys.png)

## Enabling GitHub authentication in your ASP.NET MVC Application

Next you need to install the **Owin.Security.Providers** Nuget package which contains the GitHub authentication provider.  Right click on your ASP.NET web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/github/mvc5/nuget-package-dialog.png)

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/github/mvc5/solution-explorer-startup-auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.GitHub;
{% endhighlight %}

Enable the GitHub provider by making a call to the `app.UseGitHubAuthentication` method passing in the Client ID of your GitHub application as the `clientId` parameter and the Client Secret as the `clientSecret` parameter.

{% highlight csharp %}
app.UseGitHubAuthentication(
    clientId: "bd06235dfad1d39835b4", 
    clientSecret: "c6380b026dd4a120adeec21e11df26d5e1be296a");
{% endhighlight %}

Make sure that the values you pass in for the `clientId` and `clientSecret` parameters are **exactly** the same as the values which were supplied by GitHub when registering the application.

## Testing the application
You have now created an application in GitHub and enabled the GitHub authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/github/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with GitHub.  Click the "GitHub" button.

![](/images/guides/github/mvc5/application-login-screen.png)

You will be redirected to the GitHub website.  If you are not logged in to GitHub yet you will be prompted to do so.  GitHub will then prompt you to give the application permissions to access your personal user data.

![](/images/guides/github/mvc5/authorize-application.png)

Click on the "Authorize application" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/github/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your GitHub account in the future.

## A final note about the GitHub application registration process

You are only allowed to specify one **Authorization callback URL** per application in GitHub, which means that you will not be able to use the same application in both your development and productions environments. So in a typical scenario where you have development, production and other environments such as testing, you will need to register a separate application in GitHub if you want to specify a callback URL.

Doing it this way is in any case much better for security purposes as well, as you can limit the people who have knowledge of the Client ID and Secret of the production environment to a much smaller group.

