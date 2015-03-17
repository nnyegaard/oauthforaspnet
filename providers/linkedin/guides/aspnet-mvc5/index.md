---
layout: guide
title:  "Walkthrough: Configuring LinkedIn Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with LinkedIn and allow users of your application to sign in with their LinkedIn account, you will need to register an application in LinkedIn. After you have registered the application you can use the **API Key** and **Secret Key** supplied by LinkedIn to register the LinkedIn social login provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced LinkedIn integration topics, but only covers OAuth signin with LinkedIn.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/linkedin/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/linkedin/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual User Accounts". Click OK.

![](/images/guides/linkedin/mvc5/aspnet-project-type-dialog.png)

After the project has been created, go to the web application's properties dialog and take note of the "Project Url", as you will need this when specifying the Redirect URL in LinkedIn.

![](/images/guides/linkedin/mvc5/project-properties.png)

## Registering your application in LinkedIn

In order for you to use LinkedIn as an external authentication provider in your website, you will need to create an application in LinkedIn.  Go the [LinkedIn Developer page](https://developer.linkedin.com) and select "My Apps" from the Support menu at the top of the page.

![](/images/guides/linkedin//mvc5/linkedin-my-apps-menu.png)

You will see the list of your existing LinkedIn applications (if any). Click the "Add New Application" link.

![](/images/guides/linkedin/mvc5/add-app-link.png)

Complete your company details. If you are already listed as an Administrator for a Company Page in LinkedIn you can simply select it from the dropdown list, or alternatively select "New Company" from the dropdown list to create a new company page.  If you do not have any company pages listed yet you will be prompted for the name of your company.  A new company page will be created for you along with the application.

![](/images/guides/linkedin/mvc5/linkedin-create-app-companyinfo.png)

Specify the name, description, website URL and use of your application.  Keep the "Include yourself as developer for this application" option checked.  Select the status of the application. While you are developing the application you can keep the status as "Development".  In this case any updates posted by the application through the LinkedIn API will only be visible to the developers of the application.  To use the application in a production environment set the status to "Live".  

> If you choose to leave the status as "Development" for now you can change it to "Live" at a later stage by editing the application details.

![](/images/guides/linkedin/mvc5/linkedin-create-app-applicationinfo.png)

Add your contact details, or the contact details for whoever else at your company would be the contact person.

![](/images/guides/linkedin/mvc5/linkedin-create-app-contactinfo.png)

In the OAuth User agreement section you can leave all the defaults, but you will need to specify an OAuth 2.0 Redirect URL. The **OAuth 2.0 Redirect URL** must be the URL for you application, with the `/signin-linkedin` suffix. For you local development environment, this will be something like `http://localhost:4515/signin-linkedin`.

![](/images/guides/linkedin/mvc5/linkedin-create-app-oauth.png)

Finally make sure the the Terms of Service agreement checkbox is selected and click the "Add Application" button.

![](/images/guides/linkedin/mvc5/linkedin-create-app-other.png)

On the application registration confirmation page, take note of the "API Key" and "Secret Key" fields as these will be needed when enabling LinkedIn authentication in your ASP.NET MVC application.

![](/images/guides/linkedin/mvc5/linkedin-create-app-confirmation.png)

## Enabling LinkedIn authentication in your ASP.NET MVC Application

Next you need to install the **Owin.Security.Providers** Nuget package which contains the LinkedIn authentication provider.  Right click on your ASP.NET web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/linkedin/mvc5/nuget-package-dialog.png)

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/linkedin/mvc5/solution-explorer-startup-auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.LinkedIn;
{% endhighlight %}

Enable the LinkedIn provider by making a call to the `app.UseLinkedInAuthentication` method passing in the App ID of your LinkedIn application as the `clientId` parameter and the Secret Key as the `clientSecret` parameter.

{% highlight csharp %}
app.UseLinkedInAuthentication(
    clientId: "755ghpe281q6bo", 
    clientSecret: "20S5G0D0Lh40IRKW");
{% endhighlight %}

Make sure that the values you pass in for the `clientId` and `clientSecret` parameters are **exactly** the same as the values which were supplied by LinkedIn when registering the application.

## Testing the application

You have now created an application in LinkedIn and enabled the LinkedIn authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/linkedin/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with LinkedIn.  Click the "LinkedIn" button.

![](/images/guides/linkedin/mvc5/application-login-screen.png)

You will be redirected to the LinkedIn website.  If you are not logged in to LinkedIn yet you will be prompted to do so.  LinkedIn will then prompt you to give the application permissions to access your personal user data.

![](/images/guides/linkedin/mvc5/authorize-application.png)

Click on the "Authorize application" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/linkedin/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your LinkedIn account in the future.

## Launching to Production

When your are ready to launch your application in production your need to go back and edit the application information in LinkedIn, and change the "Live Status" field from "Development" to "Live".

![](/images/guides/linkedin/mvc5/linkedin-live-status.png)

You will also need to add the URL for your production environment to the list of "OAuth 2.0 Redirect URLs". I would however strongly suggest that you create separate applications in LinkedIn for each of your application environments, e.g. Development, Testing, Production etc. Doing it this way allows you to limit the number of people who have knowledge of the API Key and Secret Key of the production environment to a much smaller group.