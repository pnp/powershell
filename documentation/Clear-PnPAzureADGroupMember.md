---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Clear-PnPAzureADGroupMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Clear-PnPAzureADGroupMember
---

# Clear-PnPAzureADGroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All, GroupMember.ReadWrite.All

Removes all current members from a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

### Default (Default)

```
Clear-PnPAzureADGroupMember -Identity <AzureADGroupPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove all current members from specified Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1

```powershell
Clear-PnPAzureADGroupMember -Identity "Project Team"
```

Removes all the current members from the Azure Active Directory group named "Project Team".

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

### -Identity

The Identity of the Azure Active Directory group to remove all members from.

```yaml
Type: AzureADGroupPipeBind
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-members)
