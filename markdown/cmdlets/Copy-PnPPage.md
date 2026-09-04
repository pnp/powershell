---
Module Name: PnP.PowerShell
title: Copy-PnPPage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Copy-PnPPage.html
---
 
# Copy-PnPPage

## SYNOPSIS
Allows a site page to be moved from one site to another site.

## SYNTAX

```powershell
Copy-PnPPage -SourceSite <SPOSitePipeBind> -DestinationSite <SPOSitePipeBind> -PageName <String>
```

## DESCRIPTION
This command allows a site page to be moved from one site to another site. The source and destination sites can be specified using the -SourceSite and -DestinationSite parameters, respectively. The page to be moved is specified using the -PageName parameter.

Use [Get-PnPPageCopyProgress](Get-PnPPageCopyProgress.md) to check the progress of the page copy operation.

Question: Will SharePoint pages retain their version history after the move?
Answer: Currently, only the latest published version will be transferred.

Question: Can recipients of SharePoint pages I shared with continue to access them after the move?
Answer: All permissions will be removed once the pages are moved.

## EXAMPLES

### EXAMPLE 1
```powershell
Copy-PnPPage -SourceSite https://tenant.sharepoint.com/sites/site1 -DestinationSite https://tenant.sharepoint.com -PageName "FAQ.aspx"
```

Moves the page named 'FAQ.aspx' from the site 'site1' to the root site of the tenant.

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

### -DestinationSite
The destination site to which the page should be moved. This can be specified as a URL or a site object.

```yaml
Type: SPOSitePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageName
The name of the page to be moved.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceSite
The source site from which the page will be moved. This can be specified as a URL or a site object.

```yaml
Type: SPOSitePipeBind
Parameter Sets: (All)

Required: True
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Online Management Shell equivallent](https://learn.microsoft.com/powershell/module/sharepoint-online/copy-spopersonalsitepage)
[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)