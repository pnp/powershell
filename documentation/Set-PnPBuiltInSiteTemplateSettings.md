---
Module Name: PnP.PowerShell
title: Set-PnPBuiltInSiteTemplateSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPBuiltInSiteTemplateSettings.html
---
 
# Set-PnPBuiltInSiteTemplateSettings

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Allows configuration of the built-in SharePoint Online site templates.

## SYNTAX

### Configure through the site template identifier

```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity <BuiltInSiteTemplateSettingsPipeBind> -IsHidden <Boolean> [-Connection <PnPConnection>] [-WhatIf]
```

### Configure through the site template name

```powershell
Set-PnPBuiltInSiteTemplateSettings -Template <BuiltInSiteTemplates> -IsHidden <Boolean> [-Connection <PnPConnection>] [-WhatIf]
```

## DESCRIPTION
This cmdlet allows the built-in SharePoint Online site templates to be shown or hidden.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity 9522236e-6802-4972-a10d-e98dc74b3344 -IsHidden $false
```

Makes the Event Planning template visible.

### EXAMPLE 2
```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity 00000000-0000-0000-0000-000000000000 -IsHidden $true
```

Hides all the default built-in SharePoint Online site templates, except those specifically configured to be visible again.

### EXAMPLE 3
```powershell
Set-PnPBuiltInSiteTemplateSettings -Template CrisisManagement -IsHidden $true
```

Hides the Crisis Management template.

### EXAMPLE 4
```powershell
Set-PnPBuiltInSiteTemplateSettings -Template All -IsHidden $false
```

Shows by the default all the built-in SharePoint Online site templates, except those specifically configured to be hidden.

## PARAMETERS

### -Identity
Id of the built-in site template to configure. You can hide all templates by specifying an empty ID of "00000000-0000-0000-0000-000000000000". Settings specified for a specific template will take precedence over the "all templates" setting. You can hide all templates and then selectively make specific templates visible. All site templates are displayed by default.

| Team site templates  | Template ID                 |  Internal name |
| :------------------- | :------------------- | :------------------- |
| Event planning  | 9522236e-6802-4972-a10d-e98dc74b3344 | EventPlanning | 
| Project management              | f0a3abf4-afe8-4409-b7f3-484113dee93e| ProjectManagement |
| Training and courses        | 695e52c9-8af7-4bd3-b7a5-46aca95e1c7e  | TrainingAndCourses |
| Training and development team     | 64aaa31e-7a1e-4337-b646-0b700aa9a52c | TrainingAndDevelopmentTeam |
| Team collaboration     | 6b96e7b1-035f-430b-92ca-31511c51ca72  | TeamCollaboration |
| Retail management     | e4ec393e-da09-4816-b6b2-195393656edd  | RetailManagement |

<br>

| Communication site templates | Template ID                 |   Internal name |
| :------------------- | :------------------- | :------------------- |
| Crisis management  | 905bb0b4-01e8-4f55-b73c-f07f08aee3a4 | CrisisManagement |
| Department  | 73495f08-0140-499b-8927-dd26a546f26a   | Department |
| Leadership connection    | cd4c26b2-b231-419a-8bb4-9b1d9b83aef6 | LeadershipConnection |
| Learning central       | b8ef3134-92a2-4c9d-bca6-c2f14e79fe98  | LearningCentral |
| New employee onboarding      | 2a23fa44-52b0-4814-baba-06fef1ab931e   | NewEmployeeOnboarding |
| Showcase  | 89f21161-0892-497a-91cb-5783eeb1f5f2   | Showcase | 
| Healthcare  | 5215c092-152f-4912-a12a-7e1efdcc6878   | Healthcare |
| Store collaboration  | 811ecf9a-b33f-44e6-81bd-da77729906dc   | StoreCollaboration |
| Volunteer center  | b6e04a41-1535-4313-a856-6f3515d31999   | VolunteerCenter |
| Topic     | a30fef54-a4e5-4beb-a8b5-962c528d753a   | Topic |
| Blank    | 665da395-e0f9-4c92-b35c-773d8c292f2d  | Blank |

```yaml
Type: Guid
Parameter Sets: ByIdentity

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Template
Internal name of the template. 

```yaml
Type: BuiltInSiteTemplates
Parameter Sets: ByTemplate

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsHidden
Defines if the built in site template should be hidden ($true) or visible ($false).

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: cf

Required: True
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

### -WhatIf
Shows what would happen if the cmdlet runs. No changes will be made.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)