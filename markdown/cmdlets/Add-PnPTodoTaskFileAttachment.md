---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTodoTaskFileAttachment.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTodoTaskFileAttachment
Module Name: PnP.PowerShell
---
 
# Add-PnPTodoTaskFileAttachment

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite (delegated). Application permissions are not supported.

Adds a file attachment to a Todo task.

## SYNTAX

```powershell
Add-PnPTodoTaskFileAttachment [-List] <String> [-Task] <TodoTaskPipeBind> [-Path] <String> [-Name <String>] [-ContentType <String>] [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to add a small file attachment to a Todo task.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTodoTaskFileAttachment -List "Travel items" -Task "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVm" -Path "c:\temp\passport.pdf" -ContentType "application/pdf"
```

This will add the file as an attachment to the specified Todo task.

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

### -ContentType
Optional MIME content type for the attachment. If omitted, application/octet-stream will be used.

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

### -Name
Optional display name for the attachment. If omitted, the local file name will be used.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Path to the local file to attach.

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
The UPN, Id or instance of an Entra ID user for which you would like to add the file attachment.

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

