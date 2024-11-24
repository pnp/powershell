---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPageLikedByInformation.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPageLikedByInformation
---

# Get-PnPPageLikedByInformation

## SYNOPSIS

Returns liked-by Information of a modern page

## SYNTAX

### Default (Default)

```
Get-PnPPageLikedByInformation -Identity <PagePipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command retrieves the LikedBy Information of a modern page.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPPageLikedByInformation -Identity "MyPage.aspx"
```

Gets the LikedBy Information of page named 'MyPage.aspx' in the current SharePoint site

### EXAMPLE 2

```powershell
Get-PnPPageLikedByInformation "MyPage"
```

Gets the LikedBy Information of page named 'MyPage.aspx' in the current SharePoint site

### EXAMPLE 3

```powershell
Get-PnPPageLikedByInformation -Identity "MyPage.aspx" -Web (Get-PnPWeb -Identity "Subsite1")
```

Gets the LikedBy Information of page named 'MyPage.aspx' from the subsite named 'Subsite1'

### Sample Output

```powershell
Name         : User 1
Mail         :
Id           : 14
LoginName    : i:0#.f|membership|user1@contoso.onmicrosoft.com
CreationDate : 2024-02-16 14:49:55

Name         : User 2
Mail         : user2@contoso.onmicrosoft.com
Id           : 6
LoginName    : i:0#.f|membership|user2@contoso.onmicrosoft.com
CreationDate : 2024-02-22 19:47:24
```

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

The name of the page

```yaml
Type: PagePipeBind
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
