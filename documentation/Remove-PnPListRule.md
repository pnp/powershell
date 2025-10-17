---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPListRule.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPListRule
---
  
# Remove-PnPListRule

## SYNOPSIS
Removes a SharePoint list or library rule.

## SYNTAX

```powershell
Remove-PnPListRule -List <ListPipeBind> -Identity <RulePipeBind> [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION
Removes a rule from a SharePoint list or library.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPListRule -List "Demo List" -Identity "12345678-1234-1234-1234-123456789012"
```

Removes the rule with the specified ID from the "Demo List". A confirmation prompt will be displayed.

### EXAMPLE 2
```powershell
Remove-PnPListRule -List "Documents" -Identity "My Rule" -Force
```

Removes the rule with title "My Rule" from the "Documents" library without prompting for confirmation.

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
If specified, the rule will be removed without prompting for confirmation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID or Title of the rule to remove.

```yaml
Type: RulePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[SharePoint Alerts Retirement](https://support.microsoft.com/en-us/office/sharepoint-alerts-retirement-813a90c7-3ff1-47a9-8a2f-152f48b2486f)
