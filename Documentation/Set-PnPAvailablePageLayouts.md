---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpavailablepagelayouts
schema: 2.0.0
title: Set-PnPAvailablePageLayouts
---

# Set-PnPAvailablePageLayouts

## SYNOPSIS
Sets the available page layouts for the current site

## SYNTAX

### SPECIFIC
```
Set-PnPAvailablePageLayouts -PageLayouts <String[]> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### ALL
```
Set-PnPAvailablePageLayouts [-AllowAllPageLayouts] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### INHERIT
```
Set-PnPAvailablePageLayouts [-InheritPageLayouts] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

## PARAMETERS

### -AllowAllPageLayouts
An array of page layout files to set as available page layouts for the site.

```yaml
Type: SwitchParameter
Parameter Sets: ALL
Aliases:

Required: True
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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InheritPageLayouts
Set the available page layouts to inherit from the parent site.

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

### -PageLayouts
An array of page layout files to set as available page layouts for the site.

```yaml
Type: String[]
Parameter Sets: SPECIFIC
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