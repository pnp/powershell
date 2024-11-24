---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnpProfileCardProperty.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnpProfileCardProperty
---

# New-PnpProfileCardProperty

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : PeopleSettings.ReadWrite.All

Adds a property to user profile card

## SYNTAX

### Default (Default)

```
New-PnpProfileCardProperty -PropertyName <ProfileCardPropertyName> -DisplayName <String>
 [-Localizations <Hashtable>] [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet may be used to add a property to user profile card. Please note that it may take up to 24 hours to reflect the changes.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnpProfileCardProperty -PropertyName CustomAttribute1 -DisplayName "Cost Centre"
```

This cmdlet will add a property with a display name to user profile card.

### EXAMPLE 2

```powershell
$localizations = @{ "pl" = "Centrum kosztów"; "de" = "Kostenstelle" }
New-PnpProfileCardProperty -PropertyName CustomAttribute1 -DisplayName "Cost Centre" -Localizations $localizations
```

This cmdlet will add a property with a display name and specified localizations to user profile card.

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

### -DisplayName

The display name of the property.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: ''
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Localizations

List of display name localizations

```yaml
Type: Hashtable
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

### -PropertyName

Name of a property to be added

```yaml
Type: Commands.Enums.ProfileCardPropertyName
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
