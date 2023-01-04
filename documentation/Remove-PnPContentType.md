---
Module Name: PnP.PowerShell
title: Remove-PnPContentType
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPContentType.html
---
 
# Remove-PnPContentType

## SYNOPSIS
Removes a content type from a web.

## SYNTAX

```powershell
Remove-PnPContentType [-Identity] <ContentTypePipeBind> [-Force] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet allows to remove a content type from the current web.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPContentType -Identity "Project Document"
```

This will remove a content type called "Project Document" from the current web.

### EXAMPLE 2
```powershell
Remove-PnPContentType -Identity "Project Document" -Force
```

This will remove a content type called "Project Document" from the current web without asking for confirmation.

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
The name or ID of the content type to remove.

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

