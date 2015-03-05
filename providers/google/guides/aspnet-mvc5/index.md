---
layout: guide
title:  "Walkthrough: Configuring Google Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with Google and allow users of your application to sign in with their Google account, you will need to register an application in the [Google Developers Console](https://console.developers.google.com). After you have registered the application you can use the `Client ID` and `Client Secret` supplied by Google to register the Google social login provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced Google integration topics, but only covers OAuth signin with Google.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/google/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/google/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual Accounts". Click OK.

![](/images/guides/google/mvc5/aspnet-project-type-dialog.png)

After the project has been created, go to the web application's properties dialog and take note of the "Project Url", as you will need this when specifying the Redirect URI in Google.

![](/images/guides/google/mvc5/project-properties.png)

## Create a new Project in the Google Developers Portal

In order for you to use Google as an external authentication provider in your website, you will need to create a project in the Google Developers Console.  Head on over to the [Google Developers Console](https://console.developers.google.com).  

You will see a list of your current projects, if any.  Click on the "Create Project" button.

![](/images/guides/google/mvc5/google-console-projects.png)

In the "New Project" dialog, give you project a name and ID and click on the "Create" button.

![](/images/guides/google/mvc5/google-new-project-dialog.png)

It will take a few moments to create the new project. While the project is busy being created you will see an activities window at the bottom. 

![](/images/guides/google/mvc5/google-activities-window.png)

Once the activity has completed you can refresh the page if it does not do so automatically. Select the newly created project by clicking on the name of the project. 

![](/images/guides/google/mvc5/google-project-list.png)

For the Google OAuth provider provider to work correctly you will need to enable the Google+ API. Click on "APIs & auth" menu in the sidebar and select "APIs". Scroll down to the item called "Google+ API" and make sure that it is turned on. If it is not turned on, click on the button labelled "OFF" to the right of it to turn it on.

![](/images/guides/google/mvc5/google-apis-screen.png)

Next click on the "Consent screen" item in the sidebar. Select the email address and specify a product name. You can also add any optional information such as your product logo. When you are done click on the Save button.

![](/images/guides/google/mvc5/google-consent-screen-settings.png)

In the sidebar select the "Credentials" menu and click on the "Create new Client ID" button.

![](/images/guides/google/mvc5/google-credentials-screen.png)

Ensure that "Web Application" is selected as the application type.  We will need to specify the authorized redirect URI, but for now you can leave both the "Authorized JavaScript origins" and the "Authorized redirect URI" fields blank.  Click on the "Create Client ID" button.

![](/images/guides/google/mvc5/google-create-client-id.png)

You will be displayed summary information about the new Client ID which you created.  Take note of the "Client ID" and "Client Secret" fields as these will be needed when we enable the Google+ authentication in our ASP.NET MVC application.

![](/images/guides/google/mvc5/client-id-and-secret.png)

## Enabling Google authentication in your ASP.NET MVC Application

Go back to your ASP.NET application and navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/google/mvc5/solution-explorer-startup-auth.png)

In the `ConfigureAuth` method, below all the existing code add the following lines of code:

{% highlight csharp %}
app.UseGoogleAuthentication(
	"1065389806317-uk5hjimnoq865oqdpg97que31rktns0c.apps.googleusercontent.com", 
	"HyQqtT7KS6QNEqukZGPsNX0M");
{% endhighlight %}

Make sure that the values you pass in for the `clientId` and `clientSecret` parameters are **exactly** the same as the values which were supplied by Google when registering the application.

> The Google OAuth provider is located in the `Microsoft.Owin.Security.Google` Nuget package, which is added by default in the new ASP.NET project template. If for some reason this is not added to your project you can do so by installing it using the Nuget dialog or Package Manager Console

## Testing the application
You have now created a project in the Google Developer Console and enabled the Google+ authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/google/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Google+.  Click the "GooglePlus" button.

![](/images/guides/google/mvc5/application-login-screen.png)

You will be redirected to the Google website.  If you are not logged in to Google yet you will be prompted to do so.  Google will then prompt you to give the application permissions to access your personal user data.

![](/images/guides/google/mvc5/google-permission.png)

Click on the "Accept" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/google/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Google account in the future.