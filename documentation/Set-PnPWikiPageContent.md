---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpwikipagecontent
schema: 2.0.0
title: Set-PnPWikiPageContent
---

# Set-PnPWikiPageContent

## SYNOPSIS
Sets the contents of a wikipage

## SYNTAX

### STRING
```powershell
Set-PnPWikiPageContent -Content <String> -ServerRelativePageUrl <String> [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### FILE
```powershell
Set-PnPWikiPageContent -Path <String> -ServerRelativePageUrl <String> [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

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

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)