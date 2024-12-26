---
Module Name: PnP.PowerShell
title: Get-PnPTodoList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTodoList.html
---
 
# Get-PnPTodoList

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Tasks.Read, Tasks.ReadWrite, Tasks.Read.All

Gets one Todo list or all Todo lists.

## SYNTAX

```powershell
Get-PnPTodoList [[-Identity] <String>] [-[User] <AzureADUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to retrieve all Todo lists or a specific Todo list.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTodoList
```

This will return all your (logged-in user) todo lists.

### EXAMPLE 2
```powershell
Get-PnPTodoList -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA="
```

This will return your (logged-in user) todo list with the specified Id.

### EXAMPLE 3
```powershell
Get-PnPTodoList -User john@doe.com
```

This will return the todo lists for the user john.

### EXAMPLE 4
```powershell
Get-PnPTodoList -User john@doe.com -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA="
```

This will return the todo list for the user john with specified Id.

## PARAMETERS

### -Identity
Id of the Todo list.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The UPN, Id or instance of an Azure AD user for which you would like to retrieve the todo list available to this user

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

