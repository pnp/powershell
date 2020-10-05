---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnptermgroup
schema: 2.0.0
title: Get-PnPTermGroup
---

# Get-PnPTermGroup

## SYNOPSIS
Returns a taxonomy term group

## SYNTAX

```
Get-PnPTermGroup
 [[-Identity] <PnP.PowerShell.Commands.Base.PipeBinds.TaxonomyItemPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermGroup]>]
 [-TermStore <PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]>]
 [-Connection <PnPConnection>] [-Includes <String[]>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTermGroup
```

Returns all Term Groups in the site collection termstore

### EXAMPLE 2
```powershell
Get-PnPTermGroup -Identity "Departments"
```

Returns the termgroup named "Departments" from the site collection termstore

### EXAMPLE 3
```powershell
Get-PnPTermGroup -Identity ab2af486-e097-4b4a-9444-527b251f1f8d
```

Returns the termgroup with the given ID from the site collection termstore

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Name of the taxonomy term group to retrieve.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.TaxonomyItemPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermGroup]
Parameter Sets: (All)
Aliases: GroupName

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermStore
Term store to check; if not specified the default term store is used.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]
Parameter Sets: (All)
Aliases: TermStoreName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)