---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPDeletedContainer.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPDeletedContainer
---

# Get-PnPDeletedContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

The Get-PnPDeletedContainer cmdlet returns a list of all deleted Containers in the Recycle Bin. There is no Identity parameter needed. The list includes the ContainerId, ContainerName, DeletedOn, and CreatedDate. Deleted Containers in the Recycle Bin are permanently deleted after 93 days. Use cmdlet Restore-PnPDeletedContainer to restore a deleted container.

## SYNTAX

### Default (Default)

```
Get-PnPDeletedContainer [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPDeletedContainer
```

Returns a list of the ContainerId, ContainerName, and CreatedDate of all deleted Containers in the Recycle Bin.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPDeletedContainer
```

Returns a list of the ContainerId, ContainerName, and CreatedDate of all deleted Containers in the Recycle Bin.

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
