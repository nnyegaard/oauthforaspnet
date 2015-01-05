---
layout: provider
title:  Microsoft
logo: microsoft.png
---
## Introduction
A lot of applications these days allow users to sign in using their existing login credentials from a social networking service such as Facebook, Twitter or even your Microsoft account.  This simplifies the login process as users do not have to remember multiple login credentials for various websites, and it also provides the application developer in turn access to certain demographical information from the user.

ASP.NET MVC 5 has support for social logins built in, but as an app developer you will still need to go trough a few steps to enable this on your application.  This guide will help you through the process of allowing users to log in with their Microsoft account in a step-by-step manner.

To follow this guide you will need to have a Microsofr Live account.  If you do not have an account then head on over to the [Microsoft Live Account Page](http://account.live.com) and register before you continue any further.

## Registering your application
To register a new Microsoft Live application go to the [Live Connect Developer Center applications page](https://account.live.com/developers/applications).  This page will list not only your web based applications you created, but also any applications you created in the Windows or Windows Phone Developer Centers.  If you do not yet have any applications created you will immediately be taken to the new application creation screen.  Alternatively click on the "Create application" link.

![](/images/guides/microsoft/live_create_application_link.png)

Enter the name of your application and select the Language.  Click on the "I accept" button to proceed.

![](/images/guides/microsoft/live_create_application.png)

After the application has been created, navigate to the "App Settings" tab and take note of the Client ID and Client Secret as you will need these when enabling authentication with your Microsoft account in ASP.NET MVC.

![](/images/guides/microsoft/client_id_and_secret.png)

We will need to come back to the application settings in the Live Connect Developer Center a bit later to do configuration of the Redirect URLs, so you can leave the browser window open for now.

##Enabling Microsoft authentication in your ASP.NET MVC Application
The next step is to add the Microsoft login to your ASP.NET MVC application.  For this we will create a new ASP.NET MVC application using Visual Studio. Go to File > New > Project and select the template for a new "ASP.NET Web Application" and click "OK".

![](/images/guides/microsoft/new_project.png)

Next, select the MVC project template and ensure that the **authentication** method is set to "Individual User Accounts".  Click "OK".

![](/images/guides/microsoft/new_project_mvc.png)

> After the project wizard has completed I would advise you to update your NuGet packages before you proceed.  To do this you can right click on the solution file and select "Manage NuGet Packages for Solution...".  In the "Manage Nuget Packages" dialog you can navigate to the Updates node and ensure that you install any updates.

Once the application has been created you can navigate to the `Startup.Auth` file located in the `App_Start` folder of your application and open the file.

![](/images/guides/microsoft/navigate_startup_auth.png)

Locate the lines of code which enables the Microsoft authentication (look for `app.UseMicrosoftAuthentication`) and uncomment it.  Take the values for the "Client ID" and "Client Secret" from your Live Connect application and pass it through as the `clientId` and `clientSecret` parameters for the `app.UseMicrosoftAccountAuthentication` method call:

    app.UseMicrosoftAccountAuthentication(
        clientId: "0000000044116236",
        clientSecret: "nLut0Tya491C9y9m0bdmAPrbbrnS41yJ");

It is important to ensure that these parameter match the values from Live Connect exactly, otherwise the Microsoft authentication for your application will fail.

![](/images/guides/microsoft/keys_matchup.png)

##Enabling redirection to your localhost
We need to specify the Redirect URL for your application in the Live Connect Developer Center. This is an extra security measure to ensure that redirection only happens to your application. The is a bit of a problem however as this redirect URL will point to the `localhost` domain while we are testing the application locally, and the Live Connect Developer Center does not allow us to specify a redirect URL which points to the `localhost` domain.  A bit of smoke and mirrors is needed from us to make this work as we essentially have to redirect a *proper* domain to our localhost domain.

First we need to determine the domain on which our application is running locally.  Right click on your web project in Visual Studio and select 'Properties".  In the properties windows navigate to the "Web" tab and take note of your application URL in the "Project Url" field:

![](/images/guides/microsoft/project_properties.png)

Next we need to complete a couple of steps:
- First we need to redirect a "proper" domain URL to out localhost.  We will use the domain `localdev.bebigrockstar.com` but you can pick anything you like really, as long as it is a proper top level domain and not something like `localhost`.
- Next we will need to configure IIS Express to bind this URL to our website
- Add URL to the Access Control List
- Lastly we will need to configure the project to run on the new URL

###Redirect a "proper" URL to localhost
Open the Notepad application in Windows, but make sure that you run it in Administrator mode.

![](/images/guides/microsoft/notepad_administrator.png)

Add an entry to the hosts file which resolves the domain name your pick to the IP address 127.0.0.1.  This will in effect. In the screenshot below you can see that we configured the host name `localdev.beabigrockstar.com` to resolve to the IP address `127.0.0.1`.

![](/images/guides/microsoft/notepad_hosts.png)

Save your hosts file and close Notepad.

###Configure IIS Express
IIS Express needs to be configured to bind this new domain name to our website.  You need to make sure that IIS Express is not running at this point in time.

Open the IIS Express configuration file which is located at `%USERPROFILE%\My Documents\IISExpress\config\applicationhost.config` in a text editor or even Visual Studio.

![](/images/guides/microsoft/explorer_iis_config.png)

Inside the file located the lines which contains the bindings for your website by searching for the URL of your website which we got from the Project Properties before. It will look like the example below

``` xml
<site name="WebApplication1" id="7">
    <application path="/" applicationPool="Clr4IntegratedAppPool">
        <virtualDirectory path="/" physicalPath="c:\users\jerrie\documents\visual studio 2013\Projects\WebApplication1\WebApplication1" />
    </application>
    <bindings>
        <binding protocol="http" bindingInformation="*:2388:localhost" />
    </bindings>
</site>
```

Under the `<bindings>` section we need to add a line similar to the current one which adds a binding on the same port, but for our new domain name.

``` xml
<bindings>
    <binding protocol="http" bindingInformation="*:2388:localhost" />
    <binding protocol="http" bindingInformation="*:2388:localdev.beabigrockstar.com" />
</bindings>
```

Save and close the configuration file.

###Add URL to the Access Control List
At this point in time the security mechanisms in Windows will not allow us to browse to this URL on the specified port.  One alternative is to run Visual Studio (and hence IIS Express) as Administrator, but I do not like that. There is a very simple alternative and that is to add the URL to the Access Control List.

Open a Command Prompt **with elevated priviledges** by running it as Administrator.  Run the following command from the command prompt to add the domain to the ACL.  Be sure to specify the URL you chose and also be sure the **add the trailing backslash** to the URL otherwise the command will not execute.

``` bash
netsh http add urlacl url=http://localdev.beabigrockstar.com/ user=everyone
```

You will receive a message indicating that the URL reservation has been added if the command is successful.

![](/images/guides/microsoft/acl_command_prompt.png)

###Configure the project to run on the new URL
The final bit is to ensure that when our project runs, it does so on the new URL.  Once again navigate to your project properties page and update the "Project URL" field (which should currently be set to something like `http://localhost:2388` to be the new URL we configured.  In my case this is `http://localdev.beabigrockstar.com:2388`.

![](/images/guides/microsoft/project_properties_updated_project_url.png)

##Specifying the Redirect URL in Live Connect Developer Center
Hopefully you have made it this far without giving up in frustration.  Hand on we're almost there! The final bit before we can test the application is to specify the correct Redirect URL for the application.  Head back to the Live Connect Developer Center and open your application settings.  Click on the API Setting tab on the left.

In the "Redirect URLs" field you must specify the domain we configured with the path "signin-microsoft" appended.  In our example this URL will be `http://localdev.beabigrockstar.com:2388/signin-microsoft`.

![](/images/guides/microsoft/live_redirect_url.png)

Click the Save button.

##Testing the application
You have now created an application in Connect Live and enabled the Microsoft authentication in your application.  The last step is to ensure that everything works.  Run your application by selecting the Debug > Start Debugging menu item or pressing the F5 key in Visual Studio.

The application will open in your web browser.  Select the "Log In" menu at the top.

![](/images/guides/microsoft/application_start_screen.png)

Under the "Use another service to log in" section you will see a button which allows you to log in with Microsoft.  Click the "Microsoft" button.

![](/images/guides/microsoft/application_login_screen.png)

You will be redirected to the Microsoft Live website.  If you are not logged in yet you will be prompted to do so.  Microsoft will then prompt you to give the application permissions to access your profile info and contact list.

![](/images/guides/microsoft/oauth_consent_dialog.png)

Click on the "Yes" button.  You will be redirected back to your application and will need to supply your email address to complete the registration process.

![](/images/guides/microsoft/complete_registration.png)

Once you have filled in your email address and clicked the "Register" button you will be logged into the application.  You can now log in to the application using your Microsoft account in the future.

##Using Microsoft Login in production
When you want to use Microsoft login in production you will need to create a separate application in the Live Connect Developer Center.  You will do this for each different environment, as each will have its own redirect URL.  This setup is also better for security purposes as you can limit the people who have knowledge of the Client ID and Secret of the production application to a much smaller group.