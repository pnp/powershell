---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPMicrosoft365GroupSettings.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPMicrosoft365GroupSettings
---

# Set-PnPMicrosoft365GroupSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Directory.ReadWrite.All, Directory.AccessUser.All

Updates Microsoft 365 Group settings for the tenant or specified Group.

## SYNTAX

### Default (Default)

```
Set-PnPMicrosoft365GroupSettings -Identity <string> [-Values <Hashtable>]
 [-Group <Microsoft365GroupPipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to modify Microsoft 365 Group settings for tenant or specified group.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPMicrosoft365GroupSettings -Identity $groupSettingId -Values @{"AllowToAddGuests"="true"}
```

Sets the properties of the setting where $groupSettingId is a Group setting ID. You can get the Group setting using the `Get-PnPMicrosoft365GroupSettings` cmdlet.

### EXAMPLE 2

```powershell
Set-PnPMicrosoft365GroupSettings -Identity $groupSettingId -Values @{"AllowToAddGuests"="true"} -Group $groupId
```

Sets the properties of the Microsoft 365 group specific setting where $groupSettingId is a setting ID of that group. You can get the Group setting using the `Get-PnPMicrosoft365GroupSettings -Group` cmdlet.

## PARAMETERS

### -Group

The Identity of the Microsoft 365 Group for which you want to update setting.

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

### -Identity

The Identity of the Microsoft 365 Group setting

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

### -Values

Hashtable of properties for the settings. Use the syntax @{AllowToAddGuests="false";GuestUsageGuidelinesUrl="https://google.com/privacy"}.

```yaml
Type: Hashtable
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/groupsetting-update)
