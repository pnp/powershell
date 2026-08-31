---
Module Name: PnP.PowerShell
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
applicable: SharePoint Online
title: New-PnPTodoTaskChecklistItem
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTodoTaskChecklistItem.html
tags: Available in the current Nightly Release only.
---
 
# New-PnPTodoTaskChecklistItem

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite (delegated). Application permissions are not supported.

Creates a new checklist item on a Todo task.

## SYNTAX

```powershell
New-PnPTodoTaskChecklistItem [-List] <String> [-Task] <TodoTaskPipeBind> [-DisplayName] <String> [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to create a checklist item on a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTodoTaskChecklistItem -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -DisplayName "Check passport validity"
```

This will create a checklist item on the specified Todo task.

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
The UPN, Id or instance of an Entra ID user for which you would like to create the checklist item.

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


