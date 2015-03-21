---
layout: guide
title:  "Walkthrough: Configuring Yahoo Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with Yahoo and allow users of your application to sign in with their Yahoo account, you will need to register an application in Yahoo. After you have registered the application you can use the **API Key** and **Secret Key** supplied by Yahoo to register the Yahoo social login provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced Yahoo integration topics, but only covers OAuth signin with Yahoo.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/yahoo/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/yahoo/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual User Accounts". Click OK.

![](/images/guides/yahoo/mvc5/aspnet-project-type-dialog.png)

## Registering an application on Yahoo

To register an application, go to the [Yahoo Developer Network website](https://developer.yahoo.com/) and under the "My Apps" menu at the top right of the page select the YDN Apps option.

![](/images/guides/yahoo/mvc5/yahoo-apps-menu.png)

You will see a list of any existing applications you may have created before.  Click on the "Create an App" button.

![](/images/guides/yahoo/mvc5/yahoo-projects-list.png)

To register a web app in Yahoo you will need to specify a callback domain, but unfortunately you cannot specify `localhost` as the callback domain. Yahoo also validates that the domain is correct, so even if you register a domain other that `localhost`, when you test the application locally, the ASP.NET Identity runtime will specify the callback URL as being on the `localhost` domain, and Yahoo will not allow this. 

This means that you will run into some issues when wanting to register and test an application locally on you computer. There are 2 ways around this.

The first is to use a tool like [Ngrok](https://ngrok.com/) to tunnel traffic from a "proper" domain which is valid according to Yahoo, to your `localhost`. This requires reconfiguring IIS Express to recognize that domain, and also reconfiguring your project to use that new URL. There is a blog post on the Twilio website entitled [Configure Windows for Local Webhook Testing Using ngrok](https://www.twilio.com/blog/2014/03/configure-windows-for-local-webhook-testing-using-ngrok.html) which describes how to do it

The second (and easier) way is that when registering the application in Yahoo, you specify the application type as "Installed Application" instead of "Web Application". If you do this you do not need to have a callback domain for an installed application.

For the sake of simplicity I will use the second approach as it works just fine, and is much simpler. I will only suggest using this for local development. For your production website you need to register the application as a "Web Application" and specify a correct callback domain.

So specify a name for the application and select "Installed Application". Leave the rest of the fields blank.

![](/images/guides/yahoo/mvc5/yahoo-create-app-1.png)

> Just to reiterate: For your production application you will need to select "Web Application" and specify a valid callback domain.

Under API Permissions you will need to select at least one of the API permission for the authentication to work properly. I just selected Contacts. Click the Create App button.

![](/images/guides/yahoo/mvc5/yahoo-create-app-2.png)

After the application has been created you need to take note of the Consumer Key and Consumer Secret as these will be needed when you enable authentication via Yahoo in your ASP.NET MVC application.

![](/images/guides/yahoo/mvc5/yahoo-client-id-and-secret.png)

## Enabling Yahoo authentication in your ASP.NET MVC Application

Next you need to install the **Owin.Security.Providers** Nuget package which contains the Yahoo authentication provider.  Right click on your ASP.NET web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/yahoo/mvc5/nuget-package-dialog.png)

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/yahoo/mvc5/solution-explorer-startup-auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.Yahoo;
{% endhighlight %}

Enable the Yahoo provider by making a call to the `app.UseYahooAuthentication` method passing in the Consumer Key of your Yahoo application as the `consumerKey` parameter and the Consumer Secret as the `consumerSecret` parameter.

{% highlight csharp %}
app.UseYahooAuthentication(
	"dj0yJmk9Sk11TWNjNDVXcHltJmQ9WVdrOU5FSklZakJoTkRJbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD1mZg--",
	"47af0a79e80c98d03eda5270c4099c5b883b3459");
{% endhighlight %}

Make sure that the values you pass in for the `consumerKey` and `consumerSecret` parameters are **exactly** the same as the values which were supplied by Yahoo when registering the application.

## Testing the application

You have now created an application in Yahoo and enabled the Yahoo authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/yahoo/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Yahoo.  Click the "Yahoo" button.

![](/images/guides/yahoo/mvc5/application-login-screen.png)

You will be redirected to the Yahoo website.  If you are not logged in to Yahoo yet you will be prompted to do so.  Yahoo will then prompt you to give the application permissions to access your personal user data.

![](/images/guides/yahoo/mvc5/authorize-application.png)

Click on the "Agree" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/yahoo/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Yahoo account in the future.