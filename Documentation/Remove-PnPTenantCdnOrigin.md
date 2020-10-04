---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnptenantcdnorigin
schema: 2.0.0
title: Remove-PnPTenantCdnOrigin
---

# Remove-PnPTenantCdnOrigin

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes an origin from the Public or Private content delivery network (CDN).

## SYNTAX

```
Remove-PnPTenantCdnOrigin -OriginUrl <String> -CdnType <SPOTenantCdnType> [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
Removes an origin from the Public or Private content delivery network (CDN).

You must be a SharePoint Online global administrator to run the cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTenantCdnOrigin -OriginUrl /sites/site/subfolder -CdnType Public
```

This example removes the specified origin from the public CDN

## PARAMETERS

### -CdnType
The cdn type to remove the origin from.

```yaml
Type: SPOTenantCdnType
Parameter Sets: (All)
Aliases:
Accepted values: Public, Private

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

### -OriginUrl
The origin to remove.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)