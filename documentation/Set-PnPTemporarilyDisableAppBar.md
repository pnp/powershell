---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTemporarilyDisableAppBar.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTemporarilyDisableAppBar
---

# Set-PnPTemporarilyDisableAppBar

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Allows the SharePoint Online App Bar to be disabled. It may take some time for the change to be reflected in SharePoint Online. Support for this may be dropped after March 31st, 2023 after which the SharePoint Online App Bar will become visible anyway. See the [Message Center Announcement](https://admin.microsoft.com/Adminportal/Home#/MessageCenter/:/messages/MC428505) on this for more information.

## SYNTAX

### Default (Default)

```
Set-PnPTemporarilyDisableAppBar -Enabled <Boolean> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to disable/enable SharePoint Online App Bar.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTemporarilyDisableAppBar $true
```

Hides the SharePoint Online App Bar.

### EXAMPLE 2

```powershell
Set-PnPTemporarilyDisableAppBar $false
```

Shows the SharePoint Online App Bar.

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

### -Enabled

Specifies whether to show or hide SharePoint Online App Bar.

```yaml
Type: Boolean
DefaultValue: True
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
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
