---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPUserOneDriveQuota.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPUserOneDriveQuota
---

# Set-PnPUserOneDriveQuota

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the quota on the OneDrive for Business site for a specific user.

## SYNTAX

### Default (Default)

```
Set-PnPUserOneDriveQuota [-Account] <String> [-Quota] <Int64> [-QuotaWarning] <Int64>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows you to set the quota on the OneDrive for Business site of a specific user. You must connect to the tenant admin website (https://\<tenant\>-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPUserOneDriveQuota -Account 'user@domain.com' -Quota 5368709120 -QuotaWarning 4831838208
```

Sets the quota on the OneDrive for Business site for the specified user to 5GB (5368709120 bytes) and sets a warning to be shown at 4.5 GB (4831838208).

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

### -Quota

The quota to set on the OneDrive for Business site of the user, in bytes.

```yaml
Type: Int64
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -QuotaWarning

The quota to set on the OneDrive for Business site of the user when to start showing warnings about the drive nearing being full, in bytes.

```yaml
Type: Int64
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 2
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
