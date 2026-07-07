---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPTodoTask
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTodoTask.html
Module Name: PnP.PowerShell
schema: 2.0.0
---
 
# Get-PnPTodoTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Tasks.Read, Tasks.ReadWrite, Tasks.Read.All

Gets one Todo task or all Todo tasks from a Todo list by list Id or display name.

## SYNTAX

```powershell
Get-PnPTodoTask [-List] <TodoTaskPipeBind> [[-Identity] <TodoTaskPipeBind>] [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to retrieve all Todo tasks from a Todo list or a specific Todo task. The Todo list can be specified by Id or display name.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTodoTask -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD"
```

This will return all tasks from the specified Todo list associated with your logged-in user account.

### EXAMPLE 2
```powershell
Get-PnPTodoTask -List "Travel items"
```

This will return all tasks from the Todo list with the specified display name associated with your logged-in user account.

### EXAMPLE 3
```powershell
Get-PnPTodoTask -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA="
```

This will return the specified Todo task from the specified Todo list associated with your logged-in user account.

### EXAMPLE 4
```powershell
Get-PnPTodoTask -User john@doe.com -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD"
```

This will return all tasks from the specified Todo list associated with John's account.

### EXAMPLE 5
```powershell
Get-PnPTodoTask -User john@doe.com -List "Travel items"
```

This will return all tasks from the Todo list with the specified display name associated with John's account.

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
Id of the Todo task or an instance returned by `Get-PnPTodoTask`.

```yaml
Type: TodoTaskPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True
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

### -User
The UPN, Id or instance of an Entra ID user for which you would like to retrieve Todo tasks.

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


