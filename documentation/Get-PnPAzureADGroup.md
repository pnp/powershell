---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureADGroup
---

# Get-PnPAzureADGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets one Azure Active Directory group or a list of Azure Active Directory groups. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

### Default (Default)

```
Get-PnPAzureADGroup [-Identity <AzureADGroupPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve a list of Azure Active Directory groups. Those can be a security, distribution or Microsoft 365 group. By specifying `Identity` option it is possible to get a single group.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureADGroup
```

Retrieves all the Azure Active Directory groups.

### EXAMPLE 2

```powershell
Get-PnPAzureADGroup -Identity $groupId
```

Retrieves a specific Azure Active Directory group based on its ID.

### EXAMPLE 3

```powershell
Get-PnPAzureADGroup -Identity $groupDisplayName
```

Retrieves a specific Azure Active Directory group that has the given DisplayName.

### EXAMPLE 4

```powershell
Get-PnPAzureADGroup -Identity $groupSiteMailNickName
```

Retrieves a specific Azure Active Directory group for which the email address equals the provided mail nickName.

### EXAMPLE 5

```powershell
Get-PnPAzureADGroup -Identity $group
```

Retrieves a specific Azure Active Directory group based on its group object instance.

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

The identity of the Azure Active Directory group. Either specify an id, a display name, email address, or a group object.

```yaml
Type: AzureADGroupPipeBind
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
