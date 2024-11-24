---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365ExpiringGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPMicrosoft365ExpiringGroup
---

# Get-PnPMicrosoft365ExpiringGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets all soon to expire Microsoft 365 Groups.

## SYNTAX

### Default (Default)

```
Get-PnPMicrosoft365ExpiringGroup [-Limit <Int32>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command returns all soon to expire Microsoft 365 Groups. By default, groups expiring in the next 31 days are returned (in accordance with SharePoint/OneDrive's retention period's 31-day months). The `-Limit` parameter can be used to specify a different time period.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMicrosoft365ExpiringGroup
```

Returns all Groups expiring within 31 days (roughly 1 month).

### EXAMPLE 2

```powershell
Get-PnPMicrosoft365ExpiringGroup -Limit 93
```

Returns all Microsoft 365 Groups expiring in 93 days (roughly 3 months)

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

### -Limit

Limits Groups to be returned to Groups expiring in as many days.

```yaml
Type: Int32
DefaultValue: 31
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
