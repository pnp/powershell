---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/connect-pnponline
schema: 2.0.0
title: Connect-PnPOnline
---

# Connect-PnPOnline

## SYNOPSIS
Connect to a SharePoint site

## SYNTAX

### Main (Default)
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Credentials <CredentialPipeBind>] [-CurrentCredentials]
 [-CreateDrive] [-DriveName <String>] [-TenantAdminUrl <String>] [-NoTelemetry] [<CommonParameters>]
```

### Token
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Realm <String>] -ClientSecret <String> [-CreateDrive]
 [-DriveName <String>] [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>] [-NoTelemetry]
 [<CommonParameters>]
```

### App-Only using a clientId and clientSecret and an URL
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Realm <String>] -ClientSecret <String> [-CreateDrive]
 [-DriveName <String>] -ClientId <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-NoTelemetry] [<CommonParameters>]
```

### App-Only using a clientId and clientSecret and an AAD Domain
```powershell
Connect-PnPOnline [-ReturnConnection] [-Realm <String>] -ClientSecret <String> [-CreateDrive]
 [-DriveName <String>] -ClientId <String> -AADDomain <String> [-NoTelemetry] [<CommonParameters>]
```

### WebLogin
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-UseWebLogin] [-CreateDrive] [-DriveName <String>]
 [-TenantAdminUrl <String>] [-NoTelemetry] [<CommonParameters>]
```

### ADFS with client Certificate
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-UseAdfsCert] [-ClientCertificate <X509Certificate2>]
 [-LoginProviderName <String>] [-CreateDrive] [-DriveName <String>] [-TenantAdminUrl <String>] [-NoTelemetry]
 [<CommonParameters>]
```

### ADFS with user credentials
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-Credentials <CredentialPipeBind>] [-UseAdfs]
 [-Kerberos] [-LoginProviderName <String>] [-CreateDrive] [-DriveName <String>] [-TenantAdminUrl <String>]
 [-NoTelemetry] [<CommonParameters>]
```

### Azure Active Directory
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -RedirectUri <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-NoTelemetry] [<CommonParameters>]
```

### App-Only with Azure Active Directory
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> [-CertificatePath <String>] [-CertificateBase64Encoded <String>]
 [-Certificate <X509Certificate2>] [-CertificatePassword <SecureString>] [-AzureEnvironment <AzureEnvironment>]
 [-TenantAdminUrl <String>] [-NoTelemetry] [<CommonParameters>]
```

### App-Only with Azure Active Directory using certificate as PEM strings
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> [-CertificatePassword <SecureString>] -PEMCertificate <String> -PEMPrivateKey <String>
 [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>] [-NoTelemetry] [<CommonParameters>]
```

### App-Only with Azure Active Directory using certificate from certificate store by thumbprint
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> -Thumbprint <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>]
 [-NoTelemetry] [<CommonParameters>]
```

### App-Only with Azure Active Directory using X502 certificates
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-CreateDrive] [-DriveName <String>] -ClientId <String>
 -Tenant <String> [-AzureEnvironment <AzureEnvironment>] [-TenantAdminUrl <String>] [-NoTelemetry]
 [<CommonParameters>]
```

### Access Token
```powershell
Connect-PnPOnline [-ReturnConnection] [[-Url] <String>] [-CreateDrive] [-DriveName <String>]
 -AccessToken <String> [-NoTelemetry] [<CommonParameters>]
```

### PnP Management Shell / DeviceLogin
```powershell
Connect-PnPOnline [-ReturnConnection] [-Url] <String> [-PnPManagementShell] [-LaunchBrowser]
 [-AzureEnvironment <AzureEnvironment>] [-NoTelemetry] [<CommonParameters>]
```

### Azure Active Directory using Scopes
```powershell
Connect-PnPOnline [-Credentials <CredentialPipeBind>] [-AzureEnvironment <AzureEnvironment>] -Scopes <String[]>
 [-NoTelemetry] [<CommonParameters>]
```

### PnP Management Shell to the Microsoft Graph
```powershell
Connect-PnPOnline [-LaunchBrowser] [-Graph] [-AzureEnvironment <AzureEnvironment>] [-NoTelemetry]
 [<CommonParameters>]
```

### Microsoft Graph using Azure Active Directory
```powershell
Connect-PnPOnline -AADDomain <String> [-NoTelemetry] [<CommonParameters>]
```

## DESCRIPTION
Connects to a SharePoint site or another API and creates a context that is required for the other PnP Cmdlets. See https://github.com/pnp/PnP-PowerShell/wiki/Connect-options for more information on the options to connect and the APIs you can access with them.

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
Connect-PnPOnline -Url "https://contoso.sharepoint.de" -ClientId 344b8aab-389c-4e4a-8fa1-4c1ae2c0a60d -ClientSecret $clientSecret -AzureEnvironment Germany
```

This will authenticate you to the German Azure environment using the German Azure endpoints for authentication

### EXAMPLE 4
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -PnPManagementShell
```

This will authenticate you using the PnP O365 Management Shell Multi-Tenant application. A browser window will have to be opened where you have to enter a code that is shown in your PowerShell window.

### EXAMPLE 5
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -PnPManagementShell -LaunchBrowser
```

This will authenticate you using the PnP O365 Management Shell Multi-Tenant application. A browser window will automatically open and the code you need to enter will be automatically copied to your clipboard.

### EXAMPLE 6
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -AccessToken $myaccesstoken
```

Connects using the provided access token

### EXAMPLE 7
```powershell
Connect-PnPOnline -Scopes "Mail.Read","Files.Read","ActivityFeed.Read"
```

Connects to Azure Active Directory interactively and gets an OAuth 2.0 Access Token to consume the resources of the declared permission scopes. It will utilize the Azure Active Directory enterprise application named PnP Management Shell with application id 31359c7f-bd7e-475c-86db-fdb8c937548e registered by the PnP team. If you want to connect using your own Azure Active Directory application registration, use one of the Connect-PnPOnline cmdlets using a -ClientId attribute instead and pre-assign the required permissions/scopes/roles in your application registration in Azure Active Directory. The available permission scopes for Microsoft Graph are defined at the following URL: https://docs.microsoft.com/graph/permissions-reference . If the requested scope(s) have been used with this connect cmdlet before, they will not be asked for consent again. You can request scopes from different APIs in one Connect, i.e. from Microsoft Graph and the Microsoft Office Management API. It will ask you to authenticate for each of the APIs you have listed scopes for.

### EXAMPLE 8
```powershell
Connect-PnPOnline -Scopes "Mail.Read","Files.Read","ActivityFeed.Read" -Credentials (New-Object System.Management.Automation.PSCredential ("johndoe@contoso.onmicrosoft.com", (ConvertTo-SecureString "password" -AsPlainText -Force)))
```

Connects to Azure Active Directory using delegated permissions and gets an OAuth 2.0 Access Token to consume the resources of the declared permission scopes. It will utilize the Azure Active Directory enterprise application named PnP Management Shell with application id 31359c7f-bd7e-475c-86db-fdb8c937548e registered by the PnP team. If you want to connect using your own Azure Active Directory application registration, use one of the Connect-PnPOnline cmdlets using a -ClientId attribute instead and pre-assign the required permissions/scopes/roles in your application registration in Azure Active Directory. The available permission scopes for Microsoft Graph are defined at the following URL: https://docs.microsoft.com/graph/permissions-reference . If the requested scope(s) have been used with this connect cmdlet before, they will not be asked for consent again. You can request scopes from different APIs in one Connect, i.e. from Microsoft Graph and the Microsoft Office Management API. You must have logged on interactively with the same scopes at least once without using -Credentials to allow for the permission grant dialog to show and allow constent for the user account you would like to use. You can provide this consent by logging in once with Connect-PnPOnline -Url <tenanturl> -PnPManagementShell -LaunchBrowser, and provide consent. This is a one-time action. From that moment on you will be able to use the cmdlet as stated here.

### EXAMPLE 9
```powershell
Connect-PnPOnline -ClientId '<id>' -ClientSecret '<secret>' -AADDomain 'contoso.onmicrosoft.com'
```

Connects to the Microsoft Graph API using application permissions via an app's declared permission scopes. See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/Graph.ConnectUsingAppPermissions for a sample on how to get started.

### EXAMPLE 10
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId '<id>' -Tenant 'contoso.onmicrosoft.com' -CertificatePath c:\absolute-path\to\pnp.pfx -CertificatePassword <if needed>
```

Connects to SharePoint using app-only tokens via an app's declared permission scopes. See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.

### EXAMPLE 11
```powershell
Connect-PnPOnline -ClientId <id> -CertificatePath 'c:\mycertificate.pfx' -CertificatePassword (ConvertTo-SecureString -AsPlainText 'myprivatekeypassword' -Force) -Url "https://contoso.sharepoint.com" -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a locally available certificate containing a private key. See https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 12
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId '<id>' -Tenant 'contoso.onmicrosoft.com' -Thumbprint 34CFAA860E5FB8C44335A38A097C1E41EEA206AA
```

Connects to SharePoint using app-only tokens via an app's declared permission scopes. See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started. Ensure you have imported the private key certificate, typically the .pfx file, into the Windows Certificate Store for the certificate with the provided thumbprint.

### EXAMPLE 13
```powershell
Connect-PnPOnline -ClientId <id> -CertificateBase64Encoded 'xxxx' -CertificatePassword (ConvertTo-SecureString -AsPlainText 'myprivatekeypassword' -Force) -Url "https://contoso.sharepoint.com" -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a certificate containing a private key encoded in base 64 such as received in an Azure Function when using Azure KeyVault. See https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 14
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId '<id>' -Tenant 'contoso.onmicrosoft.com' -PEMCertificate <PEM string> -PEMPrivateKey <PEM string> -CertificatePassword <if needed>
```

Connects to SharePoint using app-only tokens via an app's declared permission scopes. See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.

### EXAMPLE 15
```powershell
Connect-PnPOnline -ClientId <id> -Certificate $cert -CertificatePassword (ConvertTo-SecureString -AsPlainText 'myprivatekeypassword' -Force) -Url "https://contoso.sharepoint.com" -Tenant 'contoso.onmicrosoft.com'
```

Connects using an Azure Active Directory registered application using a certificate instance containing a private key. See https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azuread for a sample on how to get started.

### EXAMPLE 16
```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId '<id>' -Tenant 'contoso.onmicrosoft.com' -Certificate <X509Certificate2>
```

Connects to SharePoint using app-only auth in combination with a certificate. See https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azuread#using-this-principal-in-your-powershell-script-using-the-pnp-sites-core-library for a sample on how to get started.

## PARAMETERS

### -AADDomain
The AAD where the O365 app is registered. Eg.: contoso.com, or contoso.onmicrosoft.com.

```yaml
Type: String
Parameter Sets: App-Only using a clientId and clientSecret and an AAD Domain, Microsoft Graph using Azure Active Directory

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AccessToken
Connect with an existing Access Token

```yaml
Type: String
Parameter Sets: Access Token

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureEnvironment
The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
Parameter Sets: Token, App-Only using a clientId and clientSecret and an URL, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, PnP Management Shell / DeviceLogin, Azure Active Directory using Scopes, PnP Management Shell to the Microsoft Graph
Accepted values: Production, PPE, China, Germany, USGovernment

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Certificate
X509Certificate2 reference containing the private key to authenticate the requests to SharePoint Online

```yaml
Type: X509Certificate2
Parameter Sets: App-Only with Azure Active Directory

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CertificateBase64Encoded
Base64 Encoded X509Certificate2 certificate containing the private key to authenticate the requests to SharePoint Online such as retrieved in Azure Functions from Azure KeyVault

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory

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

### -ClientCertificate
The client certificate which you want to use for the ADFS authentication

```yaml
Type: X509Certificate2
Parameter Sets: ADFS with client Certificate

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
Parameter Sets: App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates

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
Parameter Sets: Token, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain

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
Parameter Sets: Main, Token, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, Access Token

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

### -CurrentCredentials
If you want to connect with the current user credentials

```yaml
Type: SwitchParameter
Parameter Sets: Main

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
Parameter Sets: Main, Token, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, Access Token

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Graph
Log in using the PnP O365 Management Shell application towards the Graph. You will be asked to consent to:

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
Parameter Sets: PnP Management Shell to the Microsoft Graph

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Kerberos
Authenticate using Kerberos to ADFS

```yaml
Type: SwitchParameter
Parameter Sets: ADFS with user credentials

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
Parameter Sets: PnP Management Shell / DeviceLogin, PnP Management Shell to the Microsoft Graph

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LoginProviderName
The name of the ADFS trusted login provider

```yaml
Type: String
Parameter Sets: ADFS with client Certificate, ADFS with user credentials

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoTelemetry
In order to help to make PnP PowerShell better, we can track anonymous telemetry. We track the version of the cmdlets you are using, which cmdlet you are executing and which version of SharePoint you are connecting to. Use Disable-PnPPowerShellTelemetry to turn this off in general or use the -NoTelemetry switch to turn it off for that session.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PEMCertificate
PEM encoded certificate

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory using certificate as PEM strings

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PEMPrivateKey
PEM encoded private key for the certificate

```yaml
Type: String
Parameter Sets: App-Only with Azure Active Directory using certificate as PEM strings

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PnPManagementShell
Log in using the PnP O365 Management Shell application. You will be asked to consent to:

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
Aliases: PnPO365ManagementShell

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
Parameter Sets: Token, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain

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
Parameter Sets: Main, Token, App-Only using a clientId and clientSecret and an URL, App-Only using a clientId and clientSecret and an AAD Domain, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, Access Token, PnP Management Shell / DeviceLogin

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scopes
The array of permission scopes to request from Azure Active Directory

```yaml
Type: String[]
Parameter Sets: Azure Active Directory using Scopes

Required: True
Position: Named
Default value: None
Accept pipeline input: False
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
The url to the Tenant Admin site. If not specified, the cmdlets will assume to connect automatically to https://&lt;tenantname&gt;-admin.sharepoint.com where appropriate.

```yaml
Type: String
Parameter Sets: Main, Token, App-Only using a clientId and clientSecret and an URL, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials

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
Parameter Sets: Main, Token, App-Only using a clientId and clientSecret and an URL, WebLogin, ADFS with client Certificate, ADFS with user credentials, Azure Active Directory, App-Only with Azure Active Directory, App-Only with Azure Active Directory using certificate as PEM strings, App-Only with Azure Active Directory using certificate from certificate store by thumbprint, App-Only with Azure Active Directory using X502 certificates, SPO Management Shell Credentials, PnP Management Shell / DeviceLogin

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

### -UseAdfs
If you want to connect to SharePoint using ADFS and credentials

```yaml
Type: SwitchParameter
Parameter Sets: ADFS with user credentials

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseAdfsCert
If you want to connect to SharePoint farm using ADFS with a client certificate

```yaml
Type: SwitchParameter
Parameter Sets: ADFS with client Certificate

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseWebLogin
If you want to connect to SharePoint with browser based login. This is required when you have multi-factor authentication (MFA) enabled.

```yaml
Type: SwitchParameter
Parameter Sets: WebLogin

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)