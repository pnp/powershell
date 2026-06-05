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
| :------------------- | :------------------------- | :------------------- |
| Event planning       | 9522236e-6802-4972-a10d-e98dc74b3344 | EventPlanning | 
| Project management   | f0a3abf4-afe8-4409-b7f3-484113dee93e | ProjectManagement |
| Training and courses | 695e52c9-8af7-4bd3-b7a5-46aca95e1c7e | TrainingAndCourses |
| Training and development team | 64aaa31e-7a1e-4337-b646-0b700aa9a52c | TrainingAndDevelopmentTeam |
| Retail management    | e4ec393e-da09-4816-b6b2-195393656edd | RetailManagement |
| Employee onboarding team | af9037eb-09ef-4217-80fe-465d37511b33 | EmployeeOnboardingTeam |
| Set up your home page | 33537eba-a7d6-4d76-96cc-ee1930bd3907 | SetUpYourHomePage |
| Crisis communication team | fb513aef-c06f-4dc3-b08c-963a2d2360c1 | CrisisCommunicationTeam |
| IT help desk         | 71308406-f31d-445f-85c7-b31942d1508c | ITHelpDesk |
| Contracts management | 2a7dd756-75f6-4f0f-a06a-a672939ea2a3 | ContractsManagement |
| Accounts payable     | 403ffe4e-12d4-41a2-8153-208069eaf2b8 | AccountsPayable |
| Standard team        | c8b3137a-ca4c-48a9-b356-a8e7987dd693 | StandardTeam |

<br>

| Communication site templates | Template ID                 |   Internal name |
| :-------------------------- | :------------------------- | :------------------- |
| Crisis management           | 951190b8-8541-4f8c-8e8a-10a17c466c94 | CrisisManagement |
| Department                  | 73495f08-0140-499b-8927-dd26a546f26a | Department |
| Leadership connection       | cd4c26b2-b231-419a-8bb4-9b1d9b83aef6 | LeadershipConnection |
| Learning central            | b8ef3134-92a2-4c9d-bca6-c2f14e79fe98 | LearningCentral |
| New employee onboarding     | 2a23fa44-52b0-4814-baba-06fef1ab931e | NewEmployeeOnboarding |
| Showcase                    | 6142d2a0-63a5-4ba0-aede-d9fefca2c767 | Showcase |
| Store collaboration         | 811ecf9a-b33f-44e6-81bd-da77729906dc | StoreCollaboration |
| Volunteer center            | 34a39504-194c-4605-87be-d48d00070c67 | VolunteerCenter |
| Brand central               | f2c6bb0c-9234-40c2-9ec3-ee86a70330fb | BrandCentral |
| Standard communication      | 96c933ac-3698-44c7-9f4a-5fd17d71af9e | StandardCommunication |
| Event                       | 3d5ef50b-88a0-42a7-9fb2-8036009f6f42 | Event |
| Human resources             | c298ddc9-628d-48bf-b1e5-5939a1962fb1 | HumanResources |
| Organization home           | 30eebaf6-48ea-4af9-a564-a5c50297c826 | OrganizationHome |
| Copilot Campaign            | 94e24f52-dfaf-40e4-b629-df2c85570adc | CopilotCampaign |
| Viva Campaign               | da99c5d9-baad-4e81-81f6-03a061972d49 | VivaCampaign |
| Blank                       | f6cc5403-0d63-442e-96c0-285923709ffc | Blank |

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