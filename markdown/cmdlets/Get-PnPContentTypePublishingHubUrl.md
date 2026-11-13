---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContentTypePublishingHubUrl.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContentTypePublishingHubUrl
---
  
# Get-PnPContentTypePublishingHubUrl

## SYNOPSIS
Returns the url to Content Type Publishing Hub

## SYNTAX

```powershell
Get-PnPContentTypePublishingHubUrl [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve the url of the Content Type Publishing Hub.

## EXAMPLES

### EXAMPLE 1
```powershell
$url = Get-PnPContentTypePublishingHubUrl
Connect-PnPOnline -Url $url
Get-PnPContentType
```

This will retrieve the url to the content type hub, connect to it, and then retrieve the content types form that site

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


