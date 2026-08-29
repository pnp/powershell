---
Module Name: PnP.PowerShell
schema: 2.0.0
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPTodoTaskChecklistItem
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTodoTaskChecklistItem.html
---
 
# Remove-PnPTodoTaskChecklistItem

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite (delegated). Application permissions are not supported.

Removes a Todo task checklist item.

## SYNTAX

```powershell
Remove-PnPTodoTaskChecklistItem [-List] <String> [-Task] <TodoTaskPipeBind> [-Identity] <String> [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to delete a checklist item from a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTodoTaskChecklistItem -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "e8dc83b5-8fc2-4a5c-b5c8-a18b3f60f609"
```

This will delete the specified checklist item.

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
The UPN, Id or instance of an Entra ID user for which you would like to delete the checklist item.

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



