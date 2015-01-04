---
layout: provider
title:  Google+
logo: google-plus.png
categories: providers
---
##Introduction
A lot of applications these days allow users to sign in using their existing login credentials from a social networking service such as Google+.  This simplifies the login process as users do not have to remember multiple login credentials for various websites, and it also provides the application developer in turn access to certain demographical information from the user.

Even though ASP.NET MVC 5 has support for social logins built in, it does not have support for Google+ out of the box. Support for Google+ authentication is available through an open source NuGet package. This guide will help you through the process of enabling Google+ signin in your application in a step-by-step manner.

To follow this guide you will need to have a Google account.  If you do not have an account then head on over to the [Google Accounts page](https://accounts.google.com) and register before you continue any further.

##Registering a project in Google+
In order for you to use Google+ as an external authentication provider in your website, you will need to create a project in the Google Developer Console.  Head on over to the [Google Developer Console](https://console.developers.google.com).  

You will see a list of your current projects, if any.  Click on the "Create Project" button.

![](/images/guides/google-plus/google_console_project_list.png)

In the "New Project" dialog, give you project a name and ID and click on the "Create" button.

![](/images/guides/google-plus/google_console_new_project_dialog.png)

It will take a few moments to create the new project and you may be prompted to accept Terms of Service along the way.  

Click on the "APIs & Auth" menu in the sidebar menu.

![](/images/guides/google-plus/google_console_apis_auth_menu.png)

In the API List, locate the API called "Google+ API" and make sure that it is turned on.  If it is turned off then simply click on the "OFF" toggle to the right of it to turn it on.

![](/images/guides/google-plus/google_console_enable_api.png)

Next navigate to the "Credentials" menu.

![](/images/guides/google-plus/google_console_credentials_menu.png)

Under the OAuth section click on the "Create New Client ID" button.

![](/images/guides/google-plus/google_console_create_new_client_button.png)

Ensure that "Web Application" is selected as the application type.  We will need to specify the authorized redirect URI, but for now you can leave both the "Authorized JavaScript origins" and the "Authorized redirect URI" fields blank.  Click on the "Create Client ID" button.

You will be displayed summary information about the new Client ID which you created.  Take note of the "Client ID" and "Client secret" fields as these will be needed when we enable the Google+ authentication in our ASP.NET MVC application.

![](/images/guides/google-plus/google_console_client_summary.png)

We will also be returning to this screen later on to specify a valid callback URI as this will be needed for the Google+ OAuth authentication to work correctly.

##Enabling Google+ authentication in your ASP.NET MVC Application
The next step is to add the Google+ login to your ASP.NET MVC application.  For this we will create a new ASP.NET MVC application using Visual Studio. Go to File > New > Project and select the template for a new "ASP.NET Web Application" and click "OK".

![](/images/guides/google-plus/new_project.png)

Next, select the MVC project template and ensure that the **authentication** method is set to "Individual User Accounts".  Click "OK".

![](/images/guides/google-plus/new_project_mvc.png)

> After the project wizard has completed I would advise you to update your NuGet packages before you proceed.  To do this you can right click on the solution file and select "Manage NuGet Packages for Solution...".  In the "Manage Nuget Packages" dialog you can navigate to the Updates node and ensure that you install any updates.

Next we need to install the **Owin.Security.Providers** Nuget package which will give us access to the Google+ authentication provider.  Right click on your web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/google-plus/nuget_package_dialog.png)

> The **Owin.Security.Providers** Nuget package was developed by myself with contributions from others.  If you want to add extra functionality to any of the providers or add new providers for other services I would appreciate the contributions.  Please fork the repository located at [https://github.com/owin-middleware/OwinOAuthProviders](https://github.com/owin-middleware/OwinOAuthProviders) and send a pull request.

Navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/google-plus/navigate_startup_auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

``` csharp
using Owin.Security.Providers.GooglePlus;
```

Enable the Google+ provider by making a call to the `app.UseGooglePlusAuthentication` method passing in the Client ID of your Google+ project as the `clientId` parameter and the Client Secret as the `clientSecret` parameter.

``` csharp
app.UseGooglePlusAuthentication(
    clientId: "320475075164-8mfueb58obfi6djdp2fmghuds8d18bbj.apps.googleusercontent.com",
    clientSecret: "1mpLRt829Utmb-816GgL3GFP");
```

It is important to ensure that these parameters match the values from Google exactly, otherwise the Google+ authentication for your application will fail.

![](/images/guides/google-plus/keys_matchup.png)

##Specifying the authorization callback URL for your project
In order for the Google+ authentication to work your need to specify the correct authorization callback URL in GitHub for your application.

First we need to get the domain for out website. Right click on your web project in Visual Studio and select 'Properties".  In the properties windows navigate to the "Web" tab and copy your application URL in the "Project Url" field:

![](/images/guides/google-plus/project_properties.png)

Navigate back to the Google Developer console in your web browser and click on the "Edit Settings" button below the Client ID which you created earlier.

![](/images/guides/google-plus/google_console_edit_settings_button.png)

In the "Authorized redirect URI" field, specify the URL of your application, appending the path "/signin-googleplus" to the URL as indicated in the screenshot below and click the "Update" button.

![](/images/guides/google-plus/google_console_redirect_uri.png)

> You are able to specify more than one URL, so when you go into production you can just add the URL for your production URL to the list.  It is however strongly recommended that you have different ID's, and perhaps even different Google API projects, for your development and production systems. This setup is also better for security purposes as you can limit the people who have knowledge of the Client ID and Secret of the production application to a much smaller group.

##Setting up the Google Consent screen
One final bit of configuration on the Google side is needed and that is to specify and name and email address for your application which will be used in the Google consent screen when Google prompts your users for permissions.

Click to the "Consent" menu in the sidebar menu of the Google Developer Console.

![](/images/guides/google-plus/google_console_consent_menu.png)

Select an email address and specify a name for your application.  This information will be visible to your users on the consent screen when Google asks them for permission to give their private data to your application.  Click on the "Save" button.

![](/images/guides/google-plus/google_console_consent_screen.png)

##Testing the application
You have now created a project in the Google Developer Console and enabled the Google+ authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/google-plus/application_start_screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Google+.  Click the "GooglePlus" button.

![](/images/guides/google-plus/application_login_screen.png)

You will be redirected to the Google website.  If you are not logged in to Google yet you will be prompted to do so.  Google will then prompt you to give the application permissions to access your personal user data.

![](/images/guides/google-plus/google_permission.png)

Click on the "Accept" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/google-plus/complete_registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Google+ account in the future.