---
Module Name: PnP.PowerShell
title: Remove-PnPTodoList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTodoList.html
---
 
# Remove-PnPTodoList

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Tasks.ReadWrite

Removes a new todo list.

## SYNTAX

```powershell
Remove-PnPTodoList [[-Identity] <String>] [-[User] <AzureADUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to delete a Todo list.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTodoList -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASYG0vWAAA="
```

This will delete a todo list with specified Id associated with your (logged-in user) account.

### EXAMPLE 2
```powershell
Remove-PnPTodoList -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASYG0vWAAA=" -User john@doe.com
```

This will delete a todo list with specified Id associated with John's account.

## PARAMETERS

### -Identity
Id of the Todo list.

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
The UPN, Id or instance of an Azure AD user for which you would like to create the todo list.

```yaml
Type: AzureADUserPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

