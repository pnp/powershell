---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteCollectionAdmin.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPSiteCollectionAdmin
---

# Remove-PnPSiteCollectionAdmin

## SYNOPSIS

Removes one or more users as site collection administrators from the site collection in the current context

## SYNTAX

### Default (Default)

```
Remove-PnPSiteCollectionAdmin

 -Owners <System.Collections.Generic.List`1[PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind]>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows removing one to many users as site collection administrators from the site collection in the current context. All existing site collection administrators not included in this command will remain site collection administrator.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPSiteCollectionAdmin -Owners "user@contoso.onmicrosoft.com"
```

This will remove user@contoso.onmicrosoft.com as a site collection owner from the site collection in the current context

### EXAMPLE 2

```powershell
Remove-PnPSiteCollectionAdmin -Owners @("user1@contoso.onmicrosoft.com", "user2@contoso.onmicrosoft.com")
```

This will remove user1@contoso.onmicrosoft.com and user2@contoso.onmicrosoft.com as site collection owners from the site collection in the current context

### EXAMPLE 3

```powershell
Get-PnPUser | ? Title -Like "*Doe" | Remove-PnPSiteCollectionAdmin
```

This will remove all users with their title ending with "Doe" as site collection owners from the site collection in the current context

### EXAMPLE 4

```powershell
Get-PnPSiteCollectionAdmin | Remove-PnPSiteCollectionAdmin
```

This will remove all existing site collection administrators from the site collection in the current context

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

### -Owners

Specifies owner(s) to remove as site collection administrators. Can be both users and groups.

```yaml
Type: System.Collections.Generic.List`1[PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
