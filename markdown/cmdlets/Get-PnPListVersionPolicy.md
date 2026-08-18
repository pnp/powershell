---
Module Name: PnP.PowerShell
title: Get-PnPListVersionPolicy
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListVersionPolicy.html
---

# Get-PnPListVersionPolicy

## SYNOPSIS
Gets file version policy settings from a SharePoint Online document library.

## SYNTAX

```powershell
Get-PnPListVersionPolicy
 -Identity <ListPipeBind>
 [-Site <SitePipeBind>]
 [-Connection <PnPConnection>]
```

## DESCRIPTION
Retrieves the effective version policy settings for a document library. When `-Site` is provided, the cmdlet resolves the library from that site and reads the library version policy through the SharePoint Online admin APIs.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListVersionPolicy -Identity "Documents"
```

Returns the version policy settings for the `Documents` library in the currently connected site.

### EXAMPLE 2
```powershell
Get-PnPListVersionPolicy -Site "https://contoso.sharepoint.com/sites/project-x" -Identity "Documents"
```

Returns the version policy settings for the `Documents` library in the specified site.

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
The document library to inspect. You can provide the library title, id, url, or a list instance.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Site
Optional target site containing the document library. When omitted, the cmdlet uses the currently connected SharePoint site.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### PnP.PowerShell.Commands.Model.SharePoint.ListVersionPolicy
Contains the library version policy settings and any file type override settings.

## RELATED LINKS

[Set-PnPListVersionPolicy](Set-PnPListVersionPolicy.md)