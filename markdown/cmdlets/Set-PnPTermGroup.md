---
Module Name: PnP.PowerShell
title: Set-PnPTermGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTermGroup.html
---
 
# Set-PnPTermGroup

## SYNOPSIS
Updates an existing term group.

## SYNTAX

```powershell
Set-PnPTermGroup -Identity <TaxonomyTermGroupPipeBind> [-Name <String>] [-Description <String>] 
 [-TermStore <TaxonomyTermStorePipeBind>] [-Connection <PnPConnection>] [-Contributors <string []>] [-Managers <string []>]
```

## DESCRIPTION
The cmdlet allows you to update an existing term group.

## EXAMPLES

### Example 1
```powershell
Set-PnPTermGroup -Identity "Departments" -Name "Company Units"
```

Renames the Departments termgroup to "Company Units".

### Example 2
```powershell
Set-PnPTermGroup -Identity "Departments" -Name "Company Units" -Contributors @("i:0#.f|membership|pradeepg@gautamdev.onmicrosoft.com","i:0#.f|membership|adelev@gautamdev.onmicrosoft.com") -Managers @("i:0#.f|membership|alexw@gautamdev.onmicrosoft.com","i:0#.f|membership|diegos@gautamdev.onmicrosoft.com")
```

Renames the Departments termgroup to "Company Units" and adds contributors and managers of the term group. **The user names for contributors and managers need to be encoded claim for the specified login names.**

## PARAMETERS

### -Description
Optional description of the term group.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The term group to update. Either name or a GUID.

```yaml
Type: TaxonomyTermGroupPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
The new name for the term group.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermStore
The termstore to use. If not specified the default term store is used.

```yaml
Type: TaxonomyTermStorePipeBind
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

### -Managers
The manager of the term group who can create/edit term sets in the group as well as add/remove contributors. **The user names for managers need to be encoded claim for the specified login names.**

```yaml
Type: string[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Contributors
The contributor to the term group who can create/edit term sets in the group. **The user names for contributors need to be encoded claim for the specified login names.**

```yaml
Type: string[]
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

