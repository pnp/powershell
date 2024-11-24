---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPUser
---

# Remove-PnPUser

## SYNOPSIS

Removes a specific user from the site collection User Information List

## SYNTAX

### Default (Default)

```
Remove-PnPUser [-Identity] <UserPipeBind> [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command will allow the removal of a specific user from the User Information List

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPUser -Identity 23
```

Remove the user with Id 23 from the User Information List of the current site collection

### EXAMPLE 2

```powershell
Remove-PnPUser -Identity i:0#.f|membership|user@tenant.onmicrosoft.com
```

Remove the user with LoginName i:0#.f|membership|user@tenant.onmicrosoft.com from the User Information List of the current site collection

### EXAMPLE 3

```powershell
Get-PnPUser | ? Email -eq "user@tenant.onmicrosoft.com" | Remove-PnPUser
```

Remove the user with e-mail address user@tenant.onmicrosoft.com from the User Information List of the current site collection

### EXAMPLE 4

```powershell
Remove-PnPUser -Identity i:0#.f|membership|user@tenant.onmicrosoft.com -Force:$false
```

Remove the user with LoginName i:0#.f|membership|user@tenant.onmicrosoft.com from the User Information List of the current site collection without asking to confirm the removal first

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

Specifying the Force parameter will skip the confirmation question

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

User ID or login name

```yaml
Type: UserPipeBind
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
