---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpdefaultpagelayout
schema: 2.0.0
title: Set-PnPDefaultPageLayout
---

# Set-PnPDefaultPageLayout

## SYNOPSIS
Sets a specific page layout to be the default page layout for a publishing site

## SYNTAX

### TITLE
```powershell
Set-PnPDefaultPageLayout -Title <String> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### INHERIT
```powershell
Set-PnPDefaultPageLayout [-InheritFromParentSite] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPDefaultPageLayout -Title projectpage.aspx
```

Sets projectpage.aspx to be the default page layout for the current web

### EXAMPLE 2
```powershell
Set-PnPDefaultPageLayout -Title test/testpage.aspx
```

Sets a page layout in a folder in the Master Page & Page Layout gallery, such as _catalog/masterpage/test/testpage.aspx, to be the default page layout for the current web

### EXAMPLE 3
```powershell
Set-PnPDefaultPageLayout -InheritFromParentSite
```

Sets the default page layout to be inherited from the parent site

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InheritFromParentSite
Set the default page layout to be inherited from the parent site.

```yaml
Type: SwitchParameter
Parameter Sets: INHERIT
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Title of the page layout

```yaml
Type: String
Parameter Sets: TITLE
Aliases:

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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)