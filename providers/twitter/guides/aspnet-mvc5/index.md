---
layout: guide
title:  "Walkthrough: Configuring Twitter Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with Twitter and allow users of your application to sign in with their Twitter account, you will need to register an application on the Twitter Developer website. After you have registered the application you can use the **Consumer Key** and **Consumer Secret** supplied by Twitter to register the Twitter authentication provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced Twitter integration topics, but only covers OAuth signin.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/twitter/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/twitter/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual Accounts". Click OK.

![](/images/guides/twitter/mvc5/aspnet-project-type-dialog.png)

## Registering an application in Twitter

To create an application in Twitter, you will need to head over to the [Application Management section](https://apps.twitter.com/) of the Twitter Developer Portal.  If you are not yet signed then sign in. 

Click on the "Create New App" button.

![](/images/guides/twitter/mvc5/twitter-apps-home.png)

Supply the name, description, website and callback URL for your application.  It is important to supply a URL for **Callback URL** field, otherwise the Twitter authentication will not work. 

The Callback URL does not have to be the exact URL to which the post-authentication callback will be made - the ASP.NET Identity system will ensure that the correct Callback URL is supplied at runtime.  It is however important to specify a Callback URL otherwise the Twitter authentication will not work.

![](/images/guides/twitter/mvc5/create-application-1.png)

Read and accept the Developer Agreement and click on the "Create your Twitter application" button.

![](/images/guides/twitter/mvc5/create-application-2.png)

After the application has been created, click on the Settings tab and check the "Allow this application to be used to Sign in with Twitter" checkbox.

![](/images/guides/twitter/mvc5/allow-sign-in.png)

Scroll down and click on the "Update Settings" button.

Next, go to "Keys and Access Tokens" tab and take note of the "API Key" and "API Secret" as these will be needed when enabling Twitter authentication in your ASP.NET MVC application.

![](/images/guides/twitter/mvc5/consumer-key-and-secret.png)

## Enabling Twitter authentication in your ASP.NET MVC Application

Go back to your ASP.NET application and navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/twitter/mvc5/solution-explorer-startup-auth.png)

In the `ConfigureAuth` method, below all the existing code add the following line of code:

{% highlight csharp %}
app.UseTwitterAuthentication(
    consumerKey: "mhxhd8hdakDCs4QlnyWNqRqUD", 
    consumerSecret: "lPjNTFPCDK3cbwaUdNo3mkpdNzXd8pYvfjul8rNMIw3wtSaLk1");
{% endhighlight %}

Make sure that the values you pass in for the `consumerKey` and `consumerSecret` parameters are **exactly** the same as the values which were supplied by Twitter when registering the application.

> The Twitter OAuth provider is located in the `Microsoft.Owin.Security.Twitter` Nuget package, which is added by default in the new ASP.NET project template. If for some reason this is not added to your project you can do so by installing it using the Nuget dialog or Package Manager Console

## Testing the application

You have now created an application in Twitter and enabled the Twitter authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/twitter/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Twitter.  Click the "Twitter" button.

![](/images/guides/twitter/mvc5/application-login-screen.png)

You will be redirected to the Twitter website.  If you are not logged in to Twitter yet you will be prompted to do so.  Twitter will then prompt you to Authorize the application permissions to use your account.

![](/images/guides/twitter/mvc5/twitter-authorize.png)

Click on the "Sign In" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/twitter/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Twitter account in the future.