---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPMessageCenterAnnouncementAsFavorite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPMessageCenterAnnouncementAsFavorite
---

# Set-PnPMessageCenterAnnouncementAsFavorite

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessageViewpoint.Write (delegated)

Marks one or multiple message center announcements of the Office 365 Services as favorite$.

## SYNTAX

### Default (Default)

```
Set-PnPMessageCenterAnnouncementAsFavorite [-Identity <Ids>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to mark message center announcements as favorite.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPMessageCenterAnnouncementAsFavorite -Identity "MC123456"
```

Marks message center announcement MC123456 as favorite for the current user.

### EXAMPLE 2

```powershell
Set-PnPMessageCenterAnnouncementAsFavorite -Identity "MC123456", "MC234567"
```

Marks message center announcements MC123456 and MC234567 as favorite for the current user.

### EXAMPLE 3

```powershell
Set-PnPMessageCenterAnnouncementAsFavorite
```

Marks all message center announcements as favorite for the current user.

## PARAMETERS

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

### -Identity



```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: None
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
