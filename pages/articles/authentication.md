# Authentication

Before you can authenticate using PnP PowerShell, you need to ensure you have [created your own application registration](registerapplication.md) first and that you have [set the proper permissions](determinepermissions.md) on the application registration.

PnP PowerShell offers many different ways to authenticate to your tenant. Based on what you would like to achieve, pick the method that best suits your needs below.

Instead of having to provide `-ClientId` on every connect, you can also opt to [configure a default Client ID](defaultclientid.md) that will be used if `-ClientID` is not being specified in your `Connect-PnPOnline`.

## Interactive Authentication

This is the easiest method to authenticate, but it requires you to enter your credentials and go through multi factor authentication and conditional access policy steps, if applicable, each time you connect. This works perfectly well if your intend is to manually run scripts, but is not suitable to run unattended scripts at i.e. timed intervals.

Connecting can be done using:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -Interactive -ClientId <client id of your Entra ID Application Registration>
```

This will show a popup window which will allow to authenticate and step through the multi-factor authentication flow. Ensure you provide [the Client ID of your own Entra ID Application Registration](registerapplication.md) with the `-ClientId` parameter.

## Authenticating with Credentials

This method allows you to connect by just providing your username and password. It will not work with multi factor authentication. Therefore this method is less recommended.

Connecting can be done using:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -ClientId <client id of your Entra ID Application Registration> -Credentials (Get-Credential)
```

and you will be prompted for credentials. Ensure you provide [the Client ID of your own Entra ID Application Registration](registerapplication.md) with the `-ClientId` parameter.

## Authenticating with pre-stored credentials using the Windows Credential Manager (Windows only)

This method can be used if you just intend to use PnP PowerShell on a Windows device, you want to use just a username and password for an account that does not require multi factor authentication, and you wish to store these credentials in the Windows Credential Manager so you don't have to enter them every time you connect. As this will not work with multi factor authentication, this method is less recommended.

Adding your credentials to the Windows Credential Manager, which is a one time operation, can be done using:

```powershell
Add-PnPStoredCredential -Name "yourlabel" -Username youruser@domain.com
```

You will be prompted to provide a password. After that you can login using:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -ClientId <client id of your Entra ID Application Registration> -Credentials "yourlabel"
```

When you create the stored credentials (with Add-PnPStoredCredential or any other tool) if the Name you give it is the URL for your tenant you can omit the `-Credentials` parameter with `Connect-PnPOnline`. Using the example above create your stored credential with this command:

```powershell
Add-PnPStoredCredential -Name "https://[yourtenant].sharepoint.com" -Username youruser@contoso.com
```

When connecting to https://yourtenant.sharepoint.com you can use this command:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -ClientId <client id of your Entra ID Application Registration>
```

Connect-PnPOnline will look through the Windows Credential Manager for a credential matching the URL. If it finds one it will use it. It will also match that credential with deeper connection URLs like https://yourtenant.sharepoint.com/sites/IT. You can create additional stored credentials for deeper sites if you routinely connect to them with different credentials. If you want to connect with a different set of credentials you can use the -Credentials parameter to specify them. A stored credential can be used for other URLs, like the Admin site:

```powershell
Connect-PnPOnline [yourtenant]-admin.sharepoint.com -ClientId <client id of your Entra ID Application Registration> -Credentials https://[yourtenant].sharepoint.com 
```

## Authenticating with pre-stored credentials using the Secrets Management Module from Microsoft (Multi-Platform)

This method can be used if you just intend to use PnP PowerShell on a Windows, Linux or iOS device, you want to use just a username and password for an account that does not require multi factor authentication, and you wish to store these credentials in the a Credential Manager so you don't have to enter them every time you connect. As this will not work with multi factor authentication, this method is less recommended.

Adding your credentials to the Credential Manager, which is a one time operation, can be done using:

```powershell
Install-Module -Name Microsoft.PowerShell.SecretManagement -AllowPrerelease
Install-Module -Name Microsoft.PowerShell.SecretStore -AllowPrerelease
Set-SecretStoreConfiguration
Set-Secret -Name "yourlabel" -Secret (Get-Credential)
```

This creates a new secret vault on your computer. You will be asked to provide a password to access the vault. If you access the vault you will be prompted for that password. In case you want to want to write automated scripts you will have to turn off this password prompt as follows:

```powershell
Set-SecretStoreConfiguration -Authentication None
```

For more information about these cmdlets, check out the github repositories: https://github.com/powershell/secretmanagement and https://github.com/powershell/secretstore.

After you set up the vault and you added a credential, you can connect using:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -ClientId <client id of your Entra ID Application Registration> -Credentials (Get-Secret -Name "yourlabel")
```

## Authentication to GCC or National Cloud environments

In order to connect to a GCC or a national cloud environment, ensure you have followed the [specific steps for setting up the application registration for national clouds](#special-instructions-for-gcc-or-national-cloud-environments).

Connecting can be done using:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -Interactive -ClientId [clientid] -Tenant [yourtenant].onmicrosoft.com -AzureEnvironment [USGovernment|USGovernmentHigh|USGovernmentDoD|Germany|China]
```

The AzureEnvironment parameter only allows one value. Select the correct one that matches your cloud deployment.

## Silent Authentication with Credentials for running in Pipelines

For running `Connect-PnPOnline` with user credentials in Azure DevOps pipeline, you need to make sure that authentication in your Entra ID application is configured to allow public client. 

Public client can be configured from the Azure portal from the Authentication Blade in the application or by setting the `allowPublicClient` property in the application's manifest to true.
![image](../images/authentication/allowPublicClient.png)

`username` and `password` for service account can be stored as secret pipeline variables and can be referenced in the script to achieve complete automation.
![image](../images/authentication/libraryVariables.png)

## Silent Authentication with Credentials and MFA for running in Azure DevOps Pipelines with Microsoft Hosted Agents
### Identify the possible IP ranges for Microsoft-hosted agents

- Identify the [region for your organization](https://docs.microsoft.com/en-us/azure/devops/organizations/accounts/change-organization-location?view=azure-devops) in Organization settings.
- Identify the [Azure Geography](https://azure.microsoft.com/global-infrastructure/geographies/) for your organization's region.
- Map the names of the regions in your geography to the format used in the weekly file, following the format of AzureCloud., such as AzureCloud.westus. You can map the names of the regions from the Azure Geography list to the format used in the weekly file.
- You can find the weekly IP file at the following URL: https://www.microsoft.com/en-us/download/details.aspx?id=56519

For example, if your organization is located in the South East Asia region, you would map it to the format AzureCloud.SouthEastAsia.

### Create a named location in Entra ID conditional access

- Go to Entra ID conditional access
- Open named location blade, click on `+ IP Ranges Location`
- Enter the IP ranges for Microsoft Hosted Agents, `Mark as trusted location` should be checked.
  ![image](../images/authentication/namedLocations.png)


### Create a conditional access policy

- Go to Entra ID conditional access, click on `+New Policy`.
- Give a meaningful name, click on Users and Groups -> Include select users and groups, select the user with which you want to run your pipeline.
- Include all cloud apps.
- Under conditions -> locations include `any locations` and exclude the recently created named location.
- Under grant -> choose `grant access`. Only `require multifactor authentication needs to be checked`.
- Enable the policy and click on Save.
![image](../images/authentication/conditionalAccess.png)

> [!Important]
> You need to make sure that the new policy does not conflicts with any other policy in your tenant, otherwise make the changes accordingly.

### Powershell script to be run in pipeline
```powershell
$creds = New-Object System.Management.Automation.PSCredential -ArgumentList ($username, $password)
Connect-PnPOnline -Url <site url> -Credentials $creds -ClientId <Application/Client ID of Entra ID app>
```
