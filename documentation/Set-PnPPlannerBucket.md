---
Module Name: PnP.PowerShell
title: Set-PnPPlannerBucket
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPlannerBucket.html
---
 
# Set-PnPPlannerBucket

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing Planner bucket

## SYNTAX

### By Group
```powershell
Set-PnPPlannerBucket -Bucket <PlannerBucketPipeBind> -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind>
 -Name <String>  
```

### By Plan Id
```powershell
Set-PnPPlannerBucket -Bucket <PlannerBucketPipeBind> -PlanId <String> -Name <String> 
 
```

## DESCRIPTION
This cmdlet updates an existing Planner bucket.

## EXAMPLES

### Example 1
```powershell
Set-PnPPlannerBucket -Bucket "Todos" -Group "Marketing" -Plan "Conference Plan" -Name "Pre-conf Todos"
```

This example renames the bucket called "Todos" to "Pre-conf Todos"

## PARAMETERS

### -Bucket
Specify the bucket or bucket id to update.

```yaml
Type: PlannerBucketPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
Specify the group id or name of the group owning the plan.

```yaml
Type: PlannerGroupPipeBind
Parameter Sets: By Group
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Specify the new name of the bucket

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

### -Plan
Specify the name or id of the plan to retrieve the buckets for.

```yaml
Type: PlannerPlanPipeBind
Parameter Sets: By Group
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PlanId
The id of the plan to find the bucket in.

```yaml
Type: String
Parameter Sets: By Plan Id
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

