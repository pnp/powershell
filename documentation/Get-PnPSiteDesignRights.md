---
Module Name: PnP.PowerShell
title: Get-PnPSiteDesignRights
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteDesignRights.html
---
 
# Get-PnPSiteDesignRights

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the principals with design rights on a specific Site Design

## SYNTAX

```powershell
Get-PnPSiteDesignRights [-Identity] <TenantSiteDesignPipeBind> [-Connection <PnPConnection>] 
  
```

## DESCRIPTION

Allows to retrieve the principals with design rights on a specific Site Design.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteDesignRights -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Returns the principals with rights on a specific site design

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

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

### -Identity
The ID of the Site Design to receive the rights for

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

