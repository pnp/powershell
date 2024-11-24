---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupTeam.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPMicrosoft365GroupTeam
---

# Get-PnPMicrosoft365GroupTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Group.Read.All

Returns the Microsoft Teams team behind a particular Microsoft 365 Group.

## SYNTAX

### Default (Default)

```
Get-PnPMicrosoft365GroupTeam -Identity <Microsoft365GroupPipeBind> [-Connection] [-Verbose]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of details on the Microsoft Teams team connected to a Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMicrosoft365GroupTeam
```

Retrieves the Microsoft Teams team details behind the Microsoft 365 Group of the currently connected to site.

### EXAMPLE 2

```powershell
Get-PnPMicrosoft365GroupTeam -Identity "IT Team"
```

Retrieves the Microsoft Teams team details behind the Microsoft 365 Group named "IT Team".

### EXAMPLE 3

```powershell
Get-PnPMicrosoft365GroupTeam -Identity e6212531-7f09-4c3b-bc2e-12cae26fb409
```

Retrieves the Microsoft Teams team details behind the Microsoft 365 Group with the provided Id.

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

The Identity of the Microsoft 365 Group.

```yaml
Type: Microsoft365GroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
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
