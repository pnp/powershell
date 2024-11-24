---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Clear-PnPTenantRecycleBinItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Clear-PnPTenantRecycleBinItem
---

# Clear-PnPTenantRecycleBinItem

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Permanently deletes a site collection from the tenant scoped recycle bin

## SYNTAX

### Default (Default)

```
Clear-PnPTenantRecycleBinItem -Url <String> [-Wait] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Clear-PnPTenantRecycleBinItem cmdlet allows a site collection that has been deleted and still exists in the tenant recycle bin to be permanently deleted from the recycle bin as well.

## EXAMPLES

### EXAMPLE 1

```powershell
Clear-PnPTenantRecycleBinItem -Url "https://tenant.sharepoint.com/sites/contoso"
```

This will permanently delete site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the tenant recycle bin

### EXAMPLE 2

```powershell
Clear-PnPTenantRecycleBinItem -Url "https://tenant.sharepoint.com/sites/contoso" -Wait
```

This will permanently delete site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the tenant recycle bin and will wait with executing further PowerShell commands until the operation has completed

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

If provided, no confirmation will be asked to permanently delete the site collection from the tenant recycle bin

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

### -Url

Url of the site collection to permanently delete from the tenant recycle bin

```yaml
Type: String
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

### -Wait

If provided, the PowerShell execution will halt until the operation has completed

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
