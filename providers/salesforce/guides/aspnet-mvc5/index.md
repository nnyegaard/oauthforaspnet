---
layout: guide
title:  "Walkthrough: Configuring SalesForce Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with Salesforce and allow users of your application to sign in with their Salesforce account, you will need to register an application in Salesforce. After you have registered the application you can use the **Consumer Key** and **Consumer Secret** supplied by Salesforce to register the Salesforce social login provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced Salesforce integration topics, but only covers OAuth signin with Salesforce.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/salesforce/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/salesforce/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual User Accounts". Click OK.

![](/images/guides/salesforce/mvc5/aspnet-project-type-dialog.png)

After the project has been created, go to the web application's properties dialog and take note of the "Project Url", as you will need this when specifying the Callback URL in Salesforce.

![](/images/guides/salesforce/mvc5/project-properties.png)

## Registering an application in SalesForce

In order for your application to use SalesForce as a login mechanism you will need to create an application in SalesForce.  To do this, log in to SalesForce and go to the Setup area.

![](/images/guides/salesforce/mvc5/salesforce-select-setup-menu.png)

In the navigation pane on the left hand side, navigate to Build > Create and click on the Apps link.  

![](/images/guides/salesforce/mvc5/salesforce-create-apps-menu.png)

Scroll down to the Connected Apps section and click on the New button.

![](/images/guides/salesforce/mvc5/salesforce-connected-apps.png)

Supply the Connected App Name, API Name and Contact Email.

![](/images/guides/salesforce/mvc5/salesforce-new-app-1.png)

Select "Enable OAuth Settings" and enter the Callback URL for your application and select the "Access your Basic Information" scope. The Callback URL should consist of the URL for your website, with the suffix `/signin-salesforce`, e.g. `http://localhost:4515/signin-salesforce`.  

![](/images/guides/salesforce/mvc5/salesforce-new-app-2.png)

Finally scroll down and click on the Save button. You will get a notification stating that the changes will take a few minutes to take effect. Click the Continue button.

![](/images/guides/salesforce/mvc5/salesforce-new-app-3.png)

A screen will be displayed with the details your need when enabling the SalesForce authentication in ASP.NET MVC. Take note of the "Consumer Key" and "Consumer Secret" fields.

![](/images/guides/salesforce/mvc5/salesforce-new-app-success.png)

## Enabling Salesforce authentication in your ASP.NET MVC Application

Next you need to install the **Owin.Security.Providers** Nuget package which contains the Salesforce authentication provider.  Right click on your ASP.NET web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/salesforce/mvc5/nuget-package-dialog.png)

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/salesforce/mvc5/solution-explorer-startup-auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.Salesforce;
{% endhighlight %}

For most OAuth providers you do not have to worry about setting the authentication endpoints, but for Salesforce the endpoints are going to be dependent on the Salesforce *instance* your are running on. To get the instance name, simply look at the subdomain of your salesforce URL:

![](/images/guides/salesforce/mvc5/salesforce-instance.png)

In my case the instance name is `ap1`, meaning that I am running on one of the servers in the Asia Pacific region. When registering the Salesforce authentication provider, your `AuthorizationEndpoint` and `TokenEndpoint` are going to be in the following format:

{% highlight csharp %}
AuthorizationEndpoint = "https://<instance_name>.salesforce.com/services/oauth2/authorize"
TokenEndpoint = "https://<instance_name>.salesforce.com/services/oauth2/token"
{% endhighlight %}

So to register the Salesforce provider you have to construct an instance of the `SalesforceAuthenticationOptions` class, specifiying the Consumer Key of your Salesforce application as the `ClientId` property and the Consumer Secret as the `ClientSecret` property. You will also need to set the `Endpoints` property to the correct endpoint URLs as described above.

Finally, make a call to the `app.SalesforceAuthenticationOptions` method passing in the newly constucted instance of the `SalesforceAuthenticationOptions` class:

{% highlight csharp %}
var options = new SalesforceAuthenticationOptions
{
    ClientId = "3MVG9Y6d_Btp4xp53rplRBM7p1bBinIiO99QrKFzN9aYd7HO3MchvSUhNrF0jeCayyiXTNMddk.azWta9oWRD",
    ClientSecret = "2319362822370818262",
    Endpoints = new SalesforceAuthenticationOptions.SalesforceAuthenticationEndpoints
    {
        AuthorizationEndpoint = "https://ap1.salesforce.com/services/oauth2/authorize",
        TokenEndpoint = "https://ap1.salesforce.com/services/oauth2/token"
    }
};
app.UseSalesforceAuthentication(options);
{% endhighlight %}

## Testing the application

You have now created an application in Salesforce and enabled the Salesforce authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/salesforce/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Salesforce.  Click the "Salesforce" button.

![](/images/guides/salesforce/mvc5/application-login-screen.png)

You will be redirected to the Salesforce website.  If you are not logged in to Salesforce yet you will be prompted to do so.  Salesforce will then prompt you to give the application permissions to access your personal basic information.

![](/images/guides/salesforce/mvc5/salesforce-permissions-window.png)

Click on the "Allow" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/salesforce/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Salesforce account in the future.