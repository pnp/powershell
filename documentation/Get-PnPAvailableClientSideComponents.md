---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpavailableclientsidecomponents
schema: 2.0.0
title: Get-PnPAvailableClientSideComponents
---

# Get-PnPAvailableClientSideComponents

## SYNOPSIS
Gets the available client side components on a particular page

## SYNTAX

```powershell
Get-PnPAvailableClientSideComponents [-Page] <ClientSidePagePipeBind>
 [-Component <ClientSideComponentPipeBind>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAvailableClientSideComponents -Page "MyPage.aspx"
```

Gets the list of available client side components on the page 'MyPage.aspx'

### EXAMPLE 2
```powershell
Get-PnPAvailableClientSideComponents $page
```

Gets the list of available client side components on the page contained in the $page variable

### EXAMPLE 3
```powershell
Get-PnPAvailableClientSideComponents -Page "MyPage.aspx" -ComponentName "HelloWorld"
```

Gets the client side component 'HelloWorld' if available on the page 'MyPage.aspx'

## PARAMETERS

### -Component
Specifies the component instance or Id to look for.

```yaml
Type: ClientSideComponentPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Page
The name of the page.

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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)