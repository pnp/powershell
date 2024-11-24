---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPInPlaceRecordsManagement.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPInPlaceRecordsManagement
---

# Set-PnPInPlaceRecordsManagement

## SYNOPSIS

Activates or deactivates in place records management feature.

## SYNTAX

### Default (Default)

```
Set-PnPInPlaceRecordsManagement -Enabled <Boolean> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Activates or deactivates in place records management feature in the site collection.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPInPlaceRecordsManagement -Enabled $true
```

Activates in place records management.

### EXAMPLE 2

```powershell
Set-PnPInPlaceRecordsManagement -Enabled $false
```

Deactivates in place records management.

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



```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
