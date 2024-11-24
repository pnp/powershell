---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPContext.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPContext
---

# Set-PnPContext

## SYNOPSIS

Set the ClientContext

## SYNTAX

### Default (Default)

```
Set-PnPContext -Context <ClientContext> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets the Client Context to be used by the cmdlets, which allows easy context switching. See examples for details.

## EXAMPLES

### EXAMPLE 1

```powershell
Connect-PnPOnline -Url $siteAurl -Credentials $credentials
$ctx = Get-PnPContext
Get-PnPList # returns the lists from site specified with $siteAurl
Connect-PnPOnline -Url $siteBurl -Credentials $credentials
Get-PnPList # returns the lists from the site specified with $siteBurl
Set-PnPContext -Context $ctx # switch back to site A
Get-PnPList # returns the lists from site A
```

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying -ReturnConnection on Connect-PnPOnline. If not provided, the connection will be retrieved from the current context.

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

### -Context

The ClientContext to set

```yaml
Type: ClientContext
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
  IsRequired: true
  ValueFromPipeline: true
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
