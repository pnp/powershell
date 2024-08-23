---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Register-PnPEntraIDAppForInteractiveLogin.html
external help file: PnP.PowerShell.dll-Help.xml
title: Register-PnPAzureADApp
---
 
# Register-PnPEntraIDAppForInteractiveLogin

## SYNOPSIS
Registers an Entra ID App for use with Interactive login

## SYNTAX 

### Generate App using Interactive Login
```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName <String>
                                       -Tenant <String>
                                       -Interactive]
                                       [-GraphApplicationPermissions <Permission[]>]
                                       [-GraphDelegatePermissions <Permission[]>]
                                       [-SharePointApplicationPermissions <Permission[]>]
                                       [-SharePointDelegatePermissions <Permission[]>]
                                       [-NoPopup]
                                       [-LogoFilePath <string>]
```

### Generate App using Device Login
```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName <String>
                                       -Tenant <String>
                                       -DeviceLogin
                                       [-GraphApplicationPermissions <Permission[]>]
                                       [-GraphDelegatePermissions <Permission[]>]
                                       [-SharePointApplicationPermissions <Permission[]>]
                                       [-SharePointDelegatePermissions <Permission[]>]
                                       [-NoPopup]
                                       [-LogoFilePath <string>]
```

## DESCRIPTION
Registers an Entra ID App for use with the interactive login on Connect-PnPOnline. You will have to specify either -Interactive or -DeviceLogin to authenticate.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -Interactive
```

Creates a new Entra ID Application registration. The application will be setup with the following delegate permissions to consent: AllSites.FullControl, Group.ReadWrite.All, User.ReadWrite.All, TermStore.ReadWrite.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 2
```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -GraphDelegatePermissions "Group.Read.All" -SharePointDelegatePermissions "AllSites.FullControl" -Interactive
```

Creates a new Entra ID Application registration. The application will be setup with the following delegate permissions to consent: Group.Read.All, AllSites.FullControl. A browser window will be shown allowing you to authenticate.

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

### -Tenant
The identifier of your tenant, e.g. mytenant.onmicrosoft.com

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
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

