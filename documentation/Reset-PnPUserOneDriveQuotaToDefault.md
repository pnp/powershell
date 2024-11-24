---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Reset-PnPUserOneDriveQuotaToDefault.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Reset-PnPUserOneDriveQuotaToDefault
---

# Reset-PnPUserOneDriveQuotaToDefault

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Resets the current quota set on the OneDrive for Business site for a specific user to the tenant default

## SYNTAX

### Default (Default)

```
Reset-PnPUserOneDriveQuotaToDefault [-Account] <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows you to reset the quota set on the OneDrive for Business site of a specific user to the default as set on the tenant. You must connect to the tenant admin website (https://:<tenant>-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Reset-PnPUserOneDriveQuotaToDefault -Account 'user@domain.com'
```

Resets the quota set on the OneDrive for Business site for the specified user to the tenant default

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
