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

### Main (Default)
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Credentials <CredentialPipeBind>] [-CurrentCredentials]
 [-CreateDrive] [-DriveName <String>] [-TenantAdminUrl <String>] [<CommonParameters>]
```

### ACS (Legacy) App-Only using a clientId and clientSecret and an URL
```powershell
Connect-PnPOnline [-Url] <String> -ClientId <String> -ClientSecret <String> [-ReturnConnection] [-Realm <String>]  [-CreateDrive] [-DriveName <String>]  [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [<CommonParameters>]
```

### Azure Active Directory
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -RedirectUri <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [<CommonParameters>]
```

### App-Only with Azure Active Directory with a certificate file
```powershell
Connect-PnPOnline [-Url] <String> -ClientId <String> -Tenant <String> -CertificatePath <String> [-CertificatePassword <SecureString>] [-ReturnConnection]  [-CreateDrive] [-DriveName <String>] [-AzureEnvironment <AzureEnvironment>]
 [-TenantAdminUrl <String>] [<CommonParameters>]
```

### App-Only with Azure Active Directory with a certificate file
```powershell
Connect-PnPOnline [-Url] <String> -ClientId <String> -Tenant <String> -CertificateBase64Encoded <String> [-CertificatePassword <SecureString>] [-ReturnConnection]  [-CreateDrive] [-DriveName <String>] [-AzureEnvironment <AzureEnvironment>]
 [-TenantAdminUrl <String>] [<CommonParameters>]
```

### App-Only with Azure Active Directory using certificate from certificate store by thumbprint
```powershell
Connect-PnPOnline [-Url] <String> -ClientId <String> -Tenant <String> -Thumbprint <String> [-ReturnConnection]  [-CreateDrive] [-DriveName <String>] 
 [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [<CommonParameters>]
```

### DeviceLogin
```powershell
Connect-PnPOnline [-Url] <String> -DeviceLogin [-ClientId <String>] [-ReturnConnection] [-LaunchBrowser] [-AzureEnvironment <AzureEnvironment>] [<CommonParameters>]
```

### WebLogin for Multi-Factor authentication
```powershell
Connect-PnPOnline -Url <String> -UseWebLogin [-ForceAuthentication]
```

### Interactive login for Multi-Factor authentication
```powershell
Connect-PnPOnline -Url <String> -Interactive [-ClientId <String>] [-ForceAuthentication <SwitchParameter>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
```

### On-premises login for page transformation from on-premises SharePoint to SharePoint Online
```powershell
Connect-PnPOnline -Url <String> -TransformationOnPrem [-CurrentCredential]
```

### Managed Identity for use with Azure Cloud Shell and Azure Functions
```powershell
Connect-PnPOnline -ManagedIdentity
```

### Access Token
```powershell
Connect-PnPOnline -Url <String> -AccessToken <String> [-TenantAdminUrl <String>]
```

## DESCRIPTION
Connects to a SharePoint site or another API and creates a context that is required for the other PnP Cmdlets. See https://pnp.github.io/powershell/articles/connecting.html for more information on the options to connect.

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com"
```

Connect to SharePoint prompting for the username and password. When a generic credential is added to the Windows Credential Manager with https://contoso.sharepoint.com, PowerShell will not prompt for username and password and use those stored credentials instead.

### EXAMPLE 2
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -Credentials (Get-Credential)
```

Connect to SharePoint prompting for the username and password to use to authenticate

### EXAMPLE 3
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.de" -ClientId 344b8aab-389c-4e4a-8fa1-4c1ae2c0a60d -ClientSecret $clientSecret
```

This will authenticate you to the site using Legacy ACS authentication

### EXAMPLE 4
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -DeviceLogin
```

This will authenticate you using the PnP Management Shell Multi-Tenant application. A browser window will have to be opened where you have to enter a code that is shown in your PowerShell window.

### EXAMPLE 5
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -DeviceLogin -LaunchBrowser
```

This will authenticate you using the PnP Management Shell Multi-Tenant application. A browser window will automatically open and the code you need to enter will be automatically copied to your clipboard.

### EXAMPLE 6
```powershell
$password = (ConvertTo-SecureString -AsPlainText 'myprivatekeypassword' -Force)
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificatePath 'c:\mycertificate.pfx' -CertificatePassword $password  -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a locally available certificate containing a private key. See https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 7
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -Tenant 'contoso.onmicrosoft.com' -Thumbprint 34CFAA860E5FB8C44335A38A097C1E41EEA206AA
```

Connects to SharePoint using app-only tokens via an app's declared permission scopes. See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started. Ensure you have imported the private key certificate, typically the .pfx file, into the Windows Certificate Store for the certificate with the provided thumbprint.

### EXAMPLE 8
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId 6c5c98c7-e05a-4a0f-bcfa-0cfc65aa1f28 -CertificateBase64Encoded $base64encodedstring -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a certificate with a private key that has been base64 encoded. See https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 9
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -UseWebLogin
```

Note: See Example 10 as this is a preferred option over using -UseWebLogin. 

Connects to SharePoint using legacy cookie based authentication. Notice this type of authentication is limited in its functionality. We will for instance not be able to acquire an access token for the Graph, and as a result none of the Graph related cmdlets will work. Also some of the functionality of the provisioning engine (Get-PnPSiteTemplate, Get-PnPTenantTemplate, Invoke-PnPSiteTemplate, Invoke-PnPTenantTemplate) will not work because of this reason. The cookies will in general expire within a few days and if you use -UseWebLogin within that time popup window will appear that will dissappear immediately, this is expected. Use -ForceAuthentication to reset the authentication cookies and force a new login. See example 10 for a full support for interactive logins using MFA and the ability to acquire Graph tokens.

### EXAMPLE 10
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -Interactive
```

Connects to the Azure AD, acquires an access token and allows PnP PowerShell to access both SharePoint and the Microsoft Graph. By default it will use the PnP Management Shell multi-tenant application behind the scenes, so make sure to run `Register-PnPManagementShellAccess` first.

### EXAMPLE 11
```powershell
Connect-PnPOnline -Url "https://portal.contoso.com" -TransformationOnPrem -CurrentCredential
```

Connects to on-premises SharePoint 2013, 2016 or 2019 site with the current user's on-premises Windows credential (e.g. domain\user). This option is only supported for being able to transform on-premises classic wiki, webpart, blog and publishing pages into modern pages in a SharePoint Online site. Although other PnP cmdlets might work as well, they're officially not supported for being used in an on-premises context. See http://aka.ms/sharepoint/modernization/pages for more details on page transformation.

### EXAMPLE 12 
```powershell
Connect-PnPOnline -ManagedIdentity
Get-PnPTeamsTeam
```

Using this way of connecting only works with environments that support managed identies: Azure Functions and the Azure Cloud Shell. You cannot access SharePoint artifacts using this connection method: only the cmdlets that use the Microsoft Graph or Azure AD resources behind the scenes will work: Teams cmdlets, Flow cmdlets, Planner cmdlets and the Microsoft 365 Group cmdlets.

### EXAMPLE 13
```powershell
Connect-PnPOnline -Url https://contoso.sharepoint.com -AccessToken $token
```

This method assumes you have acquired a valid OAuth2 access token from Azure AD with the correct audience and permissions set. Using this method PnP PowerShell will not acquire tokens dynamically and if the token expires (typically after 1 hour) cmdlets will fail to work using this method. 


## PARAMETERS

### -AccessToken
Using this parameter you can provide your own access token. Notice that it is recommend to use one of the other connection methods as this will limits the offered functionality on PnP PowerShell. For instance if the token expires (typically after 1 hour) will not be able to acquire a new valid token, which the other connection methods do allow. You are fully responsible for providing your own valid token, for the correct audience, with the correct permissions scopes.

```yaml
Type: String
Parameter Sets: Access Token

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureEnvironment
The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
Parameter Sets: App-Only using a clientId and clientSecret and an URL, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, DeviceLogin, Azure Active Directory using Scopes, PnP Management Shell to the Microsoft Graph
Accepted values: Production, PPE, China, Germany, USGovernment

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
Parameter Sets: App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings

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
Parameter Sets: App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, DeviceLogin

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSecret
The client secret to use.

```yaml
Type: String
Parameter Sets: App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain

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
Parameter Sets: Main, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, Access Token

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Credentials
Credentials of the user to connect with. Either specify a PSCredential object or a string. In case of a string value a lookup will be done to the Generic Credentials section of the Windows Credentials in the Windows Credential Manager for the correct credentials.

```yaml
Type: CredentialPipeBind
Parameter Sets: Main, ADFS with user credentials, Azure Active Directory using Scopes

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
Parameter Sets: Main, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, Access Token

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
Parameter Sets: DeviceLogin

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeviceLogin
Log in using the Device Code flow. By default it will use the PnP Management Shell multi-tenant Azure AD application registration. You will be asked to consent to:

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
Aliases: PnPManagementShell

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Realm
Authentication realm. If not specified will be resolved from the url specified.

```yaml
Type: String
Parameter Sets: App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain

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
Parameter Sets: Azure Active Directory

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReturnConnection
Returns the connection for use with the -Connection parameter on cmdlets.

```yaml
Type: SwitchParameter
Parameter Sets: Main, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, Access Token, DeviceLogin

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Tenant
The Azure AD Tenant name,e.g. mycompany.onmicrosoft.com

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TenantAdminUrl
The url to the Tenant Admin site. If not specified, the cmdlets will assume to connect automatically to https://[tenantname]-admin.sharepoint.com where appropriate.

```yaml
Type: String
Parameter Sets: Main, App-Only using a clientId and clientSecret and an URL, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials

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
Parameter Sets: App-Only with Azure Active Directory using certificate from certificate store by thumbprint

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The Url of the site collection to connect to

```yaml
Type: String
Parameter Sets: Main, App-Only using a clientId and clientSecret and an URL, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, DeviceLogin

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: Access Token

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TransformationOnPrem
If you want to the use page transformation cmdlets, setting this switch will allow you to connect to an on-prem server. Notice that this -only- applies to Transformation cmdlets. 

```yaml
Type: SwitchParameter
Parameter Sets: Main

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseWebLogin
Windows only: Connects to SharePoint using legacy cookie based authentication. Notice this type of authentication is limited in its functionality. We will for instance not be able to acquire an access token for the Graph, and as a result none of the Graph related cmdlets will work. Also some of the functionality of the provisioning engine (Get-PnPSiteTemplate, Get-PnPTenantTemplate, Invoke-PnPSiteTemplate, Invoke-PnPTenantTemplate) will not work because of this reason. The cookies will in general expire within a few days and if you use -UseWebLogin within that time popup window will appear that will dissappear immediately, this is expected. Use -ForceAuthentication to reset the authentication cookies and force a new login.

```yaml
Type: SwitchParameter
Parameter Sets: WebLogin

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Interactive
Connects to the Azure AD using interactive login, allowing you to authenticate using multi-factor authentication. This parameter has preference over `-UseWebLogin`.

```yaml
Type: SwitchParameter
Parameter Sets: Interactive

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
Parameter Sets: WebLogin, Interactive

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ManagedIdentity
For use with Azure Functions (if configured to use a managed identity) or Azure Cloud Shell only. This method will acquire a token using the built-in endpoints in the Azure Cloud Shell and Azure Functions. Notice that using this connection method will not allow you to access SharePoint artifacts due to limitations of token acquisition. It will however allow you to use the Teams cmdlets, Flow cmdlets, Planner cmdlets and Microsoft 365 Group cmdlets.

```yaml
Type: SwitchParameter
Parameter Sets: Managed Identity for use with Azure Cloud Shell and Azure Functions

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


