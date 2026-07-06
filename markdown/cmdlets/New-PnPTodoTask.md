---
applicable: SharePoint Online
Module Name: PnP.PowerShell
title: New-PnPTodoTask
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTodoTask.html
---
 
# New-PnPTodoTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Tasks.ReadWrite, Tasks.ReadWrite.All

Creates a new Todo task in a Todo list.

## SYNTAX

```powershell
New-PnPTodoTask [-List] <String> [-Title] <String> [-Body <String>] [-BodyContentType <MessageBodyContentType>] [-Categories <String[]>] [-DueDateTime <DateTime>] [-StartDateTime <DateTime>] [-ReminderDateTime <DateTime>] [-Importance <ToDoTaskImportance>] [-Status <ToDoTaskStatus>] [-IsReminderOn] [-TimeZone <String>] [-User <EntraIDUserPipeBind>]
```

## DESCRIPTION
Use the cmdlet to create a Todo task in a Todo list.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTodoTask -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Title "Book flights"
```

This will create a Todo task in the specified Todo list associated with your logged-in user account.

### EXAMPLE 2
```powershell
New-PnPTodoTask -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Title "Book flights" -Body "Check available direct flights" -DueDateTime "2025-03-01T17:00:00" -Importance High
```

This will create a Todo task with additional task properties.

### EXAMPLE 3
```powershell
New-PnPTodoTask -User john@doe.com -List "AQMkADAwATM0MDAAMS0yMDkyLTllN2QtMDACLTAwCgAuAAAD" -Title "Book flights"
```

This will create a Todo task in the specified Todo list associated with John's account.

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
Switch to enable a reminder for the Todo task.

```yaml
Type: SwitchParameter
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

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The UPN, Id or instance of an Entra ID user for which you would like to create the Todo task.

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

