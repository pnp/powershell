---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPMessageCenterAnnouncement.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPMessageCenterAnnouncement
---

# Get-PnPMessageCenterAnnouncement

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessage.Read.All

Gets message center announcements of the Office 365 Services from the Microsoft Graph API

## SYNTAX

### Default (Default)

```
Get-PnPMessageCenterAnnouncement [-Identity <Id>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve the available message center announcements.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMessageCenterAnnouncement
```

Retrieves all the available message center announcements

### EXAMPLE 2

```powershell
Get-PnPMessageCenterAnnouncement -Identity "MC123456"
```

Retrieves the details of the message center announcement with the Id MC123456

## PARAMETERS

### -Identity



```yaml
Type: Identity
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
