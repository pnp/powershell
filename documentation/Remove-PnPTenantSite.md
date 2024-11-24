---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTenantSite
---

# Remove-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a site collection

## SYNTAX

### Default (Default)

```
Remove-PnPTenantSite [-Url] <String> [-SkipRecycleBin] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes a site collection which is listed in your tenant administration site.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTenantSite -Url "https://tenant.sharepoint.com/sites/contoso"
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso'  and put it in the recycle bin.

### EXAMPLE 2

```powershell
Remove-PnPTenantSite -Url "https://tenant.sharepoint.com/sites/contoso" -Force -SkipRecycleBin
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' with force and it will skip the recycle bin.

### EXAMPLE 3

```powershell
Remove-PnPTenantSite -Url "https://tenant.sharepoint.com/sites/contoso" -FromRecycleBin
```

This will remove the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the recycle bin.

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

Do not ask for confirmation.

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

### -SkipRecycleBin

Do not add to the tenant scoped recycle bin when selected.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- SkipTrash
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

Specifies the full URL of the site collection that needs to be deleted

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
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
