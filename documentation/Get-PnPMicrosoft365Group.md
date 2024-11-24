---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365Group.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPMicrosoft365Group
---

# Get-PnPMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All, Group.Read.All, Group.ReadWrite.All, GroupMember.Read.All, GroupMember.ReadWrite.All

Gets one Microsoft 365 Group or a list of Microsoft 365 Groups

## SYNTAX

### Default (Default)

```
Get-PnPMicrosoft365Group [-Identity <Microsoft365GroupPipeBind>] [-IncludeSiteUrl] [-IncludeOwners]
 [-Detailed] [-Filter <string>] [-IncludeSensitivityLabels]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve Microsoft 365 Groups. By using `Identity` option you may specify the exact group that will be retrieved.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMicrosoft365Group
```

Retrieves all the Microsoft 365 Groups

### EXAMPLE 2

```powershell
Get-PnPMicrosoft365Group -Identity $groupId
```

Retrieves a specific Microsoft 365 Group based on its ID

### EXAMPLE 3

```powershell
Get-PnPMicrosoft365Group -Identity $groupDisplayName
```

Retrieves a specific or list of Microsoft 365 Groups that start with the given DisplayName

### EXAMPLE 4

```powershell
Get-PnPMicrosoft365Group -Identity $groupSiteMailNickName
```

Retrieves a specific or list of Microsoft 365 Groups for which the email starts with the provided mail nickName

### EXAMPLE 5

```powershell
Get-PnPMicrosoft365Group -Identity $group
```

Retrieves a specific Microsoft 365 Group based on its object instance

### EXAMPLE 6

```powershell
Get-PnPMicrosoft365Group -IncludeSiteUrl
```

Retrieves all Microsoft 365 Groups in this tenant and includes the URL property for the underlying SharePoint site.

### EXAMPLE 7

```powershell
$groups = Get-PnPMicrosoft365Group -IncludeOwners
$g[0].Owners
```

Retrieves all Microsoft 365 Groups in this tenant and retrieves the owners for each group. The owners are available in the "Owners" property of the returned objects.

### EXAMPLE 8

```powershell
$groups = Get-PnPMicrosoft365Group -Filter "startswith(description, 'contoso')"
```

Retrieves all Microsoft 365 Groups in this tenant with description starting with Contoso. This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/graph/aad-advanced-queries?tabs=http#group-properties).

## PARAMETERS

### -Detailed

When provided, the following properties originating from Exchange Online, will also be loaded into the returned group. Without providing this flag, they will not be populated. Providing this flag causes an extra call to be made to Microsoft Graph, so only add it when you need one of the properties below.

- AutoSubscribeNewMembers
- RequireSenderAuthenticationEnabled
- IsSubscribedByMail

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

### -Filter

Specify the query to pass to Graph API in $filter.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Filter
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

The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
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

### -IncludeOwners

Include fetching the group owners. This slows down large listings.

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

### -IncludeSensitivityLabels

Include fetching the sensitivity labels. This slows down large listings.

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

### -IncludeSiteUrl

Include fetching the site URL for Microsoft 365 Groups. This slows down large listings.

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
