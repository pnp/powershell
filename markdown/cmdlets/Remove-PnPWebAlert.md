---
Module Name: PnP.PowerShell
title: Remove-PnPWebAlert
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPWebAlert.html
---

# Remove-PnPWebAlert

## SYNOPSIS
Removes an alert from the current web.

## SYNTAX

```powershell
Remove-PnPWebAlert -Identity <AlertPipeBind> [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Removes the specified alert from the current web. By default, a confirmation prompt is shown unless Force is specified.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPWebAlert -Identity 641ac67f-0ce0-4837-874a-743c8f8572a7
```

Removes the alert with the specified ID.

### EXAMPLE 2
```powershell
Get-PnPWebAlert -ListTitle "Documents" | Remove-PnPWebAlert -Force
```

Removes all alerts for the Documents list without prompting.

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
The alert id, or the actual alert object to remove.

```yaml
Type: AlertPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
