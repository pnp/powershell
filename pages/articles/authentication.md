# Authentication

## Setting up Access

PnP PowerShell allows you to authenticate with credentials to your tenant. However, due to changes in the underlying SDKs we require you first to register a Azure AD Application which will allow you to authenticate.

The easiest way to do this by using a built-in cmdlet:

```powershell
Register-PnPManagementShellAccess
```

You'll notice that the cmdlet is not called `Register-PnPPowerShellAccess`. This is because both PnP PowerShell and the CLI for Microsoft 365 make use of this Azure AD application. 

> [!Important]
> You need to run this cmdlet with an identity that has write access to the Azure AD.
> You are not creating a new application in the sense of something that runs in your Azure AD tenant. You're only adding a registration to your Azure AD, a so called 'consent' for people in your tenant to use that application. The access rights the application requires are delegate only, so you will always have to provide credentials or another way of identifying the user actually using that application.

During execution of the cmdlet you will be talked through the consent flow. This means that a browser window will open, you will be asked to authenticate, and you will be asked to consent to a number of permissions. After this permissions has been granted a new entry will show up if you navigate to `Enterprise Applications` in your Azure AD. If you want to revoke the consent you can simply remove the entry from the Enterprise Applications. 

## Setting up access to your own Azure AD App

PnP PowerShell has a cmdlet that allows you to register a new Azure AD App, and optionally generate the certificates for you to use to login with that app. 

```powershell
$result = Register-PnPAzureADApp -ApplicationName "PnP Rocks" -Tenant mytenant.onmicrosoft.com -OutPath c:\mycertificates -DeviceLogin
$result
```

When you run the cmdlet above you will be asked to navigate to the shown url and enter the code shown. After that a new app will be registered in the Azure AD (make sure you have the rights to do this), and a certificate will be generated and uploaded to that app. After this a URL will be shown which you have to navigate to to provide consent for this application. By default a limited set of permissions scopes is added, but you can provide the one of the permission parameters (`GraphApplicationPermissions`, `GraphDelegatePermissions`, `SharePointApplicationPermissions`, `SharePointDelegatePermissions`) to provide your own permission scopes.

It also returns the private key certificate encoded in base64 encoding. As it spans multiple lines, it is recommended to assign the outcome of `Register-PnPAzureAdApp` to a variable so you have access to this value more easily. The Base64 encoded private key certificate can be used in your Connect-PnPOnline voiding the need to have access to the physical file:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -ClientId [clientid] -Tenant [yourtenant].onmicrosoft.com -CertificateBase64Encoded [pfx base64 encoded]
```

The cmdlet will also save both the CER and PFX files to the location specified with the -Outpath parameter. The names of the files will be matching the -ApplicationName parameter, e.g. in the example above the files will be called `PnP Rocks.cer` and `PnP Rocks.pfx`. The output of the cmdlet will show the clientid. After all is set up and consent has been provided you can login using:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -ClientId [clientid] -Tenant [yourtenant].onmicrosoft.com -CertificatePath [certificate.pfx]
```

## Authenticating with Credentials

Enter

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -Credentials (Get-Credential)
```

and you will be prompted for credentials. Using this method you're required to have granted the PnP Management Shell multi-tenant application access rights. You can however register your own application using `Register-PnPAzureAzureApp` and then provide the `-ClientId` parameter with the client id/app id of your custom application.

## Authenticating with pre-stored credentials using the Windows Credential Manager (Windows only)

```powershell
Add-PnPStoredCredential -Name "yourlabel" -Username youruser@domain.com
```

You will be prompted to provide a password. After that you can login using:

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -Credentials "yourlabel"
```
When you create the stored credentials (with Add-PnPStoredCredential or any other tool) if the Name you give it is the URL for your tenant you can omit the -Credentials parameter with Connect-PnPOnline. Using the example above create your stored credential with this command:

```powershell
Add-PnPStoredCredential -Name "https://[yourtenant].sharepoint.com" -Username youruser@contoso.com
```
When connecting to https://yourtenant.sharepoint.com you can use this command:
```powershell
Connect-PnPOnline [yourtenant].sharepoint.com 
```
Connect-PnPOnline will look through the Windows Credential Manager for a credential matching the URL. If it finds one it will use it. It will also match that credential with deeper connection URLs like https://yourtenant.sharepoint.com/sites/IT. You can create additional stored credentials for deeper sites if you routinely connect to them with different credentials. If you want to connect with a different set of credentials you can use the -Credentials parameter to specify them. A stored credential can be used for other URLs, like the Admin site:
```powershell
Connect-PnPOnline [yourtenant]-admin.sharepoint.com -Credentials https://[yourtenant].sharepoint.com 
```

## Authenticating with pre-stored credentials using the Secrets Management Module from Microsoft (Multi-Platform)

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

After you set up the vault and you added a credential

```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -Credentials (Get-Secret -Name "yourlabel")
```

## Authentication in case you have Multi-Factor authentication enabled

```powershell
Connect-PnPOnline[yourtenant].sharepoint.com -Interactive
```

This will show a popup window which will allow to authenticate and step through the multi-factor authentication flow.

## Authentication to GCC or National Cloud environments

In order to authentication to a GCC or a national cloud environment you have to take a few steps. Notice that this will work as of release 1.3.9-nightly or later.

### Register your own Azure AD App
You are required to register your own Azure AD App in order to authentication

```powershell
Register-PnPAzureADApp -ApplicationName "PnP PowerShell" -Tenant [yourtenant].onmicrosoft.com -Interactive -AzureEnvironment [USGovernment|USGovernmentHigh|USGovernmentDoD|Germany|China] -SharePointDelegatePermissions AllSites.FullControl -SharePointApplicationPermissions Sites.FullControl.All -GraphApplicationPermissions Group.ReadWrite.All -GraphDelegatePermissions Group.ReadWrite.All
```

The AzureEnvironment parameter only allows one value. Select the correct one that matches your cloud deployment.

The above statement grants a few permission scopes. You might want to add more if you want to. Alternatively, after registering the application, navigate to the Azure AD, locate the app registration, and grant more permissions and consent to them.

### Optionally modify the manifest for the app
There is a limitation in the Azure AD for national cloud environments where you cannot select permission scopes for SharePoint Online. In order to add specific SharePoint rights you will have to manually add them to the manifest that you can edit in Azure AD:

Locate the `requiredResourceAccess` section and add to or modify the existing entries. See the example below (notice, this is an example, do not copy and paste this as is as it will limit the permissions to only AllSites.FullControl):

```json
"requiredResourceAccess": [
{
    "resourceAppId": "00000003-0000-0ff1-ce00-000000000000",
    "resourceAccess": [
		{
			"id": "56680e0d-d2a3-4ae1-80d8-3c4f2100e3d0",
			"type": "Scope"
		}
      ]
}
```

You can add more permissions by using the following values:

The resourceAppId for SharePoint = "00000003-0000-0ff1-ce00-000000000000" 

Permission | Permission type | Id | Type
| -------| ----------- | ------ | ----- |
| Sites.FullControl.All | Application | 678536fe-1083-478a-9c59-b99265e6b0d3 | Role |
| AllSites.FullControl | Delegate | 56680e0d-d2a3-4ae1-80d8-3c4f2100e3d0 | Scope |


### Connect
```powershell
Connect-PnPOnline [yourtenant].sharepoint.com -Interactive -ClientId [clientid] -Tenant [yourtenant].onmicrosoft.com -AzureEnvironment [USGovernment|USGovernmentHigh|USGovernmentDoD|Germany|China]
```
The AzureEnvironment parameter only allows one value. Select the correct one that matches your cloud deployment.
