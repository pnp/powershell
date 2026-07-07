---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPTodoTask
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTodoTask.html
Module Name: PnP.PowerShell
schema: 2.0.0
---
 
# Remove-PnPTodoTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Tasks.ReadWrite

Removes a Todo task.

## SYNTAX

```powershell
Remove-PnPTodoTask [-List] <String> [-Identity] <TodoTaskPipeBind> [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to delete a Todo task from a Todo list.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTodoTask -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA="
```

This will delete a Todo task associated with your logged-in user account.

### EXAMPLE 2
```powershell
Remove-PnPTodoTask -User john@doe.com -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA="
```

This will delete a Todo task associated with John's account.

### EXAMPLE 3
```powershell
Get-PnPTodoTask -List "Travel items" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA=" | Remove-PnPTodoTask -List "Travel items"
```

This will delete the Todo task returned by `Get-PnPTodoTask`.

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

Required: True
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
The UPN, Id or instance of an Entra ID user for which you would like to delete the Todo task.

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

