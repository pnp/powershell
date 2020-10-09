---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnptermset
schema: 2.0.0
title: Get-PnPTermSet
---

# Get-PnPTermSet

## SYNOPSIS
Returns a taxonomy term set

## SYNTAX

```powershell
Get-PnPTermSet
 [-Identity <PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermSet]>]
 [-TermGroup] <TermGroupPipeBind>
 [-TermStore <PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]>]
 [-Connection <PnPConnection>] [-Includes <String[]>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTermSet -TermGroup "Corporate"
```

Returns all termsets in the group "Corporate" from the site collection termstore

### EXAMPLE 2
```powershell
Get-PnPTermSet -Identity "Departments" -TermGroup "Corporate"
```

Returns the termset named "Departments" from the termgroup called "Corporate" from the site collection termstore

### EXAMPLE 3
```powershell
Get-PnPTermSet -Identity ab2af486-e097-4b4a-9444-527b251f1f8d -TermGroup "Corporate
```

Returns the termset with the given id from the termgroup called "Corporate" from the site collection termstore

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

### -Identity
The Id or Name of a termset

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermSet]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermGroup
Name of the term group to check.

```yaml
Type: TermGroupPipeBind
Parameter Sets: (All)

Required: True
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)