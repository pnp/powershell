---
Module Name: PnP.PowerShell
title: New-PnPTermGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTermGroup.html
---
 
# New-PnPTermGroup

## SYNOPSIS
Creates a taxonomy term group

## SYNTAX

```powershell
New-PnPTermGroup -Name <String> [-Id <Guid>] [-Description <String>]
 [-TermStore <PnP.PowerShell.Commands.Base.PipeBinds.GenericObjectNameIdPipeBind`1[Microsoft.SharePoint.Client.Taxonomy.TermStore]>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to create a taxonomy term group.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTermGroup -GroupName "Countries"
```

Creates a new taxonomy term group named "Countries"

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

### -Description
Description to use for the term group.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
GUID to use for the term group; if not specified, or the empty GUID, a random GUID is generated and used.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases: GroupId

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Name of the taxonomy term group to create.

```yaml
Type: String
Parameter Sets: (All)
Aliases: GroupName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TermStore
Term store to add the group to; if not specified the default term store is used.

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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

