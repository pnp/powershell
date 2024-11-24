---
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Connect-PnPOnline.html
Locale: en-US
Module Name: PnP.PowerShell
ms.date: 11/24/2024
PlatyPS schema version: 2024-05-01
title: Connect-PnPOnline
---

# Connect-PnPOnline

## SYNOPSIS

Connect to a SharePoint site

## SYNTAX

### Credentials (Default)

```powershell
Connect-PnPOnline [-Url] <string> [-ReturnConnection] [-ValidateConnection]
 [-Connection <PnPConnection>] [-Credentials <CredentialPipeBind>] [-CurrentCredentials]
 [-CreateDrive] [-DriveName <string>] [-ClientId <string>] [-RedirectUri <string>]
 [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <string>] [-TransformationOnPrem]
 [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### SharePoint ACS (Legacy) App Only

```powershell
Connect-PnPOnline [-Url] <string> -ClientSecret <string> -ClientId <string> [-ReturnConnection]
 [-ValidateConnection] [-Connection <PnPConnection>] [-Realm <string>] [-CreateDrive]
 [-DriveName <string>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <string>]
 [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### App-Only with Azure Active Directory

```powershell
Connect-PnPOnline [-Url] <string> -ClientId <string> -Tenant <string> [-ReturnConnection]
 [-ValidateConnection] [-Connection <PnPConnection>] [-CreateDrive] [-DriveName <string>]
 [-CertificatePath <string>] [-CertificateBase64Encoded <string>]
 [-CertificatePassword <securestring>] [-AzureEnvironment <AzureEnvironment>]
 [-TenantAdminUrl <string>] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>]
 [<CommonParameters>]
```

### App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint

```powershell
Connect-PnPOnline [-Url] <string> -ClientId <string> -Tenant <string> -Thumbprint <string>
 [-ReturnConnection] [-ValidateConnection] [-Connection <PnPConnection>] [-CreateDrive]
 [-DriveName <string>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <string>]
 [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### SPO Management Shell Credentials

```powershell
Connect-PnPOnline [-Url] <string> -SPOManagementShell [-ReturnConnection] [-ValidateConnection]
 [-Connection <PnPConnection>] [-Credentials <CredentialPipeBind>] [-CurrentCredentials]
 [-CreateDrive] [-DriveName <string>] [-TenantAdminUrl <string>] [<CommonParameters>]
```

### PnP Management Shell / DeviceLogin

```powershell
Connect-PnPOnline [-Url] <string> -DeviceLogin [-ReturnConnection] [-ValidateConnection]
 [-Connection <PnPConnection>] [-CreateDrive] [-DriveName <string>] [-LaunchBrowser]
 [-ClientId <string>] [-Tenant <string>] [-AzureEnvironment <AzureEnvironment>]
 [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### Web Login for Multi Factor Authentication

```powershell
Connect-PnPOnline [-Url] <string> -UseWebLogin [-ReturnConnection] [-ValidateConnection]
 [-CreateDrive] [-DriveName <string>] [-TenantAdminUrl <string>] [-RelativeUrl <string>]
 [-ForceAuthentication] [<CommonParameters>]
```

### Interactive login for Multi Factor Authentication

```powershell
Connect-PnPOnline [-Url] <string> -Interactive [-ReturnConnection] [-ValidateConnection]
 [-Connection <PnPConnection>] [-CreateDrive] [-DriveName <string>] [-LaunchBrowser]
 [-ClientId <string>] [-Tenant <string>] [-AzureEnvironment <AzureEnvironment>]
 [-TenantAdminUrl <string>] [-ForceAuthentication] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### Access Token

```powershell
Connect-PnPOnline [-Url] <string> -AccessToken <string> [-ReturnConnection] [-ValidateConnection]
 [-AzureEnvironment <AzureEnvironment>] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### Environment Variable

```powershell
Connect-PnPOnline [-Url] <string> -EnvironmentVariable [-ReturnConnection] [-ValidateConnection]
 [-Connection <PnPConnection>] [-CreateDrive] [-DriveName <string>] [-RedirectUri <string>]
 [-Tenant <string>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <string>]
 [-TransformationOnPrem] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>]
 [<CommonParameters>]
```

### System Assigned Managed Identity

```powershell
Connect-PnPOnline [[-Url] <string>] -ManagedIdentity [-ReturnConnection] [-ValidateConnection]
 [-AzureEnvironment <AzureEnvironment>] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### User Assigned Managed Identity by Client Id

```powershell
Connect-PnPOnline [[-Url] <string>] -ManagedIdentity -UserAssignedManagedIdentityClientId <string>
 [-ReturnConnection] [-ValidateConnection] [-AzureEnvironment <AzureEnvironment>]
 [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### User Assigned Managed Identity by Principal Id

```powershell
Connect-PnPOnline [[-Url] <string>] -ManagedIdentity -UserAssignedManagedIdentityObjectId <string>
 [-ReturnConnection] [-ValidateConnection] [-AzureEnvironment <AzureEnvironment>]
 [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### User Assigned Managed Identity by Azure Resource Id

```powershell
Connect-PnPOnline [[-Url] <string>] -ManagedIdentity
 -UserAssignedManagedIdentityAzureResourceId <string> [-ReturnConnection] [-ValidateConnection]
 [-AzureEnvironment <AzureEnvironment>] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [<CommonParameters>]
```

### Azure AD Workload Identity

```powershell
Connect-PnPOnline [[-Url] <string>] -AzureADWorkloadIdentity [-ReturnConnection]
 [-ValidateConnection] [-Connection <PnPConnection>] [<CommonParameters>]
```

### OS login

```powershell
Connect-PnPOnline [-Url] <string> -OSLogin [-ReturnConnection] [-ValidateConnection]
 [-Connection <PnPConnection>] [-CreateDrive] [-DriveName <string>] [-ClientId <string>]
 [-Tenant <string>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <string>]
 [-ForceAuthentication] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has the doesn't have any aliases

## DESCRIPTION

Connects to a SharePoint site or another API and creates a context that is required for the other PnP Cmdlets.
See https://pnp.github.io/powershell/articles/connecting.html for more information on the options to connect.

## EXAMPLES

### EXAMPLE 1

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com"
```

Connect to SharePoint prompting for the username and password.
When a generic credential is added to the Windows Credential Manager with https://contoso.sharepoint.com, PowerShell will not prompt for username and password and use those stored credentials instead.
You will have to register your own App first, by means of `Register-PnPEntraIDApp` to use this method.
You will also have to provide the `-ClientId` parameter starting September 9, 2024.
Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created

### EXAMPLE 2

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -Credentials (Get-Credential)
```

Connect to SharePoint prompting for the username and password to use to authenticate.

### EXAMPLE 3

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.de" -ClientId 344b8aab-389c-4e4a-8fa1-4c1ae2c0a60d -ClientSecret $clientSecret
```

This will authenticate you to the site using Legacy ACS authentication

### EXAMPLE 4

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -DeviceLogin
```

This will authenticate you using the PnP Management Shell Multi-Tenant application.
A browser window will have to be opened where you have to enter a code that is shown in your PowerShell window.
Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created and we will use that value and authenticate using that Entra ID app.

### EXAMPLE 5

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -DeviceLogin -LaunchBrowser
```

This will authenticate you using the PnP Management Shell Multi-Tenant application.
Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created.
A browser window will automatically open and the code you need to enter will be automatically copied to your clipboard.

### EXAMPLE 6

```powershell
$password = (ConvertTo-SecureString -AsPlainText 'myprivatekeypassword' -Force)
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificatePath 'c:\mycertificate.pfx' -CertificatePassword $password  -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a locally available certificate containing a private key.
See https://learn.microsoft.com/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 7

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -Tenant 'contoso.onmicrosoft.com' -Thumbprint 34CFAA860E5FB8C44335A38A097C1E41EEA206AA
```

Connects to SharePoint using app-only tokens via an app's declared permission scopes.
See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.
Ensure you have imported the private key certificate, typically the .pfx file, into the Windows Certificate Store for the certificate with the provided thumbprint.

### EXAMPLE 8

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificateBase64Encoded $base64encodedstring -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a certificate with a private key that has been base64 encoded.
See https://learn.microsoft.com/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 9

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -Interactive -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28
```

Connects to the Azure AD, acquires an access token and allows PnP PowerShell to access both SharePoint and the Microsoft Graph.
Notice that you will have to register your own App first, by means of `Register-PnPEntraIDApp` to use this method.
You will also have to provide the `-ClientId` parameter starting September 9, 2024.
Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created.
If you use -Interactive and this environment variable is present you will not have to use -ClientId.

### EXAMPLE 10

```powershell
Connect-PnPOnline -Url "https://portal.contoso.com" -TransformationOnPrem -CurrentCredential
```

Connects to on-premises SharePoint 2013, 2016 or 2019 site with the current user's on-premises Windows credential (e.g.
domain\user).
This option is only supported for being able to transform on-premises classic wiki, webpart, blog and publishing pages into modern pages in a SharePoint Online site.
Although other PnP cmdlets might work as well, they're officially not supported for being used in an on-premises context.
See http://aka.ms/sharepoint/modernization/pages for more details on page transformation.

### EXAMPLE 11

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ManagedIdentity
```

Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ManagedIdentity

Connects using a system assigned managed identity to Microsoft Graph.
Using this way of connecting only works with environments that support managed identities: Azure Functions, Azure Automation Runbooks and the Azure Cloud Shell.
Read up on this article (https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity)how it can be used.

### EXAMPLE 12

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ManagedIdentity -UserAssignedManagedIdentityObjectId 363c1b31-6872-47fd-a616-574d3aec2a51
```

Connects using an user assigned managed identity with object/principal ID 363c1b31-6872-47fd-a616-574d3aec2a51 to SharePoint Online.
Using this way of connecting only works with environments that support managed identities: Azure Functions, Azure Automation Runbooks and the Azure Cloud Shell.
Read up on this article (https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity)how it can be used.

### EXAMPLE 13

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -AccessToken $token
```

This method assumes you have acquired a valid OAuth2 access token from Azure AD with the correct audience and permissions set.
Using this method PnP PowerShell will not acquire tokens dynamically and if the token expires (typically after 1 hour) cmdlets will fail to work using this method.

### EXAMPLE 14

```powershell
Connect-PnPOnline -Url contoso.sharepoint.com -EnvironmentVariable -Tenant 'contoso.onmicrosoft.com'
```

This example uses the `AZURE_CLIENT_CERTIFICATE_PATH` and `AZURE_CLIENT_CERTIFICATE_PASSWORD` environment variable values to authenticate.
The `AZURE_CLIENT_ID` environment variable must be present and `Tenant` parameter value must be provided.

If these environment variables are not present, it will try to find `ENTRAID_APP_CERTIFICATE_PATH` or `ENTRAID_CLIENT_CERTIFICATE_PATH` and for certificate password use `ENTRAID_APP_CERTIFICATE_PASSWORD` or `ENTRAID_CLIENT_CERTIFICATE_PASSWORD` as fallback.

### EXAMPLE 15

```powershell
Connect-PnPOnline -Url contoso.sharepoint.com -EnvironmentVariable
```

This example uses the `AZURE_USERNAME` and `AZURE_PASSWORD` environment variables as credentials to authenticate.
 If these environment variables are not available, it will use `ENTRAID_USERNAME` and `ENTRAID_PASSWORD` environment variables as fallback.

If `AZURE_CLIENT_ID` is not present, then it will try to use the default `PnP Management Shell Azure AD app` as fallback and attempt to authenticate.
Starting from 9th Sept 2024, it will try to use `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` environment variables as fallback.

This method assumes you have the necessary environment variables available.
For more information about the required environment variables, please refer to this article, Azure.Identity Environment Variables (https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/identity/Azure.Identity#environment-variables)here.

So, when using `-EnvironmentVariable` method for authenticating, we will require `AZURE_CLIENT_CERTIFICATE_PATH`, `AZURE_CLIENT_CERTIFICATE_PASSWORD` and `AZURE_CLIENT_ID` environment variables for using the service principal with certificate method for authentication.

If `AZURE_USERNAME`, `AZURE_PASSWORD` and `AZURE_CLIENT_ID`, we will use these environment variables and authenticate using credentials flow.

If `ENTRAID_USERNAME`, `ENTRAID_PASSWORD` and `ENTRAID_APP_ID` , we will use these environment variables and authenticate using credentials flow.

We support only Service principal with certificate and Username with password mode for authentication.
Configuration will be attempted in that order.
For example, if values for a certificate and username+password are both present, the client certificate method will be used.

### EXAMPLE 16

```powershell
Connect-PnPOnline -Url contoso.sharepoint.com -AzureEnvironment Custom -MicrosoftGraphEndPoint "custom.graph.microsoft.com" -AzureADLoginEndPoint "https://custom.login.microsoftonline.com"
```

Use this method to connect to a custom Azure Environment.
You can also specify the `MicrosoftGraphEndPoint` and `AzureADLoginEndPoint` parameters if applicable.
If specified, then these values will be used to make requests to Graph and to retrieve access token.

### EXAMPLE 17

```powershell
Connect-PnPOnline -Url contoso.sharepoint.com -AzureADWorkloadIdentity
```

This example uses Azure AD Workload Identity to retrieve access tokens.
For more information about this, please refer to this article, Azure AD Workload Identity (https://azure.github.io/azure-workload-identity/docs/introduction.html).
We are following the guidance mentioned in [this sample](https://github.com/Azure/azure-workload-identity/blob/main/examples/msal-net/akvdotnet/TokenCredential.cs)to retrieve the access tokens.

### EXAMPLE 18

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -OSLogin
```

Connects to the Azure AD with WAM (aka native Windows authentication prompt), acquires an access token and allows PnP PowerShell to access both SharePoint and the Microsoft Graph.
Notice that you will have to register your own App first, by means of Register-PnPEntraIDAdd to use this method.
You will also have to provide the -ClientId parameter starting September 9, 2024.
Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created.

WAM is a more secure & faster way of authenticating in Windows OS.
It supports Windows Hello, FIDO keys , conditional access policies and more.

## PARAMETERS

### -AccessToken

Using this parameter you can provide your own access token.
Notice that it is recommend to use one of the other connection methods as this will limits the offered functionality on PnP PowerShell.
For instance if the token expires (typically after 1 hour) will not be able to acquire a new valid token, which the other connection methods do allow.
You are responsible for providing your own valid access token when using this parameter, for the correct audience, with the correct permissions scopes.

```yaml
Type: System.String
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Access Token
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AzureADLoginEndPoint

Custom Azure AD login endpoint to be used if we are using Azure Custom environment to retrieve access token.
This will only work if `AzureEnvironment` parameter value is set to `Custom`.

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Access Token
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: System Assigned Managed Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Client Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Principal Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AzureADWorkloadIdentity

Connects using the Azure AD Workload Identity.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Azure AD Workload Identity
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AzureEnvironment

The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: PnP.Framework.AzureEnvironment
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Access Token
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: System Assigned Managed Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Client Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Principal Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CertificateBase64Encoded

Specify a base64 encoded string as representing the private certificate.

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CertificatePassword

Password to the certificate (*.pfx)

```yaml
Type: System.Security.SecureString
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CertificatePath

Path to the certificate containing the private key (*.pfx)

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ClientId

The Client ID of the Azure AD Application

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- ApplicationId
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ClientSecret

The client secret to use.
When using this, technically an Azure Access Control Service (ACS) authentication will take place.
This effectively means only cmdlets that are connecting to SharePoint Online will work.
Cmdlets using Microsoft Graph or any other API behind the scenes will not work.

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be reused by the new connection.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnP.PowerShell.Commands.Base.PnPConnection
DefaultValue: PnPConnection.Current
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Azure AD Workload Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CreateDrive

If you want to create a PSDrive connected to the URL

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Credentials

Credentials of the user to connect with.
Either specify a PSCredential object or a string.
In case of a string value a lookup will be done to the Generic Credentials section of the Windows Credentials in the Windows Credential Manager for the correct credentials.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.CredentialPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CurrentCredentials

Use credentials of the currently logged in user.
Applicable exclusively when connecting to on premises SharePoint Server via PnP.
Switch parameter.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DeviceLogin

Log in using the Device Code flow.
By default it will use the PnP Management Shell multi-tenant Azure AD application registration.
You will be asked to consent to:

* Read and write managed metadata

* Have full control of all site collections

* Read user profiles

* Invite guest users to the organization

* Read and write all groups

* Read and write directory data

* Read and write identity providers

* Access the directory as you

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases:
- PnPManagementShell
- PnPO365ManagementShell
ParameterSets:
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DriveName

Name of the PSDrive to create (default: SPO)

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EnvironmentVariable

Connects using the necessary environment variables.
For more information the required environment variables, please refer to this article, Azure.Identity Environment Variables (https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/identity/Azure.Identity#environment-variables)here.
We support only Service principal with certificate and Username with password mode for authentication.
Configuration will be attempted in that order.
For example, if values for a certificate and username+password are both present, the client certificate method will be used.
By default, it will use the `-ClientId` specified in `AZURE_CLIENT_ID` environment variable.
If that value is empty, it will fallback to the PnP Management Shell Azure AD App.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Environment Variable
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ForceAuthentication

Will clear the stored authentication information when using -UseWebLogin (Windows Only) or -Interactive (all platforms) and allows you to authenticate again towards a site with different credentials.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Interactive

Connects to the Entra ID (Azure AD) using interactive login, allowing you to authenticate using multi-factor authentication.
This parameter has preference over `-UseWebLogin`.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LaunchBrowser

Launch a browser automatically and copy the code to enter to the clipboard

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ManagedIdentity

Connects using an Azure Managed Identity.
For use with Azure Functions, Azure Automation Runbooks (if configured to use a managed identity) or Azure Cloud Shell only.
This method will acquire a token using the built-in endpoints in the Azure Cloud Shell, Azure Automation Runbooks and Azure Functions.
Read up on the documentation (https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity)on how to make use of this option.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: System Assigned Managed Identity
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Client Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Principal Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MicrosoftGraphEndPoint

Custom Microsoft Graph endpoint to be used if we are using Azure Custom environment.
This will only work if `AzureEnvironment` parameter value is set to `Custom`.

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Access Token
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: System Assigned Managed Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Client Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Principal Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OSLogin

Connects using Web Account Manager (WAM).
This works only on Windows machines, on other OS will open browser.
Use this to open the native Windows authentication prompt.
It supports Windows Hello, conditional access policies, FIDO keys and other OS integration auth options.
Requires that the Entra ID app registration have `ms-appx-web://microsoft.aad.brokerplugin/{client_id}` as a redirect URI.
For more information, visit this link (https://learn.microsoft.com/en-us/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam).

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: OS login
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Realm

Authentication realm.
If not specified will be resolved from the url specified.

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RedirectUri

The Redirect URI of the Azure AD Application

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RelativeUrl

The site-relative URL of the site you're collecting to.
Only applies if you're using -UseWebLogin.

```yaml
Type: System.String
DefaultValue: /_layouts/15/settings.aspx
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ReturnConnection

Returns the connection for use with the -Connection parameter on cmdlets.
It will not touch the current connection which can be established by omitting this parameter.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Access Token
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: System Assigned Managed Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Client Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Principal Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Azure AD Workload Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SPOManagementShell

Switch to use SPO management shell

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Tenant

The Azure Active Directory tenant name, e.g.
mycompany.onmicrosoft.com or mycompany.com if you have added custom domains to your tenant

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TenantAdminUrl

The url to the Tenant Admin site.
If not specified, the cmdlets will assume to connect automatically to https://[tenantname]-admin.sharepoint.com where appropriate.

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Thumbprint

The thumbprint of the certificate containing the private key registered with the application in Azure Active Directory

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TransformationOnPrem

If you want to the use page transformation cmdlets, setting this switch will allow you to connect to an on-prem server.
Notice that this -only- applies to Transformation cmdlets.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

The Url of the site collection or subsite to connect to, i.e.
tenant.sharepoint.com, https://tenant.sharepoint.com, tenant.sharepoint.com/sites/hr, etc.

```yaml
Type: System.String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Access Token
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Web Login for Multi Factor Authentication
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: System Assigned Managed Identity
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Client Id
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Principal Id
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Azure AD Workload Identity
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UserAssignedManagedIdentityAzureResourceId

Can be used in combination with `-ManagedIdentity` to specify the Azure Resource ID of the user assigned managed identity to use.

```yaml
Type: System.String
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UserAssignedManagedIdentityClientId

Can be used in combination with `-ManagedIdentity` to specify the client id of the user assigned managed identity to use.

```yaml
Type: System.String
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: User Assigned Managed Identity by Client Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UserAssignedManagedIdentityObjectId

Can be used in combination with `-ManagedIdentity` to specify the object/principal id of the user assigned managed identity to use.

```yaml
Type: System.String
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases:
- UserAssignedManagedIdentityPrincipalId
ParameterSets:
- Name: User Assigned Managed Identity by Principal Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UseWebLogin

Windows only: Connects to SharePoint using legacy cookie based authentication.
Notice this type of authentication is limited in its functionality.
We will for instance not be able to acquire an access token for the Graph, and as a result none of the Graph related cmdlets will work.
Also some of the functionality of the provisioning engine (Get-PnPSiteTemplate, Get-PnPTenantTemplate, Invoke-PnPSiteTemplate, Invoke-PnPTenantTemplate) will not work because of this reason.
The cookies will in general expire within a few days and if you use -UseWebLogin within that time popup window will appear that will disappear immediately, this is expected.
Use -ForceAuthentication to reset the authentication cookies and force a new login.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ValidateConnection

When provided, the cmdlet will check to ensure the SharePoint Online site specified through `-Url` exists and if not, will throw an exception.
If you omit this flag or set it to $false, it will blindly set up a connection without validating that the site actually exists.
Making use of this option does make one extra call on the connection attempt, so it is recommended to only use it in scenarios where you know the site you're trying to connect o may not exist and would like to have feedback on this during the connect.

```yaml
Type: System.Management.Automation.SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SharePoint ACS (Legacy) App Only
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: SPO Management Shell Credentials
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: PnP Management Shell / DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Web Login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Interactive login for Multi Factor Authentication
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Access Token
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Environment Variable
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: System Assigned Managed Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Client Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Principal Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: User Assigned Managed Identity by Azure Resource Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Azure AD Workload Identity
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: OS login
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Online Version:](https://pnp.github.io/powershell/cmdlets/Connect-PnPOnline.html)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
