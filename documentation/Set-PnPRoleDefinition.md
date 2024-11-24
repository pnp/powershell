---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPRoleDefinition.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPRoleDefinition
---

# Set-PnPRoleDefinition

## SYNOPSIS

Updates an existing Role Definition (Permission Level) in the site collection in the current context.

## SYNTAX

### Default (Default)

```
Set-PnPRoleDefinition -Identity <RoleDefinitionPipeBind> [-NewRoleName <String>]
 [-Description <String>] [-Order <Int32>] [-SelectAll] [-ClearAll] [-Select <PermissionKind[]>]
 [-Clear <PermissionKind[]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows updating an existing Role Definition (Permission Level) in the site collection in the current context.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -Clear EditListItems
```
Removes the EditListItems flag from an existing permission level.

### EXAMPLE 2

```powershell
Set-PnPRoleDefinition -Identity "NoDelete" -SelectAll -Clear DeleteListItems
```

Selects all flags for an existing permission level except DeleteListItems.

### EXAMPLE 3

```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -NewRoleName "NoDelete" -Description "Contribute without delete"
```

Changes the name and description of an existing permission level.

### EXAMPLE 4

```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -Order 500
```

Changes the order in which the permission level is displayed.

## PARAMETERS

### -Clear

Specifies permission flag(s) to disable. Please visit https://learn.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum.

```yaml
Type: PermissionKind[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- EmptyMask
- ViewListItems
- AddListItems
- EditListItems
- DeleteListItems
- ApproveItems
- OpenItems
- ViewVersions
- DeleteVersions
- CancelCheckout
- ManagePersonalViews
- ManageLists
- ViewFormPages
- AnonymousSearchAccessList
- Open
- ViewPages
- AddAndCustomizePages
- ApplyThemeAndBorder
- ApplyStyleSheets
- ViewUsageData
- CreateSSCSite
- ManageSubwebs
- CreateGroups
- ManagePermissions
- BrowseDirectories
- BrowseUserInfo
- AddDelPrivateWebParts
- UpdatePersonalWebParts
- ManageWeb
- AnonymousSearchAccessWebLists
- UseClientIntegration
- UseRemoteAPIs
- ManageAlerts
- CreateAlerts
- EditMyUserInfo
- EnumeratePermissions
- FullMask
HelpMessage: ''
```

### -ClearAll

Clears ​all permission flags.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Description

The new description for the permission level.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The identity of the role definition, either a RoleDefinition object or the name of the RoleDefinition.

```yaml
Type: RoleDefinitionPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -NewRoleName

The new name for the permission level.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Order

Sets the order of the permission level.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Select

Specifies permission flag(s) to enable. Please visit https://learn.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum.

```yaml
Type: PermissionKind[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- EmptyMask
- ViewListItems
- AddListItems
- EditListItems
- DeleteListItems
- ApproveItems
- OpenItems
- ViewVersions
- DeleteVersions
- CancelCheckout
- ManagePersonalViews
- ManageLists
- ViewFormPages
- AnonymousSearchAccessList
- Open
- ViewPages
- AddAndCustomizePages
- ApplyThemeAndBorder
- ApplyStyleSheets
- ViewUsageData
- CreateSSCSite
- ManageSubwebs
- CreateGroups
- ManagePermissions
- BrowseDirectories
- BrowseUserInfo
- AddDelPrivateWebParts
- UpdatePersonalWebParts
- ManageWeb
- AnonymousSearchAccessWebLists
- UseClientIntegration
- UseRemoteAPIs
- ManageAlerts
- CreateAlerts
- EditMyUserInfo
- EnumeratePermissions
- FullMask
HelpMessage: ''
```

### -SelectAll

Sets all permission flags.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
