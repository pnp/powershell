---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPContainerType.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPContainerType
---

# Get-PnPContainerType

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator is required

 Returns the list of Container Types created for a SharePoint Embedded Application in the tenant.

## SYNTAX

### Default (Default)

```
Get-PnPContainerType [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContainerType
```

Returns the list of Container Types created for a SharePoint Embedded Application in the tenant.pplication.

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

## EXAMPLES

## PARAMETERS

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [SharePoint Embedded Container Types](https://learn.microsoft.com/en-us/sharepoint/dev/embedded/concepts/app-concepts/containertypes)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
