---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADApp.html
schema: 2.0.0
applicable: SharePoint Online
title: Get-PnPAzureADApp
---

# Get-PnPAzureADApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.Read.All

Returns Azure AD App registrations

## SYNTAX

```
Get-PnPAzureADApp [-Identity <AzureADAppPipeBind>]
```

## DESCRIPTION
This cmdlets returns all app registrations or a specific one.

## EXAMPLES

### Example 1
```powershell
Get-PnPAzureADApp
```

This returns all Azure AD App registrations

### Example 2
```powershell
Get-PnPAzureADApp -Identity MyApp
```

This returns the Azure AD App registration with the display name as 'MyApp'

### Example 3
```powershell
Get-PnPAzureADApp -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

This returns the Azure AD App registration with the app id specified or the id specified.

## PARAMETERS

### -Identity
Specify the display name, id or app id.

```yaml
Type: AzureADAppPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)