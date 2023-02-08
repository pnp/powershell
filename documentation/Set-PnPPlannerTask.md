---
Module Name: PnP.PowerShell
title: Set-PnPPlannerTask
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPlannerTask.html
---
 
# Set-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing task.

## SYNTAX

```powershell
Set-PnPPlannerTask -TaskId <String> [-Title <String>] [-Bucket <PlannerBucketPipeBind>]
 [-PercentComplete <Int32>] [-DueDateTime <DateTime>] [-StartDateTime <DateTime>]
 [-AssignedTo <String[]] [-Priority <Int32>] [-Description <String>]
 [<CommonParameters>]
```

## DESCRIPTION
This cmdlets allows you to update an existing task in a Planner plan.

## EXAMPLES

### Example 1
```powershell
Set-PnPPlannerTask -TaskId RSNNbc4HM0e7jt-btAKtTZYAFAf0 -Title "New Title" -StartDateTime 2020-10-01
```

This updates the task with the specified id and sets the title to "New Title" and sets the start date to the first of October 2020.

### Example 2
```powershell
Set-PnPPlannerTask -TaskId RSNNbc4HM0e7jt-btAKtTZYAFAf0 -Title "New Title" -Bucket "To do"
```

This updates the task with the specified id and moves to the bucket "To do".

### Example 3
```powershell
Set-PnPPlannerTask -TaskId RSNNbc4HM0e7jt-btAKtTZYAFAf0 -AssignedTo "user@contoso.com","manager@contoso.com"
```

This updates the task with the specified id and replaces the assigned users with the ones specified.

## PARAMETERS

### -Bucket
Specify the bucket name or ID to move the task to.

```yaml
Type: PlannerBucketPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DueDateTime
Specify the due date.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PercentComplete
Defines the percentage of completeness of the task.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartDateTime
Defines the start date of the task.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TaskId
The Id of the task to update.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Sets the new title of the task.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AssignedTo
Specify the email(s) of the user to assign the task to. Notice that this will replace existing assignments with the ones specified here.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Priority
Sets the priority of the task. Value should be a number between 0 and 10.
- values 0 and 1 are interpreted as _Urgent_
- values 2, 3 and 4 are interpreted as _Important_
- values 5, 6 and 7 are interpreted as _Medium_
- values 8, 9 and 10 are interpreted as _Low_

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Sets the description (notes) of the task.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

