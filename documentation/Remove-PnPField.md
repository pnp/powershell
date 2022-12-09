---
Module Name: PnP.PowerShell
title: Remove-PnPField
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPField.html
---
 
# Remove-PnPField

## SYNOPSIS
Removes a field from a list or a site.

## SYNTAX

```powershell
Remove-PnPField [-Identity] <FieldPipeBind> [[-List] <ListPipeBind>] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to remove a field from a list or a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPField -Identity "Speakers"
```

Removes the "Speakers" field from the site columns.

### EXAMPLE 2
```powershell
Remove-PnPField -List "Demo list" -Identity "Speakers"
```

Removes the speakers field from the list "Demo list".

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

### -Identity
The field object or name to remove.

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
The list object or name where to remove the field from.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

