---
title: Update-PnPTodoTaskChecklistItem
external help file: PnP.PowerShell.dll-Help.xml
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Update-PnPTodoTaskChecklistItem.html
---
 
# Update-PnPTodoTaskChecklistItem

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Tasks.ReadWrite

Updates a Todo task checklist item.

## SYNTAX

```powershell
Update-PnPTodoTaskChecklistItem [-List] <String> [-Task] <TodoTaskPipeBind> [-Identity] <String> [-DisplayName <String>] [-IsChecked <Boolean>] [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to update a checklist item on a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
Update-PnPTodoTaskChecklistItem -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "e8dc83b5-8fc2-4a5c-b5c8-a18b3f60f609" -DisplayName "Check passport and visa validity"
```

This will update the display name of the specified checklist item.

### EXAMPLE 2
```powershell
Update-PnPTodoTaskChecklistItem -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "e8dc83b5-8fc2-4a5c-b5c8-a18b3f60f609" -IsChecked $true
```

This will mark the specified checklist item as checked.

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

### -DisplayName
Display name of the checklist item.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Id of the checklist item.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsChecked
Specify whether the checklist item is checked.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
Id or display name of the Todo list.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Task
Id of the Todo task or an instance returned by `Get-PnPTodoTask`.

```yaml
Type: TodoTaskPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The UPN, Id or instance of an Entra ID user for which you would like to update the checklist item.

```yaml
Type: EntraIDUserPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)



