---
Module Name: PnP.PowerShell
title: Remove-PnPTaxonomyItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTaxonomyItem.html
---
 
# Remove-PnPTaxonomyItem

## SYNOPSIS
Removes a taxonomy item.

## SYNTAX

```powershell
Remove-PnPTaxonomyItem [-TermPath] <String> [-Force] [-Connection <PnPConnection>]  
```

## DESCRIPTION
This cmdlet removes a taxonomy item.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTaxonomyItem -TermPath "HR|Recruitment|Marketing"
```

Removes the term called "Marketing" located in "Recruitment" term set in "HR" term group.

### EXAMPLE 2
```powershell
Remove-PnPTaxonomyItem -TermPath "HR|Recruitment|Marketing" -Force
```

Removes the term called "Marketing" located in "Recruitment" term set in "HR" term group, and skips the confirmation question.

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

### -TermPath
The path, delimited by | of the taxonomy item to remove, alike GROUPLABEL|TERMSETLABEL|TERMLABEL

```yaml
Type: String
Parameter Sets: (All)
Aliases: Term

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

