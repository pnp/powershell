---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUnfurlLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPUnfurlLink
---
  
# Get-PnPUnfurlLink

## SYNOPSIS
To unfurl a link for a given resource such as file, folder, list items etc.

## SYNTAX

```powershell
Get-PnPUnfurlLink -Url <String>
```

## DESCRIPTION

Creates a new organization sharing link for a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUnfurlLink -Url "https://contoso.sharepoint.com/:u:/s/testsitecol/ERs6pDuyD95LpUSUsJxi1EIBr9FMEYVBvMcs_B7cPdNPgQ?e=ZL3DPe"
```

This will fetch the information of the resource to be unfurled.

SharePoint supports many different types of links, you have direct links to lists, libraries and files, but there's also sharing links that user's have created for resources in SharePoint. Whenever your application needs to understand more about a given link we call that unfurling. A common scenario is where you allow your users to paste a link and your application gets the needed information to present the content behind the link (e.g. when you paste a link in Teams you'll you'll see the file name, thumbnail and more)

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

### -Url
The URL of the resource to be unfurled.
For more information, you can visit the [PnP Core SDK documentation](https://pnp.github.io/pnpcore/using-the-sdk/unfurl-intro.html)

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
