---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPUserProfileProperty.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPUserProfileProperty
---

# Get-PnPUserProfileProperty

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

You must connect to the tenant admin website (https://\<tenant\>-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet.

## SYNTAX

### Default (Default)

```
Get-PnPUserProfileProperty -Account <String[]> [-Properties <String[]>] [-Verbose]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Requires a connection to a SharePoint Tenant Admin site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com'
```

Returns the profile properties for the specified user

### EXAMPLE 2

```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com','user2@domain.com'
```

Returns the profile properties for the specified users

### EXAMPLE 3

```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com' -Properties 'FirstName','LastName'
```

Returns the FirstName and LastName profile properties for the specified user

## PARAMETERS

### -Account

The account of the user, formatted either as a login name, or as a claims identity, e.g. i:0#.f|membership|user@domain.com

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
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

### -Properties

The user profile properties that are requested for the user e.g. FirstName, LastName etc.

```yaml
Type: String[]
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
