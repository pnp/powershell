---
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTemporarilyDisableAppBar.html
Locale: en-US
Module Name: PnP.PowerShell
ms.date: 11/24/2024
PlatyPS schema version: 2024-05-01
title: Get-PnPTemporarilyDisableAppBar
---

# Get-PnPTemporarilyDisableAppBar

## SYNOPSIS

Required Permissions * SharePoint: Access to the SharePoint Tenant Administration site

## SYNTAX

### __AllParameterSets

```powershell
Get-PnPTemporarilyDisableAppBar [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has the no aliases.

## DESCRIPTION

Allows to retrieve disabled state of the SharePoint Online App Bar.

## EXAMPLES

### EXAMPLE 1

Get-PnPTemporarilyDisableAppBar

Returns True if the the SharePoint Online App Bar is hidden or False if it is not.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnP.PowerShell.Commands.Base.PnPConnection
DefaultValue: ''
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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Online Version:](https://pnp.github.io/powershell/cmdlets/Get-PnPTemporarilyDisableAppBar.html)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
