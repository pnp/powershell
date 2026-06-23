---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAuditing.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAuditing
---
  
# Get-PnPAuditing

## SYNOPSIS
Get the Auditing setting of a site

## SYNTAX

```powershell
Get-PnPAuditing [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to get the auditing setting of the site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAuditing
```

Gets the auditing settings of the current site

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


