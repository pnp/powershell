---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPContext.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPContext
---

# Get-PnPContext

## SYNOPSIS

Returns the current SharePoint Online CSOM context

## SYNTAX

### Default (Default)

```
Get-PnPContext [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns a SharePoint Online Client Side Object Model (CSOM) context

## EXAMPLES

### EXAMPLE 1

```powershell
$ctx = Get-PnPContext
```

This will put the current context in the $ctx variable.

### EXAMPLE 2

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

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection. If not provided, the context of the connection will be retrieved from the current connection.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
