---
Module Name: PnP.PowerShell
title: Set-PnPWikiPageContent
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPWikiPageContent.html
---
 
# Set-PnPWikiPageContent

## SYNOPSIS
Sets the contents of a wikipage

## SYNTAX

### STRING
```powershell
Set-PnPWikiPageContent -Content <String> -ServerRelativePageUrl <String> 
 [-Connection <PnPConnection>] 
```

### FILE
```powershell
Set-PnPWikiPageContent -Path <String> -ServerRelativePageUrl <String> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet updates the content of the specified wikipage to the value specified either in a string or a file.

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

Sets the content of OurWikiPage as "test". 

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Content

```yaml
Type: String
Parameter Sets: STRING

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

```yaml
Type: String
Parameter Sets: FILE

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativePageUrl
Site Relative Page Url

```yaml
Type: String
Parameter Sets: (All)
Aliases: PageUrl

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

