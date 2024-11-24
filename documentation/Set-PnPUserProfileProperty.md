---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPUserProfileProperty.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPUserProfileProperty
---

# Set-PnPUserProfileProperty

## SYNOPSIS

**Required Permissions**

* SharePoint: Sites.FullControl.All, TermStore.ReadWrite.All, User.ReadWrite.All
* Microsoft Graph: User.Read

Uses the tenant API to retrieve site information. You must connect to the tenant admin website (https://\<tenant\>-admin.sharepoint.com) with Connect-PnPOnline in order to use this command.

## SYNTAX

### Single

```
Set-PnPUserProfileProperty -Account <String> -PropertyName <String> -Value <String>
 [-Connection <PnPConnection>]
```

### Multi

```
Set-PnPUserProfileProperty -Account <String> -PropertyName <String> -Values <String[]>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Updates the value of a specific user profile property for a single user profile in the SharePoint Online environment. Requires a connection to the SharePoint Tenant Admin site.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPUserProfileProperty -Account 'john@domain.com' -Property 'SPS-Location' -Value 'Stockholm'
```

Sets the SPS-Location property to 'Stockholm' for the user john@domain.com.

### EXAMPLE 2

```powershell
Set-PnPUserProfileProperty -Account 'john@domain.com' -Property 'MyProperty' -Values 'Value 1','Value 2'
```

Sets the MyProperty multi value property for the user john@domain.com.

## PARAMETERS

### -Account

The account of the user, formatted either as a login name, or as a claims identity, e.g. i:0#.f|membership|user@domain.com

```yaml
Type: String
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

### -PropertyName

The property to set, for instance SPS-Skills or SPS-Location.

```yaml
Type: String
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

### -Value

The value to set in the case of a single value property.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Single
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Values

The values set in the case of a multi value property, e.g. "Value 1","Value 2"

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Multi
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
