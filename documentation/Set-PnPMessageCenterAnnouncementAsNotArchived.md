---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPMessageCenterAnnouncementAsNotArchived.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPMessageCenterAnnouncementAsNotArchived
---

# Set-PnPMessageCenterAnnouncementAsNotArchived

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessageViewpoint.Write (delegated)

Marks one or multiple message center announcements of the Office 365 Services as not archived.

## SYNTAX

### Default (Default)

```
Set-PnPMessageCenterAnnouncementAsNotArchived [-Identity <Ids>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to mark message center announcements as not archived.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPMessageCenterAnnouncementAsNotArchived -Identity "MC123456"
```

Marks message center announcement MC123456 as not archived for the current user.

### EXAMPLE 2

```powershell
Set-PnPMessageCenterAnnouncementAsNotArchived -Identity "MC123456", "MC234567"
```

Marks message center announcements MC123456 and MC234567 as not archived for the current user.

### EXAMPLE 3

```powershell
Set-PnPMessageCenterAnnouncementAsNotArchived
```

Marks all message center announcements as not archived for the current user.

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
