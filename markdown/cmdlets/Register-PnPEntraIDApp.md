---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Register-PnPEntraIDApp.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Register-PnPEntraIDApp
Module Name: PnP.PowerShell
---
  
# Register-PnPEntraIDApp

## SYNOPSIS
Registers an Entra ID App and optionally creates a new self-signed certificate to use with the application registration.

## SYNTAX 

### Generate Certificate
```powershell
Register-PnPEntraIDApp -ApplicationName <String>
                                       -Tenant <String>
                                       [-DeviceLogin]
                                       [-CommonName <String>]
                                       [-OutPath <String>]
                                       [-Store <StoreLocation>]
                                       [-GraphApplicationPermissions <Permission[]>]
                                       [-GraphDelegatePermissions <Permission[]>]
                                       [-SharePointApplicationPermissions <Permission[]>]
                                       [-SharePointDelegatePermissions <Permission[]>]
                                       [-O365ManagementApplicationPermissions <Permission[]>]
                                       [-O365ManagementDelegatePermissions <Permission[]>]
                                       [-ExchangeApplicationPermissions <Permission[]>]
                                       [-ExchangeDelegatePermissions <Permission[]>]
                                       [-PowerBIApplicationPermissions <Permission[]>]
                                       [-PowerBIDelegatePermissions <Permission[]>]
                                       [-DataverseDelegatePermissions <Permission[]>]
                                       [-PowerAppsDelegatePermissions <Permission[]>]
                                       [-AzureServiceManagementDelegatePermissions <Permission[]>]
                                       [-ResourcePermissions <Hashtable[]>]
                                       [-Country <String>]
                                       [-State <String>]
                                       [-Locality <String>]
                                       [-Organization <String>]
                                       [-OrganizationUnit <String>]
                                       [-ValidYears <Int>]
                                       [-CertificatePassword <SecureString>]
                                       [-LogoFilePath <string>]
                                       [-MicrosoftGraphEndPoint <string>]
                                       [-EntraIDLoginEndPoint <string>]
                                       [-SignInAudience <EntraIDSignInAudience>]
```

### Existing Certificate
```powershell
Register-PnPEntraIDApp  -CertificatePath <String>
                        -ApplicationName <String>
                        -Tenant <String>
                        [-DeviceLogin]
                        [-GraphApplicationPermissions <Permission[]>]
                        [-GraphDelegatePermissions <Permission[]>]
                        [-SharePointApplicationPermissions <Permission[]>]
                        [-SharePointDelegatePermissions <Permission[]>]
                        [-O365ManagementApplicationPermissions <Permission[]>]
                        [-O365ManagementDelegatePermissions <Permission[]>]
                        [-ExchangeApplicationPermissions <Permission[]>]
                        [-ExchangeDelegatePermissions <Permission[]>]
                        [-PowerBIApplicationPermissions <Permission[]>]
                        [-PowerBIDelegatePermissions <Permission[]>]
                        [-DataverseDelegatePermissions <Permission[]>]
                        [-PowerAppsDelegatePermissions <Permission[]>]
                        [-AzureServiceManagementDelegatePermissions <Permission[]>]
                        [-ResourcePermissions <Hashtable[]>]
                        [-CertificatePassword <SecureString>]
                        [-LogoFilePath <string>]
```

## DESCRIPTION
Registers an Entra ID App and optionally creates a new self-signed certificate to use with the application registration. 

Note: if you want to use the newly created app to authenticate with username/password. Use `Register-PnPEntraIDAppForInteractiveLogin` to create an app that allows users to login with.

Permissions can be requested for Microsoft Graph, SharePoint, the Office 365 Management APIs, Exchange Online, Power BI, Dataverse, PowerApps and Azure Resource Manager. Use `-ResourcePermissions` for any other API, or for a permission that the parameters above do not offer yet.

Dataverse, PowerApps and Azure Resource Manager expose delegated permissions only, so only a `-{Resource}DelegatePermissions` parameter is offered for them. They also expose very few: Dataverse has `user_impersonation` and `mcp.tools`, PowerApps has `User`, and Azure Resource Manager has `user_impersonation`.

For Microsoft Graph, SharePoint and the Office 365 Management APIs the available permissions ship with the module, so they can be tab completed. The permissions of all other resources are read from the tenant, which means they are not tab completed and an invalid permission name is only reported after you authenticated.

The consent flow at the end of the registration needs Microsoft Graph or SharePoint permissions to run against. If the app requests neither, grant admin consent for it through the Entra ID portal instead.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPEntraIDApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser
```

Creates a new Entra ID Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 2
```powershell
Register-PnPEntraIDApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force)
```

Creates a new Entra ID Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 3
```powershell
Register-PnPEntraIDApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -GraphApplicationPermissions "User.Read.All" -SharePointApplicationPermissions "Sites.Read.All"
```

Creates a new Entra ID Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.Read.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 4
```powershell
Register-PnPEntraIDApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -OutPath c:\ -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force)
```

Creates a new Entra ID Application registration, creates a new self signed certificate, and stores the public and private key certificates in c:\. The private key certificate will be locked with the password "password". It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 5
```powershell
Register-PnPEntraIDApp -DeviceLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) 
```

Creates a new Entra ID Application registration and asks you to authenticate using device login methods, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 6
```powershell
Register-PnPEntraIDApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) 
```

Creates a new Entra ID Application registration and asks you to authenticate using username and password, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 7
```powershell
Register-PnPEntraIDApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -LogoFilePath c:\logo.png
```

Creates a new Entra ID Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. It will also set the `logo.png` file as the logo for the Entra ID app.

### EXAMPLE 8
```powershell
Register-PnPEntraIDApp -ApplicationName "ACS App" -Tenant yourtenant.onmicrosoft.com -OutPath c:\temp -GraphApplicationPermissions "User.Read.All" -GraphDelegatePermissions "Sites.Read.All" -SharePointApplicationPermissions "Sites.Read.All" -SharePointDelegatePermissions "AllSites.Read"
```

Creates a new Entra ID Application registration, creates a new self signed certificate, writes it to the c:\temp folder. It will upload the certificate to the azure app registration and it will request the shown permissions. A browser window will be shown allowing you to authenticate.

### EXAMPLE 9
```powershell
Register-PnPEntraIDApp -ApplicationName "Reporting App" -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -ExchangeApplicationPermissions "Exchange.ManageAsApp" -PowerBIDelegatePermissions "Tenant.Read.All" -O365ManagementApplicationPermissions "ActivityFeed.Read"
```

Creates a new Entra ID Application registration requesting permissions on Exchange Online, Power BI and the Office 365 Management APIs. The Exchange Online and Power BI permissions are read from the tenant, so those two are not tab completed. The Office 365 Management APIs permissions ship with the module and are tab completed.

### EXAMPLE 10
```powershell
Register-PnPEntraIDApp -ApplicationName "Automation App" -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -ResourcePermissions @(
  @{ Resource = "Exchange"; ApplicationPermissions = "Exchange.ManageAsApp" }
  @{ Resource = "PowerBI"; DelegatePermissions = "Tenant.Read.All" }
)
```

Creates a new Entra ID Application registration requesting permissions through `-ResourcePermissions`. `Resource` takes one of `Graph`, `SharePoint`, `O365Management`, `Exchange`, `PowerBI`, `Dataverse`, `PowerApps` and `AzureServiceManagement`, or the application id of any other API that has a service principal in your tenant. Permissions requested this way are always read from the tenant, which makes this the way to request a permission that the lists shipping with the module do not have yet.

## PARAMETERS

### -DeviceLogin
If specified, a device login flow, supporting Multi-Factor Authentication will be used to authenticate towards the Microsoft Graph.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -ApplicationName
The name of the Entra ID Application to create.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Accept pipeline input: False
```

### -CertificatePassword
Optional certificate password.

```yaml
Type: SecureString
Parameter Sets: (All)

Required: False
Position: 8
Accept pipeline input: False
```

### -CertificatePath
File path to use an existing certificate.

```yaml
Type: String
Parameter Sets: Existing Certificate

Required: True
Position: Named
Accept pipeline input: False
```

### -CommonName
Common Name (e.g. server FQDN or YOUR name). It defaults to 'pnp.contoso.com'

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: 0
Accept pipeline input: False
```

### -Country
Country Name (2 letter code).

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: 1
Accept pipeline input: False
```

### -Locality
Locality Name (eg. city).

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: 3
Accept pipeline input: False
```

### -Organization
Organization Name (eg. company).

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: 4
Accept pipeline input: False
```

### -OrganizationUnit
Organizational Unit Name (eg. section).

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: 5
Accept pipeline input: False
```

### -OutPath
Folder to create certificate files in (.CER and .PFX).

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: Named
Accept pipeline input: False
```

### -GraphApplicationPermissions
Specify which Microsoft Graph Application permissions to request.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -GraphDelegatePermissions
Specify which Microsoft Graph Delegate permissions to request.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -SharePointApplicationPermissions
Specify which Microsoft SharePoint Application permissions to request.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -SharePointDelegatePermissions
Specify which Microsoft SharePoint Delegate permissions to request.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -O365ManagementApplicationPermissions
Specify which Office 365 Management APIs Application permissions to request.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -O365ManagementDelegatePermissions
Specify which Office 365 Management APIs Delegate permissions to request.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -ExchangeApplicationPermissions
Specify which Office 365 Exchange Online Application permissions to request. The available permissions are read from the tenant, so they are not tab completed.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -ExchangeDelegatePermissions
Specify which Office 365 Exchange Online Delegate permissions to request. The available permissions are read from the tenant, so they are not tab completed.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -PowerBIApplicationPermissions
Specify which Power BI Service Application permissions to request. The available permissions are read from the tenant, so they are not tab completed.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -PowerBIDelegatePermissions
Specify which Power BI Service Delegate permissions to request. The available permissions are read from the tenant, so they are not tab completed.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -DataverseDelegatePermissions
Specify which Dataverse Delegate permissions to request, being `user_impersonation` or `mcp.tools`. Dataverse exposes no application permissions. The available permissions are read from the tenant, so they are not tab completed.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -PowerAppsDelegatePermissions
Specify which PowerApps Service Delegate permissions to request, being `User`. PowerApps Service exposes no application permissions. The available permissions are read from the tenant, so they are not tab completed.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -AzureServiceManagementDelegatePermissions
Specify which Azure Resource Manager, formerly named the Windows Azure Service Management API, Delegate permissions to request, being `user_impersonation`. Azure Resource Manager exposes no application permissions. The available permissions are read from the tenant, so they are not tab completed.

```yaml
Type: Permission[]
Parameter Sets: (All)

Required: False
Position: 0
Accept pipeline input: False
```

### -ResourcePermissions
Specify permissions to request on any API, for APIs without a dedicated parameter and for permissions the dedicated parameters do not offer. Every entry is a hashtable with a `Resource` key holding either the application id of the resource or one of `Graph`, `SharePoint`, `O365Management`, `Exchange`, `PowerBI`, `Dataverse`, `PowerApps` and `AzureServiceManagement`, together with an `ApplicationPermissions` and/or a `DelegatePermissions` key. The permissions are read from the tenant, so this also covers application permissions on a resource that exposes none today.

```yaml
Type: Hashtable[]
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -State
State or Province Name (full name).

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: 2
Accept pipeline input: False
```

### -Store
Local Certificate Store to add the certificate to. Only works on Microsoft Windows.

```yaml
Type: StoreLocation
Parameter Sets: Generate Certificate

Required: False
Position: Named
Accept pipeline input: False
```

### -Tenant
The identifier of your tenant, e.g. mytenant.onmicrosoft.com

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Accept pipeline input: False
```

### -ValidYears
Number of years until expiration (default is 10, max is 30).

```yaml
Type: Int
Parameter Sets: Generate Certificate

Required: False
Position: 7
Accept pipeline input: False
```

### -AzureEnvironment
The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
Parameter Sets: (All)
Aliases:
Accepted values: Production, PPE, China, Germany, USGovernment, USGovernmentHigh, USGovernmentDoD, BleuCloud, DelosCloud, GovSGCloud, Custom

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogoFilePath

Sets the logo for the Entra ID application. Provide a full path to a local image file on your disk which you want to use as the logo.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -EntraIDLoginEndPoint

Sets the EntraID login endpoint to be used for creation of the app. This only works if Azure Environment parameter is set to `Custom`

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -MicrosoftGraphEndPoint

Sets the Microsoft Graph endpoint to be used for creation of the app. This only works if Azure Environment parameter is set to `Custom`

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -SignInAudience

Sets the sign in audience. Use this to make the app support Single tenant accounts, Multi-tenant accounts, Multi-tenant + personal accounts & personal accounts only.

```yaml
Type: String
Parameter Sets: Generate Certificate

Required: False
Position: Named
Accept pipeline input: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


