---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListRule.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPListRule
---
  
# Get-PnPListRule

## SYNOPSIS
Retrieves SharePoint list or library rules.

## SYNTAX

```powershell
Get-PnPListRule -List <ListPipeBind> [-Identity <RulePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION
Retrieves all rules or a specific rule from a SharePoint list or library. SharePoint Rules are the replacement for SharePoint Alerts which are being retired.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListRule -List "Demo List"
```

Returns all rules configured on the "Demo List".

### EXAMPLE 2
```powershell
Get-PnPListRule -List "Demo List" -Identity "12345678-1234-1234-1234-123456789012"
```

Returns the rule with the specified ID from the "Demo List".

### EXAMPLE 3
```powershell
Get-PnPListRule -List "Demo List" -Identity "My Rule"
```

Returns rules with the title "My Rule" from the "Demo List".

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
The ID or Title of the rule to retrieve. If not specified, all rules for the list will be returned.

```yaml
Type: RulePipeBind
Parameter Sets: (All)

Required: False
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
