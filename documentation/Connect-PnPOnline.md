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

### Interactive for Multi Factor Authentication (Default)
```powershell
Connect-PnPOnline -Interactive [-ReturnConnection] -Url <String> [-PersistLogin] [-CreateDrive] [-DriveName <String>]
 [-ClientId <String>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>] [-ForceAuthentication] [-ValidateConnection] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### Credentials
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Credentials <CredentialPipeBind>] [-CurrentCredentials] [-PersistLogin]
 [-CreateDrive] [-DriveName <String>] [-ClientId <String>] [-RedirectUri <String>]
 [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-TransformationOnPrem] [-ValidateConnection] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### SharePoint ACS (Legacy) App Only
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Realm <String>] -ClientSecret <String> [-CreateDrive]
 [-DriveName <String>] -ClientId <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-ValidateConnection] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### App-Only with Azure Active Directory
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> [-CertificatePath <String>] [-CertificateBase64Encoded <String>]
 [-CertificatePassword <SecureString>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-ValidateConnection] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> -Thumbprint <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-ValidateConnection] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### DeviceLogin
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-PersistLogin] [-CreateDrive] [-DriveName <String>] [-DeviceLogin]
 [-ClientId <String>] [-AzureEnvironment <AzureEnvironment>] 
 [-ValidateConnection] [-MicrosoftGraphEndPoint <string>]
 [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### On-premises login for page transformation from on-premises SharePoint to SharePoint Online
```powershell
Connect-PnPOnline -Url <String> -TransformationOnPrem [-CurrentCredential]
```

### Access Token
```
Connect-PnPOnline -Url <String> -AccessToken <String> [-AzureEnvironment <AzureEnvironment>] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [-ReturnConnection]
```

### System Assigned Managed Identity
```powershell
Connect-PnPOnline [-Url <String>] -ManagedIdentity [-ReturnConnection]
```

### User Assigned Managed Identity by Client Id
```powershell
Connect-PnPOnline [-Url <String>] -ManagedIdentity -UserAssignedManagedIdentityClientId <String> [-ReturnConnection]
```

### User Assigned Managed Identity by Principal Id
```powershell
Connect-PnPOnline [-Url <String>] -ManagedIdentity -UserAssignedManagedIdentityObjectId <String> [-ReturnConnection]
```

### User Assigned Managed Identity by Azure Resource Id
```powershell
Connect-PnPOnline [-Url <String>] -ManagedIdentity -UserAssignedManagedIdentityAzureResourceId <String> [-ReturnConnection]
```

### Environment Variable
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-EnvironmentVariable] [-CurrentCredentials]
 [-CreateDrive] [-DriveName <String>] [-RedirectUri <String>]
 [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-TransformationOnPrem] [-ValidateConnection] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### Azure AD Workload Identity
```powershell
Connect-PnPOnline [-ReturnConnection] [-ValidateConnection] [-Url] <String>
 [-AzureADWorkloadIdentity] [-Connection <PnPConnection>]
```

### OS login
```powershell
Connect-PnPOnline -OSLogin [-ReturnConnection] [-Url] <String> [-PersistLogin] [-CreateDrive] [-DriveName <String>] 
 [-ClientId <String>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>] [-ForceAuthentication] [-ValidateConnection] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

### Federated Identity
```powershell
Connect-PnPOnline [-Url <String>] [-Tenant <String>] -FederatedIdentity [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>] [-ValidateConnection] [-MicrosoftGraphEndPoint <string>] [-AzureADLoginEndPoint <string>] [-Connection <PnPConnection>]
```

## DESCRIPTION
Connects to a SharePoint site or another API and creates a context that is required for the other PnP Cmdlets.
See https://pnp.github.io/powershell/articles/connecting.html for more information on the options to connect.

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com"
```

Connect to SharePoint prompting for the username and password.
When a generic credential is added to the Windows Credential Manager with https://contoso.sharepoint.com, PowerShell will not prompt for username and password and use those stored credentials instead. You will have to register your own App first, by means of `Register-PnPEntraIDApp` to use this method. You will also have to provide the `-ClientId` parameter starting September 9, 2024. Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created

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
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -DeviceLogin -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28
```

This will authenticate you using the specified Entra ID App registration. Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created.
A browser window will automatically open and the code you need to enter will be automatically copied to your clipboard. 

### EXAMPLE 5
```powershell
$password = (ConvertTo-SecureString -AsPlainText 'myprivatekeypassword' -Force)
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificatePath 'c:\mycertificate.pfx' -CertificatePassword $password  -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a locally available certificate containing a private key.
See https://learn.microsoft.com/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 6
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -Tenant 'contoso.onmicrosoft.com' -Thumbprint 34CFAA860E5FB8C44335A38A097C1E41EEA206AA
```

Connects to SharePoint using app-only tokens via an app's declared permission scopes.
See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.
Ensure you have imported the private key certificate, typically the .pfx file, into the Windows Certificate Store for the certificate with the provided thumbprint.

### EXAMPLE 7
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificateBase64Encoded $base64encodedstring -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a certificate with a private key that has been base64 encoded.
See https://learn.microsoft.com/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.


### EXAMPLE 8
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -Interactive -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28
```

Connects to the Azure AD, acquires an access token and allows PnP PowerShell to access both SharePoint and the Microsoft Graph. Notice that you will have to register your own App first, by means of `Register-PnPEntraIDApp` to use this method. You will also have to provide the `-ClientId` parameter starting September 9, 2024. Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created. If you use -Interactive and this environment variable is present you will not have to use -ClientId.


### EXAMPLE 9
```powershell
Connect-PnPOnline -Url "https://portal.contoso.com" -TransformationOnPrem -CurrentCredential
```

Connects to on-premises SharePoint 2013, 2016 or 2019 site with the current user's on-premises Windows credential (e.g. domain\user).
This option is only supported for being able to transform on-premises classic wiki, webpart, blog and publishing pages into modern pages in a SharePoint Online site.
Although other PnP cmdlets might work as well, they're officially not supported for being used in an on-premises context.
See http://aka.ms/sharepoint/modernization/pages for more details on page transformation.

### EXAMPLE 10
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ManagedIdentity
Get-PnPTeamsTeam
```

Connects using a system assigned managed identity to Microsoft Graph. Using this way of connecting only works with environments that support managed identities: Azure Functions, Azure Automation Runbooks and the Azure Cloud Shell. Read up on [this article](https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity) how it can be used.

### EXAMPLE 11
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ManagedIdentity -UserAssignedManagedIdentityObjectId 363c1b31-6872-47fd-a616-574d3aec2a51
Get-PnPList
```

Connects using an user assigned managed identity with object/principal ID 363c1b31-6872-47fd-a616-574d3aec2a51 to SharePoint Online. Using this way of connecting only works with environments that support managed identities: Azure Functions, Azure Automation Runbooks and the Azure Cloud Shell. Read up on [this article](https://pnp.github.io/powershell/articles/azurefunctions.html#by-using-a-managed-identity) how it can be used.

### EXAMPLE 12
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -AccessToken $token
```

This method assumes you have acquired a valid OAuth2 access token from Azure AD with the correct audience and permissions set.
Using this method PnP PowerShell will not acquire tokens dynamically and if the token expires (typically after 1 hour) cmdlets will fail to work using this method.

### EXAMPLE 13
```powershell
Connect-PnPOnline -Url contoso.sharepoint.com -EnvironmentVariable -Tenant 'contoso.onmicrosoft.com'
```

This example uses the `AZURE_CLIENT_CERTIFICATE_PATH` and `AZURE_CLIENT_CERTIFICATE_PASSWORD` environment variable values to authenticate. The `AZURE_CLIENT_ID` environment variable must be present and `Tenant` parameter value must be provided.

If these environment variables are not present, it will try to find `ENTRAID_APP_CERTIFICATE_PATH` or `ENTRAID_CLIENT_CERTIFICATE_PATH` and for certificate password use `ENTRAID_APP_CERTIFICATE_PASSWORD` or `ENTRAID_CLIENT_CERTIFICATE_PASSWORD` as fallback.

### EXAMPLE 14
```powershell
Connect-PnPOnline -Url contoso.sharepoint.com -EnvironmentVariable
```

This example uses the `AZURE_USERNAME` and `AZURE_PASSWORD` environment variables as credentials to authenticate.  If these environment variables are not available, it will use `ENTRAID_USERNAME` and `ENTRAID_PASSWORD` environment variables as fallback.

If `AZURE_CLIENT_ID` is not present, alternatively it will try to use `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` environment variables as fallback.

This method assumes you have the necessary environment variables available. For more information about the required environment variables, please refer to this article, [Azure.Identity Environment Variables](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/identity/Azure.Identity#environment-variables) here.

So, when using `-EnvironmentVariable` method for authenticating, we will require `AZURE_CLIENT_CERTIFICATE_PATH`, `AZURE_CLIENT_CERTIFICATE_PASSWORD` and `AZURE_CLIENT_ID` environment variables for using the service principal with certificate method for authentication.

If `AZURE_USERNAME`, `AZURE_PASSWORD` and `AZURE_CLIENT_ID`, we will use these environment variables and authenticate using credentials flow.

If `ENTRAID_USERNAME`, `ENTRAID_PASSWORD` and `ENTRAID_APP_ID` , we will use these environment variables and authenticate using credentials flow.

We support only Service principal with certificate and Username with password mode for authentication. Configuration will be attempted in that order. For example, if values for a certificate and username+password are both present, the client certificate method will be used.

### EXAMPLE 15
```
Connect-PnPOnline -Url contoso.sharepoint.com -AzureEnvironment Custom -MicrosoftGraphEndPoint "custom.graph.microsoft.com" -AzureADLoginEndPoint "https://custom.login.microsoftonline.com"
```

Use this method to connect to a custom Azure Environment. You can also specify the `MicrosoftGraphEndPoint` and `AzureADLoginEndPoint` parameters if applicable. If specified, then these values will be used to make requests to Graph and to retrieve access token.

### EXAMPLE 16
```powershell
Connect-PnPOnline -Url contoso.sharepoint.com -AzureADWorkloadIdentity
```

This example uses Azure AD Workload Identity to retrieve access tokens. For more information about this, please refer to this article, [Azure AD Workload Identity](https://azure.github.io/azure-workload-identity/docs/introduction.html). We are following the guidance mentioned in [this sample](https://github.com/Azure/azure-workload-identity/blob/main/examples/msal-net/akvdotnet/TokenCredential.cs) to retrieve the access tokens.

### EXAMPLE 17
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -OSLogin
```

Connects to the Azure AD with WAM (aka native Windows authentication prompt), acquires an access token and allows PnP PowerShell to access both SharePoint and the Microsoft Graph. Notice that you will have to register your own App first, by means of Register-PnPEntraIDAdd to use this method. You will also have to provide the -ClientId parameter starting September 9, 2024. Alternatively, create an environment variable, call it `ENTRAID_APP_ID` or `ENTRAID_CLIENT_ID` and set the value to the app id you created.

WAM is a more secure & faster way of authenticating in Windows OS. It supports Windows Hello, FIDO keys , conditional access policies and more.

### EXAMPLE 18
```powershell
$keyStorageflags = [System.Security.Cryptography.X509Certificates.X509KeyStorageFlags]::MachineKeySet -bor [System.Security.Cryptography.X509Certificates.X509KeyStorageFlags]::PersistKeySet

Connect-PnPOnline -Url "contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificateBase64Encoded $base64encodedstring -X509KeyStorageFlags $keyStorageflags -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a certificate with a private key that has been base64 encoded.
See [Security App-only EntraId guidance](https://learn.microsoft.com/sharepoint/dev/solution-guidance/security-apponly-azuread) for a sample on how to get started.

See [X509 key storage flags](https://learn.microsoft.com/dotnet/api/system.security.cryptography.x509certificates.x509keystorageflags) for information on how to configure key storage when creating the certificate.

### EXAMPLE 19
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -Credentials "https://contoso.sharepoint.com"
```

Connect to SharePoint using Credentials (username and password) from Credential Manager (Windows) or Keychain (Mac) with the specified name to use to authenticate.

On Windows, this entry needs to be under "Generic Credentials".

### EXAMPLE 20
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -Tenant 'contoso.onmicrosoft.com' -FederatedIdentity
```

Connect to SharePoint/Microsoft Graph using federated identity credentials.

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
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Interactive, Access Token, Environment Variable, Managed Identity
Aliases:
Accepted values: Production, PPE, China, Germany, USGovernment, USGovernmentHigh, USGovernmentDoD, Custom

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
Parameter Sets: Credentials, DeviceLogin, Interactive
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

### -Connection
Optional connection to be reused by the new connection. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

When passed in, the ClientId/AppId used for the passed in connection will be used for the new connection. It will override any -ClientId or -AppId parameter passed in.

```yaml
Type: PnPConnection
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Interactive login for Multi Factor Authentication, Environment Variable

Required: False
Position: Named
Default value: PnPConnection.Current
Accept pipeline input: False
Accept wildcard characters: False
```

### -CreateDrive
If you want to create a PSDrive connected to the URL

```yaml
Type: SwitchParameter
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Environment Variable
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
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Environment Variable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeviceLogin
Log in using the Device Code flow.
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
Parameter Sets: DeviceLogin

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -PersistLogin
Persist the current access token and related information in a locally stored cache. This cache will be retained between PowerShell sessions and will also be available after a reboot. You only need to provide this switch one time on Connect-PnPOnline cmdlet, it will after that retain the information and reuse it for new connections to the same tenant. Notice that while using a cached token, if you change the permissions of an application registration, the token associated with that registration will not be updated automatically in the cache. You will have to clear the cache entry first and reauthenticate: use `Disconnect-PnPOnline -ClearPersistedLogin`

```yaml
Type: SwitchParameter
Parameter Sets: Credentials, DeviceLogin, Interactive, OSLogin

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
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Access Token, Environment Variable, Azure AD Workload Identity
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
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Environment Variable
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
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Access Token, Environment Variable, Azure AD Workload Identity
Aliases:

Required: True (Except when using -ManagedIdentity and -AzureADWorkloadIdentity)
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ValidateConnection
When provided, the cmdlet will check to ensure the SharePoint Online site specified through `-Url` exists and if not, will throw an exception. If you omit this flag or set it to $false, it will blindly set up a connection without validating that the site actually exists. Making use of this option does make one extra call on the connection attempt, so it is recommended to only use it in scenarios where you know the site you're trying to connect o may not exist and would like to have feedback on this during the connect.

```yaml
Type: SwitchParameter
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Web Login for Multi Factor Authentication, Interactive for Multi Factor Authentication, Access Token, Environment Variable, Azure AD Workload Identity
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

```yaml
Type: String
Parameter Sets: Web Login for Multi Factor Authentication
Aliases:

Required: False
Position: Named
Default value: /_layouts/15/settings.aspx
Accept pipeline input: False
Accept wildcard characters: False
```

### -Interactive
Connects to the Entra ID (Azure AD) using interactive login, allowing you to authenticate using multi-factor authentication.

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
Will clear the stored authentication information when using Interactive login (all platforms) and allows you to authenticate again towards a site with different credentials.

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
Parameter Sets: System Assigned Managed Identity, User Assigned Managed Identity by Client Id, User Assigned Managed Identity by Principal Id, User Assigned Managed Identity by Azure Resource Id
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserAssignedManagedIdentityObjectId
Can be used in combination with `-ManagedIdentity` to specify the object/principal id of the user assigned managed identity to use.

```yaml
Type: String
Parameter Sets: User Assigned Managed Identity by Principal Id
Aliases: UserAssignedManagedIdentityPrincipalId

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserAssignedManagedIdentityClientId
Can be used in combination with `-ManagedIdentity` to specify the client id of the user assigned managed identity to use.

```yaml
Type: String
Parameter Sets: User Assigned Managed Identity by Client Id
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserAssignedManagedIdentityAzureResourceId
Can be used in combination with `-ManagedIdentity` to specify the Azure Resource ID of the user assigned managed identity to use.

```yaml
Type: String
Parameter Sets: User Assigned Managed Identity by Azure Resource Id
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
Connects using the necessary environment variables. For more information the required environment variables, please refer to this article, [Azure.Identity Environment Variables](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/identity/Azure.Identity#environment-variables) here. We support only Service principal with certificate and Username with password mode for authentication. Configuration will be attempted in that order. For example, if values for a certificate and username+password are both present, the client certificate method will be used. By default, it will use the `-ClientId` specified in `AZURE_CLIENT_ID` environment variable.

```yaml
Type: SwitchParameter
Parameter Sets: Environment Variable

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MicrosoftGraphEndPoint
Custom Microsoft Graph endpoint to be used if we are using Azure Custom environment. This will only work if `AzureEnvironment` parameter value is set to `Custom`.

```yaml
Type: String
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Interactive, Access Token, Environment Variable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureADLoginEndPoint
Custom Azure AD login endpoint to be used if we are using Azure Custom environment to retrieve access token. This will only work if `AzureEnvironment` parameter value is set to `Custom`.

```yaml
Type: String
Parameter Sets: Credentials, SharePoint ACS (Legacy) App Only, App-Only with Azure Active Directory, App-Only with Azure Active Directory using a certificate from the Windows Certificate Management Store by thumbprint, DeviceLogin, Interactive, Access Token, Environment Variable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureADWorkloadIdentity
Connects using the Azure AD Workload Identity.

```yaml
Type: SwitchParameter
Parameter Sets: Azure AD Workload Identity

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OSLogin

Connects using Web Account Manager (WAM). This works only on Windows machines, on other OS will open browser. Use this to open the native Windows authentication prompt. It supports Windows Hello, conditional access policies, FIDO keys and other OS integration auth options. Requires that the Entra ID app registration have `ms-appx-web://microsoft.aad.brokerplugin/{client_id}` as a redirect URI. For more information, visit this [link](https://learn.microsoft.com/en-us/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam).

```yaml
Type: SwitchParameter
Parameter Sets: OS login
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -X509KeyStorageFlags

Defines where and how to import the private key of an X.509 certificate.

This enumeration supports a bitwise combination of its member values.

```yaml
Type: System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
Parameter Sets: App-Only with Azure Active Directory
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FederatedIdentity

Connects using Federated Identity. For more information on this, you can visit [this link](https://learn.microsoft.com/en-us/entra/workload-id/workload-identity-federation-create-trust?pivots=identity-wif-apps-methods-rest).

```yaml
Type: SwitchParameter
Parameter Sets: Federated Identity
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
