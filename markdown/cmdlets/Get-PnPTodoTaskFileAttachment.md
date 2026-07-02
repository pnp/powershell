---
title: Get-PnPTodoTaskFileAttachment
external help file: PnP.PowerShell.dll-Help.xml
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTodoTaskFileAttachment.html
---
 
# Get-PnPTodoTaskFileAttachment

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Tasks.Read, Tasks.ReadWrite

Gets one Todo task file attachment or all file attachments from a Todo task.

## SYNTAX

```powershell
Get-PnPTodoTaskFileAttachment [-List] <String> [-Task] <TodoTaskPipeBind> [[-Identity] <TodoTaskFileAttachmentPipeBind>] [-User <EntraIDUserPipeBind>] [-DoNotIncludeFileContent]
```

## DESCRIPTION
Use the cmdlet to retrieve file attachments from a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTodoTaskFileAttachment -List "Travel items" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm"
```

This will return all file attachments for the specified Todo task.

### EXAMPLE 2
```powershell
Get-PnPTodoTaskFileAttachment -List "Travel items" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmAAABEgAQAJ"
```

This will return the specified file attachment for the specified Todo task.

### EXAMPLE 3
```powershell
Get-PnPTodoTaskFileAttachment -List "Travel items" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -DoNotIncludeFileContent
```

This will return all file attachments for the specified Todo task without including file contents in `ContentBytes`.

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

### -DoNotIncludeFileContent
Specify to not retrieve and include the file contents in the `ContentBytes` property. By default file contents are included. Excluding the file content would make this cmdlet operate considerably faster, so if you just want a listing of all the attachments, but do not want the file contents, ensure to include this parameter.

```yaml
Type: SwitchParameter
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
The UPN, Id or instance of an Entra ID user for which you would like to retrieve file attachments.

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

