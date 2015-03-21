---
layout: guide
title:  "Walkthrough: Configuring Microsoft Sign-In for ASP.NET MVC 5 and Visual Studio 2013"
---

## Introduction

In order to enable OAuth signin with Microsoft and allow users of your application to sign in with their Microsoft account, you will need to register an application in Microsoft. After you have registered the application you can use the **Client ID** and **Client Secret** supplied by Microsoft to register the Microsoft social login provider in your ASP.NET MVC application.

This guide will walk you through the entire process from end-to-end. This guide does not cover any advanced Microsoft integration topics, but only covers OAuth signin with Microsoft.

## Creating a new ASP.NET MVC application

If you do not yet have an ASP.NET MVC application, you will need to create one. In Visual Studio go to the File menu and select New > Project.

![](/images/guides/microsoft/mvc5/file-new-project.png)

Select the "ASP.NET Web Application" project template. Specify the name and location for your project and click on the OK button.

![](/images/guides/microsoft/mvc5/new-project-dialog.png)

For the template select MVC and make sure that the Authentication setting is set to "Individual User Accounts". Click OK.

![](/images/guides/microsoft/mvc5/aspnet-project-type-dialog.png)

Your project has been created, but there are a few extra steps required before you can register an application on the Microsoft Account Developer centre.

## Enabling redirection to your localhost

The Microsoft OAuth implementation unfortunately does not allow for redirecting back to a URL which is on the `localhost` domain, so in order for you to test the application you will need to complete a steps to work around this problem. What this involves is creating a "proper" URL which points to you local machine (i.e. `127.0.0.1`) and then ensuring that IIS Express recognizes that URL.

First you will need to determine the domain on which our application is running locally.  Right click on your web project in Visual Studio and select 'Properties".  In the properties windows navigate to the "Web" tab and take note of your application URL in the "Project Url" field:

![](/images/guides/microsoft/mvc5/project-properties.png)

Next you will need to complete a couple of steps:
- First you need to redirect a "proper" domain URL to 127.0.0.1. In this document I will use the domain localdev.authforaspnet.com but you can pick anything you like really, as long as it is a proper top level domain and not something like localhost.
- Configure IIS Express to bind this URL to your website
- Add the URL to the Access Control List
- Configure the project to run on the new URL

### Redirect a "proper" URL to 127.0.0.1

Open the Notepad application in Windows, but make sure that you run it in Administrator mode.

![](/images/guides/microsoft/mvc5/notepad-administrator.png)

Open your hosts file. Typicall this is located at "C:\windows\system32\drivers\etc\hosts"

![](/images/guides/microsoft/mvc5/notepad-open-file-dialog.png)

Add an entry to the hosts file which resolves the domain name you picked to the IP address 127.0.0.1. In the screenshot below you can see that I configured the host name `localdev.oauthforaspnet.com` to resolve to the IP address `127.0.0.1`.

![](/images/guides/microsoft/mvc5/notepad-edit-hosts.png)

Save your hosts file and close Notepad.

### Configure IIS Express

IIS Express needs to be configured to bind this new domain name to our website.  You need to make sure that IIS Express is not running at this point in time.

Open the IIS Express configuration file which is located at `%USERPROFILE%\Documents\IISExpress\config\applicationhost.config` in a text editor or Visual Studio.

Inside the file locate the lines which contains the bindings for your website by searching for the URL of your website which we got from the Project Properties before. In my cast this was `http://localhost:4515/`, so I will search for the entry `4515:localhost`

It will look something similar to the example below:

{% highlight xml %}
<site name="OAuthDemoMVC5" id="24">
    <application path="/" applicationPool="Clr4IntegratedAppPool">
        <virtualDirectory path="/" physicalPath="C:\Development\RockstarLabs\oauthforaspnet-main\src\DemoMVC5\OAuthDemoMVC5" />
    </application>
    <bindings>
        <binding protocol="http" bindingInformation="*:4515:localhost" />
    </bindings>
</site>
{% endhighlight %}

Under the `<bindings>` section you need to add a line similar to the current one which adds a binding on the same port, but for your new domain name.

{% highlight xml %}
<bindings>
    <binding protocol="http" bindingInformation="*:4515:localhost" />
    <binding protocol="http" bindingInformation="*:4515:localdev.oauthforaspnet.com" />
</bindings>
{% endhighlight %}

Save and close the configuration file.

### Add URL to the Access Control List

At this point in time the security mechanisms in Windows will not allow you to browse to this URL on the specified port.  One alternative is to run Visual Studio (and hence IIS Express) as Administrator, but I do not like that. There is a very simple alternative and that is to add the URL to the Access Control List.

Open a Command Prompt **with elevated priviledges** by running it as Administrator.  Run the following command from the command prompt to add the domain to the ACL.  Be sure to specify the URL you chose and the port of your ASP.NET website. Also be sure the **add the trailing backslash** to the URL otherwise the command will not execute.

{% highlight bash %}
netsh http add urlacl url=http://localdev.oauthforaspnet.com:4515/ user=everyone
{% endhighlight %}

You will receive a message indicating that the URL reservation has been added if the command is successful.

![](/images/guides/microsoft/mvc5/acl-command-prompt.png)

### Configure the project to run on the new URL

The final bit is to ensure that when your project runs, it does so on the new URL.  Once again navigate to your project properties page and update the "Project URL" field (which should currently be set to something like `http://localhost:4515` to be the new URL you configured.  In my case this is `http://localdev.oauthforaspnet.com:4515`.

![](/images/guides/microsoft/mvc5/project-properties-updated-project-url.png)

## Registering your application

To register a new Microsoft Live application go to the [Microsoft Account Developer Center applications page](https://account.live.com/developers/applications). If you do not yet have any applications created you will immediately be taken to the new application creation screen.  Alternatively click on the "Create application" link.

Enter the name of your application and select the Language.  Click on the "I accept" button to proceed.

![](/images/guides/microsoft/mvc5/live-create-application.png)

After the application has been created, navigate to the "API Settings" tab. In the "Redirect URLs" field you must specify the domain you configured with the path "signin-microsoft" appended.  In my case this URL will be `http://localdev.oauthforaspnet.com:4515/signin-microsoft`. When you are done click on the Save button.

![](/images/guides/microsoft/mvc5/live-api-settings.png)

Next navigate to the "App Settings" tab and take note of the Client ID and Client Secret as you will need these when enabling authentication with your Microsoft account in ASP.NET MVC.

![](/images/guides/microsoft/mvc5/live-app-settings.png)

## Enabling Microsoft authentication in your ASP.NET MVC Application

Go back to your ASP.NET application and navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/microsoft/mvc5/solution-explorer-startup-auth.png)

In the `ConfigureAuth` method, below all the existing code add the following line of code:

{% highlight csharp %}
app.UseMicrosoftAccountAuthentication("0000000044142130", "YOJkZAM3GsgxZndIf3eYsEMZOKfZb0tS");
{% endhighlight %}

Make sure that the values you pass in for the `clientId` and `clientSecret` parameters are **exactly** the same as the values which were supplied by Microsoft when registering the application.

> The Microsoft OAuth provider is located in the `Microsoft.Owin.Security.MicrosoftAccount` Nuget package, which is added by default in the new ASP.NET project template. If for some reason this is not added to your project you can do so by installing it using the Nuget dialog or Package Manager Console

## Testing the application
You have now created an application in Connect Live and enabled the Microsoft authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/microsoft/mvc5/application-start-screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Microsoft.  Click the "Microsoft" button.

![](/images/guides/microsoft/mvc5/application-login-screen.png)

You will be redirected to the Microsoft Live website.  If you are not logged in yet you will be prompted to do so.  Microsoft will then prompt you to give the application permissions to access your profile info and contact list.

![](/images/guides/microsoft/mvc5/oauth-consent-dialog.png)

Click on the "Yes" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/microsoft/mvc5/complete-registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Microsoft account in the future.

## Using Microsoft Login in production

When you want to use Microsoft login in production you will need to create a separate application in the Microsoft Account Developer Center.  You will do this for each different environment you have (such as development, staging and production), as each will have its own redirect URL.  This setup is also better for security purposes as you can limit the people who have knowledge of the Client ID and Secret of the production application to a much smaller group.