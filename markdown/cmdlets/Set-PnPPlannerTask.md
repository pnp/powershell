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

  * Microsoft Graph API: One of Tasks.ReadWrite, Tasks.ReadWrite.All, Group.ReadWrite.All

Updates an existing task.

## SYNTAX

```powershell
Set-PnPPlannerTask -TaskId <String> [-Title <String>] [-Bucket <PlannerBucketPipeBind>]
 [-PercentComplete <Int32>] [-DueDateTime <DateTime>] [-StartDateTime <DateTime>] [-AppliedCategories <AppliedCategories>]
 [-AssignedTo <String[]] [-Priority <Int32>] [-Description <String>] [-Connection <PnPConnection>] 
 
```

## DESCRIPTION
This cmdlets allows you to update an existing task in a Planner plan.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPPlannerTask -TaskId RSNNbc4HM0e7jt-btAKtTZYAFAf0 -Title "New Title" -StartDateTime 2020-10-01
```

This updates the task with the specified id and sets the title to "New Title" and sets the start date to the first of October 2020.

### EXAMPLE 2
```powershell
Set-PnPPlannerTask -TaskId RSNNbc4HM0e7jt-btAKtTZYAFAf0 -Title "New Title" -Bucket "To do"
```

This updates the task with the specified id and moves to the bucket "To do".

### EXAMPLE 3
```powershell
Set-PnPPlannerTask -TaskId RSNNbc4HM0e7jt-btAKtTZYAFAf0 -AssignedTo "user@contoso.com","manager@contoso.com"
```

This updates the task with the specified id and replaces the assigned users with the ones specified.

### EXAMPLE 4
```powershell
Set-PnPPlannerTask -TaskId RSNNbc4HM0e7jt-btAKtTZYAFAf0 -AppliedCategories ${"Category1"=$true,"Category5"=$false}
```

This updates the task and sets the first label to true and unsets the 5th label on the task.

## PARAMETERS

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

### -Connection
Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

### -AppliedCategories
The applied categories represent the labels as shown in the UI of the planner task. Categories are 'hardcoded' as such in Planner, e.g. you can set Category1 to Category25, each having its own color. Labels, if customized in planner will be set accordingly to the ones defined. You can either copy the value from an existing task (e.g. $task = Get-PnPPlannerTask, Set-PnPPlannerTask -PlanId <yourid> -AppliedCategories $task.AppliedCategory) or you can define it as a new object: @{"Category1"=$true,"Category5"=$true}. Notice that omitting a category from the data you send in will -not- reset that category. E.g. if you want to remove a category/label from a task you will have to explicitly set it to $false.

```yaml
Type: AppliedCategories
Parameter Sets: (All)
Aliases: ß

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

