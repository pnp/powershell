---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Register-PnPEntraIDAppForInteractiveLogin.html
external help file: PnP.PowerShell.dll-Help.xml
title: Register-PnPEntraIDAppForInteractiveLogin
---
 
# Register-PnPEntraIDAppForInteractiveLogin

## SYNOPSIS
Registers an Entra ID App for use with Interactive login

## SYNTAX 

### Generate App using Interactive Login
```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName <String>
                                       -Tenant <String>
                                       [-GraphApplicationPermissions <Permission[]>]
                                       [-GraphDelegatePermissions <Permission[]>]
                                       [-SharePointApplicationPermissions <Permission[]>]
                                       [-SharePointDelegatePermissions <Permission[]>]
                                       [-LogoFilePath <string>]
                                       [-MicrosoftGraphEndPoint <string>]
                                       [-EntraIDLoginEndPoint <string>]
                                       [-SignInAudience <EntraIDSignInAudience>]
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
                                       [-LogoFilePath <string>]
                                       [-SignInAudience <EntraIDSignInAudience>]
```

## DESCRIPTION
Registers an Entra ID App for use with the interactive login on Connect-PnPOnline. By default it assumes an Interactive login, but you can decide to use Device Login auth by specifying -DeviceLogin.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com
```

Creates a new Entra ID Application registration. The application will be setup with the following delegate permissions to consent: AllSites.FullControl, Group.ReadWrite.All, User.ReadWrite.All, TermStore.ReadWrite.All. A browser window will be shown allowing you to authenticate.

### EXAMPLE 2
```powershell
Register-PnPEntraIDAppForInteractiveLogin -ApplicationName TestApp -Tenant yourtenant.onmicrosoft.com -GraphDelegatePermissions "Group.Read.All" -SharePointDelegatePermissions "AllSites.FullControl"
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

