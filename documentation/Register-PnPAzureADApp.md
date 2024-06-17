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
                                       [-Username <String>]
                                       [-Password <SecureString>]
                                       [-DeviceLogin]
                                       [-Interactive]
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
                                       [-NoPopup]
                                       [-LogoFilePath <string>]
```

### Existing Certificate
```powershell
Register-PnPAzureADApp  -CertificatePath <String>
                        -ApplicationName <String>
                        -Tenant <String>
                        [-Username <String>]
                        [-Password <SecureString>]
                        [-DeviceLogin]
                        [-Interactive]
                        [-GraphApplicationPermissions <Permission[]>]
                        [-GraphDelegatePermissions <Permission[]>]
                        [-SharePointApplicationPermissions <Permission[]>]
                        [-SharePointDelegatePermissions <Permission[]>]
                        [-CertificatePassword <SecureString>]
                        [-NoPopup]
                        [-LogoFilePath <string>]
```

## DESCRIPTION
Registers an Azure AD App and optionally creates a new self-signed certificate to use with the application registration. You can login either with username/password or you can use the -DeviceLogin option if your tenant has been configured for Multi-Factor Authentication. 

Note: if you want to use the newly created app to authenticate with username/password you will have to make a modification to the app. Navigate to the application registration in your Azure AD, select the Authentication section, and set `Allow public client flows` to `yes`. Alternatively, navigate to the `Manifest` section and set `allowPublicClient` to `true`.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -Username "yourname@domain.com" -Password (Read-Host -AsSecureString -Prompt "Enter Password")
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 2
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -Username "yourname@domain.com" -Password (Read-Host -AsSecureString -Prompt "Enter password")
```

Creates a new Azure AD Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

### EXAMPLE 3
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Store CurrentUser -GraphApplicationPermissions "User.Read.All" -SharePointApplicationPermissions "Sites.Read.All" -Username "yourname@domain.com" -Password (Read-Host -AsSecureString -Prompt "Enter Password")
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and adds it to the local certificate store. It will upload the certificate to the azure app registration and it will request the following permissions: Sites.Read.All, User.Read.All

### EXAMPLE 4
```powershell
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -OutPath c:\ -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -Username "yourname@domain.com" -Password (Read-Host -AsSecureString -Prompt "Enter Password")
```

Creates a new Azure AD Application registration, creates a new self signed certificate, and stores the public and private key certificates in c:\. The private key certificate will be locked with the password "password". It will upload the certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All

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
Register-PnPAzureADApp -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -CertificatePath c:\certificate.pfx -CertificatePassword (ConvertTo-SecureString -String "password" -AsPlainText -Force) -Username "yourname@domain.com" -Password (Read-Host -AsSecureString -Prompt "Enter password") -LogoFilePath c:\logo.png
```

Creates a new Azure AD Application registration which will use the existing private key certificate at the provided path to allow access. It will upload the provided private key certificate to the azure app registration and it will request the following permissions: Sites.FullControl.All, Group.ReadWrite.All, User.Read.All. It will also set the `logo.png` file as the logo for the Azure AD app.

## PARAMETERS

### -Username
The username to use when logging into the Microsoft Graph. Notice that this user account needs to have write access to the Azure AD.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

### -Password
The password to use when logging into the Microsoft Graph.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

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

### -NoPopup
This switch only applies to Windows and has no effect on Linux and MacOS.

If not specified and running on Windows, all authentication and consent steps will be presented in a popup. If you want to open the URLs manually in a browser, specify this switch.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

