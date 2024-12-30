---
Module Name: PnP.PowerShell
title: New-PnPTodoList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTodoList.html
---
 
# New-PnPTodoList

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Tasks.ReadWrite, Tasks.ReadWrite.All

Creates a new todo list.

## SYNTAX

```powershell
New-PnPTodoList [[-DisplayName] <String>] [-[User] <AzureADUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to create a Todo list.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTodoList -DisplayName "Travel items"
```

This will create a todo list associated with your (logged-in user) account.

### EXAMPLE 2
```powershell
New-PnPTodoList -User john@doe.com -DisplayName "Travel Items"
```

This will create a todo list associated with John's account.

## PARAMETERS

### -DisplayName
Display name of the Todo list.

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

