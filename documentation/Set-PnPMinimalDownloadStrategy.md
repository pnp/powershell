---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPMinimalDownloadStrategy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPMinimalDownloadStrategy
---

# Set-PnPMinimalDownloadStrategy

## SYNOPSIS

Activates or deactivates the minimal downloading strategy.

## SYNTAX

### On

```
Set-PnPMinimalDownloadStrategy [-On] [-Force] [-Connection <PnPConnection>]
```

### Off

```
Set-PnPMinimalDownloadStrategy [-Off] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Activates or deactivates the minimal download strategy feature of a site.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPMinimalDownloadStrategy -Off
```

Will deactivate minimal download strategy (MDS) for the current web.

### EXAMPLE 2

```powershell
Set-PnPMinimalDownloadStrategy -On
```

Will activate minimal download strategy (MDS) for the current web.

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

### -Force

Specifies whether to overwrite (when activating) or continue (when deactivating) an existing feature with the same feature identifier. This parameter is ignored if there are no errors.

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

### -Off

Turn minimal download strategy off.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Off
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -On

Turn minimal download strategy on.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: On
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
