---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupSettings.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPMicrosoft365GroupSettings
---

# Get-PnPMicrosoft365GroupSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Directory.Read.All

Gets a settings of a specific Microsoft 365 Group or a tenant wide Microsoft 365 Group Settings.

## SYNTAX

### Default (Default)

```
Get-PnPMicrosoft365GroupSettings [-Identity <Microsoft365GroupPipeBind>]
 [-GroupSetting <Microsoft365GroupSettingsPipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve tenant wide settings of Microsoft 365 Groups or by using `Identity` option you may specify the exact Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMicrosoft365GroupSettings
```

Retrieves all the Microsoft 365 Group settings from the Tenant

### EXAMPLE 2

```powershell
Get-PnPMicrosoft365GroupSettings -Identity $groupId
```

Retrieves a specific Microsoft 365 Group setting

### EXAMPLE 3

```powershell
Get-PnPMicrosoft365GroupSettings -GroupSetting $groupSettingId
```

Retrieves a tenant-wide specific Microsoft 365 Group setting.

### EXAMPLE 4

```powershell
Get-PnPMicrosoft365GroupSettings -Identity $groupId -GroupSetting $groupSettingId
```

Retrieves a group-specific Microsoft 365 Group setting

## PARAMETERS

### -GroupSetting

The Identity of the Microsoft 365 Group Setting

```yaml
Type: Microsoft365GroupSettingsPipeBind
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

The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
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
- [Microsoft Graph documentation get settings](https://learn.microsoft.com/graph/api/groupsetting-get)
- [Microsoft Graph documentation list settings](https://learn.microsoft.com/en-gb/graph/api/group-list-setting)
