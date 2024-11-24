---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupSettingTemplates.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPMicrosoft365GroupSettingTemplates
---

# Get-PnPMicrosoft365GroupSettingTemplates

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Directory.Read.All

Gets the available system wide template of settings for Microsoft 365 Groups.

## SYNTAX

### Default (Default)

```
Get-PnPMicrosoft365GroupSettingTemplates [-Identity <string>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve available system wide template of settings for Microsoft 365 Groups.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMicrosoft365GroupSettingTemplates
```

Retrieves all the available Microsoft 365 Group setting templates from the Tenant

### EXAMPLE 2

```powershell
Get-PnPMicrosoft365GroupSettingTemplates -Identity "08d542b9-071f-4e16-94b0-74abb372e3d9"
```

Retrieves a specific Microsoft 365 Group setting template. In the above example, it retrieves the `Group.Unified.Guest` setting template.

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group setting template

```yaml
Type: string
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/groupsettingtemplate-list)
