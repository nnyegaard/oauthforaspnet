---
layout: guide
title:  "Walkthrough: Configuring Facebook Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with Facebook and allow users of your application to sign in with their Facebook account, you will need to register an application in the Facebook Developer Portal. After you have registered the application you can use the `App ID` and `App Secret` supplied by Facebook to register the Facebook social login provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced Facebook integration topics, but only covers OAuth signin with Facebook.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/facebook/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/facebook/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual Accounts". Click OK.

![](/images/guides/facebook/mvc5/aspnet-project-type-dialog.png)

After the project has been created, go to the web application's properties dialog and take note of the "Project Url", as you will need this when specifying the OAuth callback URL in Facebook.

![](/images/guides/facebook/mvc5/project-properties.png)

## Registering your application

To register a new Facebook application head over to the [Facebook Developers website](https://developers.facebook.com) and select "Add a New App" from the Apps menu at the top of the page.

> If you have not registered as a Facebook developer before, you will be prompted to go through the developer registration process before you can add a new application.

![](/images/guides/facebook/mvc5/add-new-app-menu.png)

You will be prompted to select the platform for which you want to create an application. Select "advanced setup".

![](/images/guides/facebook/mvc5/platform-type-selection.png)

Complete the information on the "Create a New App ID" dialog by supplying the name of your application and optionally a namespace, as well as selecting the relevant Category. Click the Create App ID button.

![](/images/guides/facebook/mvc5/create-new-app-id.png)

After the application is created you will need to ensure that your application is enabled for OAuth (which it is by default), and also specify the correct OAuth Redirect URI.

Click on the Settings menu on the sidebar and go to Advanced tab.

![](/images/guides/facebook/mvc5/settings-advanced-tab.png)

Go to the Security section of the page and make sure that "Client OAuth Login" is enabled. You will also need to specify a valid URI. This will be the URL for you application, with the `/signin-facebook` suffix. For you local development environment, this will be something like `http://localhost:4515/signin-facebook`.

![](/images/guides/facebook/mvc5/oauth-settings.png)

Next you will need to get the App ID and App Secret so you can register the Facebook provider in your ASP.NET application. Go back to the Facebook application's dashboard and copy the values of the App ID and App Secret. (You will first need to click on the "Show" button in the App Secret field to display the value of the App Secret).

![](/images/guides/facebook/mvc5/app-id-and-secret.png)

## Enabling Facebook authentication in your ASP.NET MVC Application

Go back to your ASP.NET application and navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/facebook/mvc5/solution-explorer-startup-auth.png)

In the `ConfigureAuth` method, below all the existing code add the following line of code:

{% highlight csharp %}
app.UseFacebookAuthentication("1605576099672491", "a3bd8e42104c3ec7d2260b831f3ef51f");
{% endhighlight %}

Make sure that the values you pass in for the `appId` and `appSecret` parameters are **exactly** the same as the values which were supplied by Facebook when registering the application.

> The Facebook OAuth provider is located in the `Microsoft.Owin.Security.Facebook` Nuget package, which is added by default in the new ASP.NET project template. If for some reason this is not added to your project you can do so by installing it using the Nuget dialog or Package Manager Console

## Testing the application

You have now created an application in Facebook and enabled the Facebook authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/facebook/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Facebook.  Click the "Facebook" button.

![](/images/guides/facebook/mvc5/application-login-screen.png)

You will be redirected to the Facebook website.  If you are not logged in to Facebook yet you will be prompted to do so.  Facebook will then prompt you to give the application permissions to access your public profile.

![](/images/guides/facebook/mvc5/facebook-permission.png)

Click on the "Okay" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/facebook/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Facebook account in the future.

## A final note about the Facebook application registration and review process

As noted earlier in the application, you will need to supply Facebook with the redirect URI for your application. You can enter multiple URIs, for example one for development and one for production, but this is not advised. I will suggest that you rather register two separate applications in Facebook; one for production and one for development, each with their own settings and a different **App ID** and **App Secret**. 

This approach is also better from a security standpoint as you can limit the number of people who have access to the **App ID** and **App Secret** since you do not have to give it to all your developers, or have it checked into source control.

Facebook also has another way to manage this, by allowing you to add test applications. On your application's settings page, on the left navigation bar, navigate to the "Test Apps" menu item. From there you can add test applications, for development or testing purposes for example. Each test application has its own separate App ID, App Secret and redirect URI.

### And a final, very important note...

One final thing to take note of is that before you can make you application live (so other people can log in with it), it will have to go through a review process by Facebook. For more information on how this works, you can read the [Login Review](https://developers.facebook.com/docs/apps/review/login) document on the Facebook website.