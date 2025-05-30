---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Register-PnPAzureADApp.html
external help file: PnP.PowerShell.dll-Help.xml
title: Register-PnPAzureADApp
---
 
# Register-PnPAzureADApp

## SYNOPSIS
Registers an Azure AD App and optionally creates a new self-signed certificate to use with the application registration.

## SYNTAX 

### Generate Certificate
```powershell
Register-PnPAzureADApp -ApplicationName <String>
                                       -Tenant <String>
                                       [-DeviceLogin]
                                       [-CommonName <String>]
                                       [-OutPath <String>]
                                       [-Store <StoreLocation>]
                                       [-GraphApplicationPermissions <Permission[]>]
                                       [-GraphDelegatePermissions <Permission[]>]
                                       [-SharePointApplicationPermissions <Permission[]>]
                                       [-SharePointDelegatePermissions <Permission[]>]
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
Register-PnPAzureADApp  -CertificatePath <String>
                        -ApplicationName <String>
                        -Tenant <String>
                        [-DeviceLogin]
                        [-GraphApplicationPermissions <Permission[]>]
                        [-GraphDelegatePermissions <Permission[]>]
                        [-SharePointApplicationPermissions <Permission[]>]
                        [-SharePointDelegatePermissions <Permission[]>]
                        [-CertificatePassword <SecureString>]
                        [-LogoFilePath <string>]
```

## DESCRIPTION
Registers an Azure AD App and optionally creates a new self-signed certificate to use with the application registration. 

Note: if you want to use the newly created app to authenticate with username/password. Use `Register-PnPEntraIDAppForInteractiveLogin` to create an app that allows users to login with.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 2
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force)
```

Creates a new Azure AD Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 3
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -GraphApplicationPermissions "User.Read.All" -SharePointApplicationPermissions "Sites.Read.All"
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.Read.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 4
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -OutPath c:\ -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force)
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and stores the public and private key certificates in c:\. The private key certificate will be locked with the password "password". It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 5
```powershell
Register-PnPAzureADApp -DeviceLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) 
```

Creates a new Azure AD Application registration and asks you to authenticate using device login methods, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 6
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) 
```

Creates a new Azure AD Application registration and asks you to authenticate using username and password, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 7
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -LogoFilePath c:\logo.png
```

Creates a new Azure AD Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. It will also set the `logo.png` file as the logo for the Azure AD app.

### EXAMPLE 8
```powershell
Register-PnPAzureADApp -ApplicationName "ACS App" -Tenant yourtenant.onmicrosoft.com -OutPath c:\temp -GraphApplicationPermissions "User.Read.All" -GraphDelegatePermissions "Sites.Read.All" -SharePointApplicationPermissions "Sites.Read.All" -SharePointDelegatePermissions "AllSites.Read"
```

Creates a new Azure AD Application registration, creates a new self signed certificate, writes it to the c:\temp folder. It will upload the certificate to the azure app registration and it will request the shown permissions. A browser window will be shown allowing you to authenticate.

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
The name of the Azure AD Application to create.

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
Parameter Sets: Generate Certificate

Required: False
Position: 0
Accept pipeline input: False
```

### -GraphDelegatePermissions
Specify which Microsoft Graph Delegate permissions to request.

```yaml
Type: Permission[]
Parameter Sets: Generate Certificate

Required: False
Position: 0
Accept pipeline input: False
```

### -SharePointApplicationPermissions
Specify which Microsoft SharePoint Application permissions to request.

```yaml
Type: Permission[]
Parameter Sets: Generate Certificate

Required: False
Position: 0
Accept pipeline input: False
```

### -SharePointDelegatePermissions
Specify which Microsoft SharePoint Delegate permissions to request.

```yaml
Type: Permission[]
Parameter Sets: Generate Certificate

Required: False
Position: 0
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
Accepted values: Production, PPE, China, Germany, USGovernment, USGovernmentHigh, USGovernmentDoD, Custom

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogoFilePath

Sets the logo for the Azure AD application. Provide a full path to a local image file on your disk which you want to use as the logo.

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

