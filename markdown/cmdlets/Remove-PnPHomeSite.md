---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
Module Name: PnP.PowerShell
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPHomeSite.html
title: Remove-PnPHomeSite
external help file: PnP.PowerShell.dll-Help.xml
---
  
# Remove-PnPHomeSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes the currently set site as the home site

## SYNTAX

```powershell
Remove-PnPHomeSite [-Force] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove a site which currently is set as home site.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPHomeSite
```

Removes the currently set site as the home site

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
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


