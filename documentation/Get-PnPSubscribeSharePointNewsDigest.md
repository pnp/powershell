---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSubscribeSharePointNewsDigest.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSubscribeSharePointNewsDigest
---

# Get-PnPSubscribeSharePointNewsDigest

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

You must connect to the tenant admin website (https://tenant-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet.

Retrieves if the SharePoint News Digest mails are enabled or disabled for a particular user.

Note: The implementation behind this in SharePoint Online has changed causing this cmdlet to no longer work. Unfortunately there's no alternative way to call into this functionality from PnP PowerShell. We therefore have to remove this cmdlet in a future version. At present it does not work anymore.

## SYNTAX

### Default (Default)

```
Get-PnPSubscribeSharePointNewsDigest -Account <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Requires a connection to a SharePoint Tenant Admin site.

Retrieves if the SharePoint News Digest mails are enabled or disabled for a particular user.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSubscribeSharePointNewsDigest -Account 'user@domain.com'
```

Returns if this user will receive the SharePoint News digest mails

## PARAMETERS

### -Account

The account of the user, formatted either as a login name, e.g. user@domain.com, or as a claims identity, e.g. i:0#.f|membership|user@domain.com

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
