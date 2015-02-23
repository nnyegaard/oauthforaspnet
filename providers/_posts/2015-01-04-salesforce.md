---
layout: provider
title:  SalesForce
logo: salesforce.png
links: 
  - name: SalesForce Home Page
    url: http://www.salesforce.com/
  - name: SalesForce Developer Page
    url: https://developer.salesforce.com/
sdks:
  - name: Web Services API
    url: https://developer.salesforce.com/page/Web_Services_API
  - name: Force.com Toolkit for Azure
    url: https://developer.salesforce.com/page/Getting_Started_with_the_Force.com_Toolkit_for_Azure
guide: /salesforce/guide-mvc5
---
## Introduction
A lot of applications these days allow users to sign in using their existing login credentials from a social networking service such as Facebook and Twitter.  This simplifies the login process as users do not have to remember multiple login credentials for various websites, and it also provides the application developer in turn access to certain demographical information from the user.

ASP.NET MVC 5 has support for social logins built in, but as an app developer you will still need to go trough a few steps to enable this on your application.  This guide will help you through the process of allowing users to log in with their SalesForce account in a step-by-step manner.

To follow this guide you will need to have a SalesForce Developer account.  If you do not have an account then head on over to the [SalesForce Developer SignUp Page](https://developer.salesforce.com/en/signup) and register before you continue any further.

## Creating a new ASP.NET MVC Application
The first step is to create a new ASP.NET MVC application and enable HTTPS for the application. In Visual Studio, go to File > New > Project and select the template for a new "ASP.NET Web Application" and click "OK".

![](/images/guides/salesforce/new_project.png)

Next, select the MVC project template and ensure that the **authentication** method is set to "Individual User Accounts".  Click "OK".

![](/images/guides/salesforce/new_project_mvc.png)

> After the project wizard has completed I would advise you to update your NuGet packages before you proceed.  To do this you can right click on the solution file and select "Manage NuGet Packages for Solution...".  In the "Manage Nuget Packages" dialog you can navigate to the Updates node and ensure that you install any updates.

Next we need to install the **Owin.Security.Providers** Nuget package which will give us access to the SalesForce authentication provider.  Right click on your web project and select "Manage Nuget Packages...". Select the "Online" node in the "Manage Nuget Packages" dialog and search for the package named "Owin.Security.Providers".  Click "Install" to install the package into your project.

![](/images/guides/salesforce/nuget_package_dialog.png)

> The **Owin.Security.Providers** Nuget package was developed by myself with contributions from others.  If you want to add extra functionality to any of the providers or add new providers for other services I would appreciate the contributions.  Please fork the repository located at [https://github.com/owin-middleware/OwinOAuthProviders](https://github.com/owin-middleware/OwinOAuthProviders) and send a pull request.

The final step before creating a new application in SalesForce is to ensure that we enable HTTPS on our ASP.NET MVC application.  This is important as SalesForce requires the OAuth callback to happen on HTTPS. 

Select your project in the Solution Explorer and press F4 to view the Properties. Ensure that `SSL Enabled` is set to `True` and take note of the SSL URL as you will need that when registering your application in SalesForce.

![](/images/guides/salesforce/properties-ssl.png) 

## Registering an application in SalesForce
In order for your application to use SalesForce as a login mechanism you will need to create an application in SalesForce.  To do this, log in to SalesForce and go to the Setup area.

In the navigation pane on the left hand side, navigate to Build > Create and click on the Apps link.  

![](/images/guides/salesforce/sf-create-app-menu.png)

Scroll down to the Connected Apps section and click on the New button.

![](/images/guides/salesforce/sf-connected-apps.png)

Supply the Connected App Name, API Name and Contact Email.

![](/images/guides/salesforce/new-app-1.png)

Select "Enable OAuth Settings" and enter the Callback URL for your application and select at least one of the available scopes. The Callback URL should consist of the secure URL for your website, with the address `/signin-salesforce`, e.g. `https://localhost:44300/signin-salesforce`.  

Please note that the Callback URL **must** be HTTPS.

![](/images/guides/salesforce/new-app-2.png)

Finally scroll down and click on the Save button.

![](/images/guides/salesforce/new-app-3.png)

After the application has been created a screen will be displayed with the details your need when enabling the SalesForce authentication in ASP.NET MVC. Take note of the "Consumer Key" and "Consumer Secret" fields.

![](/images/guides/salesforce/new-app-success.png)

## Enabling SalesForce authentication in your ASP.NET MVC Application
Returning to Visual Studio, navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/salesforce/navigate_startup_auth.png)

Add a line at the top of the file to include the namespace for the Nuget provider.

{% highlight csharp %}
using Owin.Security.Providers.SalesForce;
{% endhighlight %}

Enable the SalesForce provider by making a call to the `app.UseSalesforceAuthentication` method passing in the Consumer Key of your SalesForce application as the `clientId` parameter and the Consumer Secret as the `clientSecret` parameter.

{% highlight csharp %}
app.UseSalesforceAuthentication(
    clientId: "3MVG9Y6d_Btp4xp53rplRBM7p1TZype2R5VtcbI0TWlliEmJ1qf6_Hl6UIvCJhSZ0CBmFCqcZWu.oPVRnGuTo", 
    clientSecret: "3446854973015261364");
{% endhighlight %}

It is important to ensure that these parameters match the values from SalesForce exactly, otherwise the authentication for your application will fail.

![](/images/guides/salesforce/keys-matchup.png)

## Testing the application
You have now created an application in SalesForce and enabled the SalesForce authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

Be sure to navigate to the **secure** (i.e. HTTPS) address for your website. In this example it is `https://localhost:44300`

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/salesforce/application_start_screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with SalesForce.  Click the "SalesForce" button.

![](/images/guides/salesforce/application_login_screen.png)

You will be redirected to the SalesForce website.  If you are not logged in to SalesForce yet you will be prompted to do so.  SalesForce will then prompt you to give the application permission to access your information.

![](/images/guides/salesforce/salesforce_auth_screen.png)

Click on the "Allow" button. You will be redirected back to your application and will need to supply your username to complete the registration process.

![](/images/guides/salesforce/complete_registration.png)

Once you have filled in your username and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your SalesForce account in the future.

