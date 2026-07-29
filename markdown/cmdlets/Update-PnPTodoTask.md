---
tags: Available in the current Nightly Release only.
title: Update-PnPTodoTask
Module Name: PnP.PowerShell
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Update-PnPTodoTask.html
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
---
 
# Update-PnPTodoTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Tasks.ReadWrite

Updates a Todo task.

## SYNTAX

```powershell
Update-PnPTodoTask [-List] <String> [-Identity] <TodoTaskPipeBind> [-Title <String>] [-Body <String>] [-BodyContentType <MessageBodyContentType>] [-Categories <String[]>] [-DueDateTime <DateTime>] [-StartDateTime <DateTime>] [-ReminderDateTime <DateTime>] [-Importance <ToDoTaskImportance>] [-Status <ToDoTaskStatus>] [-IsReminderOn <Boolean>] [-TimeZone <String>] [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to update properties of a Todo task in a Todo list.

## EXAMPLES

### EXAMPLE 1
```powershell
Update-PnPTodoTask -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA=" -Title "Book return flights"
```

This will update the title of a Todo task associated with your logged-in user account.

### EXAMPLE 2
```powershell
Update-PnPTodoTask -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Identity "AAMkAGU4MGE1OTRiLTUzMGEtNDRjZi05ZmVmLWFiMTkyYmQxODRjOQAuAAAAAACQV8RStyZCQJ4ydzjIK5HmAQD2LFcxdwYMRqbupn47nEYYAASUnLfyAAA=" -Status Completed
```

This will mark a Todo task as completed.

## PARAMETERS

### -Body
Body content of the Todo task.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BodyContentType
Content type of the Todo task body.

```yaml
Type: MessageBodyContentType
Parameter Sets: (All)

Required: False
Position: Named
Default value: Text
Accept pipeline input: False
Accept wildcard characters: False
```

### -Categories
Categories associated with the Todo task.

```yaml
Type: String[]
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

### -DueDateTime
Due date and time of the Todo task.

```yaml
Type: DateTime
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

### -Importance
Importance of the Todo task.

```yaml
Type: ToDoTaskImportance
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsReminderOn
Specify whether a reminder is enabled for the Todo task.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
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

### -ReminderDateTime
Reminder date and time of the Todo task.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartDateTime
Start date and time of the Todo task.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Status
Status of the Todo task.

```yaml
Type: ToDoTaskStatus
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TimeZone
Time zone used for date and time values.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: UTC
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Title of the Todo task.

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
The UPN, Id or instance of an Entra ID user for which you would like to update the Todo task.

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


