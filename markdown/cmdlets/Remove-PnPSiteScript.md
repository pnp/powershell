---
Module Name: PnP.PowerShell
title: Remove-PnPSiteScript
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteScript.html
---
 
# Remove-PnPSiteScript

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a site script.

## SYNTAX

```powershell
Remove-PnPSiteScript [-Identity] <TenantSiteScriptPipeBind> [-Force] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet removes specified site script.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteScript -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Removes the specified site script.

### EXAMPLE 2
```powershell
$sitescript = Get-PnPSiteScript -Identity MySiteScript
Remove-PnPSiteScript -Identity $sitescript
```

Removes the specified site script.

## PARAMETERS

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

### -Force
If specified you will not be asked to confirm removing the specified site script.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID of the site script to remove.

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

