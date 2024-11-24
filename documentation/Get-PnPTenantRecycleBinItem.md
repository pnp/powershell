---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantRecycleBinItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantRecycleBinItem
---

# Get-PnPTenantRecycleBinItem

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns all modern and classic site collections in the tenant scoped recycle bin

## SYNTAX

### Default (Default)

```
Get-PnPTenantRecycleBinItem [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command will return all the items in the tenant recycle bin for the Office 365 tenant you are connected to. If you are not a SharePoint Tenant Admin connect to the site where you want to manage the recycle bin and use Get-PnPRecycleBinItem.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantRecycleBinItem
```

Returns all modern and classic site collections in the tenant scoped recycle bin

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
