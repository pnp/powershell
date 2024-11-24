---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPProfileCardProperty.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPProfileCardProperty
---

# Get-PnPProfileCardProperty

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of PeopleSettings.Read.All, PeopleSettings.ReadWrite.All

Retrieves custom properties added to user profile cards

## SYNTAX

### Default (Default)

```
Get-PnPProfileCardProperty [-PropertyName <ProfileCardPropertyName>] [-Verbose]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet may be used to retrieve custom properties added to user profile card.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPProfileCardProperty
```

This will retrieve all custom properties added to user profile card.

### EXAMPLE 2

```powershell
Get-PnPProfileCardProperty -PropertyName "pnppowershell"
```

This will return information about the specified property added to a profile card.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

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

### -Identity

Name of the property to be retrieved. If not provided, all properties will be returned.

```yaml
Type: Commands.Enums.ProfileCardPropertyName
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

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

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
- [Microsoft Graph documentation](https://learn.microsoft.com/en-us/graph/add-properties-profilecard)
