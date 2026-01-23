---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADAppPermission.html
schema: 2.0.0
applicable: SharePoint Online
title: Get-PnPAzureADAppPermission
---

# Get-PnPAzureADAppPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.Read.All

Returns app permissions for Microsoft SharePoint and Microsoft Graph.

## SYNTAX

```powershell
Get-PnPAzureADAppPermission [-Identity <AzureADAppPipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet returns the appid, displayname and permissions set for Microsoft SharePoint and the Microsoft Graph APIs.

## EXAMPLES

### Example 1
```powershell
Get-PnPAzureADAppPermission
```

Returns all apps with all permissions.

### Example 2
```powershell
Get-PnPAzureADAppPermission -Identity MyApp
```

Returns permissions for the specified app.

### Example 2
```powershell
Get-PnPAzureADAppPermission -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

Returns permissions for the specified app.

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

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)