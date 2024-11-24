---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPWikiPageContent.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPWikiPageContent
---

# Set-PnPWikiPageContent

## SYNOPSIS

Sets the contents of a wiki page.

## SYNTAX

### STRING

```
Set-PnPWikiPageContent -Content <String> -ServerRelativePageUrl <String>
 [-Connection <PnPConnection>]
```

### FILE

```
Set-PnPWikiPageContent -Path <String> -ServerRelativePageUrl <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet updates the content of the specified wiki page to the value specified either in a string or a file.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPWikiPageContent -ServerRelativePageUrl /sites/PnPWikiCollection/SitePages/OurWikiPage.aspx -Path .\sampleblog.html
```
Sets the content of OurWikiPage to the content of sampleblog.html file.

### EXAMPLE 2

```powershell
$htmlContent = "<div>test</div>"
Set-PnPWikiPageContent -ServerRelativePageUrl /sites/PnPWikiCollection/SitePages/OurWikiPage.aspx -Content $htmlContent
```
Sets the content of OurWikiPage as "test". The existing content of the wiki page will be replaced with the new content provided.

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

### -Content



```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: STRING
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

The local file path of the HTML file containing the content for the wiki page.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: FILE
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ServerRelativePageUrl

The server-relative URL of the wiki page whose content should be updated.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- PageUrl
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
