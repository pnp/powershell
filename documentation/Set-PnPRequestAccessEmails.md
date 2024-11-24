---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPRequestAccessEmails.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPRequestAccessEmails
---

# Set-PnPRequestAccessEmails

## SYNOPSIS

Sets Request Access Email on a web

## SYNTAX

### Default (Default)

```
Set-PnPRequestAccessEmails [-Emails <String[]>] [-Disabled] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Enables or disables access requests to be sent and configures which e-mail address should receive these requests. The web you apply this on must have unique rights.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPRequestAccessEmails -Emails someone@example.com
```

This will enable requesting access and send the requests to the provided e-mail address

### EXAMPLE 2

```powershell
Set-PnPRequestAccessEmails -Disabled
```

This will disable the ability to request access to the site

### EXAMPLE 3

```powershell
Set-PnPRequestAccessEmails -Disabled:$false
```

This will enable the ability to request access to the site and send the requests to the default owners of the site

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

### -Disabled

Enables or disables access to be requested

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

### -Emails

Email address to send the access requests to

```yaml
Type: String[]
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
