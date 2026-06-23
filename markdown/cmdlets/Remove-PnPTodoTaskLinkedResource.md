---
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTodoTaskLinkedResource.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
applicable: SharePoint Online
Module Name: PnP.PowerShell
title: Remove-PnPTodoTaskLinkedResource
tags: Available in the current Nightly Release only.
---
 
# Remove-PnPTodoTaskLinkedResource

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Tasks.ReadWrite

Removes a linked resource from a Todo task.

## SYNTAX

```powershell
Remove-PnPTodoTaskLinkedResource [-List] <String> [-Task] <TodoTaskPipeBind> [-Identity] <String> [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to delete a linked resource from a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTodoTaskLinkedResource -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "e8dc83b5-8fc2-4a5c-b5c8-a18b3f60f609"
```

This will delete the specified linked resource.

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
Id of the linked resource.

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
The UPN, Id or instance of an Entra ID user for which you would like to delete the linked resource.

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



