---
Module Name: PnP.PowerShell
title: Set-PnPWebPartProperty
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPWebPartProperty.html
---
 
# Set-PnPWebPartProperty

## SYNOPSIS
Sets a web part property

## SYNTAX

```powershell
Set-PnPWebPartProperty -ServerRelativePageUrl <String> -Identity <Guid> -Key <String> -Value <PSObject>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to modify web part property.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPWebPartProperty -ServerRelativePageUrl /sites/demo/sitepages/home.aspx -Identity ccd2c98a-c9ae-483b-ae72-19992d583914 -Key "Title" -Value "New Title"
```

Sets the title property of the web part.

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

### -Identity
The Guid of the web part

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Key
Name of a single property to be set

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerRelativePageUrl
Full server relative url of the web part page, e.g. /sites/demo/sitepages/home.aspx

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

### -Value
Value of the property to be set

```yaml
Type: PSObject
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

