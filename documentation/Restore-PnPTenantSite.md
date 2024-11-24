---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Restore-PnPTenantSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Restore-PnPTenantSite
---

# Restore-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Restores a site collection from the Tenant recycle bin.

## SYNTAX

### Default (Default)

```
Restore-PnPTenantSite [-Identity] <String> [-Force] [-NoWait] [-Connection <PnPConnection>]
 [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Restores a site collection which is listed in your tenant administration site from the tenant's recycle bin.

## EXAMPLES

### EXAMPLE 1

```powershell
Restore-PnPTenantSite -Identity "https://tenant.sharepoint.com/sites/contoso"
```

This will restore the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the recycle bin asking for confirmation to restore the site collection and will wait with the execution of the script until the site collection is restored.

### EXAMPLE 2

```powershell
Restore-PnPTenantSite -Identity "https://tenant.sharepoint.com/sites/contoso" -Force
```

This will restore the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the recycle bin not asking for confirmation to restore the site collection and will wait with the execution of the script until the site collection is restored.

### EXAMPLE 3

```powershell
Restore-PnPTenantSite -Identity "https://tenant.sharepoint.com/sites/contoso" -Force -NoWait
```

This will restore the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the recycle bin not asking for confirmation to restore the site collection and will immediately continue with the execution of the script

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

### -Identity

Specifies the full URL of the site collection that needs to be restored.

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

### -NoWait

If specified the task will return immediately after creating the restore site job.

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
