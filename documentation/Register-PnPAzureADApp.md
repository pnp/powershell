---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Register-PnPAzureADApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Register-PnPAzureADApp
---

# Register-PnPAzureADApp

## SYNOPSIS

Registers an Azure AD App and optionally creates a new self-signed certificate to use with the application registration.

## SYNTAX

### Generate Certificate

```
Register-PnPAzureADApp -ApplicationName <String> -Tenant <String> [-Username <String>]
 [-Password <SecureString>] [-DeviceLogin] [-Interactive] [-CommonName <String>] [-OutPath <String>]
 [-Store <StoreLocation>] [-GraphApplicationPermissions <Permission[]>]
 [-GraphDelegatePermissions <Permission[]>] [-SharePointApplicationPermissions <Permission[]>]
 [-SharePointDelegatePermissions <Permission[]>] [-Country <String>] [-State <String>]
 [-Locality <String>] [-Organization <String>] [-OrganizationUnit <String>] [-ValidYears <Int>]
 [-CertificatePassword <SecureString>] [-NoPopup] [-LogoFilePath <string>]
 [-MicrosoftGraphEndPoint <string>] [-EntraIDLoginEndPoint <string>]
 [-SignInAudience <EntraIDSignInAudience>] [-LaunchBrowser <SwitchParameter>]
```

### Existing Certificate

```
Register-PnPAzureADApp -CertificatePath <String> -ApplicationName <String> -Tenant <String>
 [-Username <String>] [-Password <SecureString>] [-DeviceLogin] [-Interactive]
 [-GraphApplicationPermissions <Permission[]>] [-GraphDelegatePermissions <Permission[]>]
 [-SharePointApplicationPermissions <Permission[]>] [-SharePointDelegatePermissions <Permission[]>]
 [-CertificatePassword <SecureString>] [-NoPopup] [-LogoFilePath <string>]
 [-LaunchBrowser <SwitchParameter>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Registers an Azure AD App and optionally creates a new self-signed certificate to use with the application registration. You can login either with username/password or you can use the -DeviceLogin option if your tenant has been configured for Multi-Factor Authentication.

Note: if you want to use the newly created app to authenticate with username/password you will have to make a modification to the app. Navigate to the application registration in your Azure AD, select the Authentication section, and set `Allow public client flows` to `yes`. Alternatively, navigate to the `Manifest` section and set `allowPublicClient` to `true`.

## EXAMPLES

### EXAMPLE 1

```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -Interactive
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 2

```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -Interactive
```

Creates a new Azure AD Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 3

```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -GraphApplicationPermissions "User.Read.All" -SharePointApplicationPermissions "Sites.Read.All" -Interactive
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.Read.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 4

```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -OutPath c:\ -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -Interactive
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and stores the public and private key certificates in c:\. The private key certificate will be locked with the password "password". It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 5

```powershell
Register-PnPAzureADApp -DeviceLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force)
```

Creates a new Azure AD Application registration and asks you to authenticate using device login methods, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 6

```powershell
Register-PnPAzureADApp -Interactive -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force)
```

Creates a new Azure AD Application registration and asks you to authenticate using username and password, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 7

```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -Interactive -LogoFilePath c:\logo.png
```

Creates a new Azure AD Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. It will also set the `logo.png` file as the logo for the Azure AD app.

## PARAMETERS

### -ApplicationName

The name of the Azure AD Application to create.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
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
Type: AzureEnvironment
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Production
- PPE
- China
- Germany
- USGovernment
- USGovernmentHigh
- USGovernmentDoD
- Custom
HelpMessage: ''
```

### -CertificatePassword

Optional certificate password.

```yaml
Type: SecureString
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 8
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CertificatePath

File path to use an existing certificate.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Existing Certificate
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CommonName

Common Name (e.g. server FQDN or YOUR name). It defaults to 'pnp.contoso.com'

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Country

Country Name (2 letter code).

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 1
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DeviceLogin

If specified, a device login flow, supporting Multi-Factor Authentication will be used to authenticate towards the Microsoft Graph.

```yaml
Type: SwitchParameter
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -EntraIDLoginEndPoint

Sets the EntraID login endpoint to be used for creation of the app. This only works if Azure Environment parameter is set to `Custom`

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GraphApplicationPermissions

Specify which Microsoft Graph Application permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GraphDelegatePermissions

Specify which Microsoft Graph Delegate permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Interactive

If specified, an interactive authentication flow will be started, allowing your to authenticate with username, password and an optional second factor from your phone or other device.

```yaml
Type: SwitchParameter
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
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
Type: SwitchParameter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: DeviceLogin
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Locality

Locality Name (eg. city).

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 3
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LogoFilePath

Sets the logo for the Azure AD application. Provide a full path to a local image file on your disk which you want to use as the logo.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MicrosoftGraphEndPoint

Sets the Microsoft Graph endpoint to be used for creation of the app. This only works if Azure Environment parameter is set to `Custom`

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -NoPopup

This switch only applies to Windows and has no effect on Linux and MacOS.

If not specified and running on Windows, all authentication and consent steps will be presented in a popup. If you want to open the URLs manually in a browser, specify this switch.

```yaml
Type: SwitchParameter
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Organization

Organization Name (eg. company).

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 4
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OrganizationUnit

Organizational Unit Name (eg. section).

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 5
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OutPath

Folder to create certificate files in (.CER and .PFX).

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Password

The password to use when logging into the Microsoft Graph.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SharePointApplicationPermissions

Specify which Microsoft SharePoint Application permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SharePointDelegatePermissions

Specify which Microsoft SharePoint Delegate permissions to request.

```yaml
Type: Permission[]
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SignInAudience

Sets the sign in audience. Use this to make the app support Single tenant accounts, Multi-tenant accounts, Multi-tenant + personal accounts & personal accounts only.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -State

State or Province Name (full name).

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 2
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Store

Local Certificate Store to add the certificate to. Only works on Microsoft Windows.

```yaml
Type: StoreLocation
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Tenant

The identifier of your tenant, e.g. mytenant.onmicrosoft.com

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Username

The username to use when logging into the Microsoft Graph. Notice that this user account needs to have write access to the Azure AD.

```yaml
Type: String
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ValidYears

Number of years until expiration (default is 10, max is 30).

```yaml
Type: Int
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: 7
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
