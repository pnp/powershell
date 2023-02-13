---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Connect-PnPOnline.html
external help file: PnP.PowerShell.dll-Help.xml
title: Connect-PnPOnline
---

# Connect-PnPOnline

## SYNOPSIS
Connect to a SharePoint site

## SYNTAX

### Credentials (Default)
```
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Credentials <CredentialPipeBind>] [-CurrentCredentials]
 [-CreateDrive] [-DriveName <String>] [-ClientId <String>] [-RedirectUri <String>]
 [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-TransformationOnPrem] [-ValidateConnection]
```

### SharePoint ACS (Legacy) App Only
```
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Realm <String>] -ClientSecret <String> [-CreateDrive]
 [-DriveName <String>] -ClientId <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-ValidateConnection]
```

### App-Only with Azure Active Directory
```
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> [-CertificatePath <String>] [-CertificateBase64Encoded <String>]
 [-CertificatePassword <SecureString>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-ValidateConnection]
```

### App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
```
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> -Thumbprint <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-ValidateConnection]
```

### PnP Management Shell / DeviceLogin
```
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] [-DeviceLogin]
 [-LaunchBrowser] [-ClientId <String>] [-AzureEnvironment <AzureEnvironment>] 
 [-ValidateConnection]
```

### Web Login for Multi Factor Authentication
```
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>]
 [-TenantAdminUrl <String>] [-UseWebLogin] [-ForceAuthentication]
 [-ValidateConnection]
```

### Interactive for Multi Factor Authentication
```
Connect-PnPOnline -Interactive [-ReturnConnection] -Url <String> [-CreateDrive] [-DriveName <String>] [-LaunchBrowser]
 [-ClientId <String>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>] [-ForceAuthentication] [-ValidateConnection]
```

### On-premises login for page transformation from on-premises SharePoint to SharePoint Online
```powershell
Connect-PnPOnline -Url <String> -TransformationOnPrem [-CurrentCredential]
```

### Access Token
```
Connect-PnPOnline -Url <String> -AccessToken <String> [-AzureEnvironment <AzureEnvironment>] [-ReturnConnection]
```

### Managed Identity
```
Connect-PnPOnline -Url <String> [-UserAssignedManagedIdentityObjectId <String>]
```

### Environment Variable
```
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-EnvironmentVariable] [-CurrentCredentials]
 [-CreateDrive] [-DriveName <String>] [-RedirectUri <String>]
 [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-TransformationOnPrem] [-ValidateConnection]
```

## DESCRIPTION
Connects to a SharePoint site or another API and creates a context that is required for the other PnP Cmdlets.
See https://pnp.github.io/powershell/articles/connecting.html for more information on the options to connect.

## EXAMPLES

### EXAMPLE 1
```
Connect-PnPOnline -Url "contoso.sharepoint.com"
```

Connect to SharePoint prompting for the username and password.
When a generic credential is added to the Windows Credential Manager with https://contoso.sharepoint.com, PowerShell will not prompt for username and password and use those stored credentials instead.

### EXAMPLE 2
```
Connect-PnPOnline -Url "contoso.sharepoint.com" -Credentials (Get-Credential)
```

Connect to SharePoint prompting for the username and password to use to authenticate

### EXAMPLE 3
```
Connect-PnPOnline -Url "contoso.sharepoint.de" -ClientId 344b8aab-389c-4e4a-8fa1-4c1ae2c0a60d -ClientSecret $clientSecret
```

This will authenticate you to the site using Legacy ACS authentication

### EXAMPLE 4
```
Connect-PnPOnline -Url "contoso.sharepoint.com" -DeviceLogin
```

This will authenticate you using the PnP Management Shell Multi-Tenant application.
A browser window will have to be opened where you have to enter a code that is shown in your PowerShell window.

### EXAMPLE 5
```
Connect-PnPOnline -Url "contoso.sharepoint.com" -DeviceLogin -LaunchBrowser
```

This will authenticate you using the PnP Management Shell Multi-Tenant application.
A browser window will automatically open and the code you need to enter will be automatically copied to your clipboard.

### EXAMPLE 6
```
$password = (ConvertTo-SecureString -AsPlainText 'myprivatekeypassword' -Force)
Connect-PnPOnline -Url "contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificatePath 'c:\mycertificate.pfx' -CertificatePassword $password  -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a locally available certificate containing a private key.
See https://learn.microsoft.com/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 7
```
Connect-PnPOnline -Url "contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -Tenant 'contoso.onmicrosoft.com' -Thumbprint 34CFAA860E5FB8C44335A38A097C1E41EEA206AA
```

Connects to SharePoint using app-only tokens via an app's declared permission scopes.
See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.
Ensure you have imported the private key certificate, typically the .pfx file, into the Windows Certificate Store for the certificate with the provided thumbprint.

### EXAMPLE 8
```
Connect-PnPOnline -Url "contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificateBase64Encoded $base64encodedstring -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a certificate with a private key that has been base64 encoded.
See https://learn.microsoft.com/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 9
```
Connect-PnPOnline -Url "contoso.sharepoint.com" -UseWebLogin
```

Note: See Example 10 as this is a preferred option over using -UseWebLogin.

Connects to SharePoint using legacy cookie based authentication.
Notice this type of authentication is limited in its functionality.
We will for instance not be able to acquire an access token for the Graph, and as a result none of the Graph related cmdlets will work.
Also some of the functionality of the provisioning engine (Get-PnPSiteTemplate, Get-PnPTenantTemplate, Invoke-PnPSiteTemplate, Invoke-PnPTenantTemplate) will not work because of this reason.
The cookies will in general expire within a few days and if you use -UseWebLogin within that time popup window will appear that will dissappear immediately, this is expected.
Use -ForceAuthentication to reset the authentication cookies and force a new login.
See example 10 for a full support for interactive logins using MFA and the ability to acquire Graph tokens.

### EXAMPLE 10
```
Connect-PnPOnline -Url "contoso.sharepoint.com" -Interactive
```

Connects to the Azure AD, acquires an access token and allows PnP PowerShell to access both SharePoint and the Microsoft Graph.
By default it will use the PnP Management Shell multi-tenant application behind the scenes, so make sure to run \`Register-PnPManagementShellAccess\` first.

### EXAMPLE 11
```
Connect-PnPOnline -Url "https://portal.contoso.com" -TransformationOnPrem -CurrentCredential
```

Connects to on-premises SharePoint 2013, 2016 or 2019 site with the current user's on-premises Windows credential (e.g. domain\user).
This option is only supported for being able to transform on-premises classic wiki, webpart, blog and publishing pages into modern pages in a SharePoint Online site.
Although other PnP cmdlets might work as well, they're officially not supported for being used in an on-premises context.
See http://aka.ms/sharepoint/modernization/pages for more details on page transformation.

### EXAMPLE 12
```
Connect-PnPOnline -Url contoso.sharepoint.com -ManagedIdentity
Get-PnPTeamsTeam
```

Connects using a system assigned managed identity to Microsoft Graph. Using this way of connecting only works with environments that support managed identies: Azure Functions, Azure Automation Runbooks and the Azure Cloud Shell. Read up on [this article](https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity) how it can be used.

### EXAMPLE 13
```
Connect-PnPOnline -Url contoso.sharepoint.com -ManagedIdentity -UserAssignedManagedIdentityObjectId 363c1b31-6872-47fd-a616-574d3aec2a51
Get-PnPList
```

Connects using an user assigned managed identity with object/principal ID 363c1b31-6872-47fd-a616-574d3aec2a51 to SharePoint Online. Using this way of connecting only works with environments that support managed identies: Azure Functions, Azure Automation Runbooks and the Azure Cloud Shell. Read up on [this article](https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity) how it can be used.

### EXAMPLE 14
```
Connect-PnPOnline -Url contoso.sharepoint.com -AccessToken $token
```

This method assumes you have acquired a valid OAuth2 access token from Azure AD with the correct audience and permissions set.
Using this method PnP PowerShell will not acquire tokens dynamically and if the token expires (typically after 1 hour) cmdlets will fail to work using this method.

### EXAMPLE 15
```
Connect-PnPOnline -Url contoso.sharepoint.com -EnvironmentVariable -Tenant 'contoso.onmicrosoft.com'
```

This example uses the `AZURE_CLIENT_CERTIFICATE_PATH` and `AZURE_CLIENT_CERTIFICATE_PASSWORD` environment variable values to authenticate. The `AZURE_CLIENT_ID` environment variable must be present and `Tenant` parameter value must be provided.

### EXAMPLE 16
```
Connect-PnPOnline -Url contoso.sharepoint.com -EnvironmentVariable
```

This example uses the `AZURE_USERNAME` and `AZURE_PASSWORD` environment variables as credentials to authenticate. If `AZURE_CLIENT_ID` is not present, then it will try to use the default `PnP Management Shell Azure AD app` as fallback and attempt to authenticate.

This method assumes you have the necessary environment variables available. For more information about the required environment variables, please refer to this article, [Azure.Identity Environment Variables](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/identity/Azure.Identity#environment-variables) here.

So, when using `-EnvironmentVariable` method for authenticating, we will require `AZURE_CLIENT_CERTIFICATE_PATH`, `AZURE_CLIENT_CERTIFICATE_PASSWORD` and `AZURE_CLIENT_ID` environment variables for using the service principal with certificate method for authentication.

If `AZURE_USERNAME`, `AZURE_PASSWORD` and `AZURE_CLIENT_ID`, we will use these environment variables and authenticate using credentials flow.

We support only Service principal with certificate and Username with password mode for authentication. Configuration will be attempted in that order. For example, if values for a certificate and username+password are both present, the client certificate method will be used.
## PARAMETERS

### -AccessToken
Using this parameter you can provide your own access token.
Notice that it is recommend to use one of the other connection methods as this will limits the offered functionality on PnP PowerShell.
For instance if the token expires (typically after 1 hour) will not be able to acquire a new valid token, which the other connection methods do allow.
You are responsible for providing your own valid access token when using this parameter, for the correct audience, with the correct permissions scopes.

```yaml
Type: String
Parameter Sets: Access Token
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureEnvironment
The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, PnP Management Shell / DeviceLogin, Interactive, Access Token, Environment Variable
Aliases:
Accepted values: Production, PPE, China, Germany, USGovernment, USGovernmentHigh, USGovernmentDoD

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificatePassword
Password to the certificate (*.pfx)

```yaml
Type: SecureString
Parameter Sets: App-Only with Azure Active Directory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificatePath
Path to the certificate containing the private key (*.pfx)

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientId
The Client ID of the Azure AD Application

```yaml
Type: String
Parameter Sets: Credentials, PnP Management Shell / DeviceLogin, Interactive
Aliases: ApplicationId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSecret
The client secret to use. When using this, technically an Azure Access Control Service (ACS) authentication will take place. This effectively means only cmdlets that are connecting to SharePoint Online will work. Cmdlets using Microsoft Graph or any other API behind the scenes will not work.

```yaml
Type: String
Parameter Sets: SharePoint ACS (Legacy) App Only
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CreateDrive
If you want to create a PSDrive connected to the URL

```yaml
Type: SwitchParameter
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, SPO Management Shell Credentials, PnP Management Shell / DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Environment Variable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Credentials
Credentials of the user to connect with.
Either specify a PSCredential object or a string.
In case of a string value a lookup will be done to the Generic Credentials section of the Windows Credentials in the Windows Credential Manager for the correct credentials.

```yaml
Type: CredentialPipeBind
Parameter Sets: Credentials
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CurrentCredentials
Use credentials of the currently logged in user. Applicable exclusively when connecting to on premises SharePoint Server via PnP.
Switch parameter.

```yaml
Type: CredentialPipeBind
Parameter Sets: Credentials
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DriveName
Name of the PSDrive to create (default: SPO)

```yaml
Type: String
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, SPO Management Shell Credentials, PnP Management Shell / DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Environment Variable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LaunchBrowser
Launch a browser automatically and copy the code to enter to the clipboard

```yaml
Type: SwitchParameter
Parameter Sets: PnP Management Shell / DeviceLogin, Interactive
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
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
Type: SwitchParameter
Parameter Sets: PnP Management Shell / DeviceLogin
Aliases: PnPManagementShell, PnPO365ManagementShell

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Realm
Authentication realm.
If not specified will be resolved from the url specified.

```yaml
Type: String
Parameter Sets: SharePoint ACS (Legacy) App Only
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RedirectUri
The Redirect URI of the Azure AD Application

```yaml
Type: String
Parameter Sets: Credentials
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReturnConnection
Returns the connection for use with the -Connection parameter on cmdlets. It will not touch the current connection which can be established by omitting this parameter.

```yaml
Type: SwitchParameter
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, SPO Management Shell Credentials, PnP Management Shell / DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Access Token, Environment Variable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Tenant
The Azure Active Directory tenant name, e.g. mycompany.onmicrosoft.com or mycompany.com if you have added custom domains to your tenant

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, Environment Variable
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TenantAdminUrl
The url to the Tenant Admin site.
If not specified, the cmdlets will assume to connect automatically to https://\[tenantname\]-admin.sharepoint.com where appropriate.

```yaml
Type: String
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, SPO Management Shell Credentials, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Environment Variable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Thumbprint
The thumbprint of the certificate containing the private key registered with the application in Azure Active Directory

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The Url of the site collection or subsite to connect to, i.e. tenant.sharepoint.com, https://tenant.sharepoint.com, tenant.sharepoint.com/sites/hr, etc.

```yaml
Type: String
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, SPO Management Shell Credentials, PnP Management Shell / DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Access Token, Environment Variable
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ValidateConnection
When provided, the cmdlet will check to ensure the SharePoint Online site specified through `-Url` exists and if not, will throw an exception. If you omit this flag or set it to $false, it will blindly set up a connection without validating that the site actually exists. Making use of this option does make one extra call on the connection attempt, so it is recommended to only use it in scenarios where you know the site you're trying to connect o may not exist and would like to have feedback on this during the connect.

```yaml
Type: SwitchParameter
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, SPO Management Shell Credentials, PnP Management Shell / DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Access Token, Environment Variable
Aliases:

Required: False
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -TransformationOnPrem
If you want to the use page transformation cmdlets, setting this switch will allow you to connect to an on-prem server.
Notice that this -only- applies to Transformation cmdlets.

```yaml
Type: SwitchParameter
Parameter Sets: Credentials, Environment Variable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseWebLogin
Windows only: Connects to SharePoint using legacy cookie based authentication.
Notice this type of authentication is limited in its functionality.
We will for instance not be able to acquire an access token for the Graph, and as a result none of the Graph related cmdlets will work.
Also some of the functionality of the provisioning engine (Get-PnPSiteTemplate, Get-PnPTenantTemplate, Invoke-PnPSiteTemplate, Invoke-PnPTenantTemplate) will not work because of this reason.
The cookies will in general expire within a few days and if you use -UseWebLogin within that time popup window will appear that will dissappear immediately, this is expected.
Use -ForceAuthentication to reset the authentication cookies and force a new login.

```yaml
Type: SwitchParameter 
Parameter Sets: Web Login for Multi Factor Authentication
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Interactive
Connects to the Azure AD using interactive login, allowing you to authenticate using multi-factor authentication.
This parameter has preference over \`-UseWebLogin\`.

```yaml
Type: SwitchParameter
Parameter Sets: Interactive for Multi Factor Authentication
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ForceAuthentication
Will clear the stored authentication information when using -UseWebLogin (Windows Only) or -Interactive (all platforms) and allows you to authenticate again towards a site with different credentials.

```yaml
Type: SwitchParameter
Parameter Sets: Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ManagedIdentity
Connects using an Azure Managed Identity. For use with Azure Functions, Azure Automation Runbooks (if configured to use a managed identity) or Azure Cloud Shell only.
This method will acquire a token using the built-in endpoints in the Azure Cloud Shell, Azure Automation Runbooks and Azure Functions.
Read up on [the documentation](https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity) on how to make use of this option.

```yaml
Type: SwitchParameter
Parameter Sets: Managed Identity
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserAssignedManagedIdentityObjectId
Can be used in combination with `-ManagedIdentity` to specify the object/principal id of the user assigned managed identity to use. Alternative to UserAssignedManagedIdentityClientId. If both are not provided, a system assigned managed identity will be used.

```yaml
Type: String
Parameter Sets: Managed Identity
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```


### -UserAssignedManagedIdentityClientId
Can be used in combination with `-ManagedIdentity` to specify the client id of the user assigned managed identity to use. Alternative to UserAssignedManagedIdentityObjectId. If both are not provided, a system assigned managed identity will be used.

```yaml
Type: String
Parameter Sets: Managed Identity
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificateBase64Encoded
Specify a base64 encoded string as representing the private certificate.

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while going through setting up a connection.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnvironmentVariable
Connects using the necessary environment variables. For more information the required environment variables, please refer to this article, [Azure.Identity Environment Variables](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/identity/Azure.Identity#environment-variables) here. We support only Service principal with certificate and Username with password mode for authentication. Configuration will be attempted in that order. For example, if values for a certificate and username+password are both present, the client certificate method will be used. By default, it will use the `-ClientId` specified in `AZURE_CLIENT_ID` environment variable. If that value is empty, it will fallback to the PnP Management Shell Azure AD App.

```yaml
Type: SwitchParameter
Parameter Sets: Environment Variable

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
