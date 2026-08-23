---
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTodoTaskFileAttachment.html
title: Remove-PnPTodoTaskFileAttachment
Module Name: PnP.PowerShell
schema: 2.0.0
---
 
# Remove-PnPTodoTaskFileAttachment

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite (delegated). Application permissions are not supported.

Removes a file attachment from a Todo task.

## SYNTAX

```powershell
Remove-PnPTodoTaskFileAttachment [-List] <String> [-Task] <TodoTaskPipeBind> [-Identity] <TodoTaskFileAttachmentPipeBind> [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to delete a file attachment from a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTodoTaskFileAttachment -List "Travel items" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmAAABEgAQAJ"
```

This will delete the specified file attachment.

### EXAMPLE 2
```powershell
Get-PnPTodoTaskFileAttachment -List "Travel items" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmAAABEgAQAJ" | Remove-PnPTodoTaskFileAttachment -List "Travel items" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm"
```

This will delete the file attachment returned by `Get-PnPTodoTaskFileAttachment`.

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
Id of the file attachment or an instance returned by `Get-PnPTodoTaskFileAttachment`.

```yaml
Type: TodoTaskFileAttachmentPipeBind
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
The UPN, Id or instance of an Entra ID user for which you would like to delete the file attachment.

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

