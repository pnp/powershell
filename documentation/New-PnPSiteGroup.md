---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPSiteGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPSiteGroup
---

# New-PnPSiteGroup

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new group in a SharePoint Online site collection.

## SYNTAX

### Default (Default)

```
New-PnPSiteGroup
 -Name <String> -PermissionLevels <String[]> [-Site <SitePipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

A SharePoint group is a set of individual users. SharePoint groups enable you to manage sets of users instead of individual users.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/siteA" -Name "Project Leads" -PermissionLevels "Full Control"
```

This example creates a group named Project Leads with the Full Control permission level on the site collection https://contoso.sharepoint.com/sites/siteA.

### EXAMPLE 2

```powershell
New-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/marketing" -Name "NewGroupName" -PermissionLevels "Design"
```
This example creates a group named NewGroupName with the Design permission level on the site collection https://contoso.sharepoint.com/sites/marketing.

## PARAMETERS

### -Name

Specifies the name of the group to add

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Group
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PermissionLevels

Specifies the permission levels to grant to the newly created group. It can be any permission level that exists on the site collection on which the group is being created.

> [!NOTE]
> Permission Levels, are defined on the top-level site of the site collection, please see [How to create and edit permission levels](https://learn.microsoft.com/sharepoint/how-to-create-and-edit-permission-levels) for more information.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Site

Specifies the site collection to add the group to. If not specified the currently connected site collection will be used.

```yaml
Type: SitePipeBind
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
