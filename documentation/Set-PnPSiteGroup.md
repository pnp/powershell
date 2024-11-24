---
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteGroup.html
Locale: en-US
Module Name: PnP.PowerShell
ms.date: 11/24/2024
PlatyPS schema version: 2024-05-01
title: Set-PnPSiteGroup
---

# Set-PnPSiteGroup

## SYNOPSIS

Updates the SharePoint Online owner and permission levels on a group inside a site collection.

## SYNTAX

### __AllParameterSets

```powershell
Set-PnPSiteGroup -Identity <string> [-Site <SitePipeBind>] [-Name <string>] [-Owner <string>]
 [-PermissionLevelsToAdd <string[]>] [-PermissionLevelsToRemove <string[]>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has the no aliases.

## DESCRIPTION

For permissions and the most current information about Windows PowerShell for SharePoint Online, see the online documentation at Intro to SharePoint Online Management Shell (<https://learn.microsoft.com/powershell/sharepoint/sharepoint-online/introduction-sharepoint-online-management-shell?view=sharepoint-ps>).

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/siteA" -Identity "ProjectViewers" -PermissionLevelsToRemove "Full Control" 
-PermissionLevelsToAdd "View Only"
```

Example 1 changes permission level of the ProjectViewers group inside site collection https://contoso.sharepoint.com/sites/siteA from Full Control to View Only.

### EXAMPLE 2

```powershell
Set-PnPSiteGroup -Site "https://contoso.sharepoint.com" -Identity "ProjectViewers" -Owner user@domain.com
```

Example 2 sets user@domain.com as the owner of the ProjectViewers group.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnP.PowerShell.Commands.Base.PnPConnection
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

Specifies the name of the group.

```yaml
Type: System.String
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

### -Name

Specifies the new name of the group.

```yaml
Type: System.String
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

### -Owner

Specifies the owner (individual or a security group) of the group to be set.

```yaml
Type: System.String
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

### -PermissionLevelsToAdd

Specifies the permission levels to grant to the group.

> [!NOTE] > Permission levels are defined by SharePoint Online administrators from SharePoint Online Administration Center.

```yaml
Type: System.String[]
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

### -PermissionLevelsToRemove

Specifies the permission levels to remove from the group.

> [!NOTE] > Permission levels are defined by SharePoint Online administrators from SharePoint Online Administration Center.

```yaml
Type: System.String[]
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

### -Site

Specifies the site collection the group belongs to.
If not defined, the currently connected site will be used.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.SitePipeBind
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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### PnP.PowerShell.Commands.Model.SiteGroup

## NOTES

## RELATED LINKS

- [Online Version:](https://pnp.github.io/powershell/cmdlets/Set-PnPSiteGroup.html)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
