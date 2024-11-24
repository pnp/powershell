---
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Disable-PnPSiteClassification.html
Locale: en-US
Module Name: PnP.PowerShell
ms.date: 11/24/2024
PlatyPS schema version: 2024-05-01
title: Disable-PnPSiteClassification
---

# Disable-PnPSiteClassification

## SYNOPSIS

Required Permissions * Microsoft Graph API: Directory.ReadWrite.All

## SYNTAX

### __AllParameterSets

```powershell
Disable-PnPSiteClassification [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to disable site classifications for the tenant.

## EXAMPLES

### EXAMPLE 1

Disable-PnPSiteClassification

Disables Site Classifications for your tenant.

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

### System.Void

## NOTES

## RELATED LINKS

- [Online Version:](https://pnp.github.io/powershell/cmdlets/Disable-PnPSiteClassification.html)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
