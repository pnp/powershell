---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpclientsidepage
schema: 2.0.0
title: Remove-PnPClientSidePage
---

# Remove-PnPClientSidePage

## SYNOPSIS
Removes a Client-Side Page

## SYNTAX

```powershell
Remove-PnPClientSidePage [-Identity] <ClientSidePagePipeBind> [-Force] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPClientSidePage -Identity "MyPage"
```

Removes the Client-Side page named 'MyPage.aspx'

### EXAMPLE 2
```powershell
Remove-PnPClientSidePage -Identity "Templates/MyPageTemplate"
```

Removes the specified Client-Side page which is located in the Templates folder of the Site Pages library.

### EXAMPLE 3
```powershell
Remove-PnPClientSidePage $page
```

Removes the specified Client-Side page which is contained in the $page variable.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Specifying the Force parameter will skip the confirmation question.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The name of the page

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: ClientSidePagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

Only applicable to: SharePoint Online, SharePoint Server 2019

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