---
schema: 2.0.0
Module Name: PnP.PowerShell
external help file: PnP.PowerShell.dll-Help.xml
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTodoTaskLinkedResource.html
tags: Available in the current Nightly Release only.
title: Get-PnPTodoTaskLinkedResource
---
 
# Get-PnPTodoTaskLinkedResource

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Tasks.Read, Tasks.ReadWrite

Gets one Todo task linked resource or all linked resources from a Todo task.

## SYNTAX

```powershell
Get-PnPTodoTaskLinkedResource [-List] <String> [-Task] <TodoTaskPipeBind> [[-Identity] <String>] [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to retrieve linked resources from a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTodoTaskLinkedResource -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm"
```

This will return all linked resources for the specified Todo task.

### EXAMPLE 2
```powershell
Get-PnPTodoTaskLinkedResource -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "e8dc83b5-8fc2-4a5c-b5c8-a18b3f60f609"
```

This will return the specified linked resource for the specified Todo task.

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

Required: False
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
The UPN, Id or instance of an Entra ID user for which you would like to retrieve linked resources.

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



